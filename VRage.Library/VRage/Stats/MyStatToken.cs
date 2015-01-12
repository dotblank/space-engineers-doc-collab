// Decompiled with JetBrains decompiler
// Type: VRage.Stats.MyStatToken
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using VRage;

namespace VRage.Stats
{
    public struct MyStatToken : IDisposable
    {
        private readonly MyGameTimer m_timer;
        private readonly MyTimeSpan m_startTime;
        private readonly MyStat m_stat;

        internal MyStatToken(MyGameTimer timer, MyStat stat)
        {
            this.m_timer = timer;
            this.m_startTime = timer.Elapsed;
            this.m_stat = stat;
        }

        public void Dispose()
        {
            this.m_stat.Write((float) (this.m_timer.Elapsed - this.m_startTime).Miliseconds);
        }
    }
}