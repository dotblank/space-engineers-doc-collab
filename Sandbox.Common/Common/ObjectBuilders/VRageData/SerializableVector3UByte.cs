// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.VRageData.SerializableVector3UByte
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.VRageData
{
    [ProtoContract]
    public struct SerializableVector3UByte
    {
        public byte X;
        public byte Y;
        public byte Z;

        [XmlAttribute]
        [ProtoMember(1)]
        public byte x
        {
            get { return this.X; }
            set { this.X = value; }
        }

        [XmlAttribute]
        [ProtoMember(2)]
        public byte y
        {
            get { return this.Y; }
            set { this.Y = value; }
        }

        [XmlAttribute]
        [ProtoMember(3)]
        public byte z
        {
            get { return this.Z; }
            set { this.Z = value; }
        }

        public SerializableVector3UByte(byte x, byte y, byte z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static implicit operator Vector3UByte(SerializableVector3UByte v)
        {
            return new Vector3UByte(v.X, v.Y, v.Z);
        }

        public static implicit operator SerializableVector3UByte(Vector3UByte v)
        {
            return new SerializableVector3UByte(v.X, v.Y, v.Z);
        }

        public bool ShouldSerializeX()
        {
            return false;
        }

        public bool ShouldSerializeY()
        {
            return false;
        }

        public bool ShouldSerializeZ()
        {
            return false;
        }
    }
}