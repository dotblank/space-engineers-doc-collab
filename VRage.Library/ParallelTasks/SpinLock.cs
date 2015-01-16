// Decompiled with JetBrains decompiler
// Type: ParallelTasks.SpinLock
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Threading;

namespace ParallelTasks
{
    public struct SpinLock
    {
        private Thread owner;
        private int recursion;

        public void Enter()
        {
            Thread currentThread = Thread.CurrentThread;
            if (this.owner == currentThread)
            {
                Interlocked.Increment(ref this.recursion);
            }
            else
            {
                do
                    ; while (Interlocked.CompareExchange<Thread>(ref this.owner, currentThread, (Thread) null) != null);
                Interlocked.Increment(ref this.recursion);
            }
        }

        public bool TryEnter()
        {
            Thread currentThread = Thread.CurrentThread;
            if (this.owner == currentThread)
            {
                Interlocked.Increment(ref this.recursion);
                return true;
            }
            else
            {
                bool flag = Interlocked.CompareExchange<Thread>(ref this.owner, currentThread, (Thread) null) == null;
                if (flag)
                    Interlocked.Increment(ref this.recursion);
                return flag;
            }
        }

        public void Exit()
        {
            if (Thread.CurrentThread != this.owner)
                throw new InvalidOperationException(
                    "Exit cannot be called by a thread which does not currently own the lock.");
            Interlocked.Decrement(ref this.recursion);
            if (this.recursion != 0)
                return;
            this.owner = (Thread) null;
        }
    }
}