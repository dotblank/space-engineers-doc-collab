// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.IMyInventoryItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using VRage;

namespace Sandbox.ModAPI.Interfaces
{
    public interface IMyInventoryItem
    {
        /// <summary>
        ///     Gets or sets how many units of the content exist in the item.
        /// </summary>
        /// <value>Amount of units existing in the item</value>
        MyFixedPoint Amount { get; set; }

        /// <summary>
        ///     Gets or sets the physical object contained in the inventory.
        /// </summary>
        /// <value>Content of the inventory item</value>
        MyObjectBuilder_PhysicalObject Content { get; set; }

        /// <summary>
        ///     Gets or sets the item's inventory ID
        /// </summary>
        /// <remarks>
        ///     The ID represents the ordinal number of the item in a list of all items ever added to the inventory. If an item is
        ///     removed and added again into an inventory, it is assigned a new ID.
        ///     For example, if an item is added to an inventory and it is the first item to ever be added to it, its <c>ItemId</c>
        ///     will be 1. If the item is removed from the inventory and inserted again, its <c>ItemId</c> would be 2.
        /// </remarks>
        /// <value>ID of the item in the inventory</value>
        uint ItemId { get; set; }
    }
}