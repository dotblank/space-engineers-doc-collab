// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializers.TupleSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Compiler;
using ProtoBuf.Meta;
using System;
using System.Reflection;

namespace ProtoBuf.Serializers
{
    internal sealed class TupleSerializer : IProtoTypeSerializer, IProtoSerializer
    {
        private readonly MemberInfo[] members;
        private readonly ConstructorInfo ctor;
        private IProtoSerializer[] tails;

        public Type ExpectedType
        {
            get { return this.ctor.DeclaringType; }
        }

        public bool RequiresOldValue
        {
            get { return true; }
        }

        public bool ReturnsValue
        {
            get { return false; }
        }

        public TupleSerializer(RuntimeTypeModel model, ConstructorInfo ctor, MemberInfo[] members)
        {
            if (ctor == (ConstructorInfo) null)
                throw new ArgumentNullException("ctor");
            if (members == null)
                throw new ArgumentNullException("members");
            this.ctor = ctor;
            this.members = members;
            this.tails = new IProtoSerializer[members.Length];
            ParameterInfo[] parameters = ctor.GetParameters();
            for (int index = 0; index < members.Length; ++index)
            {
                Type parameterType = parameters[index].ParameterType;
                Type itemType = (Type) null;
                Type defaultType = (Type) null;
                MetaType.ResolveListTypes((TypeModel) model, parameterType, ref itemType, ref defaultType);
                Type type = itemType == (Type) null ? parameterType : itemType;
                bool asReference = false;
                if (model.FindOrAddAuto(type, false, true, false) >= 0)
                    asReference = model[type].AsReferenceDefault;
                WireType defaultWireType;
                IProtoSerializer coreSerializer = ValueMember.TryGetCoreSerializer(model, DataFormat.Default, type,
                    out defaultWireType, asReference, false, false, true);
                if (coreSerializer == null)
                    throw new InvalidOperationException("No serializer defined for type: " + type.FullName);
                IProtoSerializer tail =
                    (IProtoSerializer) new TagDecorator(index + 1, defaultWireType, false, coreSerializer);
                IProtoSerializer protoSerializer = !(itemType == (Type) null)
                    ? (!parameterType.IsArray
                        ? (IProtoSerializer)
                            new ListDecorator((TypeModel) model, parameterType, defaultType, tail, index + 1, false,
                                defaultWireType, true, false, false)
                        : (IProtoSerializer)
                            new ArrayDecorator((TypeModel) model, tail, index + 1, false, defaultWireType, parameterType,
                                false, false))
                    : tail;
                this.tails[index] = protoSerializer;
            }
        }

        public bool HasCallbacks(TypeModel.CallbackType callbackType)
        {
            return false;
        }

        public void EmitCallback(CompilerContext ctx, Local valueFrom, TypeModel.CallbackType callbackType)
        {
        }

        void IProtoTypeSerializer.Callback(object value, TypeModel.CallbackType callbackType,
            SerializationContext context)
        {
        }

        object IProtoTypeSerializer.CreateInstance(ProtoReader source)
        {
            throw new NotSupportedException();
        }

        private object GetValue(object obj, int index)
        {
            PropertyInfo propertyInfo;
            if ((propertyInfo = this.members[index] as PropertyInfo) != (PropertyInfo) null)
            {
                if (obj != null)
                    return propertyInfo.GetValue(obj, (object[]) null);
                if (!Helpers.IsValueType(propertyInfo.PropertyType))
                    return (object) null;
                else
                    return Activator.CreateInstance(propertyInfo.PropertyType);
            }
            else
            {
                FieldInfo fieldInfo;
                if (!((fieldInfo = this.members[index] as FieldInfo) != (FieldInfo) null))
                    throw new InvalidOperationException();
                if (obj != null)
                    return fieldInfo.GetValue(obj);
                if (!Helpers.IsValueType(fieldInfo.FieldType))
                    return (object) null;
                else
                    return Activator.CreateInstance(fieldInfo.FieldType);
            }
        }

