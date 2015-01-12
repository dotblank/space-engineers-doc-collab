void Main()
{
    var list = new List<IMyTerminalBlock>();
    // Gets all blocks which implement the IMyFunctionalBlock interface and which return true when passed to FilterMethod
    GridTerminalSystem.GetBlocksOfType<IMyFunctionalBlock>(list, FilterMethod);
    if (list.Count == 0)
        return;

    for (int i = 0; i < list.Count; i++)
    {
        // Cast and enable the block
        (list[i] as IMyFunctionalBlock).RequestEnable(true);
    }
}
bool FilterMethod(IMyTerminalBlock block)
{
    // If the block is enabled, the return value will be false
    return !(block as IMyFunctionalBlock).Enabled;
}