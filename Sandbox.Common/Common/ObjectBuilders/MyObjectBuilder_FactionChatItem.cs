// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_FactionChatItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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
        [XmlAttribute("t")] [ProtoMember(1)] public string Text;
        [XmlElement(ElementName = "I")] [ProtoMember(2)] public long IdentityIdUniqueNumber;
        [XmlElement(ElementName = "T")] [ProtoMember(3)] public long TimestampMs;

        [ProtoMember(4)] [DefaultValue(null)] [XmlElement(ElementName = "PTST")] public List<long>
            PlayersToSendToUniqueNumber;

        [XmlElement(ElementName = "IAST")] [DefaultValue(null)] [ProtoMember(5)] public List<bool> IsAlreadySentTo;
    }
}