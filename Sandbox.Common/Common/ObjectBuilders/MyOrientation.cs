// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyOrientation
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    public struct MyOrientation
    {
        [ProtoMember(1)] [XmlAttribute] public float Yaw;
        [XmlAttribute] [ProtoMember(2)] public float Pitch;
        [ProtoMember(3)] [XmlAttribute] public float Roll;

        public MyOrientation(float yaw, float pitch, float roll)
        {
            this.Yaw = yaw;
            this.Pitch = pitch;
            this.Roll = roll;
        }
    }
}