// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlMultilineLabel
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Gui
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_GuiControlMultilineLabel : MyObjectBuilder_GuiControlBase
    {
        [ProtoMember(1)] public float TextScale = 1f;
        [ProtoMember(3)] public Vector4 TextColor = Vector4.One;
        [ProtoMember(2)] public int TextAlign;
        [ProtoMember(4)] public string Text;
        [ProtoMember(5)] public int TextBoxAlign;
        [ProtoMember(6)] public MyFontEnum Font;
    }
}