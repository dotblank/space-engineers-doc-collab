// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_DefinitionBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public abstract class MyObjectBuilder_DefinitionBase : MyObjectBuilder_Base
    {
        [DefaultValue(true)] [ProtoMember(5)] public bool Public = true;
        [DefaultValue(true)] [XmlAttribute(AttributeName = "Enabled")] [ProtoMember(6)] public bool Enabled = true;
        [ProtoMember(1)] public SerializableDefinitionId Id;
        [ProtoMember(2)] [DefaultValue("")] public string DisplayName;
        [ProtoMember(3)] [DefaultValue("")] public string Description;
        [ProtoMember(4)] [ModdableContentFile("dds")] public string Icon;
    }
}