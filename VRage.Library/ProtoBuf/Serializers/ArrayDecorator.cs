// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.ArrayDecorator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace ProtoBuf.Serializers
{
    internal sealed class ArrayDecorator : ProtoDecoratorBase
    {
        private const byte OPTIONS_WritePacked = (byte) 1;
        private const byte OPTIONS_OverwriteList = (byte) 2;
        private const byte OPTIONS_SupportNull = (byte) 4;
        private readonly int fieldNumber;
        private readonly byte options;
        private readonly WireType packedWireType;
        private readonly Type arrayType;
        private readonly Type itemType;

        public override Type ExpectedType
        {
            get { return this.arrayType; }
        }

        public override bool RequiresOldValue
        {
            get { return this.AppendToCollection; }
        }

        public override bool ReturnsValue
        {
            get { return true; }
        }

        private bool AppendToCollection
        {
            get { return ((int) this.options & 2) == 0; }
        }

        private bool SupportNull
        {
            get { return ((int) this.options & 4) != 0; }
        }

        public ArrayDecorator(TypeModel model, IProtoSerializer tail, int fieldNumber, bool writePacked,
            WireType packedWireType, Type arrayType, bool overwriteList, bool supportNull)
            : base(tail)
        {
            this.itemType = arrayType.GetElementType();
            if (!supportNull)
                Helpers.GetUnderlyingType(this.itemType);
            if ((writePacked || packedWireType != WireType.None) && fieldNumber <= 0)
                throw new ArgumentOutOfRangeException("fieldNumber");
            if (!ListDecorator.CanPack(packedWireType))
            {
                if (writePacked)
                    throw new InvalidOperationException("Only simple data-types can use packed encoding");
                packedWireType = WireType.None;
            }
            this.fieldNumber = fieldNumber;
            this.packedWireType = packedWireType;
            if (writePacked)
                this.options |= (byte) 1;
            if (overwriteList)
                this.options |= (byte) 2;
            if (supportNull)
                this.options |= (byte) 4;
            this.arrayType = arrayType;
        }

        protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            using (Local localWithValue = ctx.GetLocalWithValue(this.arrayType, valueFrom))
            {
                using (Local i = new Local(ctx, ctx.MapType(typeof (int))))
                {
                    bool flag = ((int) this.options & 1) != 0;
                    using (Local local = flag ? new Local(ctx, ctx.MapType(typeof (SubItemToken))) : (Local) null)
                    {
                        Type type = ctx.MapType(typeof (ProtoWriter));
                        if (flag)
                        {
                            ctx.LoadValue(this.fieldNumber);
                            ctx.LoadValue(2);
                            ctx.LoadReaderWriter();
                            ctx.EmitCall(type.GetMethod("WriteFieldHeader"));
                            ctx.LoadValue(localWithValue);
                            ctx.LoadReaderWriter();
                            ctx.EmitCall(type.GetMethod("StartSubItem"));
                            ctx.StoreValue(local);
                            ctx.LoadValue(this.fieldNumber);
                            ctx.LoadReaderWriter();
                            ctx.EmitCall(type.GetMethod("SetPackedField"));
                        }
                        this.EmitWriteArrayLoop(ctx, i, localWithValue);
                        if (!flag)
                            return;
                        ctx.LoadValue(local);
                        ctx.LoadReaderWriter();
                        ctx.EmitCall(type.GetMethod("EndSubItem"));
                    }
                }
            }
        }

        private void EmitWriteArrayLoop(CompilerContext ctx, Local i, Local arr)
        {
            ctx.LoadValue(0);
            ctx.StoreValue(i);
            CodeLabel label1 = ctx.DefineLabel();
            CodeLabel label2 = ctx.DefineLabel();
            ctx.Branch(label1, false);
            ctx.MarkLabel(label2);
            ctx.LoadArrayValue(arr, i);
            if (this.SupportNull)
                this.Tail.EmitWrite(ctx, (Local) null);
            else
                ctx.WriteNullCheckedTail(this.itemType, this.Tail, (Local) null);
            ctx.LoadValue(i);
            ctx.LoadValue(1);
            ctx.Add();
            ctx.StoreValue(i);
            ctx.MarkLabel(label1);
            ctx.LoadValue(i);
            ctx.LoadLength(arr, false);
            ctx.BranchIfLess(label2, false);
        }

        public override void Write(object value, ProtoWriter dest)
        {
            IList list = (IList) value;
            int count = list.Count;
            bool flag1 = ((int) this.options & 1) != 0;
            SubItemToken token;
            if (flag1)
            {
                ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
                token = ProtoWriter.StartSubItem(value, dest);
                ProtoWriter.SetPackedField(this.fieldNumber, dest);
            }
            else
                token = new SubItemToken();
            bool flag2 = !this.SupportNull;
            for (int index = 0; index < count; ++index)
            {
                object obj = list[index];
                if (flag2 && obj == null)
                    throw new NullReferenceException();
                this.Tail.Write(obj, dest);
            }
            if (!flag1)
                return;
            ProtoWriter.EndSubItem(token, dest);
        }

        public override object Read(object value, ProtoReader source)
        {
            int fieldNumber = source.FieldNumber;
            BasicList basicList = new BasicList();
            if (this.packedWireType != WireType.None && source.WireType == WireType.String)
            {
                SubItemToken token = ProtoReader.StartSubItem(source);
                while (ProtoReader.HasSubValue(this.packedWireType, source))
                    basicList.Add(this.Tail.Read((object) null, source));
                ProtoReader.EndSubItem(token, source);
            }
            else
            {
                do
                {
                    basicList.Add(this.Tail.Read((object) null, source));
                } while (source.TryReadFieldHeader(fieldNumber));
            }
            int offset = this.AppendToCollection ? (value == null ? 0 : ((Array) value).Length) : 0;
            Array instance = Array.CreateInstance(this.itemType, offset + basicList.Count);
            if (offset != 0)
                ((Array) value).CopyTo(instance, 0);
            basicList.CopyTo(instance, offset);
            return (object) instance;
        }

        protected override void EmitRead(CompilerContext ctx, Local valueFrom)
        {
            Type type = ctx.MapType(typeof (List<>)).MakeGenericType(this.itemType);
            using (
                Local local1 = this.AppendToCollection
                    ? ctx.GetLocalWithValue(this.ExpectedType, valueFrom)
                    : (Local) null)
            {
                using (Local local2 = new Local(ctx, this.ExpectedType))
                {
                    using (Local local3 = new Local(ctx, type))
                    {
                        ctx.EmitCtor(type);
                        ctx.StoreValue(local3);
                        ListDecorator.EmitReadList(ctx, local3, this.Tail, type.GetMethod("Add"), this.packedWireType);
                        using (
                            Local local4 = this.AppendToCollection
                                ? new Local(ctx, ctx.MapType(typeof (int)))
                                : (Local) null)
                        {
                            Type[] types = new Type[2]
                            {
                                ctx.MapType(typeof (Array)),
                                ctx.MapType(typeof (int))
                            };
                            if (this.AppendToCollection)
                            {
                                ctx.LoadLength(local1, true);
                                ctx.CopyValue();
                                ctx.StoreValue(local4);
                                ctx.LoadAddress(local3, type);
                                ctx.LoadValue(type.GetProperty("Count"));
                                ctx.Add();
                                ctx.CreateArray(this.itemType, (Local) null);
                                ctx.StoreValue(local2);
                                ctx.LoadValue(local4);
                                CodeLabel label = ctx.DefineLabel();
                                ctx.BranchIfFalse(label, true);
                                ctx.LoadValue(local1);
                                ctx.LoadValue(local2);
                                ctx.LoadValue(0);
                                ctx.EmitCall(this.ExpectedType.GetMethod("CopyTo", types));
                                ctx.MarkLabel(label);
                                ctx.LoadValue(local3);
                                ctx.LoadValue(local2);
                                ctx.LoadValue(local4);
                            }
                            else
                            {
                                ctx.LoadAddress(local3, type);
                                ctx.LoadValue(type.GetProperty("Count"));
                                ctx.CreateArray(this.itemType, (Local) null);
                                ctx.StoreValue(local2);
                                ctx.LoadAddress(local3, type);
                                ctx.LoadValue(local2);
                                ctx.LoadValue(0);
                            }
                            types[0] = this.ExpectedType;
                            MethodInfo method = type.GetMethod("CopyTo", types);
                            if (method == (MethodInfo) null)
                            {
                                types[1] = ctx.MapType(typeof (Array));
                                method = type.GetMethod("CopyTo", types);
                            }
                            ctx.EmitCall(method);
                        }
                        ctx.LoadValue(local2);
                    }
                }
            }
        }
    }
}