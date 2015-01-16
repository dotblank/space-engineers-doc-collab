// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlButton
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.Localization;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Gui
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_GuiControlButton : MyObjectBuilder_GuiControlBase
    {
        [ProtoMember(1)] public string Text;
        [ProtoMember(2)] public MyTextsWrapperEnum TextEnum;
        [ProtoMember(5)] public float TextScale;
        [ProtoMember(6)] public int TextAlignment;
        [ProtoMember(7)] public bool DrawCrossTextureWhenDisabled;
        [ProtoMember(10)] public bool DrawRedTextureWhenDisabled;
        [ProtoMember(12)] public MyGuiControlButtonStyleEnum VisualStyle;
    }
}