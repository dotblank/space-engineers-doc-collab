// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_SpawnGroupDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_SpawnGroupDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(1)] [DefaultValue(1f)] public float Frequency = 1f;
        [XmlArrayItem("Prefab")] [ProtoMember(2)] public MyObjectBuilder_SpawnGroupDefinition.SpawnGroupPrefab[] Prefabs;
        [XmlArrayItem("Voxel")] [ProtoMember(3)] public MyObjectBuilder_SpawnGroupDefinition.SpawnGroupVoxel[] Voxels;
        [ProtoMember(4)] [DefaultValue(false)] public bool IsEncounter;

        [ProtoContract]
        public class SpawnGroupPrefab
        {
            [ProtoMember(3)] [DefaultValue("")] public string BeaconText = "";
            [ProtoMember(4)] [DefaultValue(10f)] public float Speed = 10f;
            [XmlAttribute] [ProtoMember(1)] public string SubtypeId;
            [ProtoMember(2)] public Vector3 Position;
        }

        [ProtoContract]
        public class SpawnGroupVoxel
        {
            [ProtoMember(1)] [XmlAttribute] public string StorageName;
            [ProtoMember(2)] public Vector3 Offset;
        }
    }
}