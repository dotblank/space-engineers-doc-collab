// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.UInt32Serializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
  internal sealed class UInt32Serializer : IProtoSerializer
  {
    private static readonly Type expectedType = typeof (uint);

    public Type ExpectedType
    {
      get
      {
        return UInt32Serializer.expectedType;
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

    public UInt32Serializer(TypeModel model)
    {
    }

    public object Read(object value, ProtoReader source)
    {
      return (object) source.ReadUInt32();
    }

    public void Write(object value, ProtoWriter dest)
    {
      ProtoWriter.WriteUInt32((uint) value, dest);
    }

    void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicWrite("WriteUInt32", valueFrom);
    }

    void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicRead("ReadUInt32", ctx.MapType(typeof (uint)));
    }
  }
}
