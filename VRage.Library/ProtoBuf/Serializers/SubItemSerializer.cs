// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.SubItemSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ProtoBuf.Serializers
{
    internal sealed class SubItemSerializer : IProtoTypeSerializer, IProtoSerializer
    {
        private readonly int key;
        private readonly Type type;
        private readonly ISerializerProxy proxy;
        private readonly bool recursionCheck;

        Type IProtoSerializer.ExpectedType
        {
            get { return this.type; }
        }

        bool IProtoSerializer.RequiresOldValue
        {
            get { return true; }
        }

        bool IProtoSerializer.ReturnsValue
        {
            get { return true; }
        }

        public SubItemSerializer(Type type, int key, ISerializerProxy proxy, bool recursionCheck)
        {
            if (type == (Type) null)
                throw new ArgumentNullException("type");
            if (proxy == null)
                throw new ArgumentNullException("proxy");
            this.type = type;
            this.proxy = proxy;
            this.key = key;
            this.recursionCheck = recursionCheck;
        }

        bool IProtoTypeSerializer.HasCallbacks(TypeModel.CallbackType callbackType)
        {
            return ((IProtoTypeSerializer) this.proxy.Serializer).HasCallbacks(callbackType);
        }

        bool IProtoTypeSerializer.CanCreateInstance()
        {
            return ((IProtoTypeSerializer) this.proxy.Serializer).CanCreateInstance();
        }

        void IProtoTypeSerializer.EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
        {
            ((IProtoTypeSerializer) this.proxy.Serializer).EmitCallback(ctx, valueFrom, callbackType);
        }

        void IProtoTypeSerializer.EmitCreateInstance(CompilerContext ctx)
        {
            ((IProtoTypeSerializer) this.proxy.Serializer).EmitCreateInstance(ctx);
        }

        void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType,
            SerializationContext context)
        {
            ((IProtoTypeSerializer) this.proxy.Serializer).Callback(value, callbackType, context);
        }

        object IProtoTypeSerializer.CreateInstance(ProtoReader source)
        {
            return ((IProtoTypeSerializer) this.proxy.Serializer).CreateInstance(source);
        }

        void IProtoSerializer.Write(object value, ProtoWriter dest)
        {
            if (this.recursionCheck)
                ProtoWriter.WriteObject(value, this.key, dest);
            else
                ProtoWriter.WriteRecursionSafeObject(value, this.key, dest);
        }

        object IProtoSerializer.Read(object value, ProtoReader source)
        {
            return ProtoReader.ReadObject(value, this.key, source);
        }

        private bool EmitDedicatedMethod(CompilerContext ctx, Local valueFrom, bool read)
        {
            MethodBuilder dedicatedMethod = ctx.GetDedicatedMethod(this.key, read);
            if ((MethodInfo) dedicatedMethod == (MethodInfo) null)
                return false;
            using (Local local = new Local(ctx, ctx.MapType(typeof (SubItemToken))))
            {
                Type type = ctx.MapType(read ? typeof (ProtoReader) : typeof (ProtoWriter));
                ctx.LoadValue(valueFrom);
                if (!read)
                {
                    if (this.type.IsValueType || !this.recursionCheck)
                        ctx.LoadNullRef();
                    else
                        ctx.CopyValue();
                }
                ctx.LoadReaderWriter();
                ctx.EmitCall(type.GetMethod("StartSubItem"));
                ctx.StoreValue(local);
                ctx.LoadReaderWriter();
                ctx.EmitCall((MethodInfo) dedicatedMethod);
                if (read && this.type != dedicatedMethod.ReturnType)
                    ctx.Cast(this.type);
                ctx.LoadValue(local);
                ctx.LoadReaderWriter();
                ctx.EmitCall(type.GetMethod("EndSubItem"));
            }
            return true;
        }

        void IProtoSerializer.EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            if (this.EmitDedicatedMethod(ctx, valueFrom, false))
                return;
            ctx.LoadValue(valueFrom);
            if (this.type.IsValueType)
                ctx.CastToObject(this.type);
            ctx.LoadValue(ctx.MapMetaKeyToCompiledKey(this.key));
            ctx.LoadReaderWriter();
            ctx.EmitCall(
                ctx.MapType(typeof (ProtoWriter))
                    .GetMethod(this.recursionCheck ? "WriteObject" : "WriteRecursionSafeObject"));
        }

        void IProtoSerializer.EmitRead(CompilerContext ctx, Local valueFrom)
        {
            if (this.EmitDedicatedMethod(ctx, valueFrom, true))
                return;
            ctx.LoadValue(valueFrom);
            if (this.type.IsValueType)
                ctx.CastToObject(this.type);
            ctx.LoadValue(ctx.MapMetaKeyToCompiledKey(this.key));
            ctx.LoadReaderWriter();
            ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("ReadObject"));
            ctx.CastFromObject(this.type);
        }
    }
}