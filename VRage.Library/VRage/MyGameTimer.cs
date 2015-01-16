// Decompiled with JetBrains decompiler
// Type: VRage.MyGameTimer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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