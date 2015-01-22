// Decompiled with JetBrains decompiler
// Type: VRage.TimeUtil
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace VRage
{
  public static class TimeUtil
  {
    public static DateTime LocalTime
    {
      get
      {
        TimeUtil.SYSTEMTIME time;
        TimeUtil.GetLocalTime(out time);
        return new DateTime((int) time.Year, (int) time.Month, (int) time.Day, (int) time.Hour, (int) time.Minute, (int) time.Second, (int) time.Milliseconds, DateTimeKind.Local);
      }
    }

    [DllImport("kernel32.dll")]
    private static extern void GetLocalTime(out TimeUtil.SYSTEMTIME time);

    private struct SYSTEMTIME
    {
      public ushort Year;
      public ushort Month;
      public ushort DayOfWeek;
      public ushort Day;
      public ushort Hour;
      public ushort Minute;
      public ushort Second;
      public ushort Milliseconds;
    }
  }
}
