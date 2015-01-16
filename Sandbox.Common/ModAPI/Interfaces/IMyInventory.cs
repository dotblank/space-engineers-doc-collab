// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.IMyInventory
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using System.Collections.Generic;
using VRage;
using VRageMath;

namespace Sandbox.ModAPI.Interfaces
{
    public interface IMyInventory
    {
        /// <summary>
        ///     Gets a boolean value depicting whether the inventory has reached its maximum volume or not.
        /// </summary>
        bool IsFull { get; }

        /// <summary>
        ///     Gets the inventory's physical dimensions.
        /// </summary>
        Vector3 Size { get; }

        /// <summary>
        ///     Gets the inventory items' total mass in kilograms.
        /// </summary>
        MyFixedPoint CurrentMass { get; }

        /// <summary>
        ///     Gets the inventory's maximum volume in liters.
        /// </summary>
        MyFixedPoint MaxVolume { get; }

        /// <summary>
        ///     Gets the inventory items' total volume in liters.
        /// </summary>
        MyFixedPoint CurrentVolume { get; }

        /// <summary>
        ///     Gets the inventory's owner.
        /// </summary>
        IMyInventoryOwner Owner { get; }

        /// <summary>
        ///     Returns a boolean value depicting whether an inventory item resides at the specified index in the list of all
        ///     inventory items.
        /// </summary>
        /// <param name="position">A zero-based index</param>
        /// <returns><see langword="true" /> if an item is found at the specified index, <see langword="false" /> if not. </returns>
        bool IsItemAt(int position);

        bool CanItemsBeAdded(MyFixedPoint amount, SerializableDefinitionId contentId);

        bool ContainItems(MyFixedPoint amount, MyObjectBuilder_PhysicalObject ob);

        /// <summary>
        ///     Returns how many units of an item is present in the inventory.
        /// </summary>
        /// <param name="contentId">ID of the item to check</param>
        /// <param name="flags">
        ///     <see cref="MyItemFlags">Damaged</see> to get only damaged items, <see cref="MyItemFlags">None</see>
        ///     to get items regardless of their condition
        /// </param>
        /// <returns>Amount of units</returns>
        /// <seealso cref="VRage.MyFixedPoint" />
        MyFixedPoint GetItemAmount(SerializableDefinitionId contentId, MyItemFlags flags = MyItemFlags.None);

        /// <summary>
        ///     Transfers items from the inventory to the target inventory.
        /// </summary>
        /// <param name="dst">Target inventory</param>
        /// <param name="sourceItemIndex">Index of the item being transferred in the source inventory</param>
        /// <param name="targetItemIndex">Index to which the item will be placed in the target inventory</param>
        /// <param name="stackIfPossible"></param>
        /// <param name="amount">Amount of items to transfer</param>
        /// <returns></returns>
        /// <remarks>
        ///     <note type="caution">
        ///         When using this method in a loop, the item indexes will change as the inventory automatically
        ///         fills the empty inventory spaces left by item transfers. It is thus recommended to set
        ///         <paramref name="sourceItemIndex" /> to zero when iterating over every element in the inventory.
        ///     </note>
        /// </remarks>
        /// <example>
        ///     The following example demonstrates the <c>TransferItemTo</c> method.
        ///     <code source="Examples\Interfaces.IMyInventory.TransferItemTo.cs" lang="cs"></code>
        /// </example>
        bool TransferItemTo(IMyInventory dst, int sourceItemIndex, int? targetItemIndex = null,
            bool? stackIfPossible = null, MyFixedPoint? amount = null);

        // TODO: What does TransferItemTo/From's return depict?
        /// <summary>
        ///     Transfers items from the target inventory.
        /// </summary>
        /// <param name="sourceInventory">Source inventory from which the items will be transferred</param>
        /// <param name="sourceItemIndex">Index of the item being transferred in the source inventory</param>
        /// <param name="targetItemIndex">Index to which the item will be placed in the inventory</param>
        /// <param name="stackIfPossible"></param>
        /// <param name="amount">Amount of items to transfer</param>
        /// <returns></returns>
        /// <remarks>
        ///     <note type="caution">
        ///         When using this method in a loop, the item indexes will change as the inventory automatically
        ///         fills the empty inventory spaces left by item transfers. It is thus recommended to set
        ///         <paramref name="sourceItemIndex" /> to zero when iterating over every element in the inventory.
        ///     </note>
        /// </remarks>
        /// <example>
        ///     The following example demonstrates the <c>TransferItemFrom</c> method.
        ///     <code source="Examples\Interfaces.IMyInventory.TransferItemFrom.cs" lang="cs"></code>
        /// </example>
        bool TransferItemFrom(IMyInventory sourceInventory, int sourceItemIndex, int? targetItemIndex = null,
            bool? stackIfPossible = null, MyFixedPoint? amount = null);

        /// <summary>
        ///     Returns a list of all <see cref="IMyInventoryItem">IMyInventoryItems</see> in the inventory.
        /// </summary>
        /// <returns>A list containing all of of the inventory's items.</returns>
        List<IMyInventoryItem> GetItems();

        IMyInventoryItem GetItemByID(uint id);

        IMyInventoryItem FindItem(SerializableDefinitionId contentId);

        bool IsConnectedTo(IMyInventory dst);
    }
}