// Decompiled with JetBrains decompiler
// Type: ParallelTasks.ForEachLoopWork`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Threading;

namespace ParallelTasks
{
    internal class ForEachLoopWork<T> : IWork
    {
        private static Pool<ForEachLoopWork<T>> pool = Singleton<Pool<ForEachLoopWork<T>>>.Instance;
        private Action<T> action;
        private IEnumerator<T> enumerator;
        private volatile bool notDone;
        private object syncLock;

        public WorkOptions Options { get; private set; }

        public ForEachLoopWork()
        {
            this.Options = new WorkOptions()
            {
                MaximumThreads = int.MaxValue
            };
            this.syncLock = new object();
        }

        public void Prepare(Action<T> action, IEnumerator<T> enumerator)
        {
            this.action = action;
            this.enumerator = enumerator;
            this.notDone = true;
        }

        public void DoWork()
        {
            T obj = default (T);
            while (this.notDone)
            {
                bool flag = false;
                lock (this.syncLock)
                {
                    if (this.notDone = this.enumerator.MoveNext())
                    {
                        obj = this.enumerator.Current;
                        flag = true;
                    }
                }
                if (flag)
                    this.action(obj);
            }
        }

        public static ForEachLoopWork<T> Get()
        {
            return ForEachLoopWork<T>.pool.Get(Thread.CurrentThread);
        }

        public void Return()
        {
            this.enumerator = (IEnumerator<T>) null;
            this.action = (Action<T>) null;
            ForEachLoopWork<T>.pool.Return(Thread.CurrentThread, this);
        }
    }
}