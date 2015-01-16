// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.NullDecorator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;

namespace ProtoBuf.Serializers
{
    internal sealed class NullDecorator : ProtoDecoratorBase
    {
        public const int Tag = 1;
        private readonly Type expectedType;

        public override Type ExpectedType
        {
            get { return this.expectedType; }
        }

        public override bool ReturnsValue
        {
            get { return true; }
        }

        public override bool RequiresOldValue
        {
            get { return true; }
        }

        public NullDecorator(TypeModel model, IProtoSerializer tail)
            : base(tail)
        {
            if (!tail.ReturnsValue)
                throw new NotSupportedException("NullDecorator only supports implementations that return values");
            if (Helpers.IsValueType(tail.ExpectedType))
                this.expectedType = model.MapType(typeof (Nullable<>)).MakeGenericType(tail.ExpectedType);
            else
                this.expectedType = tail.ExpectedType;
        }

        protected override void EmitRead(CompilerContext ctx, Local valueFrom)
        {
            using (Local localWithValue = ctx.GetLocalWithValue(this.expectedType, valueFrom))
            {
                using (Local local1 = new Local(ctx, ctx.MapType(typeof (SubItemToken))))
                {
                    using (Local local2 = new Local(ctx, ctx.MapType(typeof (int))))
                    {
                        ctx.LoadReaderWriter();
                        ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("StartSubItem"));
                        ctx.StoreValue(local1);
                        CodeLabel label1 = ctx.DefineLabel();
                        CodeLabel label2 = ctx.DefineLabel();
                        CodeLabel label3 = ctx.DefineLabel();
                        ctx.MarkLabel(label1);
                        ctx.EmitBasicRead("ReadFieldHeader", ctx.MapType(typeof (int)));
                        ctx.CopyValue();
                        ctx.StoreValue(local2);
                        ctx.LoadValue(1);
                        ctx.BranchIfEqual(label2, true);
                        ctx.LoadValue(local2);
                        ctx.LoadValue(1);
                        ctx.BranchIfLess(label3, false);
                        ctx.LoadReaderWriter();
                        ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("SkipField"));
                        ctx.Branch(label1, true);
                        ctx.MarkLabel(label2);
                        if (this.Tail.RequiresOldValue)
                        {
                            if (this.expectedType.IsValueType)
                            {
                                ctx.LoadAddress(localWithValue, this.expectedType);
                                ctx.EmitCall(this.expectedType.GetMethod("GetValueOrDefault", Helpers.EmptyTypes));
                            }
                            else
                                ctx.LoadValue(localWithValue);
                        }
                        this.Tail.EmitRead(ctx, (Local) null);
                        if (this.expectedType.IsValueType)
                            ctx.EmitCtor(this.expectedType, this.Tail.ExpectedType);
                        ctx.StoreValue(localWithValue);
                        ctx.Branch(label1, false);
                        ctx.MarkLabel(label3);
                        ctx.LoadValue(local1);
                        ctx.LoadReaderWriter();
                        ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("EndSubItem"));
                        ctx.LoadValue(localWithValue);
                    }
                }
            }
        }

        protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            using (Local localWithValue = ctx.GetLocalWithValue(this.expectedType, valueFrom))
            {
                using (Local local = new Local(ctx, ctx.MapType(typeof (SubItemToken))))
                {
                    ctx.LoadNullRef();
                    ctx.LoadReaderWriter();
                    ctx.EmitCall(ctx.MapType(typeof (ProtoWriter)).GetMethod("StartSubItem"));
                    ctx.StoreValue(local);
                    if (this.expectedType.IsValueType)
                    {
                        ctx.LoadAddress(localWithValue, this.expectedType);
                        ctx.LoadValue(this.expectedType.GetProperty("HasValue"));
                    }
                    else
                        ctx.LoadValue(localWithValue);
                    CodeLabel label = ctx.DefineLabel();
                    ctx.BranchIfFalse(label, false);
                    if (this.expectedType.IsValueType)
                    {
                        ctx.LoadAddress(localWithValue, this.expectedType);
                        ctx.EmitCall(this.expectedType.GetMethod("GetValueOrDefault", Helpers.EmptyTypes));
                    }
                    else
                        ctx.LoadValue(localWithValue);
                    this.Tail.EmitWrite(ctx, (Local) null);
                    ctx.MarkLabel(label);
                    ctx.LoadValue(local);
                    ctx.LoadReaderWriter();
                    ctx.EmitCall(ctx.MapType(typeof (ProtoWriter)).GetMethod("EndSubItem"));
                }
            }
        }

        public override object Read(object value, ProtoReader source)
        {
            SubItemToken token = ProtoReader.StartSubItem(source);
            int num;
            while ((num = source.ReadFieldHeader()) > 0)
            {
                if (num == 1)
                    value = this.Tail.Read(value, source);
                else
                    source.SkipField();
            }
            ProtoReader.EndSubItem(token, source);
            return value;
        }

        public override void Write(object value, ProtoWriter dest)
        {
            SubItemToken token = ProtoWriter.StartSubItem((object) null, dest);
            if (value != null)
                this.Tail.Write(value, dest);
            ProtoWriter.EndSubItem(token, dest);
        }
    }
}