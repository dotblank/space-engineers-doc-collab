// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_FactionChatItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_FactionChatItem : MyObjectBuilder_Base
  {
    [XmlAttribute("t")]
    [ProtoMember(1)]
    public string Text;
    [XmlElement(ElementName = "I")]
    [ProtoMember(2)]
    public long IdentityIdUniqueNumber;
    [XmlElement(ElementName = "T")]
    [ProtoMember(3)]
    public long TimestampMs;
    [DefaultValue(null)]
    [ProtoMember(4)]
    [XmlElement(ElementName = "PTST")]
    public List<long> PlayersToSendToUniqueNumber;
    [ProtoMember(5)]
    [DefaultValue(null)]
    [XmlElement(ElementName = "IAST")]
    public List<bool> IsAlreadySentTo;
  }
}
