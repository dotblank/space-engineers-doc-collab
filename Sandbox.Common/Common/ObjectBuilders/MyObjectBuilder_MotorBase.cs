// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_MotorBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
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
