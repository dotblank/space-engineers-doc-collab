// Decompiled with JetBrains decompiler
// Type: LitJson.ObjectMetadata
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;

namespace LitJson
{
  internal struct ObjectMetadata
  {
    private Type element_type;
    private bool is_dictionary;
    private IDictionary<string, PropertyMetadata> properties;

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

    public bool IsDictionary
    {
      get
      {
        return this.is_dictionary;
      }
      set
      {
        this.is_dictionary = value;
      }
    }

    public IDictionary<string, PropertyMetadata> Properties
    {
      get
      {
        return this.properties;
      }
      set
      {
        this.properties = value;
      }
    }
  }
}
