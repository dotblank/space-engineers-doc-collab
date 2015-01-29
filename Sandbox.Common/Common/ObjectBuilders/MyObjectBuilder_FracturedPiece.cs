// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_FracturedPiece
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Collections.Generic;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_FracturedPiece : MyObjectBuilder_EntityBase
  {
    [ProtoMember(1)]
    public List<SerializableDefinitionId> BlockDefinitions = new List<SerializableDefinitionId>();
    [ProtoMember(2)]
    public List<MyObjectBuilder_FracturedPiece.Shape> Shapes = new List<MyObjectBuilder_FracturedPiece.Shape>();

    [ProtoContract]
    public struct Shape
    {
      [ProtoMember(1)]
      public string Name;
      [ProtoMember(2)]
      public SerializableQuaternion Orientation;
    }
  }
}
