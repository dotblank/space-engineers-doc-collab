// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ShipToolBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_ShipToolBase : MyObjectBuilder_FunctionalBlock
    {
        [DefaultValue(true)] [ProtoMember(2)] public bool UseConveyorSystem = true;
        [ProtoMember(1)] public MyObjectBuilder_Inventory Inventory;

        public MyObjectBuilder_ShipToolBase()
        {
            this.Enabled = false;
            this.DeformationRatio = 0.5f;
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