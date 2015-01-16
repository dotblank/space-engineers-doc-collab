// Decompiled with JetBrains decompiler
// Type: ParallelTasks.Semaphore
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Threading;

namespace ParallelTasks
{
    public class Semaphore
    {
        private object free_lock = new object();
        private AutoResetEvent gate;
        private int free;

        public Semaphore(int maximumCount)
        {
            this.free = maximumCount;
            this.gate = new AutoResetEvent(this.free > 0);
        }

        public void WaitOne()
        {
            this.gate.WaitOne();
            lock (this.free_lock)
            {
                --this.free;
                if (this.free <= 0)
                    return;
                this.gate.Set();
            }
        }

        public void Release()
        {
            lock (this.free_lock)
            {
                ++this.free;
                this.gate.Set();
            }
        }
    }
}