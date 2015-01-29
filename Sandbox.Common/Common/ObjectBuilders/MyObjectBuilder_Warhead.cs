// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Warhead
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_Warhead : MyObjectBuilder_TerminalBlock
  {
    [ProtoMember(1)]
    public int CountdownMs = 10000;
    [ProtoMember(2)]
    public bool IsArmed;
    [ProtoMember(3)]
    public bool IsCountingDown;
  }
}
