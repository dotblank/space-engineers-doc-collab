// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ButtonPanel
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_ButtonPanel : MyObjectBuilder_TerminalBlock
    {
        [ProtoMember(1)] public MyObjectBuilder_Toolbar Toolbar;
        [ProtoMember(2)] public bool AnyoneCanUse;

        public override void Remap(IMyRemapHelper remapHelper)
        {
            base.Remap(remapHelper);
            this.Toolbar.Remap(remapHelper);
        }
    }
}