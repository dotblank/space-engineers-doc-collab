// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_FactionCollection
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;
using System.Collections.Generic;
using VRage.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_FactionCollection : MyObjectBuilder_Base
    {
        [ProtoMember(1)] public List<MyObjectBuilder_Faction> Factions;
        [ProtoMember(2)] public SerializableDictionary<long, long> Players;
        [ProtoMember(3)] public List<MyObjectBuilder_FactionRelation> Relations;
        [ProtoMember(4)] public List<MyObjectBuilder_FactionRequests> Requests;
    }
}