// Decompiled with JetBrains decompiler
// Type: VRage.FastResourceLock
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Diagnostics;
using System.Threading;

namespace VRage
{
  public sealed class FastResourceLock : IDisposable, IResourceLock
  {
    private static readonly int SpinCount = NativeMethods.SpinCount;
    private const int LockOwned = 1;
    private const int LockExclusiveWaking = 2;
    private const int LockSharedOwnersShift = 2;
    private const int LockSharedOwnersMask = 1023;
    private const int LockSharedOwnersIncrement = 4;
    private const int LockSharedWaitersShift = 12;
    private const int LockSharedWaitersMask = 1023;
    private const int LockSharedWaitersIncrement = 4096;
    private const int LockExclusiveWaitersShift = 22;
    private const int LockExclusiveWaitersMask = 1023;
    private const int LockExclusiveWaitersIncrement = 4194304;
    private const int ExclusiveMask = -4194302;
    private int _value;
    private IntPtr _sharedWakeEvent;
    private IntPtr _exclusiveWakeEvent;

    public int ExclusiveWaiters
    {
      get
      {
        return this._value >> 22 & 1023;
      }
    }

    public bool Owned
    {
      get
      {
        return (this._value & 1) != 0;
      }
    }

    public int SharedOwners
    {
      get
      {
        return this._value >> 2 & 1023;
      }
    }

    public int SharedWaiters
    {
      get
      {
        return this._value >> 12 & 1023;
      }
    }

    public FastResourceLock()
    {
      this._value = 0;
    }

    ~FastResourceLock()
    {
      this.Dispose(false);
    }

