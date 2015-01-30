// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_SpawnGroupDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_SpawnGroupDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(1)]
    [DefaultValue(1f)]
    public float Frequency = 1f;
    [XmlArrayItem("Prefab")]
    [ProtoMember(2)]
    public MyObjectBuilder_SpawnGroupDefinition.SpawnGroupPrefab[] Prefabs;
    [XmlArrayItem("Voxel")]
    [ProtoMember(3)]
    public MyObjectBuilder_SpawnGroupDefinition.SpawnGroupVoxel[] Voxels;
    [DefaultValue(false)]
    [ProtoMember(4)]
    public bool IsEncounter;

    [ProtoContract]
    public class SpawnGroupPrefab
    {
      [DefaultValue("")]
      [ProtoMember(3)]
      public string BeaconText = "";
      [ProtoMember(4)]
      [DefaultValue(10f)]
      public float Speed = 10f;
      [XmlAttribute]
      [ProtoMember(1)]
      public string SubtypeId;
      [ProtoMember(2)]
      public Vector3 Position;
    }

    [ProtoContract]
    public class SpawnGroupVoxel
    {
      [XmlAttribute]
      [ProtoMember(1)]
      public string StorageName;
      [ProtoMember(2)]
      public Vector3 Offset;
    }
  }
}
