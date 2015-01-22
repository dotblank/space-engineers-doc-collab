// Decompiled with JetBrains decompiler
// Type: VRage.MyTimeSpan
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace VRage
{
  public struct MyTimeSpan
  {
    public static readonly MyTimeSpan Zero = new MyTimeSpan();
    public static readonly MyTimeSpan MaxValue = new MyTimeSpan(long.MaxValue);
    public readonly long Ticks;

    public double Nanoseconds
    {
      get
      {
        return (double) this.Ticks / ((double) MyGameTimer.Frequency / 1000000000.0);
      }
    }

    public double Microseconds
    {
      get
      {
        return (double) this.Ticks / ((double) MyGameTimer.Frequency / 1000000.0);
      }
    }

    public double Miliseconds
    {
      get
      {
        return (double) this.Ticks / ((double) MyGameTimer.Frequency / 1000.0);
      }
    }

    public double Seconds
    {
      get
      {
        return (double) this.Ticks / (double) MyGameTimer.Frequency;
      }
    }

    public TimeSpan TimeSpan
    {
      get
      {
        return TimeSpan.FromTicks((long) Math.Round((double) this.Ticks * (10000000.0 / (double) MyGameTimer.Frequency)));
      }
    }

    public MyTimeSpan(long stopwatchTicks)
    {
      this.Ticks = stopwatchTicks;
    }

    public static MyTimeSpan operator +(MyTimeSpan a, MyTimeSpan b)
    {
      return new MyTimeSpan(a.Ticks + b.Ticks);
    }

    public static MyTimeSpan operator -(MyTimeSpan a, MyTimeSpan b)
    {
      return new MyTimeSpan(a.Ticks - b.Ticks);
    }

    public static bool operator !=(MyTimeSpan a, MyTimeSpan b)
    {
      return a.Ticks != b.Ticks;
    }

    public static bool operator ==(MyTimeSpan a, MyTimeSpan b)
    {
      return a.Ticks == b.Ticks;
    }

    public static bool operator >(MyTimeSpan a, MyTimeSpan b)
    {
      return a.Ticks > b.Ticks;
    }

    public static bool operator <(MyTimeSpan a, MyTimeSpan b)
    {
      return a.Ticks < b.Ticks;
    }

    public static bool operator >=(MyTimeSpan a, MyTimeSpan b)
    {
      return a.Ticks >= b.Ticks;
    }

    public static bool operator <=(MyTimeSpan a, MyTimeSpan b)
    {
      return a.Ticks <= b.Ticks;
    }

    public override bool Equals(object obj)
    {
      return this.Ticks == ((MyTimeSpan) obj).Ticks;
    }

    public override int GetHashCode()
    {
      return this.Ticks.GetHashCode();
    }

    public static MyTimeSpan FromTicks(long ticks)
    {
      return new MyTimeSpan(ticks);
    }

    public static MyTimeSpan FromSeconds(double seconds)
    {
      return MyTimeSpan.FromMiliseconds(seconds * 1000.0);
    }

    public static MyTimeSpan FromMiliseconds(double miliseconds)
    {
      return new MyTimeSpan((long) (miliseconds * (1.0 / 1000.0) * (double) MyGameTimer.Frequency));
    }

    public override string ToString()
    {
      return ((int) Math.Round(this.Miliseconds)).ToString();
    }
  }
}
