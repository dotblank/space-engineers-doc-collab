// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.BlueprintItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  public class BlueprintItem
  {
    [ProtoMember(1)]
    [XmlIgnore]
    public SerializableDefinitionId Id;
    [XmlAttribute]
    [ProtoMember(2)]
    public string Amount;

    [XmlAttribute]
    public string TypeId
    {
      get
      {
        return this.Id.TypeId.ToString();
      }
      set
      {
        this.Id.TypeId = MyObjectBuilderType.ParseBackwardsCompatible(value);
      }
    }

    [XmlAttribute]
    public string SubtypeId
    {
      get
      {
        return this.Id.SubtypeId;
      }
      set
      {
        this.Id.SubtypeId = value;
      }
    }
  }
}
