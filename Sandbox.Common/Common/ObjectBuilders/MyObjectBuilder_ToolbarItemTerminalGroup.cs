// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ToolbarItemTerminalGroup
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_ToolbarItemTerminalGroup : MyObjectBuilder_ToolbarItemTerminal
  {
    public long GridEntityId;
    [ProtoMember(1)]
    public long BlockEntityId;
    [ProtoMember(2)]
    public string GroupName;

    public override void Remap(IMyRemapHelper remapHelper)
    {
      if (this.BlockEntityId == 0L)
        return;
      this.BlockEntityId = remapHelper.RemapEntityId(this.BlockEntityId);
    }
  }
}
