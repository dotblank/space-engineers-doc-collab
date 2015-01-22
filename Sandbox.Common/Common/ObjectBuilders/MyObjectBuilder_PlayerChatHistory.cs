// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_PlayerChatHistory
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_PlayerChatHistory : MyObjectBuilder_Base
    {
        [ProtoMember(1)] [XmlArrayItem("PCI")] public List<MyObjectBuilder_PlayerChatItem> Chat;
        [XmlElement(ElementName = "ID")] [ProtoMember(2)] public long IdentityId;
    }
}