        public object Read(object value, ProtoReader source)
        {
            object[] parameters = new object[this.members.Length];
            bool flag = false;
            if (value == null)
                flag = true;
            for (int index = 0; index < parameters.Length; ++index)
                parameters[index] = this.GetValue(value, index);
            int num;
            while ((num = source.ReadFieldHeader()) > 0)
            {
                flag = true;
                if (num <= this.tails.Length)
                {
                    IProtoSerializer protoSerializer = this.tails[num - 1];
                    parameters[num - 1] =
                        this.tails[num - 1].Read(
                            protoSerializer.RequiresOldValue ? parameters[num - 1] : (object) null, source);
                }
                else
                    source.SkipField();
            }
            if (!flag)
                return value;
            else
                return this.ctor.Invoke(parameters);
        }

        public void Write(object value, ProtoWriter dest)
        {
            for (int index = 0; index < this.tails.Length; ++index)
            {
                object obj = this.GetValue(value, index);
                if (obj != null)
                    this.tails[index].Write(obj, dest);
            }
        }

        private Type GetMemberType(int index)
        {
            Type memberType = Helpers.GetMemberType(this.members[index]);
            if (memberType == (Type) null)
                throw new InvalidOperationException();
            else
                return memberType;
        }

        bool IProtoTypeSerializer.CanCreateInstance()
        {
            return false;
        }

        public void EmitWrite(CompilerContext ctx, Local valueFrom)
        {
            using (Local localWithValue = ctx.GetLocalWithValue(this.ctor.DeclaringType, valueFrom))
            {
                for (int index = 0; index < this.tails.Length; ++index)
                {
                    Type memberType = this.GetMemberType(index);
                    ctx.LoadAddress(localWithValue, this.ExpectedType);
                    switch (this.members[index].MemberType)
                    {
                        case MemberTypes.Field:
                            ctx.LoadValue((FieldInfo) this.members[index]);
                            break;
                        case MemberTypes.Property:
                            ctx.LoadValue((PropertyInfo) this.members[index]);
                            break;
                    }
                    ctx.WriteNullCheckedTail(memberType, this.tails[index], (Local) null);
                }
            }
        }

        void IProtoTypeSerializer.EmitCreateInstance(CompilerContext ctx)
        {
            throw new NotSupportedException();
        }

