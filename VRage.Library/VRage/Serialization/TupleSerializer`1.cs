// Decompiled with JetBrains decompiler
// Type: VRage.Serialization.TupleSerializer`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using VRage;

namespace VRage.Serialization
{
    public class TupleSerializer<T1> : ISerializer<MyTuple<T1>>
    {
        public readonly ISerializer<T1> m_serializer1;

        public TupleSerializer(ISerializer<T1> serializer1)
        {
            this.m_serializer1 = serializer1;
        }

        void ISerializer<MyTuple<T1>>.Serialize(ByteStream destination, ref MyTuple<T1> data)
        {
            this.m_serializer1.Serialize(destination, ref data.Item1);
        }

        void ISerializer<MyTuple<T1>>.Deserialize(ByteStream source, out MyTuple<T1> data)
        {
            this.m_serializer1.Deserialize(source, out data.Item1);
        }
    }
}