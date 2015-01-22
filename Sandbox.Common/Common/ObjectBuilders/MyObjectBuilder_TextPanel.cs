// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_TextPanel
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_TextPanel : MyObjectBuilder_TerminalBlock
    {
        [ProtoMember(1)] public string Description = "";
        [ProtoMember(2)] public string Title = "Title";
        [ProtoMember(3)] public TextPanelAccessFlag AccessFlag = TextPanelAccessFlag.READ_AND_WRITE_FACTION;
    }
}