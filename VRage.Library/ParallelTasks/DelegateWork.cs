// Decompiled with JetBrains decompiler
// Type: ParallelTasks.DelegateWork
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using VRage.Collections;

namespace ParallelTasks
{
    internal class DelegateWork : IWork
    {
        private static MyConcurrentPool<DelegateWork> instances = new MyConcurrentPool<DelegateWork>(0, false);

        public Action Action { get; set; }

        public WorkOptions Options { get; set; }

        public void DoWork()
        {
            this.Action();
            this.Action = (Action) null;
            DelegateWork.instances.Return(this);
        }

        internal static DelegateWork GetInstance()
        {
            return DelegateWork.instances.Get();
        }
    }
}