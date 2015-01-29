// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_VoxelMaterialDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [XmlType("VoxelMaterial")]
  [MyObjectBuilderDefinition]
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
    [ProtoMember(12)]
    [ModdableContentFile("dds")]
    public string DiffuseXZ;
    [ModdableContentFile("dds")]
    [ProtoMember(13)]
    public string NormalXZ;
    [ProtoMember(14)]
    [ModdableContentFile("dds")]
    public string DiffuseY;
    [ModdableContentFile("dds")]
    [ProtoMember(15)]
    public string NormalY;
  }
}
