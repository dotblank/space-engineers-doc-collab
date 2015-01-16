// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_MotorStator
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_MotorStator : MyObjectBuilder_MotorBase
    {
        [ProtoMember(3)] [DefaultValue(1f)] public float Force = 1f;
        [ProtoMember(4)] [DefaultValue(0.0f)] public float Friction;
        [ProtoMember(5)] public float TargetVelocity;
        [ProtoMember(6)] public float? MinAngle;
        [ProtoMember(7)] public float? MaxAngle;
        [ProtoMember(8)] public float CurrentAngle;
        [ProtoMember(9)] public bool LimitsActive;
        [ProtoMember(10)] [DefaultValue(0.0f)] public float DummyDisplacement;
    }
}