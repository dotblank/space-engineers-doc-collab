// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ToolbarItemTerminalGroup
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
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
