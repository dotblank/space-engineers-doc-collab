void Main()
{
    var block = GridTerminalSystem.GetBlockWithName("Assembler 1");
    var action = block.GetActionWithName("OnOff");
    StringBuilder sb = new StringBuilder();
    action.WriteValue(block, sb);
}