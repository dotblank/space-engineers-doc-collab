// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.VRageData.SerializableVector2
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.VRageData
{
    [ProtoContract]
    public struct SerializableVector2
    {
        public float X;
        public float Y;

        [ProtoMember(1)]
        [XmlAttribute]
        public float x
        {
            get { return this.X; }
            set { this.X = value; }
        }

        [XmlAttribute]
        [ProtoMember(2)]
        public float y
        {
            get { return this.Y; }
            set { this.Y = value; }
        }

        public SerializableVector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator Vector2(SerializableVector2 v)
        {
            return new Vector2(v.X, v.Y);
        }

        public static implicit operator SerializableVector2(Vector2 v)
        {
            return new SerializableVector2(v.X, v.Y);
        }

        public bool ShouldSerializeX()
        {
            return false;
        }

        public bool ShouldSerializeY()
        {
            return false;
        }
    }
}