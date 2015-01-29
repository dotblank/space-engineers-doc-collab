// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_EnvironmentDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_EnvironmentDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(10)]
    public SerializableVector3 SunDiffuse = new SerializableVector3(0.7843137f, 0.7843137f, 0.7843137f);
    [ProtoMember(11)]
    public float SunIntensity = 1.456f;
    [ProtoMember(12)]
    public SerializableVector3 SunSpecular = new SerializableVector3(0.7843137f, 0.7843137f, 0.7843137f);
    [ProtoMember(13)]
    public SerializableVector3 BackLightDiffuse = new SerializableVector3(0.7843137f, 0.7843137f, 0.7843137f);
    [ProtoMember(14)]
    public float BackLightIntensity = 0.239f;
    [ProtoMember(15)]
    public SerializableVector3 AmbientColor = new SerializableVector3(0.1411765f, 0.1411765f, 0.1411765f);
    [ProtoMember(16)]
    public float AmbientMultiplier = 0.969f;
    [ProtoMember(17)]
    public float EnvironmentAmbientIntensity = 0.5f;
    [ProtoMember(18)]
    public SerializableVector3 BackgroundColor = new SerializableVector3(1f, 1f, 1f);
    [ProtoMember(19)]
    public string SunMaterial = "SunDisk";
    [ProtoMember(20)]
    public float SunSizeMultiplier = 200f;
    [ProtoMember(21)]
    public float SmallShipMaxSpeed = 100f;
    [ProtoMember(22)]
    public float LargeShipMaxSpeed = 100f;
    [ProtoMember(23)]
    public float SmallShipMaxAngularSpeed = 36000f;
    [ProtoMember(24)]
    public float LargeShipMaxAngularSpeed = 18000f;
    [ProtoMember(1)]
    public SerializableVector3 SunDirection;
    [ProtoMember(2)]
    [ModdableContentFile("dds")]
    public string EnvironmentTexture;
    [ProtoMember(3)]
    public MyOrientation EnvironmentOrientation;
    [ProtoMember(4)]
    public bool EnableFog;
    [ProtoMember(5)]
    public float FogNear;
    [ProtoMember(6)]
    public float FogFar;
    [ProtoMember(7)]
    public float FogMultiplier;
    [ProtoMember(8)]
    public float FogBacklightMultiplier;
    [ProtoMember(9)]
    public SerializableVector3 FogColor;
  }
}
