// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ShipController
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_ShipController : MyObjectBuilder_TerminalBlock
    {
        [ProtoMember(2)] [DefaultValue(true)] public bool ControlThrusters = true;

        [ProtoMember(5)] [DefaultValue(null)] public SerializableDefinitionId? SelectedGunId =
            new SerializableDefinitionId?();

        [ProtoMember(1)] public bool UseSingleWeaponMode;
        [DefaultValue(false)] [ProtoMember(3)] public bool ControlWheels;
        [ProtoMember(4)] public MyObjectBuilder_Toolbar Toolbar;

        public override void Remap(IMyRemapHelper remapHelper)
        {
            base.Remap(remapHelper);
            if (this.Toolbar == null)
                return;
            this.Toolbar.Remap(remapHelper);
        }
    }
}