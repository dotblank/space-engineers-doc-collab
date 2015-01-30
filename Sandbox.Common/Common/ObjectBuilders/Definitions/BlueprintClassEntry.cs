// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.BlueprintClassEntry
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  public class BlueprintClassEntry
  {
    [DefaultValue(true)]
    [ProtoMember(4)]
    public bool Enabled = true;
    [ProtoMember(1)]
    [XmlAttribute]
    public string Class;
    [XmlIgnore]
    public MyObjectBuilderType TypeId;
    [ProtoMember(3)]
    [XmlAttribute]
    public string BlueprintSubtypeId;

    [XmlAttribute]
    [ProtoMember(2)]
    public string BlueprintTypeId
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

    public override bool Equals(object other)
    {
      BlueprintClassEntry blueprintClassEntry = other as BlueprintClassEntry;
      if (blueprintClassEntry != null && blueprintClassEntry.Class.Equals(this.Class))
        return blueprintClassEntry.BlueprintSubtypeId.Equals(this.BlueprintSubtypeId);
      else
        return false;
    }

    public override int GetHashCode()
    {
      return this.Class.GetHashCode() * 7607 + this.BlueprintSubtypeId.GetHashCode();
    }
  }
}
