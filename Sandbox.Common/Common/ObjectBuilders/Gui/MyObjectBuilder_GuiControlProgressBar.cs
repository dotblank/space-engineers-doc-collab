// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlProgressBar
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Gui
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_GuiControlProgressBar : MyObjectBuilder_GuiControlBase
    {
        [ProtoMember(1)] public Vector4? ProgressColor;

        public bool ShouldSerializeProgressColor()
        {
            return this.ProgressColor.HasValue;
        }
    }
}