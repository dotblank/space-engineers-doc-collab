// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.IMyInventory
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
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
    bool IsFull { get; }

    Vector3 Size { get; }

    MyFixedPoint CurrentMass { get; }

    MyFixedPoint MaxVolume { get; }

    MyFixedPoint CurrentVolume { get; }

    IMyInventoryOwner Owner { get; }

    bool IsItemAt(int position);

    bool CanItemsBeAdded(MyFixedPoint amount, SerializableDefinitionId contentId);

    bool ContainItems(MyFixedPoint amount, MyObjectBuilder_PhysicalObject ob);

    MyFixedPoint GetItemAmount(SerializableDefinitionId contentId, MyItemFlags flags = MyItemFlags.None);
    
    /// <summary>
    /// Transfers items from the inventory to the target inventory.
    /// </summary>
    /// <param name="dst">Target inventory</param>
    /// <param name="sourceItemIndex">Index of the item being transferred in the source inventory</param>
    /// <param name="targetItemIndex">Index to which the item will be placed in the target inventory</param>
    /// <param name="stackIfPossible"></param>
    /// <param name="amount">Amount of items to transfer</param>
    /// <returns></returns>
    /// <remarks>
    /// 	<note type="caution">When using this method in a loop, the item indexes will change as the inventory automatically fills the empty inventory spaces left by item transfers. It is thus recommended to set <paramref name="sourceItemIndex"/> to zero when iterating over every element in the inventory.</note>
    /// </remarks>
    /// <example>
    /// 	The following example demonstrates the <c>TransferItemTo</c> method.
    /// 	<code source="Examples\Interfaces.IMyInventory.TransferItemTo.cs" lang="cs"></code>
    /// </example>
    bool TransferItemTo(IMyInventory dst, int sourceItemIndex, int? targetItemIndex = null, bool? stackIfPossible = null, MyFixedPoint? amount = null);
    // TODO: What does TransferItemTo/From's return depict?
    /// <summary>
    /// Transfers items from the target inventory
    /// </summary>
    /// <param name="sourceInventory">Source inventory from which the items will be transferred</param>
    /// <param name="sourceItemIndex">Index of the item being transferred in the source inventory</param>
    /// <param name="targetItemIndex">Index to which the item will be placed in the inventory</param>
    /// <param name="stackIfPossible"></param>
    /// <param name="amount">Amount of items to transfer</param>
    /// <returns></returns>
    /// <remarks>
    ///   When using this method in a loop, note that the item indexes will automatically change as the inventory fills the empty inventory spaces left by item transfers. It is thus recommended to set <paramref name="sourceItemIndex"/> to zero when looping over every element in the inventory.
    /// </remarks>
    /// <example>
    ///   The following example demonstrates the <c>TransferItemFrom</c> method.
    ///   <code source="Examples\Interfaces.IMyInventory.TransferItemFrom.cs" lang="cs"></code>
    /// </example>
    bool TransferItemFrom(IMyInventory sourceInventory, int sourceItemIndex, int? targetItemIndex = null, bool? stackIfPossible = null, MyFixedPoint? amount = null);
    /// <summary>
    /// Returns a list of all <see cref="IMyInventoryItem"/>s in the inventory.
    /// </summary>
    /// <returns>A list containing all of of the inventory's items.</returns>
    List<IMyInventoryItem> GetItems();

    IMyInventoryItem GetItemByID(uint id);

    IMyInventoryItem FindItem(SerializableDefinitionId contentId);
  }
}
