// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ProtoIncludeAttribute
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf.Meta;
using System;
using System.ComponentModel;
using System.Reflection;

namespace ProtoBuf
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
  public sealed class ProtoIncludeAttribute : Attribute
  {
    private readonly int tag;
    private readonly string knownTypeName;
    private DataFormat dataFormat;

    public int Tag
    {
      get
      {
        return this.tag;
      }
    }

    public string KnownTypeName
    {
      get
      {
        return this.knownTypeName;
      }
    }

    public Type KnownType
    {
      get
      {
        return TypeModel.ResolveKnownType(this.KnownTypeName, (TypeModel) null, (Assembly) null);
      }
    }

    [DefaultValue(DataFormat.Default)]
    public DataFormat DataFormat
    {
      get
      {
        return this.dataFormat;
      }
      set
      {
        this.dataFormat = value;
      }
    }

    public ProtoIncludeAttribute(int tag, Type knownType)
      : this(tag, knownType == (Type) null ? "" : knownType.AssemblyQualifiedName)
    {
    }

    public ProtoIncludeAttribute(int tag, string knownTypeName)
    {
      if (tag <= 0)
        throw new ArgumentOutOfRangeException("tag", "Tags must be positive integers");
      if (Helpers.IsNullOrEmpty(knownTypeName))
        throw new ArgumentNullException("knownTypeName", "Known type cannot be blank");
      this.tag = tag;
      this.knownTypeName = knownTypeName;
    }
  }
}