        public void EmitRead(CompilerContext ctx, Local incoming)
        {
            using (Local localWithValue = ctx.GetLocalWithValue(this.ExpectedType, incoming))
            {
                Local[] localArray = new Local[this.members.Length];
                try
                {
                    for (int index = 0; index < localArray.Length; ++index)
                    {
                        Type memberType = this.GetMemberType(index);
                        bool flag = true;
                        localArray[index] = new Local(ctx, memberType);
                        if (!this.ExpectedType.IsValueType)
                        {
                            if (memberType.IsValueType)
                            {
                                switch (Helpers.GetTypeCode(memberType))
                                {
                                    case ProtoTypeCode.Boolean:
                                    case ProtoTypeCode.SByte:
                                    case ProtoTypeCode.Byte:
                                    case ProtoTypeCode.Int16:
                                    case ProtoTypeCode.UInt16:
                                    case ProtoTypeCode.Int32:
                                    case ProtoTypeCode.UInt32:
                                        ctx.LoadValue(0);
                                        break;
                                    case ProtoTypeCode.Int64:
                                    case ProtoTypeCode.UInt64:
                                        ctx.LoadValue(0L);
                                        break;
                                    case ProtoTypeCode.Single:
                                        ctx.LoadValue(0.0f);
                                        break;
                                    case ProtoTypeCode.Double:
                                        ctx.LoadValue(0.0);
                                        break;
                                    case ProtoTypeCode.Decimal:
                                        ctx.LoadValue(new Decimal(0));
                                        break;
                                    case ProtoTypeCode.Guid:
                                        ctx.LoadValue(Guid.Empty);
                                        break;
                                    default:
                                        ctx.LoadAddress(localArray[index], memberType);
                                        ctx.EmitCtor(memberType);
                                        flag = false;
                                        break;
                                }
                            }
                            else
                                ctx.LoadNullRef();
                            if (flag)
                                ctx.StoreValue(localArray[index]);
                        }
                    }
                    CodeLabel label1 = this.ExpectedType.IsValueType ? new CodeLabel() : ctx.DefineLabel();
                    if (!this.ExpectedType.IsValueType)
                    {
                        ctx.LoadAddress(localWithValue, this.ExpectedType);
                        ctx.BranchIfFalse(label1, false);
                    }
                    for (int index = 0; index < this.members.Length; ++index)
                    {
                        ctx.LoadAddress(localWithValue, this.ExpectedType);
                        switch (this.members[index].MemberType)
                        {
                            case MemberTypes.Field:
                                ctx.LoadValue((FieldInfo) this.members[index]);
                                break;
                            case MemberTypes.Property:
                                ctx.LoadValue((PropertyInfo) this.members[index]);
                                break;
                        }
                        ctx.StoreValue(localArray[index]);
                    }
                    if (!this.ExpectedType.IsValueType)
                        ctx.MarkLabel(label1);
                    using (Local local = new Local(ctx, ctx.MapType(typeof (int))))
                    {
                        CodeLabel label2 = ctx.DefineLabel();
                        CodeLabel label3 = ctx.DefineLabel();
                        CodeLabel label4 = ctx.DefineLabel();
                        ctx.Branch(label2, false);
                        CodeLabel[] jumpTable = new CodeLabel[this.members.Length];
                        for (int index = 0; index < this.members.Length; ++index)
                            jumpTable[index] = ctx.DefineLabel();
                        ctx.MarkLabel(label3);
                        ctx.LoadValue(local);
                        ctx.LoadValue(1);
                        ctx.Subtract();
                        ctx.Switch(jumpTable);
                        ctx.Branch(label4, false);
                        for (int index = 0; index < jumpTable.Length; ++index)
                        {
                            ctx.MarkLabel(jumpTable[index]);
                            IProtoSerializer tail = this.tails[index];
                            Local valueFrom = tail.RequiresOldValue ? localArray[index] : (Local) null;
                            ctx.ReadNullCheckedTail(localArray[index].Type, tail, valueFrom);
                            if (tail.ReturnsValue)
                            {
                                if (localArray[index].Type.IsValueType)
                                {
                                    ctx.StoreValue(localArray[index]);
                                }
                                else
                                {
                                    CodeLabel label5 = ctx.DefineLabel();
                                    CodeLabel label6 = ctx.DefineLabel();
                                    ctx.CopyValue();
                                    ctx.BranchIfTrue(label5, true);
                                    ctx.DiscardValue();
                                    ctx.Branch(label6, true);
                                    ctx.MarkLabel(label5);
                                    ctx.StoreValue(localArray[index]);
                                    ctx.MarkLabel(label6);
                                }
                            }
                            ctx.Branch(label2, false);
                        }
                        ctx.MarkLabel(label4);
                        ctx.LoadReaderWriter();
                        ctx.EmitCall(ctx.MapType(typeof (ProtoReader)).GetMethod("SkipField"));
                        ctx.MarkLabel(label2);
                        ctx.EmitBasicRead("ReadFieldHeader", ctx.MapType(typeof (int)));
                        ctx.CopyValue();
                        ctx.StoreValue(local);
                        ctx.LoadValue(0);
                        ctx.BranchIfGreater(label3, false);
                    }
                    for (int index = 0; index < localArray.Length; ++index)
                        ctx.LoadValue(localArray[index]);
                    ctx.EmitCtor(this.ctor);
                    ctx.StoreValue(localWithValue);
                }
                finally
                {
                    for (int index = 0; index < localArray.Length; ++index)
                    {
                        if (localArray[index] != null)
                            localArray[index].Dispose();
                    }
                }
            }
        }
    }
}