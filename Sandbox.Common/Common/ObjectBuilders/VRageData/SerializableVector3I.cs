// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.VRageData.SerializableVector3I
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.VRageData
{
    [ProtoContract]
    public struct SerializableVector3I
    {
        public int X;
        public int Y;
        public int Z;

        [XmlAttribute]
        [ProtoMember(1)]
        public int x
        {
            get { return this.X; }
            set { this.X = value; }
        }

        [ProtoMember(2)]
        [XmlAttribute]
        public int y
        {
            get { return this.Y; }
            set { this.Y = value; }
        }

        [XmlAttribute]
        [ProtoMember(3)]
        public int z
        {
            get { return this.Z; }
            set { this.Z = value; }
        }

        public SerializableVector3I(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static implicit operator Vector3I(SerializableVector3I v)
        {
            return new Vector3I(v.X, v.Y, v.Z);
        }

        public static implicit operator SerializableVector3I(Vector3I v)
        {
            return new SerializableVector3I(v.X, v.Y, v.Z);
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