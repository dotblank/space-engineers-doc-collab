// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_MultiBlockDefinition
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
  public class MyObjectBuilder_MultiBlockDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(1)]
    [XmlArrayItem("BlockDefinition")]
    [DefaultValue(null)]
    public MyObjectBuilder_MultiBlockDefinition.MyOBMultiBlockPartDefinition[] BlockDefinitions;

    [ProtoContract]
    public class MyOBMultiBlockPartDefinition
    {
      [ProtoMember(1)]
      public SerializableDefinitionId Id;
      [ProtoMember(2)]
      public SerializableVector3I Position;
      [ProtoMember(3)]
      public SerializableBlockOrientation Orientation;
    }
  }
}
