// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.ListDecorator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
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
    internal sealed class ListDecorator : ProtoDecoratorBase
    {
        private static readonly Type ienumeratorType = typeof (IEnumerator);
        private static readonly Type ienumerableType = typeof (IEnumerable);
        private const byte OPTIONS_IsList = (byte) 1;
        private const byte OPTIONS_SuppressIList = (byte) 2;
        private const byte OPTIONS_WritePacked = (byte) 4;
        private const byte OPTIONS_ReturnList = (byte) 8;
        private const byte OPTIONS_OverwriteList = (byte) 16;
        private const byte OPTIONS_SupportNull = (byte) 32;
        private readonly byte options;
        private readonly Type declaredType;
        private readonly Type concreteType;
        private readonly MethodInfo add;
        private readonly int fieldNumber;
        private readonly WireType packedWireType;

        private bool IsList
        {
            get { return ((int) this.options & 1) != 0; }
        }

        private bool SuppressIList
        {
            get { return ((int) this.options & 2) != 0; }
        }

        private bool WritePacked
        {
            get { return ((int) this.options & 4) != 0; }
        }

        private bool SupportNull
        {
            get { return ((int) this.options & 32) != 0; }
        }

        private bool ReturnList
        {
            get { return ((int) this.options & 8) != 0; }
        }

        public override Type ExpectedType
        {
            get { return this.declaredType; }
        }

        public override bool RequiresOldValue
        {
            get { return this.AppendToCollection; }
        }

        public override bool ReturnsValue
        {
            get { return this.ReturnList; }
        }

        private bool AppendToCollection
        {
            get { return ((int) this.options & 16) == 0; }
        }

        public ListDecorator(TypeModel model, Type declaredType, Type concreteType, IProtoSerializer tail,
            int fieldNumber, bool writePacked, WireType packedWireType, bool returnList, bool overwriteList,
            bool supportNull)
            : base(tail)
        {
            if (returnList)
                this.options |= (byte) 8;
            if (overwriteList)
                this.options |= (byte) 16;
            if (supportNull)
                this.options |= (byte) 32;
            if ((writePacked || packedWireType != WireType.None) && fieldNumber <= 0)
                throw new ArgumentOutOfRangeException("fieldNumber");
            if (!ListDecorator.CanPack(packedWireType))
            {
                if (writePacked)
                    throw new InvalidOperationException("Only simple data-types can use packed encoding");
                packedWireType = WireType.None;
            }
            this.fieldNumber = fieldNumber;
            if (writePacked)
                this.options |= (byte) 4;
            this.packedWireType = packedWireType;
            if (declaredType == (Type) null)
                throw new ArgumentNullException("declaredType");
            if (declaredType.IsArray)
                throw new ArgumentException("Cannot treat arrays as lists", "declaredType");
            this.declaredType = declaredType;
            this.concreteType = concreteType;
            bool isList;
            this.add = TypeModel.ResolveListAdd(model, declaredType, tail.ExpectedType, out isList);
            if (isList)
            {
                this.options |= (byte) 1;
                string fullName = declaredType.FullName;
                if (fullName != null && fullName.StartsWith("System.Data.Linq.EntitySet`1[["))
                    this.options |= (byte) 2;
            }
            if (this.add == (MethodInfo) null)
                throw new InvalidOperationException("Unable to resolve a suitable Add method for " +
                                                    declaredType.FullName);
        }

        internal static bool CanPack(WireType wireType)
        {
            switch (wireType)
            {
                case WireType.Variant:
                case WireType.Fixed64:
                case WireType.Fixed32:
                case WireType.SignedVariant:
                    return true;
                default:
                    return false;
            }
        }

        protected override void EmitRead(CompilerContext ctx, Local valueFrom)
        {
            bool returnList = this.ReturnList;
            using (
                Local local1 = this.AppendToCollection
                    ? ctx.GetLocalWithValue(this.ExpectedType, valueFrom)
                    : new Local(ctx, this.declaredType))
            {
                using (
                    Local local2 = !returnList || !this.AppendToCollection
                        ? (Local) null
                        : new Local(ctx, this.ExpectedType))
                {
                    if (!this.AppendToCollection)
                    {
                        ctx.LoadNullRef();
                        ctx.StoreValue(local1);
                    }
                    else if (returnList)
                    {
                        ctx.LoadValue(local1);
                        ctx.StoreValue(local2);
                    }
                    if (this.concreteType != (Type) null)
                    {
                        ctx.LoadValue(local1);
                        CodeLabel label = ctx.DefineLabel();
                        ctx.BranchIfTrue(label, true);
                        ctx.EmitCtor(this.concreteType);
                        ctx.StoreValue(local1);
                        ctx.MarkLabel(label);
                    }
                    ListDecorator.EmitReadList(ctx, local1, this.Tail, this.add, this.packedWireType);
                    if (!returnList)
                        return;
                    if (this.AppendToCollection)
                    {
                        ctx.LoadValue(local2);
                        ctx.LoadValue(local1);
                        CodeLabel label1 = ctx.DefineLabel();
                        CodeLabel label2 = ctx.DefineLabel();
                        ctx.BranchIfEqual(label1, true);
                        ctx.LoadValue(local1);
                        ctx.Branch(label2, true);
                        ctx.MarkLabel(label1);
                        ctx.LoadNullRef();
                        ctx.MarkLabel(label2);
                    }
                    else
                        ctx.LoadValue(local1);
                }
            }
        }

        internal static void EmitReadList(CompilerContext ctx, Local list, IProtoSerializer tail, MethodInfo add,
            WireType packedWireType)
        {
            using (Local local = new Local(ctx, ctx.MapType(typeof (int))))
            {
                CodeLabel label1 = packedWireType == WireType.None ? new CodeLabel() : ctx.DefineLabel();
                if (packedWireType != WireType.None)
                {
                    ctx.LoadReaderWriter();
                    ctx.LoadValue(typeof (ProtoReader).GetProperty("WireType"));
                    ctx.LoadValue(2);
                    ctx.BranchIfEqual(label1, false);
                }
                ctx.LoadReaderWriter();
                ctx.LoadValue(typeof (ProtoReader).GetProperty("FieldNumber"));
                ctx.StoreValue(local);
                CodeLabel label2 = ctx.DefineLabel();
                ctx.MarkLabel(label2);
                ListDecorator.EmitReadAndAddItem(ctx, list, tail, add);
                ctx.LoadReaderWriter();
                ctx.LoadValue(local);
                ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("TryReadFieldHeader"));
                ctx.BranchIfTrue(label2, false);
                if (packedWireType == WireType.None)
                    return;
                CodeLabel label3 = ctx.DefineLabel();
                ctx.Branch(label3, false);
                ctx.MarkLabel(label1);
                ctx.LoadReaderWriter();
                ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("StartSubItem"));
                CodeLabel label4 = ctx.DefineLabel();
                CodeLabel label5 = ctx.DefineLabel();
                ctx.MarkLabel(label4);
                ctx.LoadValue((int) packedWireType);
                ctx.LoadReaderWriter();
                ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("HasSubValue"));
                ctx.BranchIfFalse(label5, false);
                ListDecorator.EmitReadAndAddItem(ctx, list, tail, add);
                ctx.Branch(label4, false);
                ctx.MarkLabel(label5);
                ctx.LoadReaderWriter();
                ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("EndSubItem"));
                ctx.MarkLabel(label3);
            }
        }

        private static void EmitReadAndAddItem(CompilerContext ctx, Local list, IProtoSerializer tail, MethodInfo add)
        {
            ctx.LoadValue(list);
            Type expectedType = tail.ExpectedType;
            if (tail.RequiresOldValue)
            {
                if (expectedType.IsValueType || !tail.ReturnsValue)
                {
                    using (Local local = new Local(ctx, expectedType))
                    {
                        if (expectedType.IsValueType)
                        {
                            ctx.LoadAddress(local, expectedType);
                            ctx.EmitCtor(expectedType);
                        }
                        else
                        {
                            ctx.LoadNullRef();
                            ctx.StoreValue(local);
                        }
                        tail.EmitRead(ctx, local);
                        if (!tail.ReturnsValue)
                            ctx.LoadValue(local);
                    }
                }
                else
                {
                    ctx.LoadNullRef();
                    tail.EmitRead(ctx, (Local) null);
                }
            }
            else
            {
                if (!tail.ReturnsValue)
                    throw new InvalidOperationException();
                tail.EmitRead(ctx, (Local) null);
            }
            Type parameterType = add.GetParameters()[0].ParameterType;
            if (parameterType != expectedType)
            {
                if (parameterType == ctx.MapType(typeof (object)))
                {
                    ctx.CastToObject(expectedType);
                }
                else
                {
                    if (!(Helpers.GetUnderlyingType(parameterType) == expectedType))
                        throw new InvalidOperationException("Conflicting item/add type");
                    ConstructorInfo constructor = Helpers.GetConstructor(parameterType, new Type[1]
                    {
                        expectedType
                    }, 0 != 0);
                    ctx.EmitCtor(constructor);
                }
            }
            ctx.EmitCall(add);
            if (!(add.ReturnType != ctx.MapType(typeof (void))))
                return;
            ctx.DiscardValue();
        }

        private MethodInfo GetEnumeratorInfo(TypeModel model, out MethodInfo moveNext, out MethodInfo current)
        {
            moveNext = null;
            current = null;
            return null;
        }

        protected override void EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, valueFrom))
            {
                MethodInfo moveNext;
                MethodInfo current;
                MethodInfo enumeratorInfo = this.GetEnumeratorInfo(ctx.Model, out moveNext, out current);
                Type returnType = enumeratorInfo.ReturnType;
                bool writePacked = this.WritePacked;
                using (Local local1 = new Local(ctx, returnType))
                {
                    using (
                        Local local2 = writePacked ? new Local(ctx, ctx.MapType(typeof (SubItemToken))) : (Local) null)
                    {
                        if (writePacked)
                        {
                            ctx.LoadValue(this.fieldNumber);
                            ctx.LoadValue(2);
                            ctx.LoadReaderWriter();
                            ctx.EmitCall(ctx.MapType(typeof (ProtoWriter)).GetMethod("WriteFieldHeader"));
                            ctx.LoadValue(localWithValue);
                            ctx.LoadReaderWriter();
                            ctx.EmitCall(ctx.MapType(typeof (ProtoWriter)).GetMethod("StartSubItem"));
                            ctx.StoreValue(local2);
                            ctx.LoadValue(this.fieldNumber);
                            ctx.LoadReaderWriter();
                            ctx.EmitCall(ctx.MapType(typeof (ProtoWriter)).GetMethod("SetPackedField"));
                        }
                        ctx.LoadAddress(localWithValue, this.ExpectedType);
                        ctx.EmitCall(enumeratorInfo);
                        ctx.StoreValue(local1);
                        using (ctx.Using(local1))
                        {
                            CodeLabel label1 = ctx.DefineLabel();
                            CodeLabel label2 = ctx.DefineLabel();
                            ctx.Branch(label2, false);
                            ctx.MarkLabel(label1);
                            ctx.LoadAddress(local1, returnType);
                            ctx.EmitCall(current);
                            Type expectedType = this.Tail.ExpectedType;
                            if (expectedType != ctx.MapType(typeof (object)) &&
                                current.ReturnType == ctx.MapType(typeof (object)))
                                ctx.CastFromObject(expectedType);
                            this.Tail.EmitWrite(ctx, (Local) null);
                            ctx.MarkLabel(label2);
                            ctx.LoadAddress(local1, returnType);
                            ctx.EmitCall(moveNext);
                            ctx.BranchIfTrue(label1, false);
                        }
                        if (!writePacked)
                            return;
                        ctx.LoadValue(local2);
                        ctx.LoadReaderWriter();
                        ctx.EmitCall(ctx.MapType(typeof (ProtoWriter)).GetMethod("EndSubItem"));
                    }
                }
            }
        }

        public override void Write(object value, ProtoWriter dest)
        {
            bool writePacked = this.WritePacked;
            SubItemToken token;
            if (writePacked)
            {
                ProtoWriter.WriteFieldHeader(this.fieldNumber, WireType.String, dest);
                token = ProtoWriter.StartSubItem(value, dest);
                ProtoWriter.SetPackedField(this.fieldNumber, dest);
            }
            else
                token = new SubItemToken();
            bool flag = !this.SupportNull;
            foreach (object obj in (IEnumerable) value)
            {
                if (flag && obj == null)
                    throw new NullReferenceException();
                this.Tail.Write(obj, dest);
            }
            if (!writePacked)
                return;
            ProtoWriter.EndSubItem(token, dest);
        }

        public override object Read(object value, ProtoReader source)
        {
            int fieldNumber = source.FieldNumber;
            object obj = value;
            if (value == null)
                value = Activator.CreateInstance(this.concreteType);
            bool flag = this.IsList && !this.SuppressIList;
            if (this.packedWireType != WireType.None && source.WireType == WireType.String)
            {
                SubItemToken token = ProtoReader.StartSubItem(source);
                if (flag)
                {
                    IList list = (IList) value;
                    while (ProtoReader.HasSubValue(this.packedWireType, source))
                        list.Add(this.Tail.Read((object) null, source));
                }
                else
                {
                    object[] parameters = new object[1];
                    while (ProtoReader.HasSubValue(this.packedWireType, source))
                    {
                        parameters[0] = this.Tail.Read((object) null, source);
                        this.add.Invoke(value, parameters);
                    }
                }
                ProtoReader.EndSubItem(token, source);
            }
            else if (flag)
            {
                IList list = (IList) value;
                do
                {
                    list.Add(this.Tail.Read((object) null, source));
                } while (source.TryReadFieldHeader(fieldNumber));
            }
            else
            {
                object[] parameters = new object[1];
                do
                {
                    parameters[0] = this.Tail.Read((object) null, source);
                    this.add.Invoke(value, parameters);
                } while (source.TryReadFieldHeader(fieldNumber));
            }
            if (obj != value)
                return value;
            else
                return (object) null;
        }
    }
}