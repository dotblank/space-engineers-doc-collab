// Decompiled with JetBrains decompiler
// Type: ParallelTasks.ActionWork
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
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