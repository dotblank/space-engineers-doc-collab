// Decompiled with JetBrains decompiler
// Type: VRage.Algorithms.IMyPathVertex`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;

namespace VRage.Algorithms
{
    public interface IMyPathVertex<V> : IEnumerable<IMyPathEdge<V>>, IEnumerable
    {
        MyPathfindingData<V> PathfindingData { get; }

        float EstimateDistanceTo(IMyPathVertex<V> other);

        int GetNeighborCount();

        IMyPathVertex<V> GetNeighbor(int index);

        IMyPathEdge<V> GetEdge(int index);
    }
}