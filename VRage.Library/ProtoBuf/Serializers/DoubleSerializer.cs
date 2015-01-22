// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.DoubleSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
  internal sealed class DoubleSerializer : IProtoSerializer
  {
    private static readonly Type expectedType = typeof (double);

    public Type ExpectedType
    {
      get
      {
        return DoubleSerializer.expectedType;
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

    public DoubleSerializer(TypeModel model)
    {
    }

    public object Read(object value, ProtoReader source)
    {
      return (object) source.ReadDouble();
    }

    public void Write(object value, ProtoWriter dest)
    {
      ProtoWriter.WriteDouble((double) value, dest);
    }

    void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicWrite("WriteDouble", valueFrom);
    }

    void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicRead("ReadDouble", this.ExpectedType);
    }
  }
}
