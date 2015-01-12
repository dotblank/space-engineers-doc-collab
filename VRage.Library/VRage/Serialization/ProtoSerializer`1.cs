// Decompiled with JetBrains decompiler
// Type: VRage.Serialization.ProtoSerializer`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf.Meta;
using System.IO;
using VRage;

namespace VRage.Serialization
{
    public class ProtoSerializer<T> : ISerializer<T>
    {
        public static readonly ProtoSerializer<T> Default = new ProtoSerializer<T>((RuntimeTypeModel) null);
        public readonly RuntimeTypeModel Model;

        public ProtoSerializer(RuntimeTypeModel model = null)
        {
            this.Model = model ?? RuntimeTypeModel.Default;
        }

        public void Serialize(ByteStream destination, ref T data)
        {
            ((TypeModel) this.Model).Serialize((Stream) destination, (object) data);
        }

        public void Deserialize(ByteStream source, out T data)
        {
            data = (T) ((TypeModel) this.Model).Deserialize((Stream) source, (object) null, typeof (T));
        }
    }
}