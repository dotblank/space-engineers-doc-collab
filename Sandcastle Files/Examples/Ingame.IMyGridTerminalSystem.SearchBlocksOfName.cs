void Main()
{
    var list = new List<IMyTerminalBlock>();
    // Gets all blocks whose names contain "Debug" and which return true when passed to FilterMethod
    GridTerminalSystem.SearchBlocksOfName("Debug", list, FilterMethod);
    if (list.Count == 0) return;
    
    // Change all of the blocks' names
    for (int i = 0; i < list.Count; i++)
        list[i].SetCustomName("Debug\r\nWorks!");
}
bool FilterMethod(IMyTerminalBlock block)
{
    // If 'block' cannot be casted to an IMyRadioAntenna, it will be null
    IMyRadioAntenna antenna = block as IMyRadioAntenna;
    return antenna != null;
}