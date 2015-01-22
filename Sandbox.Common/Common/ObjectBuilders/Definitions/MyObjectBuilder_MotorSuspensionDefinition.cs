// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_MotorSuspensionDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_MotorSuspensionDefinition : MyObjectBuilder_MotorStatorDefinition
    {
        [ProtoMember(1)] public float MaxSteer = 0.45f;
        [ProtoMember(2)] public float SteeringSpeed = 0.02f;
        [ProtoMember(3)] public float PropulsionForce = 10000f;
        [ProtoMember(4)] public float SuspensionLimit = 0.1f;
    }
}