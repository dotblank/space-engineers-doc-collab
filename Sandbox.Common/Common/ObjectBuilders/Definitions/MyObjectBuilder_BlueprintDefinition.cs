// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_BlueprintDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_BlueprintDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(5)] public float BaseProductionTimeInSeconds = 1f;
        [ProtoMember(1)] [XmlArrayItem("Item")] public BlueprintItem[] Prerequisites;
        [ProtoMember(3)] public BlueprintItem Result;
        [ProtoMember(4)] [XmlArrayItem("Item")] public BlueprintItem[] Results;
    }
}