// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_TimerBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_TimerBlock : MyObjectBuilder_FunctionalBlock
  {
    [ProtoMember(3)]
    public int Delay = 10000;
    [ProtoMember(1)]
    public MyObjectBuilder_Toolbar Toolbar;
    [DefaultValue(false)]
    [ProtoMember(2)]
    public bool JustTriggered;
    [ProtoMember(4)]
    public int CurrentTime;

    public override void Remap(IMyRemapHelper remapHelper)
    {
      base.Remap(remapHelper);
      this.Toolbar.Remap(remapHelper);
    }
  }
}
