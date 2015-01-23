﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_FactionChatItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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
        [ProtoMember(1)] [XmlAttribute("t")] public string Text;
        [ProtoMember(2)] [XmlElement(ElementName = "I")] public long IdentityIdUniqueNumber;
        [XmlElement(ElementName = "T")] [ProtoMember(3)] public long TimestampMs;

        [DefaultValue(null)] [XmlElement(ElementName = "PTST")] [ProtoMember(4)] public List<long>
            PlayersToSendToUniqueNumber;

        [XmlElement(ElementName = "IAST")] [ProtoMember(5)] [DefaultValue(null)] public List<bool> IsAlreadySentTo;
    }
}