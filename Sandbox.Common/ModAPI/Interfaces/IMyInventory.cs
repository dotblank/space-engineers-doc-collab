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
    
    // TODO: What does TransferItemTo's return depict?
    /// <summary>
    /// Transfers items from this inventory to the target inventory.
    /// </summary>
    /// <include file='/IMyInventory.xml' path='Documentation/Member[@name="TransferItemsTo"]/*'/>
    bool TransferItemTo(IMyInventory dst, int sourceItemIndex, int? targetItemIndex = null, bool? stackIfPossible = null, MyFixedPoint? amount = null);

    bool TransferItemFrom(IMyInventory sourceInventory, int sourceItemIndex, int? targetItemIndex = null, bool? stackIfPossible = null, MyFixedPoint? amount = null);

    List<IMyInventoryItem> GetItems();

    IMyInventoryItem GetItemByID(uint id);

    IMyInventoryItem FindItem(SerializableDefinitionId contentId);
  }
}
