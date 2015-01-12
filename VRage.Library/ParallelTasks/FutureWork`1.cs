﻿// Decompiled with JetBrains decompiler
// Type: ParallelTasks.FutureWork`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Threading;

namespace ParallelTasks
{
    internal class FutureWork<T> : IWork
    {
        public int ID { get; private set; }

        public WorkOptions Options { get; set; }

        public Func<T> Function { get; set; }

        public T Result { get; set; }

        public void DoWork()
        {
            this.Result = this.Function();
        }

        public static FutureWork<T> GetInstance()
        {
            return Singleton<Pool<FutureWork<T>>>.Instance.Get(Thread.CurrentThread);
        }

        public void ReturnToPool()
        {
            if (this.ID >= int.MaxValue)
                return;
            ++this.ID;
            this.Function = (Func<T>) null;
            this.Result = default (T);
            Singleton<Pool<FutureWork<T>>>.Instance.Return(Thread.CurrentThread, this);
        }
    }
}