// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_PhysicalCubeBlockDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_PhysicalCubeBlockDefinition : MyObjectBuilder_PhysicalItemDefinition
    {
        [ProtoMember(1)] [DefaultValue(null)] public string CubeBlockSubtypeId;
        [ProtoMember(2)] [DefaultValue(null)] public string OreSubtypeId;
        [ProtoMember(3)] [DefaultValue(null)] public string PhysicalToolSubtypeId;
    }
}