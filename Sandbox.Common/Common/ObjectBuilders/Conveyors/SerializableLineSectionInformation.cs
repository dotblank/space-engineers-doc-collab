// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Conveyors.SerializableLineSectionInformation
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Conveyors
{
    [ProtoContract]
    public struct SerializableLineSectionInformation
    {
        [XmlAttribute] [ProtoMember(1)] public Base6Directions.Direction Direction;
        [XmlAttribute] [ProtoMember(2)] public int Length;
    }
}