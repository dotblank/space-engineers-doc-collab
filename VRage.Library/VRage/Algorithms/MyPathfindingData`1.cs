﻿// Decompiled with JetBrains decompiler
// Type: VRage.Algorithms.MyPathfindingData`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
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