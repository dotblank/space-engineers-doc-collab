// Decompiled with JetBrains decompiler
// Type: ParallelTasks.IWorkScheduler
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ParallelTasks
{
  public interface IWorkScheduler
  {
    int ThreadCount { get; }

    void Schedule(Task item);

    bool WaitForTasksToFinish(TimeSpan waitTimeout);
  }
}
