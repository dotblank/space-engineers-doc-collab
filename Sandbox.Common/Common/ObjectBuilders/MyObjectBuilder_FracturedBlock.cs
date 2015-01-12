// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_FracturedBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Collections.Generic;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_FracturedBlock : MyObjectBuilder_CubeBlock
    {
        [ProtoMember(1)] public List<SerializableDefinitionId> BlockDefinitions = new List<SerializableDefinitionId>();

        [ProtoMember(2)] public List<MyObjectBuilder_FracturedBlock.ShapeB> Shapes =
            new List<MyObjectBuilder_FracturedBlock.ShapeB>();

        [ProtoContract]
        public struct ShapeB
        {
            [ProtoMember(1)] public string Name;
            [ProtoMember(2)] public SerializableQuaternion Orientation;
        }
    }
}