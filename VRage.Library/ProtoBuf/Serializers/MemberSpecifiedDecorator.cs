// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.MemberSpecifiedDecorator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
    internal sealed class MemberSpecifiedDecorator : ProtoDecoratorBase
    {
        private readonly MethodInfo getSpecified;
        private readonly MethodInfo setSpecified;

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

        public MemberSpecifiedDecorator(MethodInfo getSpecified, MethodInfo setSpecified, IProtoSerializer tail)
            : base(tail)
        {
            if (getSpecified == (MethodInfo) null && setSpecified == (MethodInfo) null)
                throw new InvalidOperationException();
            this.getSpecified = getSpecified;
            this.setSpecified = setSpecified;
        }

        public override void Write(object value, ProtoWriter dest)
        {
            if (!(this.getSpecified == (MethodInfo) null) && !(bool) this.getSpecified.Invoke(value, (object[]) null))
                return;
            this.Tail.Write(value, dest);
        }

        public override object Read(object value, ProtoReader source)
        {
            object obj = this.Tail.Read(value, source);
            if (this.setSpecified != (MethodInfo) null)
                this.setSpecified.Invoke(value, new object[1]
                {
                    (object) true
                });
            return obj;
        }

        protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            if (this.getSpecified == (MethodInfo) null)
            {
                this.Tail.EmitWrite(ctx, valueFrom);
            }
            else
            {
                using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
                {
                    ctx.LoadAddress(localWithValue, this.ExpectedType);
                    ctx.EmitCall(this.getSpecified);
                    CodeLabel label = ctx.DefineLabel();
                    ctx.BranchIfFalse(label, false);
                    this.Tail.EmitWrite(ctx, localWithValue);
                    ctx.MarkLabel(label);
                }
            }
        }

        protected override void EmitRead(CompilerContext ctx, Local valueFrom)
        {
            if (this.setSpecified == (MethodInfo) null)
            {
                this.Tail.EmitRead(ctx, valueFrom);
            }
            else
            {
                using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
                {
                    this.Tail.EmitRead(ctx, localWithValue);
                    ctx.LoadAddress(localWithValue, this.ExpectedType);
                    ctx.LoadValue(1);
                    ctx.EmitCall(this.setSpecified);
                }
            }
        }
    }
}