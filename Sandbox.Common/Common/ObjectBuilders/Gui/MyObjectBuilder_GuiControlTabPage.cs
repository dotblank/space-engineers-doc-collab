// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlTabPage
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.Localization;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Gui
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_GuiControlTabPage : MyObjectBuilder_GuiControlParent
    {
        [ProtoMember(1)] public int PageKey;
        [ProtoMember(2)] public MyTextsWrapperEnum TextEnum;
        [ProtoMember(3)] public float TextScale;
    }
}