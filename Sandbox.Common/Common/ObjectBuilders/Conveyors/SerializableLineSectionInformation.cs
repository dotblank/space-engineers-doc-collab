// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Conveyors.SerializableLineSectionInformation
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Conveyors
{
    [ProtoContract]
    public struct SerializableLineSectionInformation
    {
        [ProtoMember(1)] [XmlAttribute] public Base6Directions.Direction Direction;
        [XmlAttribute] [ProtoMember(2)] public int Length;
    }
}