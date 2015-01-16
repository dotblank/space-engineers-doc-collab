// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_WorldGeneratorOperation_SetupBasePrefab
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [XmlType("SetupBasePrefab")]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_WorldGeneratorOperation_SetupBasePrefab : MyObjectBuilder_WorldGeneratorOperation
    {
        [XmlAttribute] [ProtoMember(1)] public string PrefabFile;
        [ProtoMember(2)] public SerializableVector3 Offset;
        [ProtoMember(3)] [XmlAttribute] public string AsteroidName;
        [ProtoMember(4)] [XmlAttribute] public string BeaconName;

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