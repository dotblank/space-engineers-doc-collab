// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.BlueprintItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    public class BlueprintItem
    {
        [ProtoMember(1)] [XmlIgnore] public SerializableDefinitionId Id;
        [ProtoMember(2)] [XmlAttribute] public string Amount;

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