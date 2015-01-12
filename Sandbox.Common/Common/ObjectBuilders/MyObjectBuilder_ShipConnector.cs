// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ShipConnector
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_ShipConnector : MyObjectBuilder_FunctionalBlock
    {
        [ProtoMember(1)] public MyObjectBuilder_Inventory Inventory;
        [DefaultValue(false)] [ProtoMember(2)] public bool ThrowOut;
        [DefaultValue(false)] [ProtoMember(3)] public bool CollectAll;
        [DefaultValue(false)] [ProtoMember(4)] public bool Connected;
        [ProtoMember(5)] [DefaultValue(0)] public long ConnectedEntityId;

        public MyObjectBuilder_ShipConnector()
        {
            this.DeformationRatio = 0.5f;
        }

        public bool ShouldSerializeConnectedEntityId()
        {
            return this.ConnectedEntityId != 0L;
        }

        public override void Remap(IMyRemapHelper remapHelper)
        {
            base.Remap(remapHelper);
            if (this.ConnectedEntityId == 0L)
                return;
            this.ConnectedEntityId = remapHelper.RemapEntityId(this.ConnectedEntityId);
        }

        public override void SetupForProjector()
        {
            base.SetupForProjector();
            if (this.Inventory == null)
                return;
            this.Inventory.Clear();
        }
    }
}