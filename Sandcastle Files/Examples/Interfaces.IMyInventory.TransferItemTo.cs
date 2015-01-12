void Main()
{
    var targetInventory = (GridTerminalSystem.GetBlockWithName("Target") as IMyInventoryOwner).GetInventory(0);
    var sourceInventory = (GridTerminalSystem.GetBlockWithName("Source") as IMyInventoryOwner).GetInventory(0);
    var secondarySourceInventory = (GridTerminalSystem.GetBlockWithName("Secondary Source") as IMyInventoryOwner).GetInventory(0);
    // Transfers every item in the inventory
    for (int i = 0; i < sourceInventory.GetItems().Count; i++)
        sourceInventory.TransferItemsTo(targetInventory, 0, stackIfPossible: true);
    // Transfers 23 units of the item residing at index 8 in the source inventory
    secondarySourceInventory.TransferItemsTo(targetInventory, 8, 12, true, 23);
}