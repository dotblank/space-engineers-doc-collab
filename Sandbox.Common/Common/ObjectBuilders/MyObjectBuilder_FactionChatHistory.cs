// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_FactionChatHistory
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_FactionChatHistory : MyObjectBuilder_Base
    {
        [ProtoMember(1)] [XmlArrayItem("FCI")] public List<MyObjectBuilder_FactionChatItem> Chat;
        [XmlElement(ElementName = "ID1")] [ProtoMember(2)] public long FactionId1;
        [XmlElement(ElementName = "ID2")] public long FactionId2;
    }
}