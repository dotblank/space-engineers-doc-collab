// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_MotorSuspension
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_MotorSuspension : MyObjectBuilder_MotorBase
    {
        [DefaultValue(true)] [ProtoMember(2)] public bool Steering = true;
        [ProtoMember(3)] public float Damping = 0.02f;
        [ProtoMember(4)] public float Strength = 0.04f;
        [ProtoMember(5)] public bool Propulsion = true;
        [ProtoMember(6)] public float Friction = 3.0f/16.0f;
        [ProtoMember(7)] public float Power = 1f;
        [ProtoMember(1)] public float SteerAngle;
    }
}