// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.SByteSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
  internal sealed class SByteSerializer : IProtoSerializer
  {
    private static readonly Type expectedType = typeof (sbyte);

    public Type ExpectedType
    {
      get
      {
        return SByteSerializer.expectedType;
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

    public SByteSerializer(TypeModel model)
    {
    }

    public object Read(object value, ProtoReader source)
    {
      return (object) source.ReadSByte();
    }

    public void Write(object value, ProtoWriter dest)
    {
      ProtoWriter.WriteSByte((sbyte) value, dest);
    }

    void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicWrite("WriteSByte", valueFrom);
    }

    void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicRead("ReadSByte", this.ExpectedType);
    }
  }
}
