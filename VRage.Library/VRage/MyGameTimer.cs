// Decompiled with JetBrains decompiler
// Type: VRage.MyGameTimer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Diagnostics;

namespace VRage
{
    public class MyGameTimer
    {
        public static readonly long Frequency = Stopwatch.Frequency;
        private long m_startTicks;

        public TimeSpan ElapsedTimeSpan
        {
            get { return this.Elapsed.TimeSpan; }
        }

        public long ElapsedTicks
        {
            get { return Stopwatch.GetTimestamp() - this.m_startTicks; }
        }

        public MyTimeSpan Elapsed
        {
            get { return new MyTimeSpan(Stopwatch.GetTimestamp() - this.m_startTicks); }
        }

        public MyGameTimer()
        {
            this.m_startTicks = Stopwatch.GetTimestamp();
        }
    }
}