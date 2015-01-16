// Decompiled with JetBrains decompiler
// Type: ParallelTasks.WorkOptions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace ParallelTasks
{
    public struct WorkOptions
    {
        public bool DetachFromParent { get; set; }

        public int MaximumThreads { get; set; }

        public bool QueueFIFO { get; set; }
    }
}