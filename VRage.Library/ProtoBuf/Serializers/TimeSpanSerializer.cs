// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.TimeSpanSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
  internal sealed class TimeSpanSerializer : IProtoSerializer
  {
    private static readonly Type expectedType = typeof (TimeSpan);

    public Type ExpectedType
    {
      get
      {
        return TimeSpanSerializer.expectedType;
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

    public TimeSpanSerializer(TypeModel model)
    {
    }

    public object Read(object value, ProtoReader source)
    {
      return (object) BclHelpers.ReadTimeSpan(source);
    }

    public void Write(object value, ProtoWriter dest)
    {
      BclHelpers.WriteTimeSpan((TimeSpan) value, dest);
    }

    void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitWrite(ctx.MapType(typeof (BclHelpers)), "WriteTimeSpan", valueFrom);
    }

    void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicRead(ctx.MapType(typeof (BclHelpers)), "ReadTimeSpan", this.ExpectedType);
    }
  }
}
