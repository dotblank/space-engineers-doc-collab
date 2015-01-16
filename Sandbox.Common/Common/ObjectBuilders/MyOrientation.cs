// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyOrientation
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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
        [XmlAttribute] [ProtoMember(3)] public float Roll;

        public MyOrientation(float yaw, float pitch, float roll)
        {
            this.Yaw = yaw;
            this.Pitch = pitch;
            this.Roll = roll;
        }
    }
}