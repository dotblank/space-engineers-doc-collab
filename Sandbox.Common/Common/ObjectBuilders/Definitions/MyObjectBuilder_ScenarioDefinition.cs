// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ScenarioDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [XmlType("ScenarioDefinition")]
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_ScenarioDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(1)] public MyObjectBuilder_ScenarioDefinition.AsteroidClustersSettings AsteroidClusters;

        [ProtoMember(2)] [XmlArrayItem("StartingState",
            Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_WorldGeneratorPlayerStartingState>))] public
            MyObjectBuilder_WorldGeneratorPlayerStartingState[] PossibleStartingStates;

        [XmlArrayItem("Operation", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_WorldGeneratorOperation>))] [ProtoMember(3)] public MyObjectBuilder_WorldGeneratorOperation[] WorldGeneratorOperations;
        [ProtoMember(4)] [XmlArrayItem("Weapon")] public string[] CreativeModeWeapons;
        [ProtoMember(5)] [XmlArrayItem("Weapon")] public string[] SurvivalModeWeapons;
        [ProtoMember(6)] public MyObjectBuilder_ScenarioDefinition.WorldBoundingBox WorldBoundaries;

        [ProtoContract]
        public struct AsteroidClustersSettings
        {
            [XmlAttribute] [ProtoMember(1)] public bool Enabled;
            [XmlAttribute] [ProtoMember(2)] public float Offset;
            [ProtoMember(3)] [XmlAttribute] public bool CentralCluster;

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