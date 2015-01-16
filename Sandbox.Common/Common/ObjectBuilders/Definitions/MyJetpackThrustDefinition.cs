// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyJetpackThrustDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    public class MyJetpackThrustDefinition
    {
        [ProtoMember(2)] public Vector4 ThrustColor = new Vector4(Color.CornflowerBlue.ToVector3()*0.7f, 0.75f);
        [ProtoMember(3)] public float ThrustGlareSize = 5.585f;
        [ProtoMember(4)] public string ThrustMaterial = "EngineThrustMiddle";
        [ProtoMember(5)] public float SideFlameOffset = 0.12f;
        [ProtoMember(6)] public float FrontFlameOffset = 0.04f;
        [ProtoMember(1)] public string ThrustBone;
    }
}