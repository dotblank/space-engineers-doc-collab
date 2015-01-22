// Decompiled with JetBrains decompiler
// Type: LitJson.ArrayMetadata
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
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
      set
      {
        this.element_type = value;
      }
    }

    public bool IsArray
    {
      get
      {
        return this.is_array;
      }
      set
      {
        this.is_array = value;
      }
    }

    public bool IsList
    {
      get
      {
        return this.is_list;
      }
      set
      {
        this.is_list = value;
      }
    }
  }
}
