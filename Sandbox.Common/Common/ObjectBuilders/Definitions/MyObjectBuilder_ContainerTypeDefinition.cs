// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ContainerTypeDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_ContainerTypeDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(2)] [XmlAttribute] public int CountMin;
        [XmlAttribute] [ProtoMember(3)] public int CountMax;
        [ProtoMember(4)] [XmlArrayItem("Item")] public MyObjectBuilder_ContainerTypeDefinition.ContainerTypeItem[] Items;

        [ProtoContract]
        public class ContainerTypeItem
        {
            [ProtoMember(3)] [DefaultValue(1f)] public float Frequency = 1f;
            [ProtoMember(1)] [XmlAttribute] public string AmountMin;
            [ProtoMember(2)] [XmlAttribute] public string AmountMax;
            [ProtoMember(4)] public SerializableDefinitionId Id;
        }
    }
}