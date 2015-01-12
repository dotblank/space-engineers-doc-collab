// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_FactionRelation
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    public struct MyObjectBuilder_FactionRelation
    {
        [ProtoMember(1)] public long FactionId1;
        [ProtoMember(2)] public long FactionId2;
        [ProtoMember(3)] public MyRelationsBetweenFactions Relation;
    }
}