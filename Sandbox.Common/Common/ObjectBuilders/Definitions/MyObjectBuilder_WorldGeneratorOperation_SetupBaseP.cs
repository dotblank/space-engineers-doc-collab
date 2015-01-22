// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_WorldGeneratorOperation_SetupBasePrefab
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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
        [ProtoMember(1)] [XmlAttribute] public string PrefabFile;
        [ProtoMember(2)] public SerializableVector3 Offset;
        [XmlAttribute] [ProtoMember(3)] public string AsteroidName;
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