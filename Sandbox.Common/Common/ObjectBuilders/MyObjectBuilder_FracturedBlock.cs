// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_FracturedBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Collections.Generic;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_FracturedBlock : MyObjectBuilder_CubeBlock
  {
    [ProtoMember(1)]
    public List<SerializableDefinitionId> BlockDefinitions = new List<SerializableDefinitionId>();
    [ProtoMember(2)]
    public List<MyObjectBuilder_FracturedBlock.ShapeB> Shapes = new List<MyObjectBuilder_FracturedBlock.ShapeB>();

    [ProtoContract]
    public struct ShapeB
    {
      [ProtoMember(1)]
      public string Name;
      [ProtoMember(2)]
      public SerializableQuaternion Orientation;
    }
  }
}
