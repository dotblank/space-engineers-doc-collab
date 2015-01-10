// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_PrefabDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_PrefabDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(1)]
    public bool RespawnShip;
    [ProtoMember(2)]
    public MyObjectBuilder_CubeGrid CubeGrid;
    [XmlArrayItem("CubeGrid")]
    [ProtoMember(3)]
    public MyObjectBuilder_CubeGrid[] CubeGrids;

    public bool ShouldSerializeRespawnShip()
    {
      return false;
    }

    public bool ShouldSerializeCubeGrid()
    {
      return false;
    }
  }
}
