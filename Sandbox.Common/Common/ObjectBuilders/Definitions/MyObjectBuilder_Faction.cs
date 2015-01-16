// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_Faction
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Collections.Generic;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    public class MyObjectBuilder_Faction
    {
        [ProtoMember(1)] public long FactionId;
        [ProtoMember(2)] public string Tag;
        [ProtoMember(3)] public string Name;
        [ProtoMember(4)] public string Description;
        [ProtoMember(5)] public string PrivateInfo;
        [ProtoMember(6)] public List<MyObjectBuilder_FactionMember> Members;
        [ProtoMember(7)] public List<MyObjectBuilder_FactionMember> JoinRequests;
        [ProtoMember(8)] public bool AutoAcceptMember;
        [ProtoMember(9)] public bool AutoAcceptPeace;
    }
}