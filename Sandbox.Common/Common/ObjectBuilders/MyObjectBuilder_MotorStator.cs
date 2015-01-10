// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_MotorStator
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_MotorStator : MyObjectBuilder_MotorBase
  {
    [ProtoMember(3)]
    [DefaultValue(1f)]
    public float Force = 1f;
    [DefaultValue(0.0f)]
    [ProtoMember(4)]
    public float Friction;
    [ProtoMember(5)]
    public float TargetVelocity;
    [ProtoMember(6)]
    public float? MinAngle;
    [ProtoMember(7)]
    public float? MaxAngle;
    [ProtoMember(8)]
    public float CurrentAngle;
    [ProtoMember(9)]
    public bool LimitsActive;
    [DefaultValue(0.0f)]
    [ProtoMember(10)]
    public float DummyDisplacement;
  }
}
