// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_PlayerChatItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_PlayerChatItem : MyObjectBuilder_Base
    {
        [XmlElement(ElementName = "S")] [ProtoMember(4)] [DefaultValue(true)] public bool Sent = true;
        [ProtoMember(1)] [XmlAttribute("t")] public string Text;
        [ProtoMember(2)] [XmlElement(ElementName = "I")] public long IdentityIdUniqueNumber;
        [XmlElement(ElementName = "T")] [ProtoMember(3)] public long TimestampMs;
    }
}