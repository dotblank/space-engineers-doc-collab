void Main()
{
    IMyTerminalBlock block = GridTerminalSystem.GetBlockWithName("Assembler 1");
    ITerminalAction action = block.GetActionWithName("OnOff");
    throw new Exception(action.Name);
}