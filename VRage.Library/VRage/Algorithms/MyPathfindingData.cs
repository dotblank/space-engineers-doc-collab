// Decompiled with JetBrains decompiler
// Type: VRage.Algorithms.MyPathfindingData
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using VRage.Collections;

namespace VRage.Algorithms
{
  public class MyPathfindingData : HeapItem<float>
  {
    internal long Timestamp;
    internal MyPathfindingData Predecessor;
    internal float PathLength;

    public object Parent { get; private set; }

    public MyPathfindingData(object parent)
    {
      this.Parent = parent;
    }
  }
}
