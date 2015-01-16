// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.BlueprintItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    public class BlueprintItem
    {
        [XmlIgnore] [ProtoMember(1)] public SerializableDefinitionId Id;
        [XmlAttribute] [ProtoMember(2)] public string Amount;

        [XmlAttribute]
        public string TypeId
        {
            get { return this.Id.TypeId.ToString(); }
            set { this.Id.TypeId = MyObjectBuilderType.ParseBackwardsCompatible(value); }
        }

        [XmlAttribute]
        public string SubtypeId
        {
            get { return this.Id.SubtypeId; }
            set { this.Id.SubtypeId = value; }
        }
    }
}