// Decompiled with JetBrains decompiler
// Type: VRage.TimeUtil
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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
                return new DateTime((int) time.Year, (int) time.Month, (int) time.Day, (int) time.Hour,
                    (int) time.Minute, (int) time.Second, (int) time.Milliseconds, DateTimeKind.Local);
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