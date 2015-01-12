// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_TransparentMaterialDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_TransparentMaterialDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(13)] public Vector4 Color = Vector4.One;
        [ProtoMember(1)] [ModdableContentFile("dds")] public string Texture;
        [ProtoMember(2)] public bool CanBeAffectedByLights;
        [ProtoMember(3)] public bool AlphaMistingEnable;
        [ProtoMember(4)] public bool IgnoreDepth;
        [ProtoMember(5)] public bool NeedSort;
        [ProtoMember(6)] public bool UseAtlas;
        [ProtoMember(7)] public float AlphaMistingStart;
        [ProtoMember(8)] public float AlphaMistingEnd;
        [ProtoMember(9)] public float SoftParticleDistanceScale;
        [ProtoMember(10)] public float Emissivity;
        [ProtoMember(11)] public float AlphaSaturation;
        [ProtoMember(12)] public bool Reflection;
        [ProtoMember(14)] public float Reflectivity;
    }
}