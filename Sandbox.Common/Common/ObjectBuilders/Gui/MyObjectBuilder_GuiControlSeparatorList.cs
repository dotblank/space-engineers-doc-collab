// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlSeparatorList
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Gui
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_GuiControlSeparatorList : MyObjectBuilder_GuiControlBase
    {
        [ProtoMember(1)] public List<MyObjectBuilder_GuiControlSeparatorList.Separator> Separators =
            new List<MyObjectBuilder_GuiControlSeparatorList.Separator>();

        [ProtoContract]
        public struct Separator
        {
            [DefaultValue(0.0f)]
            [XmlAttribute]
            [ProtoMember(1)]
            public float StartX { get; set; }

            [ProtoMember(2)]
            [DefaultValue(0.0f)]
            [XmlAttribute]
            public float StartY { get; set; }

            [ProtoMember(3)]
            [XmlAttribute]
            [DefaultValue(0.0f)]
            public float SizeX { get; set; }

            [XmlAttribute]
            [DefaultValue(0.0f)]
            [ProtoMember(4)]
            public float SizeY { get; set; }
        }
    }
}