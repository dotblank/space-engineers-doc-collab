﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlCheckbox
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Gui
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_GuiControlCheckbox : MyObjectBuilder_GuiControlBase
    {
        [ProtoMember(1)] public bool IsChecked;
        [ProtoMember(2)] public string CheckedTexture;
        [ProtoMember(3)] public MyGuiControlCheckboxStyleEnum VisualStyle;
    }
}