// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_BlueprintDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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
        [XmlArrayItem("Item")] [ProtoMember(4)] public BlueprintItem[] Results;
    }
}