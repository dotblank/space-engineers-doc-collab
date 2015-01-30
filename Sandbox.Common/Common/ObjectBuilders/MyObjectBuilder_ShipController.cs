// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ShipController
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_ShipController : MyObjectBuilder_TerminalBlock
  {
    [DefaultValue(true)]
    [ProtoMember(2)]
    public bool ControlThrusters = true;
    [ProtoMember(5)]
    [DefaultValue(null)]
    public SerializableDefinitionId? SelectedGunId = new SerializableDefinitionId?();
    [ProtoMember(1)]
    public bool UseSingleWeaponMode;
    [ProtoMember(3)]
    [DefaultValue(false)]
    public bool ControlWheels;
    [ProtoMember(4)]
    public MyObjectBuilder_Toolbar Toolbar;

    public override void Remap(IMyRemapHelper remapHelper)
    {
      base.Remap(remapHelper);
      if (this.Toolbar == null)
        return;
      this.Toolbar.Remap(remapHelper);
    }
  }
}
