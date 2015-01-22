// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_PhysicalCubeBlockDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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
        [DefaultValue(null)] [ProtoMember(1)] public string CubeBlockSubtypeId;
        [ProtoMember(2)] [DefaultValue(null)] public string OreSubtypeId;
        [DefaultValue(null)] [ProtoMember(3)] public string PhysicalToolSubtypeId;
    }
}