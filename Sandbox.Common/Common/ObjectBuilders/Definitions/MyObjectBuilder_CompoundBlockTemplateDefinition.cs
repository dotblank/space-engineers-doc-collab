// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_CompoundBlockTemplateDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
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
    [XmlArrayItem("Binding")]
    [ProtoMember(1)]
    public MyObjectBuilder_CompoundBlockTemplateDefinition.CompoundBlockBinding[] Bindings;

    [ProtoContract]
    public class CompoundBlockRotationBinding
    {
      [ProtoMember(1)]
      [XmlAttribute]
      public string BuildTypeReference;
      [XmlArrayItem("Rotation")]
      [ProtoMember(2)]
      public SerializableBlockOrientation[] Rotations;
    }

    [ProtoContract]
    public class CompoundBlockBinding
    {
      [ProtoMember(1)]
      [XmlAttribute]
      public string BuildType;
      [XmlAttribute]
      [ProtoMember(2)]
      [DefaultValue(false)]
      public bool Multiple;
      [XmlArrayItem("RotationBind")]
      [ProtoMember(3)]
      public MyObjectBuilder_CompoundBlockTemplateDefinition.CompoundBlockRotationBinding[] RotationBinds;
    }
  }
}
