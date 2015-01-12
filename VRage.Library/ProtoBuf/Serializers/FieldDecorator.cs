// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.FieldDecorator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
    internal sealed class FieldDecorator : ProtoDecoratorBase
    {
        private readonly FieldInfo field;
        private readonly Type forType;

        public override Type ExpectedType
        {
            get { return this.forType; }
        }

        public override bool RequiresOldValue
        {
            get { return true; }
        }

        public override bool ReturnsValue
        {
            get { return false; }
        }

        public FieldDecorator(Type forType, FieldInfo field, IProtoSerializer tail)
            : base(tail)
        {
            this.forType = forType;
            this.field = field;
        }

        public override void Write(object value, ProtoWriter dest)
        {
            value = this.field.GetValue(value);
            if (value == null)
                return;
            this.Tail.Write(value, dest);
        }

        public override object Read(object value, ProtoReader source)
        {
            object obj = this.Tail.Read(this.Tail.RequiresOldValue ? this.field.GetValue(value) : (object) null, source);
            if (obj != null)
                this.field.SetValue(value, obj);
            return (object) null;
        }

        protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            ctx.LoadAddress(valueFrom, this.ExpectedType);
            ctx.LoadValue(this.field);
            ctx.WriteNullCheckedTail(this.field.FieldType, this.Tail, (Local) null);
        }

        protected override void EmitRead(CompilerContext ctx, Local valueFrom)
        {
            using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
            {
                ctx.LoadAddress(localWithValue, this.ExpectedType);
                if (this.Tail.RequiresOldValue)
                {
                    ctx.CopyValue();
                    ctx.LoadValue(this.field);
                }
                ctx.ReadNullCheckedTail(this.field.FieldType, this.Tail, (Local) null);
                if (this.Tail.ReturnsValue)
                {
                    if (this.field.FieldType.IsValueType)
                    {
                        ctx.StoreValue(this.field);
                    }
                    else
                    {
                        CodeLabel label1 = ctx.DefineLabel();
                        CodeLabel label2 = ctx.DefineLabel();
                        ctx.CopyValue();
                        ctx.BranchIfTrue(label1, true);
                        ctx.DiscardValue();
                        ctx.DiscardValue();
                        ctx.Branch(label2, true);
                        ctx.MarkLabel(label1);
                        ctx.StoreValue(this.field);
                        ctx.MarkLabel(label2);
                    }
                }
                else
                    ctx.DiscardValue();
            }
        }
    }
}