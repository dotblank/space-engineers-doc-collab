// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_MotorSuspension
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_MotorSuspension : MyObjectBuilder_MotorBase
  {
    [DefaultValue(true)]
    [ProtoMember(2)]
    public bool Steering = true;
    [ProtoMember(3)]
    public float Damping = 0.02f;
    [ProtoMember(4)]
    public float Strength = 0.04f;
    [ProtoMember(5)]
    public bool Propulsion = true;
    [ProtoMember(6)]
    public float Friction = 3.0f / 16.0f;
    [ProtoMember(7)]
    public float Power = 1f;
    [ProtoMember(1)]
    public float SteerAngle;
  }
}
