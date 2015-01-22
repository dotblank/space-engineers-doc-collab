// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_PhysicalItemDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.Localization;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_PhysicalItemDefinition : MyObjectBuilder_DefinitionBase
    {
        [ModdableContentFile("mwm")] [ProtoMember(3)] public string Model = "Models\\Components\\Sphere.mwm";
        [DefaultValue(null)] [ProtoMember(4)] public MyTextsWrapperEnum? IconSymbol = new MyTextsWrapperEnum?();
        [DefaultValue(null)] [ProtoMember(5)] public float? Volume = new float?();
        [ProtoMember(1)] public Vector3 Size;
        [ProtoMember(2)] public float Mass;

        public bool ShouldSerializeIconSymbol()
        {
            return this.IconSymbol.HasValue;
        }
    }
}