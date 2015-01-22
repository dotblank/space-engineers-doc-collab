// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_PlayerChatItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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
        [DefaultValue(true)] [ProtoMember(4)] [XmlElement(ElementName = "S")] public bool Sent = true;
        [ProtoMember(1)] [XmlAttribute("t")] public string Text;
        [XmlElement(ElementName = "I")] [ProtoMember(2)] public long IdentityIdUniqueNumber;
        [XmlElement(ElementName = "T")] [ProtoMember(3)] public long TimestampMs;
    }
}