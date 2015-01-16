// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Base
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Serializer;
using System.ComponentModel;
using System.Xml.Serialization;
using VRage.Common.Utils;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    public abstract class MyObjectBuilder_Base
    {
        private MyStringId m_subtypeId;
        private string m_subtypeName;

        [DefaultValue(0)]
        public MyStringId SubtypeId
        {
            get { return this.m_subtypeId; }
        }

        [DefaultValue(null)]
        [ProtoMember(2)]
        public string SubtypeName
        {
            get { return this.m_subtypeName; }
            set
            {
                this.m_subtypeName = value;
                this.m_subtypeId = MyStringId.GetOrCompute(value);
            }
        }

        [XmlIgnore]
        public MyObjectBuilderType TypeId
        {
            get { return (MyObjectBuilderType) this.GetType(); }
        }

        public bool ShouldSerializeSubtypeId()
        {
            return false;
        }

        public MyObjectBuilder_Base Clone()
        {
            return MyObjectBuilderSerializer.Clone(this);
        }
    }
}