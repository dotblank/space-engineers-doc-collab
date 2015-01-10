// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ContainerTypeDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
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
    [XmlAttribute]
    [ProtoMember(2)]
    public int CountMin;
    [ProtoMember(3)]
    [XmlAttribute]
    public int CountMax;
    [XmlArrayItem("Item")]
    [ProtoMember(4)]
    public MyObjectBuilder_ContainerTypeDefinition.ContainerTypeItem[] Items;

    [ProtoContract]
    public class ContainerTypeItem
    {
      [ProtoMember(3)]
      [DefaultValue(1f)]
      public float Frequency = 1f;
      [ProtoMember(1)]
      [XmlAttribute]
      public string AmountMin;
      [ProtoMember(2)]
      [XmlAttribute]
      public string AmountMax;
      [ProtoMember(4)]
      public SerializableDefinitionId Id;
    }
  }
}
