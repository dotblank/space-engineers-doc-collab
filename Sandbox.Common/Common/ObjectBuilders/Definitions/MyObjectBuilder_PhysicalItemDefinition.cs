// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_PhysicalItemDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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
        [ModdableContentFile("mwm")] [ProtoMember(3)] public string Model = "Models\\Components\\Sphere.mwm";
        [ProtoMember(4)] [DefaultValue(null)] public MyTextsWrapperEnum? IconSymbol = new MyTextsWrapperEnum?();
        [ProtoMember(5)] [DefaultValue(null)] public float? Volume = new float?();
        [ProtoMember(1)] public Vector3 Size;
        [ProtoMember(2)] public float Mass;

        public bool ShouldSerializeIconSymbol()
        {
            return this.IconSymbol.HasValue;
        }
    }
}