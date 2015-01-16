// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoEnumAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ProtoBuf
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class ProtoEnumAttribute : Attribute
    {
        private bool hasValue;
        private int enumValue;
        private string name;

        public int Value
        {
            get { return this.enumValue; }
            set
            {
                this.enumValue = value;
                this.hasValue = true;
            }
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public bool HasValue()
        {
            return this.hasValue;
        }
    }
}