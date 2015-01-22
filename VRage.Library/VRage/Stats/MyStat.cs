// Decompiled with JetBrains decompiler
// Type: VRage.Stats.MyStat
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ParallelTasks;
using System;
using System.Runtime.InteropServices;
using VRage;

namespace VRage.Stats
{
  internal class MyStat
  {
    private SpinLock Lock = new SpinLock();
    public string DrawText;
    private MyStatTypeEnum Type;
    private int RefreshRate;
    private int ClearRate;
    private int NumDecimals;
    private MyTimeSpan LastRefresh;
    private MyTimeSpan LastClear;
    private MyStat.Value Sum;
    private int Count;
    private MyStat.Value Min;
    private MyStat.Value Max;
    private MyStat.Value Last;
    private MyStat.Value DrawSum;
    private int DrawCount;
    private MyStat.Value DrawMin;
    private MyStat.Value DrawMax;
    private MyStat.Value DrawLast;

    private int DeltaTimeToInt(MyTimeSpan delta)
    {
      return (int) (1000.0 * delta.Miliseconds + 0.5);
    }

    private MyTimeSpan IntToDeltaTime(int v)
    {
      return MyTimeSpan.FromMiliseconds((double) v / 1000.0);
    }

    public void ReadAndClear(MyTimeSpan currentTime, out MyStat.Value sum, out int count, out MyStat.Value min, out MyStat.Value max, out MyStat.Value last, out MyStatTypeEnum type, out int decimals, out MyTimeSpan inactivityMs)
    {
      this.Lock.Enter();
      try
      {
        inactivityMs = MyTimeSpan.Zero;
        if (this.Count <= 0)
        {
          MyTimeSpan delta = this.IntToDeltaTime(-this.Count) + (this.Count < 0 ? currentTime - this.LastClear : MyTimeSpan.FromMiliseconds(1.0));
          this.Count = -this.DeltaTimeToInt(delta);
          inactivityMs = delta;
          this.LastClear = currentTime;
        }
        else
        {
          if (currentTime >= this.LastRefresh + MyTimeSpan.FromMiliseconds((double) this.RefreshRate))
          {
            this.DrawSum = this.Sum;
            this.DrawCount = this.Count;
            this.DrawMin = this.Min;
            this.DrawMax = this.Max;
            this.DrawLast = this.Last;
            this.LastRefresh = currentTime;
            if (this.ClearRate == -1)
            {
              this.Count = 0;
              this.ClearUnsafe();
            }
          }
          if (this.ClearRate != -1 && currentTime >= this.LastClear + MyTimeSpan.FromMiliseconds((double) this.ClearRate))
          {
            this.Count = 0;
            this.ClearUnsafe();
            this.LastClear = currentTime;
          }
        }
        type = this.Type;
        decimals = this.NumDecimals;
      }
      finally
      {
        this.Lock.Exit();
      }
      sum = this.DrawSum;
      count = this.DrawCount;
      min = this.DrawMin;
      max = this.DrawMax;
      last = this.DrawLast;
    }

    public void Clear()
    {
      this.Lock.Enter();
      try
      {
        this.ClearUnsafe();
        if (this.Count > 0)
          this.Count = 0;
        this.LastRefresh = MyTimeSpan.Zero;
      }
      finally
      {
        this.Lock.Exit();
      }
    }

    public void ChangeSettings(MyStatTypeEnum type, int refreshRate, int numDecimals, int clearRate)
    {
      this.Lock.Enter();
      try
      {
        this.ChangeSettingsUnsafe(type, refreshRate, numDecimals, clearRate);
      }
      finally
      {
        this.Lock.Exit();
      }
    }

    public void Write(long value, MyStatTypeEnum type, int refreshRate, int numDecimals, int clearRate)
    {
      this.Lock.Enter();
      try
      {
        this.ChangeSettingsUnsafe(type | MyStatTypeEnum.LongFlag, refreshRate, numDecimals, clearRate);
        this.WriteUnsafe(value);
      }
      finally
      {
        this.Lock.Exit();
      }
    }

    public void Write(float value, MyStatTypeEnum type, int refreshRate, int numDecimals, int clearRate)
    {
      this.Lock.Enter();
      try
      {
        this.ChangeSettingsUnsafe(type & (MyStatTypeEnum) 191, refreshRate, numDecimals, clearRate);
        this.WriteUnsafe(value);
      }
      finally
      {
        this.Lock.Exit();
      }
    }

    public void Write(float value)
    {
      this.Lock.Enter();
      try
      {
        this.WriteUnsafe(value);
      }
      finally
      {
        this.Lock.Exit();
      }
    }

    private void ChangeSettingsUnsafe(MyStatTypeEnum type, int refreshRate, int numDecimals, int clearRate)
    {
      this.Type = type;
      this.RefreshRate = refreshRate;
      this.ClearRate = clearRate;
      this.NumDecimals = numDecimals;
    }

    private void WriteUnsafe(float value)
    {
      this.Last.AsFloat = value;
      this.Count = Math.Max(1, this.Count + 1);
      this.Sum.AsFloat += value;
      this.Min.AsFloat = Math.Min(this.Min.AsFloat, value);
      this.Max.AsFloat = Math.Max(this.Max.AsFloat, value);
    }

    private void WriteUnsafe(long value)
    {
      this.Last.AsLong = value;
      this.Count = Math.Max(1, this.Count + 1);
      this.Sum.AsLong += value;
      this.Min.AsLong = Math.Min(this.Min.AsLong, value);
      this.Max.AsLong = Math.Max(this.Max.AsLong, value);
    }

    private void ClearUnsafe()
    {
      if ((this.Type & MyStatTypeEnum.LongFlag) == MyStatTypeEnum.LongFlag)
      {
        this.Sum.AsLong = 0L;
        this.Min.AsLong = long.MaxValue;
        this.Max.AsLong = long.MinValue;
        this.Last.AsLong = 0L;
      }
      else
      {
        this.Sum.AsFloat = 0.0f;
        this.Min.AsFloat = float.MaxValue;
        this.Max.AsFloat = float.MinValue;
        this.Last.AsFloat = 0.0f;
      }
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct Value
    {
      [FieldOffset(0)]
      public float AsFloat;
      [FieldOffset(0)]
      public long AsLong;
    }
  }
}
