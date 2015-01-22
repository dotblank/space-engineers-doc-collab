// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.TypeFormatEventArgs
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using System;

namespace ProtoBuf.Meta
{
  public class TypeFormatEventArgs : EventArgs
  {
    private Type type;
    private string formattedName;
    private readonly bool typeFixed;

    public Type Type
    {
      get
      {
        return this.type;
      }
      set
      {
        if (!(this.type != value))
          return;
        if (this.typeFixed)
          throw new InvalidOperationException("The type is fixed and cannot be changed");
        this.type = value;
      }
    }

    public string FormattedName
    {
      get
      {
        return this.formattedName;
      }
      set
      {
        if (!(this.formattedName != value))
          return;
        if (!this.typeFixed)
          throw new InvalidOperationException("The formatted-name is fixed and cannot be changed");
        this.formattedName = value;
      }
    }

    internal TypeFormatEventArgs(string formattedName)
    {
      if (Helpers.IsNullOrEmpty(formattedName))
        throw new ArgumentNullException("formattedName");
      this.formattedName = formattedName;
      this.typeFixed = false;
    }

    internal TypeFormatEventArgs(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException("type");
      this.type = type;
      this.typeFixed = true;
    }
  }
}
