// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.TagDecorator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
    internal sealed class TagDecorator : ProtoDecoratorBase, IProtoTypeSerializer, IProtoSerializer
    {
        private readonly bool strict;
        private readonly int fieldNumber;
        private readonly WireType wireType;

        public override Type ExpectedType
        {
            get { return this.Tail.ExpectedType; }
        }

        public override bool RequiresOldValue
        {
            get { return this.Tail.RequiresOldValue; }
        }

        public override bool ReturnsValue
        {
            get { return this.Tail.ReturnsValue; }
        }

        private bool NeedsHint
        {
            get { return (this.wireType & (WireType) 8) != WireType.Variant; }
        }

        public TagDecorator(int fieldNumber, WireType wireType, bool strict, IProtoSerializer tail)
            : base(tail)
        {
            this.fieldNumber = fieldNumber;
            this.wireType = wireType;
            this.strict = strict;
        }

        public bool HasCallbacks(TypeModel.CallbackType callbackType)
        {
            IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
            if (protoTypeSerializer != null)
                return protoTypeSerializer.HasCallbacks(callbackType);
            else
                return false;
        }

        public bool CanCreateInstance()
        {
            IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
            if (protoTypeSerializer != null)
                return protoTypeSerializer.CanCreateInstance();
            else
                return false;
        }

        public object CreateInstance(ProtoReader source)
        {
            return ((IProtoTypeSerializer) this.Tail).CreateInstance(source);
        }

        public void Callback(object value, TypeModel.CallbackType callbackType, SerializationContext context)
        {
            IProtoTypeSerializer protoTypeSerializer = this.Tail as IProtoTypeSerializer;
            if (protoTypeSerializer == null)
                return;
            protoTypeSerializer.Callback(value, callbackType, context);
        }

        public void EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
        {
            ((IProtoTypeSerializer) this.Tail).EmitCallback(ctx, valueFrom, callbackType);
        }

        public void EmitCreateInstance(CompilerContext ctx)
        {
            ((IProtoTypeSerializer) this.Tail).EmitCreateInstance(ctx);
        }

        public override object Read(object value, ProtoReader source)
        {
            if (this.strict)
                source.Assert(this.wireType);
            else if (this.NeedsHint)
                source.Hint(this.wireType);
            return this.Tail.Read(value, source);
        }

        public override void Write(object value, ProtoWriter dest)
        {
            ProtoWriter.WriteFieldHeader(this.fieldNumber, this.wireType, dest);
            this.Tail.Write(value, dest);
        }

        protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.LoadValue(this.fieldNumber);
            ctx.LoadValue((int) this.wireType);
            ctx.LoadReaderWriter();
            ctx.EmitCall(ctx.MapType(typeof (ProtoWriter)).GetMethod("WriteFieldHeader"));
            this.Tail.EmitWrite(ctx, valueFrom);
        }

        protected override void EmitRead(CompilerContext ctx, Local valueFrom)
        {
            if (this.strict || this.NeedsHint)
            {
                ctx.LoadReaderWriter();
                ctx.LoadValue((int) this.wireType);
                ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod(this.strict ? "Assert" : "Hint"));
            }
            this.Tail.EmitRead(ctx, valueFrom);
        }
    }
}