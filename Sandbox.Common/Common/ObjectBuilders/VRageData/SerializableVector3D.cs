// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.VRageData.SerializableVector3D
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.VRageData
{
    [ProtoContract]
    public struct SerializableVector3D
    {
        public double X;
        public double Y;
        public double Z;

        [XmlAttribute]
        [ProtoMember(1)]
        public double x
        {
            get { return this.X; }
            set { this.X = value; }
        }

        [ProtoMember(2)]
        [XmlAttribute]
        public double y
        {
            get { return this.Y; }
            set { this.Y = value; }
        }

        [XmlAttribute]
        [ProtoMember(3)]
        public double z
        {
            get { return this.Z; }
            set { this.Z = value; }
        }

        public bool IsZero
        {
            get
            {
                if (this.X == 0.0 && this.Y == 0.0)
                    return this.Z == 0.0;
                else
                    return false;
            }
        }

        public SerializableVector3D(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public SerializableVector3D(Vector3D v)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z;
        }

        public static implicit operator Vector3D(SerializableVector3D v)
        {
            return new Vector3D(v.X, v.Y, v.Z);
        }

        public static implicit operator SerializableVector3D(Vector3D v)
        {
            return new SerializableVector3D(v.X, v.Y, v.Z);
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