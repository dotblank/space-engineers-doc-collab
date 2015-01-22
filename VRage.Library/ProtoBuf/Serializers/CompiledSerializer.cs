// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.CompiledSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
  internal sealed class CompiledSerializer : IProtoTypeSerializer, IProtoSerializer
  {
    private readonly IProtoTypeSerializer head;
    private readonly ProtoSerializer serializer;
    private readonly ProtoDeserializer deserializer;

    bool IProtoSerializer.RequiresOldValue
    {
      get
      {
        return this.head.RequiresOldValue;
      }
    }

    bool IProtoSerializer.ReturnsValue
    {
      get
      {
        return this.head.ReturnsValue;
      }
    }

    Type IProtoSerializer.ExpectedType
    {
      get
      {
        return this.head.ExpectedType;
      }
    }

    private CompiledSerializer(IProtoTypeSerializer head, TypeModel model)
    {
      this.head = head;
      this.serializer = CompilerContext.BuildSerializer((IProtoSerializer) head, model);
      this.deserializer = CompilerContext.BuildDeserializer((IProtoSerializer) head, model);
    }

    bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
    {
      return this.head.HasCallbacks(callbackType);
    }

    bool IProtoTypeSerializer.CanCreateInstance()
    {
      return this.head.CanCreateInstance();
    }

    object IProtoTypeSerializer.CreateInstance(ProtoReader source)
    {
      return this.head.CreateInstance(source);
    }

    public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
    {
      this.head.Callback(value, callbackType, context);
    }

    public static CompiledSerializer Wrap(IProtoTypeSerializer head, TypeModel model)
    {
      return head as CompiledSerializer ?? new CompiledSerializer(head, model);
    }

    void IProtoSerializer.Write(object value, ProtoWriter dest)
    {
      this.serializer(value, dest);
    }

    object IProtoSerializer.Read(object value, ProtoReader source)
    {
      return this.deserializer(value, source);
    }

    void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
    {
      this.head.EmitWrite(ctx, valueFrom);
    }

    void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
    {
      this.head.EmitRead(ctx, valueFrom);
    }

    void IProtoTypeSerializer.EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
    {
      this.head.EmitCallback(ctx, valueFrom, callbackType);
    }

    void IProtoTypeSerializer.EmitCreateInstance(CompilerContext ctx)
    {
      this.head.EmitCreateInstance(ctx);
    }
  }
}
