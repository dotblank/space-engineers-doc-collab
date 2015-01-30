// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlCheckbox
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Gui
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_GuiControlCheckbox : MyObjectBuilder_GuiControlBase
  {
    [ProtoMember(1)]
    public bool IsChecked;
    [ProtoMember(2)]
    public string CheckedTexture;
    [ProtoMember(3)]
    public MyGuiControlCheckboxStyleEnum VisualStyle;
  }
}
