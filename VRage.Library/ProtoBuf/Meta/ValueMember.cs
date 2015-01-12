// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.ValueMember
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace ProtoBuf.Meta
{
    public class ValueMember
    {
        private const byte OPTIONS_IsStrict = (byte) 1;
        private const byte OPTIONS_IsPacked = (byte) 2;
        private const byte OPTIONS_IsRequired = (byte) 4;
        private const byte OPTIONS_OverwriteList = (byte) 8;
        private const byte OPTIONS_SupportNull = (byte) 16;
        private readonly int fieldNumber;
        private readonly MemberInfo member;
        private readonly Type parentType;
        private readonly Type itemType;
        private readonly Type defaultType;
        private readonly Type memberType;
        private object defaultValue;
        private readonly RuntimeTypeModel model;
        private IProtoSerializer serializer;
        private DataFormat dataFormat;
        private bool asReference;
        private bool dynamicType;
        private MethodInfo getSpecified;
        private MethodInfo setSpecified;
        private string name;
        private byte flags;

        public int FieldNumber
        {
            get { return this.fieldNumber; }
        }

        public MemberInfo Member
        {
            get { return this.member; }
        }

        public Type ItemType
        {
            get { return this.itemType; }
        }

        public Type MemberType
        {
            get { return this.memberType; }
        }

        public Type DefaultType
        {
            get { return this.defaultType; }
        }

        public Type ParentType
        {
            get { return this.parentType; }
        }

        public object DefaultValue
        {
            get { return this.defaultValue; }
            set
            {
                this.ThrowIfFrozen();
                this.defaultValue = value;
            }
        }

        internal IProtoSerializer Serializer
        {
            get
            {
                if (this.serializer == null)
                    this.serializer = this.BuildSerializer();
                return this.serializer;
            }
        }

        public DataFormat DataFormat
        {
            get { return this.dataFormat; }
            set
            {
                this.ThrowIfFrozen();
                this.dataFormat = value;
            }
        }

        public bool IsStrict
        {
            get { return this.HasFlag((byte) 1); }
            set { this.SetFlag((byte) 1, value, true); }
        }

        public bool IsPacked
        {
            get { return this.HasFlag((byte) 2); }
            set { this.SetFlag((byte) 2, value, true); }
        }

        public bool OverwriteList
        {
            get { return this.HasFlag((byte) 8); }
            set { this.SetFlag((byte) 8, value, true); }
        }

        public bool IsRequired
        {
            get { return this.HasFlag((byte) 4); }
            set { this.SetFlag((byte) 4, value, true); }
        }

        public bool AsReference
        {
            get { return this.asReference; }
            set
            {
                this.ThrowIfFrozen();
                this.asReference = value;
            }
        }

        public bool DynamicType
        {
            get { return this.dynamicType; }
            set
            {
                this.ThrowIfFrozen();
                this.dynamicType = value;
            }
        }

        public string Name
        {
            get
            {
                if (!Helpers.IsNullOrEmpty(this.name))
                    return this.name;
                else
                    return this.member.Name;
            }
        }

        public bool SupportNull
        {
            get { return this.HasFlag((byte) 16); }
            set { this.SetFlag((byte) 16, value, true); }
        }

        public ValueMember(RuntimeTypeModel model, Type parentType, int fieldNumber, MemberInfo member, Type memberType,
            Type itemType, Type defaultType, DataFormat dataFormat, object defaultValue)
            : this(model, fieldNumber, memberType, itemType, defaultType, dataFormat)
        {
            if (member == (MemberInfo) null)
                throw new ArgumentNullException("member");
            if (parentType == (Type) null)
                throw new ArgumentNullException("parentType");
            if (fieldNumber < 1 && !Helpers.IsEnum(parentType))
                throw new ArgumentOutOfRangeException("fieldNumber");
            this.member = member;
            this.parentType = parentType;
            if (fieldNumber < 1 && !Helpers.IsEnum(parentType))
                throw new ArgumentOutOfRangeException("fieldNumber");
            if (defaultValue != null && model.MapType(defaultValue.GetType()) != memberType)
                defaultValue = ValueMember.ParseDefaultValue(memberType, defaultValue);
            this.defaultValue = defaultValue;
            MetaType withoutAdd = model.FindWithoutAdd(memberType);
            if (withoutAdd == null)
                return;
            this.asReference = withoutAdd.AsReferenceDefault;
        }

        internal ValueMember(RuntimeTypeModel model, int fieldNumber, Type memberType, Type itemType, Type defaultType,
            DataFormat dataFormat)
        {
            if (memberType == (Type) null)
                throw new ArgumentNullException("memberType");
            if (model == null)
                throw new ArgumentNullException("model");
            this.fieldNumber = fieldNumber;
            this.memberType = memberType;
            this.itemType = itemType;
            this.defaultType = defaultType;
            this.model = model;
            this.dataFormat = dataFormat;
        }

        internal object GetRawEnumValue()
        {
            return ((FieldInfo) this.member).GetRawConstantValue();
        }

        private static object ParseDefaultValue(Type type, object value)
        {
            Type underlyingType = Helpers.GetUnderlyingType(type);
            if (underlyingType != (Type) null)
                type = underlyingType;
            if (value is string)
            {
                string str = (string) value;
                if (Helpers.IsEnum(type))
                    return Helpers.ParseEnum(type, str);
                switch (Helpers.GetTypeCode(type))
                {
                    case ProtoTypeCode.Boolean:
                        return (object) (bool.Parse(str) ? 1 : 0);
                    case ProtoTypeCode.Char:
                        if (str.Length == 1)
                            return (object) str[0];
                        else
                            throw new FormatException("Single character expected: \"" + str + "\"");
                    case ProtoTypeCode.SByte:
                        return
                            (object)
                                sbyte.Parse(str, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.Byte:
                        return
                            (object)
                                byte.Parse(str, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.Int16:
                        return
                            (object) short.Parse(str, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.UInt16:
                        return
                            (object) ushort.Parse(str, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.Int32:
                        return (object) int.Parse(str, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.UInt32:
                        return
                            (object) uint.Parse(str, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.Int64:
                        return
                            (object) long.Parse(str, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.UInt64:
                        return
                            (object) ulong.Parse(str, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.Single:
                        return
                            (object) float.Parse(str, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.Double:
                        return
                            (object) double.Parse(str, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.Decimal:
                        return
                            (object)
                                Decimal.Parse(str, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.DateTime:
                        return (object) DateTime.Parse(str, (IFormatProvider) CultureInfo.InvariantCulture);
                    case ProtoTypeCode.String:
                        return (object) str;
                    case ProtoTypeCode.TimeSpan:
                        return (object) TimeSpan.Parse(str);
                    case ProtoTypeCode.Guid:
                        return (object) new Guid(str);
                    case ProtoTypeCode.Uri:
                        return (object) str;
                }
            }
            if (Helpers.IsEnum(type))
                return Enum.ToObject(type, value);
            else
                return Convert.ChangeType(value, type, (IFormatProvider) CultureInfo.InvariantCulture);
        }

        public void SetSpecified(MethodInfo getSpecified, MethodInfo setSpecified)
        {
            if (getSpecified != (MethodInfo) null &&
                (getSpecified.ReturnType != this.model.MapType(typeof (bool)) || getSpecified.IsStatic ||
                 getSpecified.GetParameters().Length != 0))
                throw new ArgumentException("Invalid pattern for checking member-specified", "getSpecified");
            ParameterInfo[] parameters;
            if (setSpecified != (MethodInfo) null &&
                (setSpecified.ReturnType != this.model.MapType(typeof (void)) || setSpecified.IsStatic ||
                 ((parameters = setSpecified.GetParameters()).Length != 1 ||
                  parameters[0].ParameterType != this.model.MapType(typeof (bool)))))
                throw new ArgumentException("Invalid pattern for setting member-specified", "setSpecified");
            this.ThrowIfFrozen();
            this.getSpecified = getSpecified;
            this.setSpecified = setSpecified;
        }

        private void ThrowIfFrozen()
        {
            if (this.serializer != null)
                throw new InvalidOperationException("The type cannot be changed once a serializer has been generated");
        }

        private IProtoSerializer BuildSerializer()
        {
            int opaqueToken = 0;
            try
            {
                this.model.TakeLock(ref opaqueToken);
                Type type = this.itemType == (Type) null ? this.memberType : this.itemType;
                WireType defaultWireType;
                IProtoSerializer coreSerializer = ValueMember.TryGetCoreSerializer(this.model, this.dataFormat, type,
                    out defaultWireType, this.asReference, this.dynamicType, this.OverwriteList, true);
                if (coreSerializer == null)
                    throw new InvalidOperationException("No serializer defined for type: " + type.FullName);
                IProtoSerializer tail;
                if (this.itemType != (Type) null && this.SupportNull)
                {
                    if (this.IsPacked)
                        throw new NotSupportedException("Packed encodings cannot support null values");
                    tail =
                        (IProtoSerializer)
                            new TagDecorator(this.fieldNumber, WireType.StartGroup, false,
                                (IProtoSerializer)
                                    new NullDecorator((TypeModel) this.model,
                                        (IProtoSerializer)
                                            new TagDecorator(1, defaultWireType, this.IsStrict, coreSerializer)));
                }
                else
                    tail =
                        (IProtoSerializer)
                            new TagDecorator(this.fieldNumber, defaultWireType, this.IsStrict, coreSerializer);
                if (this.itemType != (Type) null)
                {
                    if (!this.SupportNull)
                        Helpers.GetUnderlyingType(this.itemType);
                    tail = !this.memberType.IsArray
                        ? (IProtoSerializer)
                            new ListDecorator((TypeModel) this.model, this.memberType, this.defaultType, tail,
                                this.fieldNumber, this.IsPacked, defaultWireType,
                                this.member != (MemberInfo) null &&
                                PropertyDecorator.CanWrite((TypeModel) this.model, this.member), this.OverwriteList,
                                this.SupportNull)
                        : (IProtoSerializer)
                            new ArrayDecorator((TypeModel) this.model, tail, this.fieldNumber, this.IsPacked,
                                defaultWireType, this.memberType, this.OverwriteList, this.SupportNull);
                }
                else if (this.defaultValue != null && !this.IsRequired && this.getSpecified == (MethodInfo) null)
                    tail = (IProtoSerializer) new DefaultValueDecorator((TypeModel) this.model, this.defaultValue, tail);
                if (this.memberType == this.model.MapType(typeof (Uri)))
                    tail = (IProtoSerializer) new UriDecorator((TypeModel) this.model, tail);
                if (this.member != (MemberInfo) null)
                {
                    if (this.member as PropertyInfo != (PropertyInfo) null)
                    {
                        tail =
                            (IProtoSerializer)
                                new PropertyDecorator((TypeModel) this.model, this.parentType,
                                    (PropertyInfo) this.member, tail);
                    }
                    else
                    {
                        if (!(this.member as FieldInfo != (FieldInfo) null))
                            throw new InvalidOperationException();
                        tail = (IProtoSerializer) new FieldDecorator(this.parentType, (FieldInfo) this.member, tail);
                    }
                    if (this.getSpecified != (MethodInfo) null || this.setSpecified != (MethodInfo) null)
                        tail =
                            (IProtoSerializer) new MemberSpecifiedDecorator(this.getSpecified, this.setSpecified, tail);
                }
                return tail;
            }
            finally
            {
                this.model.ReleaseLock(opaqueToken);
            }
        }

        private static WireType GetIntWireType(DataFormat format, int width)
        {
            switch (format)
            {
                case DataFormat.Default:
                case DataFormat.TwosComplement:
                    return WireType.Variant;
                case DataFormat.ZigZag:
                    return WireType.SignedVariant;
                case DataFormat.FixedSize:
                    return width != 32 ? WireType.Fixed64 : WireType.Fixed32;
                default:
                    throw new InvalidOperationException();
            }
        }

        private static WireType GetDateTimeWireType(DataFormat format)
        {
            switch (format)
            {
                case DataFormat.Default:
                    return WireType.String;
                case DataFormat.FixedSize:
                    return WireType.Fixed64;
                case DataFormat.Group:
                    return WireType.StartGroup;
                default:
                    throw new InvalidOperationException();
            }
        }

        internal static IProtoSerializer TryGetCoreSerializer(RuntimeTypeModel model, DataFormat dataFormat, Type type,
            out WireType defaultWireType, bool asReference, bool dynamicType, bool overwriteList, bool allowComplexTypes)
        {
            type = Helpers.GetUnderlyingType(type) ?? type;
            if (Helpers.IsEnum(type))
            {
                if (allowComplexTypes && model != null)
                {
                    defaultWireType = WireType.Variant;
                    return (IProtoSerializer) new EnumSerializer(type, model.GetEnumMap(type));
                }
                else
                {
                    defaultWireType = WireType.None;
                    return (IProtoSerializer) null;
                }
            }
            else
            {
                switch (Helpers.GetTypeCode(type))
                {
                    case ProtoTypeCode.Boolean:
                        defaultWireType = WireType.Variant;
                        return (IProtoSerializer) new BooleanSerializer((TypeModel) model);
                    case ProtoTypeCode.Char:
                        defaultWireType = WireType.Variant;
                        return (IProtoSerializer) new CharSerializer((TypeModel) model);
                    case ProtoTypeCode.SByte:
                        defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
                        return (IProtoSerializer) new SByteSerializer((TypeModel) model);
                    case ProtoTypeCode.Byte:
                        defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
                        return (IProtoSerializer) new ByteSerializer((TypeModel) model);
                    case ProtoTypeCode.Int16:
                        defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
                        return (IProtoSerializer) new Int16Serializer((TypeModel) model);
                    case ProtoTypeCode.UInt16:
                        defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
                        return (IProtoSerializer) new UInt16Serializer((TypeModel) model);
                    case ProtoTypeCode.Int32:
                        defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
                        return (IProtoSerializer) new Int32Serializer((TypeModel) model);
                    case ProtoTypeCode.UInt32:
                        defaultWireType = ValueMember.GetIntWireType(dataFormat, 32);
                        return (IProtoSerializer) new UInt32Serializer((TypeModel) model);
                    case ProtoTypeCode.Int64:
                        defaultWireType = ValueMember.GetIntWireType(dataFormat, 64);
                        return (IProtoSerializer) new Int64Serializer((TypeModel) model);
                    case ProtoTypeCode.UInt64:
                        defaultWireType = ValueMember.GetIntWireType(dataFormat, 64);
                        return (IProtoSerializer) new UInt64Serializer((TypeModel) model);
                    case ProtoTypeCode.Single:
                        defaultWireType = WireType.Fixed32;
                        return (IProtoSerializer) new SingleSerializer((TypeModel) model);
                    case ProtoTypeCode.Double:
                        defaultWireType = WireType.Fixed64;
                        return (IProtoSerializer) new DoubleSerializer((TypeModel) model);
                    case ProtoTypeCode.Decimal:
                        defaultWireType = WireType.String;
                        return (IProtoSerializer) new DecimalSerializer((TypeModel) model);
                    case ProtoTypeCode.DateTime:
                        defaultWireType = ValueMember.GetDateTimeWireType(dataFormat);
                        return (IProtoSerializer) new DateTimeSerializer((TypeModel) model);
                    case ProtoTypeCode.String:
                        defaultWireType = WireType.String;
                        if (asReference)
                            return
                                (IProtoSerializer)
                                    new NetObjectSerializer((TypeModel) model, model.MapType(typeof (string)), 0,
                                        BclHelpers.NetObjectOptions.AsReference);
                        else
                            return (IProtoSerializer) new ProtoBuf.Serializers.StringSerializer((TypeModel) model);
                    case ProtoTypeCode.TimeSpan:
                        defaultWireType = ValueMember.GetDateTimeWireType(dataFormat);
                        return (IProtoSerializer) new TimeSpanSerializer((TypeModel) model);
                    case ProtoTypeCode.ByteArray:
                        defaultWireType = WireType.String;
                        return (IProtoSerializer) new BlobSerializer((TypeModel) model, overwriteList);
                    case ProtoTypeCode.Guid:
                        defaultWireType = WireType.String;
                        return (IProtoSerializer) new GuidSerializer((TypeModel) model);
                    case ProtoTypeCode.Uri:
                        defaultWireType = WireType.String;
                        return (IProtoSerializer) new ProtoBuf.Serializers.StringSerializer((TypeModel) model);
                    case ProtoTypeCode.Type:
                        defaultWireType = WireType.String;
                        return (IProtoSerializer) new SystemTypeSerializer((TypeModel) model);
                    default:
                        IProtoSerializer protoSerializer = model.AllowParseableTypes
                            ? (IProtoSerializer) ParseableSerializer.TryCreate(type, (TypeModel) model)
                            : (IProtoSerializer) null;
                        if (protoSerializer != null)
                        {
                            defaultWireType = WireType.String;
                            return protoSerializer;
                        }
                        else
                        {
                            if (allowComplexTypes && model != null)
                            {
                                int key = model.GetKey(type, false, true);
                                if (asReference || dynamicType)
                                {
                                    defaultWireType = WireType.String;
                                    BclHelpers.NetObjectOptions options = BclHelpers.NetObjectOptions.None;
                                    if (asReference)
                                        options |= BclHelpers.NetObjectOptions.AsReference;
                                    if (dynamicType)
                                        options |= BclHelpers.NetObjectOptions.DynamicType;
                                    if (key >= 0)
                                    {
                                        if (asReference && Helpers.IsValueType(type))
                                        {
                                            string str = "AsReference cannot be used with value-types";
                                            throw new InvalidOperationException(!(type.Name == "KeyValuePair`2")
                                                ? str + ": " + type.FullName
                                                : str + "; please see http://stackoverflow.com/q/14436606/");
                                        }
                                        else
                                        {
                                            MetaType metaType = model[type];
                                            if (asReference && metaType.IsAutoTuple)
                                                options |= BclHelpers.NetObjectOptions.LateSet;
                                            if (metaType.UseConstructor)
                                                options |= BclHelpers.NetObjectOptions.UseConstructor;
                                        }
                                    }
                                    return
                                        (IProtoSerializer)
                                            new NetObjectSerializer((TypeModel) model, type, key, options);
                                }
                                else if (key >= 0)
                                {
                                    defaultWireType = dataFormat == DataFormat.Group
                                        ? WireType.StartGroup
                                        : WireType.String;
                                    return
                                        (IProtoSerializer)
                                            new SubItemSerializer(type, key, (ISerializerProxy) model[type], true);
                                }
                            }
                            defaultWireType = WireType.None;
                            return (IProtoSerializer) null;
                        }
                }
            }
        }

        internal void SetName(string name)
        {
            this.ThrowIfFrozen();
            this.name = name;
        }

        private bool HasFlag(byte flag)
        {
            return ((int) this.flags & (int) flag) == (int) flag;
        }

        private void SetFlag(byte flag, bool value, bool throwIfFrozen)
        {
            if (throwIfFrozen && this.HasFlag(flag) != value)
                this.ThrowIfFrozen();
            if (value)
                this.flags |= flag;
            else
                this.flags = (byte) ((uint) this.flags & (uint) ~flag);
        }

        internal string GetSchemaTypeName(bool applyNetObjectProxy, ref bool requiresBclImport)
        {
            Type effectiveType = this.ItemType;
            if (effectiveType == (Type) null)
                effectiveType = this.MemberType;
            return this.model.GetSchemaTypeName(effectiveType, this.DataFormat, applyNetObjectProxy && this.asReference,
                applyNetObjectProxy && this.dynamicType, ref requiresBclImport);
        }

        internal class Comparer : IComparer, IComparer<ValueMember>
        {
            public static readonly ValueMember.Comparer Default = new ValueMember.Comparer();

            public int Compare(object x, object y)
            {
                return this.Compare(x as ValueMember, y as ValueMember);
            }

            public int Compare(ValueMember x, ValueMember y)
            {
                if (object.ReferenceEquals((object) x, (object) y))
                    return 0;
                if (x == null)
                    return -1;
                if (y == null)
                    return 1;
                else
                    return x.FieldNumber.CompareTo(y.FieldNumber);
            }
        }
    }
}