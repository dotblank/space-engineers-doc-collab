// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_SolarPanelDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_SolarPanelDefinition : MyObjectBuilder_PowerProducerDefinition
  {
    [ProtoMember(1)]
    public Vector3 PanelOrientation = new Vector3(0.0f, 0.0f, 0.0f);
    [ProtoMember(2)]
    public bool TwoSidedPanel = true;
    [ProtoMember(3)]
    public float PanelOffset = 1f;
  }
}
