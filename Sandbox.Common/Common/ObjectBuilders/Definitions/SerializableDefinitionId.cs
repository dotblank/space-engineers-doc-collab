// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.SerializableDefinitionId
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  public struct SerializableDefinitionId
  {
    [XmlIgnore]
    public MyObjectBuilderType TypeId;
    [XmlIgnore]
    public string SubtypeName;

    [XmlElement("TypeId")]
    [ProtoMember(1)]
    public string TypeIdString
    {
      get
      {
        return this.TypeId.ToString();
      }
      set
      {
        this.TypeId = MyObjectBuilderType.ParseBackwardsCompatible(value);
      }
    }

    [ProtoMember(2)]
    public string SubtypeId
    {
      get
      {
        return this.SubtypeName;
      }
      set
      {
        this.SubtypeName = value;
      }
    }

    public SerializableDefinitionId(MyObjectBuilderType typeId, string subtypeName)
    {
      this.TypeId = typeId;
      this.SubtypeName = subtypeName;
    }

    public override string ToString()
    {
      return string.Format("{0}/{1}", (object) this.TypeId, (object) this.SubtypeName);
    }
  }
}
