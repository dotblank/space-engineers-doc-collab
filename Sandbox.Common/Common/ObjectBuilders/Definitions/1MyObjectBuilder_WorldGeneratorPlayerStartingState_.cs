// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_WorldGeneratorPlayerStartingState_RespawnShip
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [XmlType("RespawnShip")]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_WorldGeneratorPlayerStartingState_RespawnShip : MyObjectBuilder_WorldGeneratorPlayerStartingState
  {
    [XmlAttribute]
    [ProtoMember(1)]
    public bool DampenersEnabled;
    [ProtoMember(2)]
    [XmlAttribute]
    public string RespawnShip;
  }
}
