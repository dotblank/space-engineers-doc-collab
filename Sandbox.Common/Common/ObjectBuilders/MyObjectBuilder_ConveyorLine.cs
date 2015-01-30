// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ConveyorLine
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
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
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_ConveyorLine : MyObjectBuilder_Base
  {
    [ProtoMember(5)]
    public List<MyObjectBuilder_ConveyorPacket> PacketsForward = new List<MyObjectBuilder_ConveyorPacket>();
    [ProtoMember(6)]
    public List<MyObjectBuilder_ConveyorPacket> PacketsBackward = new List<MyObjectBuilder_ConveyorPacket>();
    [ProtoMember(1)]
    public SerializableVector3I StartPosition;
    [ProtoMember(2)]
    public Base6Directions.Direction StartDirection;
    [ProtoMember(3)]
    public SerializableVector3I EndPosition;
    [ProtoMember(4)]
    public Base6Directions.Direction EndDirection;
    [XmlArrayItem("Section")]
    [ProtoMember(7)]
    [DefaultValue(null)]
    public List<SerializableLineSectionInformation> Sections;
    [ProtoMember(8)]
    [DefaultValue(MyObjectBuilder_ConveyorLine.LineType.DEFAULT_LINE)]
    public MyObjectBuilder_ConveyorLine.LineType ConveyorLineType;

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