    private void Dispose(bool disposing)
    {
      if (this._sharedWakeEvent != IntPtr.Zero)
      {
        NativeMethods.CloseHandle(this._sharedWakeEvent);
        this._sharedWakeEvent = IntPtr.Zero;
      }
      if (!(this._exclusiveWakeEvent != IntPtr.Zero))
        return;
      NativeMethods.CloseHandle(this._exclusiveWakeEvent);
      this._exclusiveWakeEvent = IntPtr.Zero;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    [DebuggerStepThrough]
    public void AcquireExclusive()
    {
      int num = 0;
      while (true)
      {
        int comparand = this._value;
        if ((comparand & 3) == 0)
        {
          if (Interlocked.CompareExchange(ref this._value, comparand + 1, comparand) == comparand)
            break;
        }
        else if (num >= FastResourceLock.SpinCount)
        {
          this.EnsureEventCreated(ref this._exclusiveWakeEvent);
          if (Interlocked.CompareExchange(ref this._value, comparand + 4194304, comparand) == comparand)
            goto label_6;
        }
        ++num;
      }
      return;
label_6:
      NativeMethods.WaitForSingleObject(this._exclusiveWakeEvent, -1);
      int comparand1;
      do
      {
        comparand1 = this._value;
      }
      while (Interlocked.CompareExchange(ref this._value, comparand1 + 1 - 2, comparand1) != comparand1);
    }

    [DebuggerStepThrough]
    public void AcquireShared()
    {
      int num = 0;
      while (true)
      {
        do
        {
          int comparand = this._value;
          if ((comparand & -4190209) == 0)
          {
            if (Interlocked.CompareExchange(ref this._value, comparand + 1 + 4, comparand) == comparand)
              return;
            else
              goto label_10;
          }
          else if ((comparand & 1) != 0 && (comparand >> 2 & 1023) != 0 && (comparand & -4194302) == 0)
          {
            if (Interlocked.CompareExchange(ref this._value, comparand + 4, comparand) == comparand)
              return;
            else
              goto label_10;
          }
          else if (num >= FastResourceLock.SpinCount)
          {
            this.EnsureEventCreated(ref this._sharedWakeEvent);
            if (Interlocked.CompareExchange(ref this._value, comparand + 4096, comparand) != comparand)
              goto label_10;
          }
          else
            goto label_10;
        }
        while (NativeMethods.WaitForSingleObject(this._sharedWakeEvent, -1) == 0);
        continue;
label_10:
        ++num;
      }
    }

    public void ConvertExclusiveToShared()
    {
      int comparand;
      int ReleaseCount;
      do
      {
        comparand = this._value;
        ReleaseCount = comparand >> 12 & 1023;
      }
      while (Interlocked.CompareExchange(ref this._value, comparand + 4 & -4190209, comparand) != comparand);
      if (ReleaseCount == 0)
        return;
      NativeMethods.ReleaseSemaphore(this._sharedWakeEvent, ReleaseCount, IntPtr.Zero);
    }

    private void EnsureEventCreated(ref IntPtr handle)
    {
      if (Thread.VolatileRead(ref handle) != IntPtr.Zero)
        return;
      IntPtr semaphore = NativeMethods.CreateSemaphore(IntPtr.Zero, 0, int.MaxValue, (string) null);
      if (!(Interlocked.CompareExchange(ref handle, semaphore, IntPtr.Zero) != IntPtr.Zero))
        return;
      NativeMethods.CloseHandle(semaphore);
    }

    public FastResourceLock.Statistics GetStatistics()
    {
      return new FastResourceLock.Statistics();
    }

    public void ReleaseExclusive()
    {

    }

    public void ReleaseShared()
    {
      int comparand;
      do
      {
        comparand = this._value;
        if ((comparand >> 2 & 1023) > 1)
        {
          if (Interlocked.CompareExchange(ref this._value, comparand - 4, comparand) == comparand)
            break;
        }
        else if ((comparand >> 22 & 1023) != 0)
        {
          if (Interlocked.CompareExchange(ref this._value, comparand - 1 + 2 - 4 - 4194304, comparand) == comparand)
          {
            NativeMethods.ReleaseSemaphore(this._exclusiveWakeEvent, 1, IntPtr.Zero);
            break;
          }
        }
      }
      while (Interlocked.CompareExchange(ref this._value, comparand - 1 - 4, comparand) != comparand);
    }

    [DebuggerStepThrough]
    public void SpinAcquireExclusive()
    {
      while (true)
      {
        int comparand = this._value;
        if ((comparand & 3) != 0 || Interlocked.CompareExchange(ref this._value, comparand + 1, comparand) != comparand)
        {
          if (NativeMethods.SpinEnabled)
            Thread.SpinWait(8);
          else
            Thread.Sleep(0);
        }
        else
          break;
      }
    }

    [DebuggerStepThrough]
    public void SpinAcquireShared()
    {
      while (true)
      {
        int comparand = this._value;
        if ((comparand & -4194302) == 0)
        {
          if ((comparand & 1) == 0)
          {
            if (Interlocked.CompareExchange(ref this._value, comparand + 1 + 4, comparand) == comparand)
              break;
          }
          else if ((comparand >> 2 & 1023) != 0 && Interlocked.CompareExchange(ref this._value, comparand + 4, comparand) == comparand)
            goto label_3;
        }
        if (NativeMethods.SpinEnabled)
          Thread.SpinWait(8);
        else
          Thread.Sleep(0);
      }
      return;
label_3:;
    }

    [DebuggerStepThrough]
    public void SpinConvertSharedToExclusive()
    {
      while (true)
      {
        int comparand = this._value;
        if ((comparand >> 2 & 1023) != 1 || Interlocked.CompareExchange(ref this._value, comparand - 4, comparand) != comparand)
        {
          if (NativeMethods.SpinEnabled)
            Thread.SpinWait(8);
          else
            Thread.Sleep(0);
        }
        else
          break;
      }
    }

    public bool TryAcquireExclusive()
    {
      int comparand = this._value;
      if ((comparand & 3) != 0)
        return false;
      else
        return Interlocked.CompareExchange(ref this._value, comparand + 1, comparand) == comparand;
    }

    public bool TryAcquireShared()
    {
      int comparand = this._value;
      if ((comparand & -4194302) != 0)
        return false;
      if ((comparand & 1) == 0)
        return Interlocked.CompareExchange(ref this._value, comparand + 1 + 4, comparand) == comparand;
      if ((comparand >> 2 & 1023) != 0)
        return Interlocked.CompareExchange(ref this._value, comparand + 4, comparand) == comparand;
      else
        return false;
    }

    public bool TryConvertSharedToExclusive()
    {
      int comparand;
      do
      {
        comparand = this._value;
        if ((comparand >> 2 & 1023) != 1)
          return false;
      }
      while (Interlocked.CompareExchange(ref this._value, comparand - 4, comparand) != comparand);
      return true;
    }

    public struct Statistics
    {
      public int AcqExcl;
      public int AcqShrd;
      public int AcqExclCont;
      public int AcqShrdCont;
      public int AcqExclSlp;
      public int AcqShrdSlp;
      public int PeakExclWtrsCount;
      public int PeakShrdWtrsCount;
    }
  }
}
