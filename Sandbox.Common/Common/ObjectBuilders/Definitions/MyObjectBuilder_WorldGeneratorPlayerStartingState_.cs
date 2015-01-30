// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_WorldGeneratorPlayerStartingState_Transform
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [XmlType("Transform")]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_WorldGeneratorPlayerStartingState_Transform : MyObjectBuilder_WorldGeneratorPlayerStartingState
  {
    [ProtoMember(1)]
    public MyPositionAndOrientation? Transform;
    [XmlAttribute]
    [ProtoMember(2)]
    public bool JetpackEnabled;
    [XmlAttribute]
    [ProtoMember(3)]
    public bool DampenersEnabled;

    public bool ShouldSerializeTransform()
    {
      return this.Transform.HasValue;
    }
  }
}
