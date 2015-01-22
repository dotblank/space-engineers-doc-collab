// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Inventory
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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