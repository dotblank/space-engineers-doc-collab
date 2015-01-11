void Main()
{
    var targetInventory = (GridTerminalSystem.GetBlockWithName("Target") as IMyInventory).GetInventory(0);
    var sourceInventory = (GridTerminalSystem.GetBlockWithName("Source") as IMyInventory).GetInventory(0);
    
    for (int i = 0; i < sourceInventory.GetItems().Count; i++)
        sourceInventory.TransferItemsTo(targetInventory, 0, stackIfPossible: true);
}