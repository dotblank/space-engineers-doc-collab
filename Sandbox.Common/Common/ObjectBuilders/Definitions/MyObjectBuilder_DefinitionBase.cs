// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_DefinitionBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public abstract class MyObjectBuilder_DefinitionBase : MyObjectBuilder_Base
    {
        [ProtoMember(5)] [DefaultValue(true)] public bool Public = true;
        [ProtoMember(6)] [XmlAttribute(AttributeName = "Enabled")] [DefaultValue(true)] public bool Enabled = true;
        [ProtoMember(1)] public SerializableDefinitionId Id;
        [ProtoMember(2)] [DefaultValue("")] public string DisplayName;
        [ProtoMember(3)] [DefaultValue("")] public string Description;
        [ModdableContentFile("dds")] [ProtoMember(4)] public string Icon;
    }
}