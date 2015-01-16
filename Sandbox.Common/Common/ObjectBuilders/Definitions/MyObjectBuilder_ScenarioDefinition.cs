// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ScenarioDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    [XmlType("ScenarioDefinition")]
    public class MyObjectBuilder_ScenarioDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(1)] public MyObjectBuilder_ScenarioDefinition.AsteroidClustersSettings AsteroidClusters;

        [XmlArrayItem("StartingState",
            Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_WorldGeneratorPlayerStartingState>))] [ProtoMember(2)] public MyObjectBuilder_WorldGeneratorPlayerStartingState[] PossibleStartingStates;

        [ProtoMember(3)] [XmlArrayItem("Operation", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_WorldGeneratorOperation>))] public MyObjectBuilder_WorldGeneratorOperation[] WorldGeneratorOperations;
        [ProtoMember(4)] [XmlArrayItem("Weapon")] public string[] CreativeModeWeapons;
        [ProtoMember(5)] [XmlArrayItem("Weapon")] public string[] SurvivalModeWeapons;
        [ProtoMember(6)] public MyObjectBuilder_ScenarioDefinition.WorldBoundingBox WorldBoundaries;

        [ProtoContract]
        public struct AsteroidClustersSettings
        {
            [ProtoMember(1)] [XmlAttribute] public bool Enabled;
            [ProtoMember(2)] [XmlAttribute] public float Offset;
            [XmlAttribute] [ProtoMember(3)] public bool CentralCluster;

            public bool ShouldSerializeOffset()
            {
                return this.Enabled;
            }

            public bool ShouldSerializeCentralCluster()
            {
                return this.Enabled;
            }
        }

        [ProtoContract]
        public struct WorldBoundingBox
        {
            [ProtoMember(1)] public SerializableVector3 Min;
            [ProtoMember(2)] public SerializableVector3 Max;
        }
    }
}