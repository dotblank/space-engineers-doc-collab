// Decompiled with JetBrains decompiler
// Type: ParallelTasks.ActionWork
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
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
