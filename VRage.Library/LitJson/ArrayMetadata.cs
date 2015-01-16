// Decompiled with JetBrains decompiler
// Type: LitJson.ArrayMetadata
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace LitJson
{
    internal struct ArrayMetadata
    {
        private Type element_type;
        private bool is_array;
        private bool is_list;

        public Type ElementType
        {
            get
            {
                if (this.element_type == (Type) null)
                    return typeof (JsonData);
                else
                    return this.element_type;
            }
            set { this.element_type = value; }
        }

        public bool IsArray
        {
            get { return this.is_array; }
            set { this.is_array = value; }
        }

        public bool IsList
        {
            get { return this.is_list; }
            set { this.is_list = value; }
        }
    }
}