﻿// Decompiled with JetBrains decompiler
// Type: ParallelTasks.IPrioritizedWork
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace ParallelTasks
{
  public interface IPrioritizedWork : IWork
  {
    WorkPriority Priority { get; }
  }
}
