// Decompiled with JetBrains decompiler
// Type: VRage.Algorithms.IMyPathVertex`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;

namespace VRage.Algorithms
{
  public interface IMyPathVertex<V> : IEnumerable<IMyPathEdge<V>>, IEnumerable
  {
    MyPathfindingData PathfindingData { get; }

    float EstimateDistanceTo(IMyPathVertex<V> other);

    int GetNeighborCount();

    IMyPathVertex<V> GetNeighbor(int index);

    IMyPathEdge<V> GetEdge(int index);
  }
}
