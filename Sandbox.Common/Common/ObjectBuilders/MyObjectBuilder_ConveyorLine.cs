// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ConveyorLine
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Conveyors;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_ConveyorLine : MyObjectBuilder_Base
    {
        [ProtoMember(5)] public List<MyObjectBuilder_ConveyorPacket> PacketsForward =
            new List<MyObjectBuilder_ConveyorPacket>();

        [ProtoMember(6)] public List<MyObjectBuilder_ConveyorPacket> PacketsBackward =
            new List<MyObjectBuilder_ConveyorPacket>();

        [ProtoMember(1)] public SerializableVector3I StartPosition;
        [ProtoMember(2)] public Base6Directions.Direction StartDirection;
        [ProtoMember(3)] public SerializableVector3I EndPosition;
        [ProtoMember(4)] public Base6Directions.Direction EndDirection;

        [DefaultValue(null)] [XmlArrayItem("Section")] [ProtoMember(7)] public List<SerializableLineSectionInformation>
            Sections;

        [DefaultValue(MyObjectBuilder_ConveyorLine.LineType.DEFAULT_LINE)] [ProtoMember(8)] public
            MyObjectBuilder_ConveyorLine.LineType ConveyorLineType;

        public bool ShouldSerializePacketsForward()
        {
            return this.PacketsForward.Count != 0;
        }

        public bool ShouldSerializePacketsBackward()
        {
            return this.PacketsBackward.Count != 0;
        }

        public bool ShouldSerializeSections()
        {
            return this.Sections != null;
        }

        public enum LineType
        {
            DEFAULT_LINE,
            SMALL_LINE,
            LARGE_LINE,
        }
    }
}