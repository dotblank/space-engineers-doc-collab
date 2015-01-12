// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Inventory
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Collections.Generic;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_Inventory : MyObjectBuilder_Base
    {
        [ProtoMember(1)] public List<MyObjectBuilder_InventoryItem> Items = new List<MyObjectBuilder_InventoryItem>();
        [ProtoMember(2)] public uint nextItemId;

        internal void Clear()
        {
            this.Items.Clear();
            this.nextItemId = 0U;
        }
    }
}