// Decompiled with JetBrains decompiler
// Type: ParallelTasks.DelegateWork
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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