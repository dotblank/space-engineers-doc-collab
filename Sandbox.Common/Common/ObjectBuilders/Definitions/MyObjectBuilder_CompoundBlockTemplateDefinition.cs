// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_CompoundBlockTemplateDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_CompoundBlockTemplateDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(1)] [XmlArrayItem("Binding")] public
            MyObjectBuilder_CompoundBlockTemplateDefinition.CompoundBlockBinding[] Bindings;

        [ProtoContract]
        public class CompoundBlockRotationBinding
        {
            [XmlAttribute] [ProtoMember(1)] public string BuildTypeReference;
            [ProtoMember(2)] [XmlArrayItem("Rotation")] public SerializableBlockOrientation[] Rotations;
        }

        [ProtoContract]
        public class CompoundBlockBinding
        {
            [XmlAttribute] [ProtoMember(1)] public string BuildType;
            [ProtoMember(2)] [DefaultValue(false)] [XmlAttribute] public bool Multiple;

            [XmlArrayItem("RotationBind")] [ProtoMember(3)] public
                MyObjectBuilder_CompoundBlockTemplateDefinition.CompoundBlockRotationBinding[] RotationBinds;
        }
    }
}