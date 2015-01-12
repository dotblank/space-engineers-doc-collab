void Main()
{
	// Get the inventory owners
    var assemblerOwner = GridTerminalSystem.GetBlockWithName("Assembler") as IMyInventoryOwner;
	var containerOwner = GridTerminalSystem.GetBlockWithName("Small Cargo Container") as IMyInventoryOwner;
	
	// Get the assembler's output inventory
	IMyInventory assemblerInventory = assemblerOwner.GetInventory(1);
	// Get the cargo container's inventory
	IMyInventory containerInventory = containerOwner.GetInventory(0);
	
	// Get all of the items in the inventory and store it in a list
	List<IMyInventoryItem> items = assemblerOwner.GetItems();
	// Transfer all finished items from the assembler to the container
	for (int i = 0; i < items.Count; i++)
		assemblerOwner.TransferItemTo(containerInventory, 0, stackIfPossible: true);
}