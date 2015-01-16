// Decompiled with JetBrains decompiler
// Type: VRage.Algorithms.MyPathfindingData`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using VRage.Collections;

namespace VRage.Algorithms
{
    public class MyPathfindingData<V> : HeapItem<float>
    {
        internal long Timestamp;
        internal MyPathfindingData<V> Predecessor;
        internal float PathLength;

        public V Parent { get; private set; }

        public MyPathfindingData(V parent)
        {
            this.Parent = parent;
        }
    }
}