// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Conveyors.SerializableLineSectionInformation
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Conveyors
{
  [ProtoContract]
  public struct SerializableLineSectionInformation
  {
    [XmlAttribute]
    [ProtoMember(1)]
    public Base6Directions.Direction Direction;
    [ProtoMember(2)]
    [XmlAttribute]
    public int Length;
  }
}
