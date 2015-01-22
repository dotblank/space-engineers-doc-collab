// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_WorldGeneratorOperation_AddAsteroidPrefab
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [XmlType("AddAsteroidPrefab")]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_WorldGeneratorOperation_AddAsteroidPrefab : MyObjectBuilder_WorldGeneratorOperation
    {
        [XmlAttribute] [ProtoMember(1)] public string PrefabFile;
        [ProtoMember(2)] [XmlAttribute] public string Name;
        [ProtoMember(3)] public SerializableVector3 Position;
    }
}