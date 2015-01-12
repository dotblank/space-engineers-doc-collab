// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlLabel
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using Sandbox.Common.Localization;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Gui
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_GuiControlLabel : MyObjectBuilder_GuiControlBase
    {
        [ProtoMember(1)] public MyTextsWrapperEnum TextEnum;
        [ProtoMember(2)] public string Text;
        [ProtoMember(4)] public float TextScale;
        [ProtoMember(5)] public MyFontEnum Font;
    }
}