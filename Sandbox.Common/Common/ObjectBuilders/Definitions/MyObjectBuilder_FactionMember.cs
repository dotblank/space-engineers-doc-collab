// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_FactionMember
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  public struct MyObjectBuilder_FactionMember
  {
    [ProtoMember(1)]
    public long PlayerId;
    [ProtoMember(2)]
    public bool IsLeader;
    [ProtoMember(3)]
    public bool IsFounder;
  }
}
