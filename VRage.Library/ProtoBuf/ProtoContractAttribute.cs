// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoContractAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ProtoBuf
{
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface,
        AllowMultiple = false, Inherited = false)]
    public sealed class ProtoContractAttribute : Attribute
    {
        private const byte OPTIONS_InferTagFromName = (byte) 1;
        private const byte OPTIONS_InferTagFromNameHasValue = (byte) 2;
        private const byte OPTIONS_UseProtoMembersOnly = (byte) 4;
        private const byte OPTIONS_SkipConstructor = (byte) 8;
        private const byte OPTIONS_IgnoreListHandling = (byte) 16;
        private const byte OPTIONS_AsReferenceDefault = (byte) 32;
        private string name;
        private int implicitFirstTag;
        private ImplicitFields implicitFields;
        private int dataMemberOffset;
        private byte flags;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int ImplicitFirstTag
        {
            get { return this.implicitFirstTag; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("ImplicitFirstTag");
                this.implicitFirstTag = value;
            }
        }

        public bool UseProtoMembersOnly
        {
            get { return this.HasFlag((byte) 4); }
            set { this.SetFlag((byte) 4, value); }
        }

        public bool IgnoreListHandling
        {
            get { return this.HasFlag((byte) 16); }
            set { this.SetFlag((byte) 16, value); }
        }

        public ImplicitFields ImplicitFields
        {
            get { return this.implicitFields; }
            set { this.implicitFields = value; }
        }

        public bool InferTagFromName
        {
            get { return this.HasFlag((byte) 1); }
            set
            {
                this.SetFlag((byte) 1, value);
                this.SetFlag((byte) 2, true);
            }
        }

        internal bool InferTagFromNameHasValue
        {
            get { return this.HasFlag((byte) 2); }
        }

        public int DataMemberOffset
        {
            get { return this.dataMemberOffset; }
            set { this.dataMemberOffset = value; }
        }

        public bool SkipConstructor
        {
            get { return this.HasFlag((byte) 8); }
            set { this.SetFlag((byte) 8, value); }
        }

        public bool AsReferenceDefault
        {
            get { return this.HasFlag((byte) 32); }
            set { this.SetFlag((byte) 32, value); }
        }

        private bool HasFlag(byte flag)
        {
            return ((int) this.flags & (int) flag) == (int) flag;
        }

        private void SetFlag(byte flag, bool value)
        {
            if (value)
                this.flags |= flag;
            else
                this.flags = (byte) ((uint) this.flags & (uint) ~flag);
        }
    }
}