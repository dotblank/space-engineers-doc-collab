// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlButton
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.Localization;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Gui
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
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