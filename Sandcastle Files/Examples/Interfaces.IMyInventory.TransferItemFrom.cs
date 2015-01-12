void Main()
{
    var sourceInventory = (GridTerminalSystem.GetBlockWithName("Source") as IMyInventoryOwner).GetInventory(0);
    var secondarySourceInventory = (GridTerminalSystem.GetBlockWithName("Secondary Source") as IMyInventoryOwner).GetInventory(0);
    var targetInventory = (GridTerminalSystem.GetBlockWithName("Target") as IMyInventoryOwner).GetInventory(0);
    // Transfers every item in the inventory
    for (int i = 0; i < sourceInventory.GetItems().Count; i++)
        targetInventory.TransferItemsFrom(sourceInventory, 0, stackIfPossible: true);
    // Transfers 10 units of the item residing at index 3 in the source inventory
    targetInventory.TransferItemsFrom(secondarySourceInventory, 3, 5, true, 10);
}