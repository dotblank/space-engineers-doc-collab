// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_PrefabDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_PrefabDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(1)] public bool RespawnShip;
        [ProtoMember(2)] public MyObjectBuilder_CubeGrid CubeGrid;
        [ProtoMember(3)] [XmlArrayItem("CubeGrid")] public MyObjectBuilder_CubeGrid[] CubeGrids;

        public bool ShouldSerializeRespawnShip()
        {
            return false;
        }

        public bool ShouldSerializeCubeGrid()
        {
            return false;
        }
    }
}