// Decompiled with JetBrains decompiler
// Type: VRage.Serialization.TupleSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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