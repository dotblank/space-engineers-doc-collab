// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ContainerTypeDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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
        [ProtoMember(3)] [XmlAttribute] public int CountMax;
        [XmlArrayItem("Item")] [ProtoMember(4)] public MyObjectBuilder_ContainerTypeDefinition.ContainerTypeItem[] Items;

        [ProtoContract]
        public class ContainerTypeItem
        {
            [DefaultValue(1f)] [ProtoMember(3)] public float Frequency = 1f;
            [XmlAttribute] [ProtoMember(1)] public string AmountMin;
            [XmlAttribute] [ProtoMember(2)] public string AmountMax;
            [ProtoMember(4)] public SerializableDefinitionId Id;
        }
    }
}