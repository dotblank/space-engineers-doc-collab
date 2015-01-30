// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlButton
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Gui
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_GuiControlButton : MyObjectBuilder_GuiControlBase
  {
    [ProtoMember(1)]
    public string Text;
    [ProtoMember(2)]
    public string TextEnum;
    [ProtoMember(5)]
    public float TextScale;
    [ProtoMember(6)]
    public int TextAlignment;
    [ProtoMember(7)]
    public bool DrawCrossTextureWhenDisabled;
    [ProtoMember(10)]
    public bool DrawRedTextureWhenDisabled;
    [ProtoMember(12)]
    public MyGuiControlButtonStyleEnum VisualStyle;
  }
}
