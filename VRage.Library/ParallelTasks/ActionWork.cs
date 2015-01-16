// Decompiled with JetBrains decompiler
// Type: ParallelTasks.ActionWork
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ParallelTasks
{
    public class ActionWork : IWork
    {
        public readonly Action Action;

        public WorkOptions Options { get; private set; }

        public ActionWork(Action action)
            : this(action, Parallel.DefaultOptions)
        {
        }

        public ActionWork(Action action, WorkOptions options)
        {
            this.Action = action;
            this.Options = options;
        }

        public void DoWork()
        {
            this.Action();
        }
    }
}