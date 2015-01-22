// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.SubType
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ProtoBuf.Meta
{
  public sealed class SubType
  {
    private readonly int fieldNumber;
    private readonly MetaType derivedType;
    private readonly DataFormat dataFormat;
    private IProtoSerializer serializer;

    public int FieldNumber
    {
      get
      {
        return this.fieldNumber;
      }
    }

    public MetaType DerivedType
    {
      get
      {
        return this.derivedType;
      }
    }

    internal IProtoSerializer Serializer
    {
      get
      {
        if (this.serializer == null)
          this.serializer = this.BuildSerializer();
        return this.serializer;
      }
    }

    public SubType(int fieldNumber, MetaType derivedType, DataFormat format)
    {
      if (derivedType == null)
        throw new ArgumentNullException("derivedType");
      if (fieldNumber <= 0)
        throw new ArgumentOutOfRangeException("fieldNumber");
      this.fieldNumber = fieldNumber;
      this.derivedType = derivedType;
      this.dataFormat = format;
    }

    private IProtoSerializer BuildSerializer()
    {
      WireType wireType = WireType.String;
      if (this.dataFormat == DataFormat.Group)
        wireType = WireType.StartGroup;
      IProtoSerializer tail = (IProtoSerializer) new SubItemSerializer(this.derivedType.Type, this.derivedType.GetKey(false, false), (ISerializerProxy) this.derivedType, false);
      return (IProtoSerializer) new TagDecorator(this.fieldNumber, wireType, false, tail);
    }

    internal class Comparer : IComparer, IComparer<SubType>
    {
      public static readonly SubType.Comparer Default = new SubType.Comparer();

      public int Compare(object x, object y)
      {
        return this.Compare(x as SubType, y as SubType);
      }

      public int Compare(SubType x, SubType y)
      {
        if (object.ReferenceEquals((object) x, (object) y))
          return 0;
        if (x == null)
          return -1;
        if (y == null)
          return 1;
        else
          return x.FieldNumber.CompareTo(y.FieldNumber);
      }
    }
  }
}
