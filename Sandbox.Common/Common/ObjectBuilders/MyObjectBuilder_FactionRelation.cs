// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_FactionRelation
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  public struct MyObjectBuilder_FactionRelation
  {
    [ProtoMember(1)]
    public long FactionId1;
    [ProtoMember(2)]
    public long FactionId2;
    [ProtoMember(3)]
    public MyRelationsBetweenFactions Relation;
  }
}
