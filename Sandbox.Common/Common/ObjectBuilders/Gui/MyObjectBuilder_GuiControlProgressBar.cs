// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Gui.MyObjectBuilder_GuiControlProgressBar
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Gui
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_GuiControlProgressBar : MyObjectBuilder_GuiControlBase
  {
    [ProtoMember(1)]
    public Vector4? ProgressColor;

    public bool ShouldSerializeProgressColor()
    {
      return this.ProgressColor.HasValue;
    }
  }
}
