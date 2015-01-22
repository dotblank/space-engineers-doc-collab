// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.StringSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
  internal sealed class StringSerializer : IProtoSerializer
  {
    private static readonly Type expectedType = typeof (string);

    public Type ExpectedType
    {
      get
      {
        return StringSerializer.expectedType;
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

    public StringSerializer(TypeModel model)
    {
    }

    public void Write(object value, ProtoWriter dest)
    {
      ProtoWriter.WriteString((string) value, dest);
    }

    public object Read(object value, ProtoReader source)
    {
      return (object) source.ReadString();
    }

    void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicWrite("WriteString", valueFrom);
    }

    void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
    {
      ctx.EmitBasicRead("ReadString", this.ExpectedType);
    }
  }
}
