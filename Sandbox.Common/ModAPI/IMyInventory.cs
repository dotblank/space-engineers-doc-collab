// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyInventory
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using VRage;

namespace Sandbox.ModAPI
{
  public interface IMyInventory : Sandbox.ModAPI.Interfaces.IMyInventory
  {
    bool Empty();

    void Clear(bool sync = true);

    void AddItems(MyFixedPoint amount, MyObjectBuilder_PhysicalObject objectBuilder, int index = -1);

    void RemoveItemsOfType(MyFixedPoint amount, MyObjectBuilder_PhysicalObject objectBuilder, bool spawn = false);

    void RemoveItemsOfType(MyFixedPoint amount, SerializableDefinitionId contentId, MyItemFlags flags = MyItemFlags.None, bool spawn = false);

    void RemoveItemsAt(int itemIndex, MyFixedPoint? amount = null, bool sendEvent = true, bool spawn = false);

    void RemoveItems(uint itemId, MyFixedPoint? amount = null, bool sendEvent = true, bool spawn = false);

    bool TransferItemTo(Sandbox.ModAPI.Interfaces.IMyInventory dst, int sourceItemIndex, int? targetItemIndex = null, bool? stackIfPossible = null, MyFixedPoint? amount = null, bool checkConnection = true);

    bool TransferItemFrom(Sandbox.ModAPI.Interfaces.IMyInventory sourceInventory, int sourceItemIndex, int? targetItemIndex = null, bool? stackIfPossible = null, MyFixedPoint? amount = null, bool checkConnection = true);
  }
}
