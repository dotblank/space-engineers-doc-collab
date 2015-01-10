// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_VoxelMaterialDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  [XmlType("VoxelMaterial")]
  public class MyObjectBuilder_VoxelMaterialDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(2)]
    public string MinedOre;
    [ProtoMember(3)]
    public float MinedOreRatio;
    [ProtoMember(4)]
    public bool CanBeHarvested;
    [ProtoMember(5)]
    public bool IsRare;
    [ProtoMember(7)]
    public float DamageRatio;
    [ProtoMember(9)]
    public bool UseTwoTextures;
    [ProtoMember(10)]
    public float SpecularPower;
    [ProtoMember(11)]
    public float SpecularShininess;
    [ModdableContentFile("dds")]
    [ProtoMember(12)]
    public string DiffuseXZ;
    [ProtoMember(13)]
    [ModdableContentFile("dds")]
    public string NormalXZ;
    [ProtoMember(14)]
    [ModdableContentFile("dds")]
    public string DiffuseY;
    [ModdableContentFile("dds")]
    [ProtoMember(15)]
    public string NormalY;
  }
}
