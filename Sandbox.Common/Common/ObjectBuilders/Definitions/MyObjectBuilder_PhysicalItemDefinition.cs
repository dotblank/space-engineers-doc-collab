// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_PhysicalItemDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.Localization;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_PhysicalItemDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(3)] [ModdableContentFile("mwm")] public string Model = "Models\\Components\\Sphere.mwm";
        [DefaultValue(null)] [ProtoMember(4)] public MyTextsWrapperEnum? IconSymbol = new MyTextsWrapperEnum?();
        [ProtoMember(5)] [DefaultValue(null)] public float? Volume = new float?();
        [ProtoMember(1)] public Vector3 Size;
        [ProtoMember(2)] public float Mass;

        public bool ShouldSerializeIconSymbol()
        {
            return this.IconSymbol.HasValue;
        }
    }
}