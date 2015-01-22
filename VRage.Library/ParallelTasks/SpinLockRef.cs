// Decompiled with JetBrains decompiler
// Type: ParallelTasks.SpinLockRef
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ParallelTasks
{
  public class SpinLockRef
  {
    private SpinLock m_spinLock;

    public SpinLockRef.Token Acquire()
    {
      return new SpinLockRef.Token(this);
    }

    private void Enter()
    {
      this.m_spinLock.Enter();
    }

    private void Exit()
    {
      this.m_spinLock.Exit();
    }

    public struct Token : IDisposable
    {
      private SpinLockRef m_lock;

      public Token(SpinLockRef spin)
      {
        this.m_lock = spin;
        this.m_lock.Enter();
      }

      public void Dispose()
      {
        this.m_lock.Exit();
      }
    }
  }
}
