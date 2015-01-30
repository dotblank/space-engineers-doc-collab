// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_PlayerChatItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_PlayerChatItem : MyObjectBuilder_Base
  {
    [DefaultValue(true)]
    [XmlElement(ElementName = "S")]
    [ProtoMember(4)]
    public bool Sent = true;
    [ProtoMember(1)]
    [XmlAttribute("t")]
    public string Text;
    [XmlElement(ElementName = "I")]
    [ProtoMember(2)]
    public long IdentityIdUniqueNumber;
    [ProtoMember(3)]
    [XmlElement(ElementName = "T")]
    public long TimestampMs;
  }
}
