// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ContainerTypeDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_ContainerTypeDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(2)]
    [XmlAttribute]
    public int CountMin;
    [ProtoMember(3)]
    [XmlAttribute]
    public int CountMax;
    [ProtoMember(4)]
    [XmlArrayItem("Item")]
    public MyObjectBuilder_ContainerTypeDefinition.ContainerTypeItem[] Items;

    [ProtoContract]
    public class ContainerTypeItem
    {
      [ProtoMember(3)]
      [DefaultValue(1f)]
      public float Frequency = 1f;
      [XmlAttribute]
      [ProtoMember(1)]
      public string AmountMin;
      [XmlAttribute]
      [ProtoMember(2)]
      public string AmountMax;
      [ProtoMember(4)]
      public SerializableDefinitionId Id;
    }
  }
}
