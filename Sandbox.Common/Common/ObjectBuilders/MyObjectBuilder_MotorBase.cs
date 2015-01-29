// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_MotorBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_MotorBase : MyObjectBuilder_FunctionalBlock
  {
    [ProtoMember(1)]
    public long RotorEntityId;

    public override void Remap(IMyRemapHelper remapHelper)
    {
      base.Remap(remapHelper);
      if (this.RotorEntityId == 0L)
        return;
      this.RotorEntityId = remapHelper.RemapEntityId(this.RotorEntityId);
    }
  }
}
