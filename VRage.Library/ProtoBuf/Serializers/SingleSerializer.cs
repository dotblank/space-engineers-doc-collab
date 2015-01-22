// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.SingleSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
  internal sealed class SingleSerializer : IProtoSerializer
  {
    private static readonly Type expectedType = typeof (float);

    public Type ExpectedType
    {
      get
      {
        return SingleSerializer.expectedType;
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

    public SingleSerializer(TypeModel model)
    {
    }

    public object Read(object value, ProtoReader source)
    {
      return (object) source.ReadSingle();
    }

    public void Write(object value, ProtoWriter dest)
    {
      ProtoWriter.WriteSingle((float) value, dest);
    }

    void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicWrite("WriteSingle", valueFrom);
    }

    void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicRead("ReadSingle", this.ExpectedType);
    }
  }
}
