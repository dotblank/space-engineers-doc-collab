// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_MotorStatorDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_MotorStatorDefinition : MyObjectBuilder_CubeBlockDefinition
  {
    [ProtoMember(1)]
    public float RequiredPowerInput;
    [ProtoMember(2)]
    public float MaxForceMagnitude;
    [ProtoMember(3)]
    public string RotorPart;
    [ProtoMember(4)]
    public float RotorDisplacementMin;
    [ProtoMember(5)]
    public float RotorDisplacementMax;
    [ProtoMember(6)]
    public float RotorDisplacementInModel;
  }
}
