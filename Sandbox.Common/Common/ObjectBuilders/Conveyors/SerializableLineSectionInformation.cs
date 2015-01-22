// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Conveyors.SerializableLineSectionInformation
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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