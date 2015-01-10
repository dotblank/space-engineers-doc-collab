// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ButtonPanelDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_ButtonPanelDefinition : MyObjectBuilder_CubeBlockDefinition
  {
    [ProtoMember(1)]
    public int ButtonCount;
    [ProtoMember(2)]
    public string[] ButtonSymbols;
    [ProtoMember(3)]
    public Vector4[] ButtonColors;
    [ProtoMember(4)]
    public Vector4 UnassignedButtonColor;
  }
}
