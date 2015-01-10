// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ProductionBlockDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_ProductionBlockDefinition : MyObjectBuilder_CubeBlockDefinition
  {
    [ProtoMember(1)]
    public float InventoryMaxVolume;
    [ProtoMember(2)]
    public Vector3 InventorySize;
    [ProtoMember(3)]
    public float StandbyPowerConsumption;
    [ProtoMember(4)]
    public float OperationalPowerConsumption;
    [XmlArrayItem("Class")]
    [ProtoMember(5)]
    public string[] BlueprintClasses;
  }
}
