// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_CompoundBlockTemplateDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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
            [ProtoMember(1)] [XmlAttribute] public string BuildTypeReference;
            [ProtoMember(2)] [XmlArrayItem("Rotation")] public SerializableBlockOrientation[] Rotations;
        }

        [ProtoContract]
        public class CompoundBlockBinding
        {
            [ProtoMember(1)] [XmlAttribute] public string BuildType;
            [DefaultValue(false)] [ProtoMember(2)] [XmlAttribute] public bool Multiple;

            [XmlArrayItem("RotationBind")] [ProtoMember(3)] public
                MyObjectBuilder_CompoundBlockTemplateDefinition.CompoundBlockRotationBinding[] RotationBinds;
        }
    }
}