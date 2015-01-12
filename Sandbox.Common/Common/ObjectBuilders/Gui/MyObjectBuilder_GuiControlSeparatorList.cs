// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlSeparatorList
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
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

            [DefaultValue(0.0f)]
            [XmlAttribute]
            [ProtoMember(2)]
            public float StartY { get; set; }

            [DefaultValue(0.0f)]
            [XmlAttribute]
            [ProtoMember(3)]
            public float SizeX { get; set; }

            [XmlAttribute]
            [DefaultValue(0.0f)]
            [ProtoMember(4)]
            public float SizeY { get; set; }
        }
    }
}