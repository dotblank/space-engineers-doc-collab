// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_BlueprintClassDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_BlueprintClassDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(4)] [ModdableContentFile("dds")] public string HighlightIcon;
        [ProtoMember(5)] [ModdableContentFile("dds")] public string InputConstraintIcon;
        [ProtoMember(6)] [ModdableContentFile("dds")] public string OutputConstraintIcon;
    }
}