// Decompiled with JetBrains decompiler
// Type: VRage.Parallelization.MyPausableJob
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Threading;

namespace VRage.Parallelization
{
    public class MyPausableJob
    {
        private AutoResetEvent m_pausedEvent = new AutoResetEvent(false);
        private AutoResetEvent m_resumedEvent = new AutoResetEvent(false);
        private volatile bool m_pause;

        public bool IsPaused
        {
            get { return this.m_pause; }
        }

        public void Pause()
        {
            this.m_pause = true;
            this.m_pausedEvent.WaitOne();
        }

        public void Resume()
        {
            this.m_pause = false;
            this.m_resumedEvent.Set();
        }

        public void AllowPauseHere()
        {
            if (!this.m_pause)
                return;
            this.m_pausedEvent.Set();
            this.m_resumedEvent.WaitOne();
        }
    }
}