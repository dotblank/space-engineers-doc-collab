// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.DecimalSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
  internal sealed class DecimalSerializer : IProtoSerializer
  {
    private static readonly Type expectedType = typeof (Decimal);

    public Type ExpectedType
    {
      get
      {
        return DecimalSerializer.expectedType;
      }
    }

    bool IProtoSerializer.RequiresOldValue
    {
      get
      {
        return false;
      }
    }

    bool IProtoSerializer.ReturnsValue
    {
      get
      {
        return true;
      }
    }

    public DecimalSerializer(TypeModel model)
    {
    }

    public object Read(object value, ProtoReader source)
    {
      return (object) BclHelpers.ReadDecimal(source);
    }

    public void Write(object value, ProtoWriter dest)
    {
      BclHelpers.WriteDecimal((Decimal) value, dest);
    }

    void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitWrite(ctx.MapType(typeof (BclHelpers)), "WriteDecimal", valueFrom);
    }

    void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicRead(ctx.MapType(typeof (BclHelpers)), "ReadDecimal", this.ExpectedType);
    }
  }
}
