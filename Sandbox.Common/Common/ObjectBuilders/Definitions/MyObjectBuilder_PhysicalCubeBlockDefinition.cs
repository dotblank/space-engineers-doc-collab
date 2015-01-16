// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_PhysicalCubeBlockDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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
        [DefaultValue(null)] [ProtoMember(2)] public string OreSubtypeId;
        [ProtoMember(3)] [DefaultValue(null)] public string PhysicalToolSubtypeId;
    }
}