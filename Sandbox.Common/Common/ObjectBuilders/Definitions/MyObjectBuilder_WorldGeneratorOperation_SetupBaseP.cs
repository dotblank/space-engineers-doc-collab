// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_WorldGeneratorOperation_SetupBasePrefab
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [XmlType("SetupBasePrefab")]
    public class MyObjectBuilder_WorldGeneratorOperation_SetupBasePrefab : MyObjectBuilder_WorldGeneratorOperation
    {
        [ProtoMember(1)] [XmlAttribute] public string PrefabFile;
        [ProtoMember(2)] public SerializableVector3 Offset;
        [XmlAttribute] [ProtoMember(3)] public string AsteroidName;
        [XmlAttribute] [ProtoMember(4)] public string BeaconName;

        public bool ShouldSerializeOffset()
        {
            return (Vector3) this.Offset != Vector3.Zero;
        }

        public bool ShouldSerializeBeaconName()
        {
            return !string.IsNullOrEmpty(this.BeaconName);
        }
    }
}