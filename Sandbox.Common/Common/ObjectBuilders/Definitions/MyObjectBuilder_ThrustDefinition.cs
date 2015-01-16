// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ThrustDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_ThrustDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        private static readonly Vector4 DefaultThrustColor = new Vector4(Color.CornflowerBlue.ToVector3()*0.7f, 0.75f);
        [ProtoMember(4)] public float FlameDamageLengthScale = 0.6f;
        [ProtoMember(5)] public float FlameLengthScale = 1.15f;
        [ProtoMember(6)] public Vector4 FlameFullColor = MyObjectBuilder_ThrustDefinition.DefaultThrustColor;
        [ProtoMember(7)] public Vector4 FlameIdleColor = MyObjectBuilder_ThrustDefinition.DefaultThrustColor;
        [ProtoMember(8)] public string FlamePointMaterial = "EngineThrustMiddle";
        [ProtoMember(9)] public string FlameLengthMaterial = "EngineThrustMiddle";
        [ProtoMember(10)] public string FlameGlareMaterial = "GlareSsThrustSmall";
        [ProtoMember(11)] public float FlameVisibilityDistance = 200f;
        [ProtoMember(12)] public float FlameGlareSize = 0.391f;
        [ProtoMember(13)] public float FlameGlareQuerySize = 1f;
        [ProtoMember(14)] public float FlameDamage = 0.5f;
        [ProtoMember(1)] public float ForceMagnitude;
        [ProtoMember(2)] public float MaxPowerConsumption;
        [ProtoMember(3)] public float MinPowerConsumption;
    }
}