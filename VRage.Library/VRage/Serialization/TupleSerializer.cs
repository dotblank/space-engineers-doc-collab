// Decompiled with JetBrains decompiler
// Type: VRage.Serialization.TupleSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using VRage;

namespace VRage.Serialization
{
  public class TupleSerializer : ISerializer<MyTuple>
  {
    void ISerializer<MyTuple>.Serialize(ByteStream destination, ref MyTuple data)
    {
    }

    void ISerializer<MyTuple>.Deserialize(ByteStream source, out MyTuple data)
    {
    }
  }
}
