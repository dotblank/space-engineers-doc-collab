// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.BlueprintClassEntry
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    public class BlueprintClassEntry
    {
        [ProtoMember(4)] [DefaultValue(true)] public bool Enabled = true;
        [ProtoMember(1)] [XmlAttribute] public string Class;
        [XmlIgnore] public MyObjectBuilderType TypeId;
        [ProtoMember(3)] [XmlAttribute] public string BlueprintSubtypeId;

        [XmlAttribute]
        [ProtoMember(2)]
        public string BlueprintTypeId
        {
            get { return this.TypeId.ToString(); }
            set { this.TypeId = MyObjectBuilderType.ParseBackwardsCompatible(value); }
        }

        public override bool Equals(object other)
        {
            BlueprintClassEntry blueprintClassEntry = other as BlueprintClassEntry;
            if (blueprintClassEntry != null && blueprintClassEntry.Class.Equals(this.Class))
                return blueprintClassEntry.BlueprintSubtypeId.Equals(this.BlueprintSubtypeId);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return this.Class.GetHashCode()*7607 + this.BlueprintSubtypeId.GetHashCode();
        }
    }
}