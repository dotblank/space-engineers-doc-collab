// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlSeparatorList
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Gui
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_GuiControlSeparatorList : MyObjectBuilder_GuiControlBase
  {
    [ProtoMember(1)]
    public List<MyObjectBuilder_GuiControlSeparatorList.Separator> Separators = new List<MyObjectBuilder_GuiControlSeparatorList.Separator>();

    [ProtoContract]
    public struct Separator
    {
      [XmlAttribute]
      [ProtoMember(1)]
      [DefaultValue(0.0f)]
      public float StartX { get; set; }

      [XmlAttribute]
      [DefaultValue(0.0f)]
      [ProtoMember(2)]
      public float StartY { get; set; }

      [XmlAttribute]
      [ProtoMember(3)]
      [DefaultValue(0.0f)]
      public float SizeX { get; set; }

      [XmlAttribute]
      [ProtoMember(4)]
      [DefaultValue(0.0f)]
      public float SizeY { get; set; }
    }
  }
}
