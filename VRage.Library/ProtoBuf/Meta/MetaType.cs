// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.MetaType
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProtoBuf.Meta
{
    public class MetaType : ISerializerProxy
    {
        internal static readonly Type ienumerable = typeof (IEnumerable);
        private readonly BasicList fields = new BasicList();
        private const byte OPTIONS_Pending = (byte) 1;
        private const byte OPTIONS_EnumPassThru = (byte) 2;
        private const byte OPTIONS_Frozen = (byte) 4;
        private const byte OPTIONS_PrivateOnApi = (byte) 8;
        private const byte OPTIONS_SkipConstructor = (byte) 16;
        private const byte OPTIONS_AsReferenceDefault = (byte) 32;
        private const byte OPTIONS_AutoTuple = (byte) 64;
        private const byte OPTIONS_IgnoreListHandling = (byte) 128;
        private MetaType baseType;
        private BasicList subTypes;
        private CallbackSet callbacks;
        private string name;
        private MethodInfo factory;
        private readonly RuntimeTypeModel model;
        private readonly Type type;
        private IProtoTypeSerializer serializer;
        private Type constructType;
        private Type surrogate;
        private volatile byte flags;

        IProtoSerializer ISerializerProxy.Serializer
        {
            get { return (IProtoSerializer) this.Serializer; }
        }

        public MetaType BaseType
        {
            get { return this.baseType; }
        }

        internal TypeModel Model
        {
            get { return (TypeModel) this.model; }
        }

        public bool IncludeSerializerMethod
        {
            get { return !this.HasFlag((byte) 8); }
            set { this.SetFlag((byte) 8, !value, true); }
        }

        public bool AsReferenceDefault
        {
            get { return this.HasFlag((byte) 32); }
            set { this.SetFlag((byte) 32, value, true); }
        }

        public bool HasCallbacks
        {
            get
            {
                if (this.callbacks != null)
                    return this.callbacks.NonTrivial;
                else
                    return false;
            }
        }

        public bool HasSubtypes
        {
            get
            {
                if (this.subTypes != null)
                    return this.subTypes.Count != 0;
                else
                    return false;
            }
        }

        public CallbackSet Callbacks
        {
            get
            {
                if (this.callbacks == null)
                    this.callbacks = new CallbackSet(this);
                return this.callbacks;
            }
        }

        private bool IsValueType
        {
            get { return this.type.IsValueType; }
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                this.ThrowIfFrozen();
                this.name = value;
            }
        }

        public Type Type
        {
            get { return this.type; }
        }

        internal IProtoTypeSerializer Serializer
        {
            get
            {
                if (this.serializer == null)
                {
                    int opaqueToken = 0;
                    try
                    {
                        this.model.TakeLock(ref opaqueToken);
                        if (this.serializer == null)
                        {
                            this.SetFlag((byte) 4, true, false);
                            this.serializer = this.BuildSerializer();
                            if (this.model.AutoCompile)
                                this.CompileInPlace();
                        }
                    }
                    finally
                    {
                        this.model.ReleaseLock(opaqueToken);
                    }
                }
                return this.serializer;
            }
        }

        internal bool IsList
        {
            get
            {
                return (this.IgnoreListHandling
                    ? (Type) null
                    : TypeModel.GetListItemType((TypeModel) this.model, this.type)) != (Type) null;
            }
        }

        public bool UseConstructor
        {
            get { return !this.HasFlag((byte) 16); }
            set { this.SetFlag((byte) 16, !value, true); }
        }

        public Type ConstructType
        {
            get { return this.constructType; }
            set
            {
                this.ThrowIfFrozen();
                this.constructType = value;
            }
        }

        public ValueMember this[int fieldNumber]
        {
            get
            {
                foreach (ValueMember valueMember in this.fields)
                {
                    if (valueMember.FieldNumber == fieldNumber)
                        return valueMember;
                }
                return (ValueMember) null;
            }
        }

        public ValueMember this[MemberInfo member]
        {
            get
            {
                if (member == (MemberInfo) null)
                    return (ValueMember) null;
                foreach (ValueMember valueMember in this.fields)
                {
                    if (valueMember.Member == member)
                        return valueMember;
                }
                return (ValueMember) null;
            }
        }

        public bool EnumPassthru
        {
            get { return this.HasFlag((byte) 2); }
            set { this.SetFlag((byte) 2, value, true); }
        }

        public bool IgnoreListHandling
        {
            get; set; }

        internal bool Pending
        {
            get { return this.HasFlag((byte) 1); }
            set { this.SetFlag((byte) 1, value, false); }
        }

        internal IEnumerable Fields
        {
            get { return (IEnumerable) this.fields; }
        }

        internal bool IsAutoTuple
        {
            get { return this.HasFlag((byte) 64); }
        }

        internal MetaType(RuntimeTypeModel model, Type type, MethodInfo factory)
        {
            this.factory = factory;
            if (model == null)
                throw new ArgumentNullException("model");
            if (type == (Type) null)
                throw new ArgumentNullException("type");
            WireType defaultWireType;
            if (
                ValueMember.TryGetCoreSerializer(model, DataFormat.Default, type, out defaultWireType, false, false,
                    false, false) != null)
                throw new ArgumentException(
                    "Data of this type has inbuilt behaviour, and cannot be added to a model in this way: " +
                    type.FullName);
            this.type = type;
            this.model = model;
            if (!Helpers.IsEnum(type))
                return;
            this.EnumPassthru = type.IsDefined(model.MapType(typeof (FlagsAttribute)), false);
        }

        public override string ToString()
        {
            return this.type.ToString();
        }

        private bool IsValidSubType(Type subType)
        {
            return this.type.IsAssignableFrom(subType);
        }

        public MetaType AddSubType(int fieldNumber, Type derivedType)
        {
            return this.AddSubType(fieldNumber, derivedType, DataFormat.Default);
        }

        public MetaType AddSubType(int fieldNumber, Type derivedType, DataFormat dataFormat)
        {
            if (derivedType == (Type) null)
                throw new ArgumentNullException("derivedType");
            if (fieldNumber < 1)
                throw new ArgumentOutOfRangeException("fieldNumber");
            if (!this.type.IsClass && !this.type.IsInterface || this.type.IsSealed)
                throw new InvalidOperationException("Sub-types can only be added to non-sealed classes");
            if (!this.IsValidSubType(derivedType))
                throw new ArgumentException(derivedType.Name + " is not a valid sub-type of " + this.type.Name,
                    "derivedType");
            MetaType derivedType1 = this.model[derivedType];
            this.ThrowIfFrozen();
            derivedType1.ThrowIfFrozen();
            SubType subType = new SubType(fieldNumber, derivedType1, dataFormat);
            this.ThrowIfFrozen();
            derivedType1.SetBaseType(this);
            if (this.subTypes == null)
                this.subTypes = new BasicList();
            this.subTypes.Add((object) subType);
            return this;
        }

        private void SetBaseType(MetaType baseType)
        {
            if (baseType == null)
                throw new ArgumentNullException("baseType");
            if (this.baseType == baseType)
                return;
            if (this.baseType != null)
                throw new InvalidOperationException("A type can only participate in one inheritance hierarchy");
            for (MetaType metaType = baseType; metaType != null; metaType = metaType.baseType)
            {
                if (object.ReferenceEquals((object) metaType, (object) this))
                    throw new InvalidOperationException("Cyclic inheritance is not allowed");
            }
            this.baseType = baseType;
        }

        public MetaType SetCallbacks(MethodInfo beforeSerialize, MethodInfo afterSerialize, MethodInfo beforeDeserialize,
            MethodInfo afterDeserialize)
        {
            CallbackSet callbacks = this.Callbacks;
            callbacks.BeforeSerialize = beforeSerialize;
            callbacks.AfterSerialize = afterSerialize;
            callbacks.BeforeDeserialize = beforeDeserialize;
            callbacks.AfterDeserialize = afterDeserialize;
            return this;
        }

        public MetaType SetCallbacks(string beforeSerialize, string afterSerialize, string beforeDeserialize,
            string afterDeserialize)
        {
            if (this.IsValueType)
                throw new InvalidOperationException();
            CallbackSet callbacks = this.Callbacks;
            callbacks.BeforeSerialize = this.ResolveMethod(beforeSerialize, true);
            callbacks.AfterSerialize = this.ResolveMethod(afterSerialize, true);
            callbacks.BeforeDeserialize = this.ResolveMethod(beforeDeserialize, true);
            callbacks.AfterDeserialize = this.ResolveMethod(afterDeserialize, true);
            return this;
        }

        internal string GetSchemaTypeName()
        {
            if (this.surrogate != (Type) null)
                return this.model[this.surrogate].GetSchemaTypeName();
            if (!Helpers.IsNullOrEmpty(this.name))
                return this.name;
            if (!this.type.IsGenericType)
                return this.type.Name;
            StringBuilder stringBuilder = new StringBuilder(this.type.Name);
            int num = this.type.Name.IndexOf('`');
            if (num >= 0)
                stringBuilder.Length = num;
            foreach (Type type1 in this.type.GetGenericArguments())
            {
                stringBuilder.Append('_');
                Type type2 = type1;
                MetaType metaType;
                if (((TypeModel) this.model).GetKey(ref type2) >= 0 && (metaType = this.model[type2]) != null &&
                    metaType.surrogate == (Type) null)
                    stringBuilder.Append(metaType.GetSchemaTypeName());
                else
                    stringBuilder.Append(type2.Name);
            }
            return ((object) stringBuilder).ToString();
        }

        public MetaType SetFactory(MethodInfo factory)
        {
            this.model.VerifyFactory(factory, this.type);
            this.ThrowIfFrozen();
            this.factory = factory;
            return this;
        }

        public MetaType SetFactory(string factory)
        {
            return this.SetFactory(this.ResolveMethod(factory, false));
        }

        private MethodInfo ResolveMethod(string name, bool instance)
        {
            if (Helpers.IsNullOrEmpty(name))
                return (MethodInfo) null;
            if (!instance)
                return Helpers.GetStaticMethod(this.type, name);
            else
                return Helpers.GetInstanceMethod(this.type, name);
        }

        protected internal void ThrowIfFrozen()
        {
            if (((int) this.flags & 4) != 0)
                throw new InvalidOperationException(
                    "The type cannot be changed once a serializer has been generated for " + this.type.FullName);
        }

        internal void Freeze()
        {
            this.flags |= (byte) 4;
        }

        private IProtoTypeSerializer BuildSerializer()
        {
            if (Helpers.IsEnum(this.type))
                return
                    (IProtoTypeSerializer)
                        new TagDecorator(1, WireType.Variant, false,
                            (IProtoSerializer) new EnumSerializer(this.type, this.GetEnumMap()));
            Type itemType = this.IgnoreListHandling
                ? (Type) null
                : TypeModel.GetListItemType((TypeModel) this.model, this.type);
            if (itemType != (Type) null)
            {
                if (this.surrogate != (Type) null)
                    throw new ArgumentException(
                        "Repeated data (a list, collection, etc) has inbuilt behaviour and cannot use a surrogate");
                if (this.subTypes != null && this.subTypes.Count != 0)
                    throw new ArgumentException(
                        "Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be subclassed");
                return (IProtoTypeSerializer) new TypeSerializer((TypeModel) this.model, this.type, new int[1]
                {
                    1
                }, new IProtoSerializer[1]
                {
                    new ValueMember(this.model, 1, this.type, itemType, this.type, DataFormat.Default).Serializer
                }, (MethodInfo[]) null, 1 != 0, 1 != 0, (CallbackSet) null, this.constructType, this.factory);
            }
            else if (this.surrogate != (Type) null)
            {
                MetaType metaType1 = this.model[this.surrogate];
                MetaType metaType2;
                while ((metaType2 = metaType1.baseType) != null)
                    metaType1 = metaType2;
                return (IProtoTypeSerializer) new SurrogateSerializer(this.type, this.surrogate, metaType1.Serializer);
            }
            else if (this.IsAutoTuple)
            {
                MemberInfo[] mappedMembers;
                ConstructorInfo ctor = MetaType.ResolveTupleConstructor(this.type, out mappedMembers);
                if (ctor == (ConstructorInfo) null)
                    throw new InvalidOperationException();
                else
                    return (IProtoTypeSerializer) new TupleSerializer(this.model, ctor, mappedMembers);
            }
            else
            {
                this.fields.Trim();
                int count = this.fields.Count;
                int num = this.subTypes == null ? 0 : this.subTypes.Count;
                int[] fieldNumbers = new int[count + num];
                IProtoSerializer[] serializers = new IProtoSerializer[count + num];
                int index = 0;
                if (num != 0)
                {
                    foreach (SubType subType in this.subTypes)
                    {
                        if (!subType.DerivedType.IgnoreListHandling &&
                            this.model.MapType(MetaType.ienumerable).IsAssignableFrom(subType.DerivedType.Type))
                            throw new ArgumentException(
                                "Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be used as a subclass");
                        fieldNumbers[index] = subType.FieldNumber;
                        serializers[index++] = subType.Serializer;
                    }
                }
                if (count != 0)
                {
                    foreach (ValueMember valueMember in this.fields)
                    {
                        fieldNumbers[index] = valueMember.FieldNumber;
                        serializers[index++] = valueMember.Serializer;
                    }
                }
                BasicList basicList = (BasicList) null;
                for (MetaType baseType = this.BaseType; baseType != null; baseType = baseType.BaseType)
                {
                    MethodInfo methodInfo = baseType.HasCallbacks
                        ? baseType.Callbacks.BeforeDeserialize
                        : (MethodInfo) null;
                    if (methodInfo != (MethodInfo) null)
                    {
                        if (basicList == null)
                            basicList = new BasicList();
                        basicList.Add((object) methodInfo);
                    }
                }
                MethodInfo[] baseCtorCallbacks = (MethodInfo[]) null;
                if (basicList != null)
                {
                    baseCtorCallbacks = new MethodInfo[basicList.Count];
                    basicList.CopyTo((Array) baseCtorCallbacks, 0);
                    Array.Reverse((Array) baseCtorCallbacks);
                }
                return
                    (IProtoTypeSerializer)
                        new TypeSerializer((TypeModel) this.model, this.type, fieldNumbers, serializers,
                            baseCtorCallbacks, this.baseType == null, this.UseConstructor, this.callbacks,
                            this.constructType, this.factory);
            }
        }

        private static Type GetBaseType(MetaType type)
        {
            return type.type.BaseType;
        }

        internal void ApplyDefaultBehaviour()
        {
            Type baseType = MetaType.GetBaseType(this);
            if (baseType != (Type) null && this.model.FindWithoutAdd(baseType) == null &&
                MetaType.GetContractFamily(this.model, baseType, (AttributeMap[]) null) != MetaType.AttributeFamily.None)
                this.model.FindOrAddAuto(baseType, true, false, false);
            AttributeMap[] attributes1 = AttributeMap.Create((TypeModel) this.model, this.type, false);
            MetaType.AttributeFamily contractFamily = MetaType.GetContractFamily(this.model, this.type, attributes1);
            if (contractFamily == MetaType.AttributeFamily.AutoTuple)
                this.SetFlag((byte) 64, true, true);
            bool isEnum = !this.EnumPassthru && Helpers.IsEnum(this.type);
            if (contractFamily == MetaType.AttributeFamily.None && !isEnum)
                return;
            BasicList basicList = (BasicList) null;
            BasicList partialMembers = (BasicList) null;
            int dataMemberOffset = 0;
            int num1 = 1;
            bool inferTagByName = this.model.InferTagFromNameDefault;
            ImplicitFields implicitMode = ImplicitFields.None;
            string str = (string) null;
            for (int index = 0; index < attributes1.Length; ++index)
            {
                AttributeMap attributeMap = attributes1[index];
                object obj;
                if (!isEnum && attributeMap.AttributeType.FullName == "ProtoBuf.ProtoIncludeAttribute")
                {
                    int fieldNumber = 0;
                    if (attributeMap.TryGet("tag", out obj))
                        fieldNumber = (int) obj;
                    DataFormat dataFormat = DataFormat.Default;
                    if (attributeMap.TryGet("DataFormat", out obj))
                        dataFormat = (DataFormat) obj;
                    Type type = (Type) null;
                    try
                    {
                        if (attributeMap.TryGet("knownTypeName", out obj))
                            type = this.model.GetType((string) obj, this.type.Assembly);
                        else if (attributeMap.TryGet("knownType", out obj))
                            type = (Type) obj;
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Unable to resolve sub-type of: " + this.type.FullName, ex);
                    }
                    if (type == (Type) null)
                        throw new InvalidOperationException("Unable to resolve sub-type of: " + this.type.FullName);
                    if (this.IsValidSubType(type))
                        this.AddSubType(fieldNumber, type, dataFormat);
                }
                if (attributeMap.AttributeType.FullName == "ProtoBuf.ProtoPartialIgnoreAttribute" &&
                    attributeMap.TryGet("MemberName", out obj) && obj != null)
                {
                    if (basicList == null)
                        basicList = new BasicList();
                    basicList.Add((object) (string) obj);
                }
                if (!isEnum && attributeMap.AttributeType.FullName == "ProtoBuf.ProtoPartialMemberAttribute")
                {
                    if (partialMembers == null)
                        partialMembers = new BasicList();
                    partialMembers.Add((object) attributeMap);
                }
                if (attributeMap.AttributeType.FullName == "ProtoBuf.ProtoContractAttribute")
                {
                    if (attributeMap.TryGet("Name", out obj))
                        str = (string) obj;
                    if (!isEnum)
                    {
                        if (attributeMap.TryGet("DataMemberOffset", out obj))
                            dataMemberOffset = (int) obj;
                        if (attributeMap.TryGet("InferTagFromNameHasValue", false, out obj) && (bool) obj &&
                            attributeMap.TryGet("InferTagFromName", out obj))
                            inferTagByName = (bool) obj;
                        if (attributeMap.TryGet("ImplicitFields", out obj) && obj != null)
                            implicitMode = (ImplicitFields) obj;
                        if (attributeMap.TryGet("SkipConstructor", out obj))
                            this.UseConstructor = !(bool) obj;
                        if (attributeMap.TryGet("IgnoreListHandling", out obj))
                            this.IgnoreListHandling = (bool) obj;
                        if (attributeMap.TryGet("AsReferenceDefault", out obj))
                            this.AsReferenceDefault = (bool) obj;
                        if (attributeMap.TryGet("ImplicitFirstTag", out obj) && (int) obj > 0)
                            num1 = (int) obj;
                    }
                }
                if (attributeMap.AttributeType.FullName == "System.Runtime.Serialization.DataContractAttribute" &&
                    str == null && attributeMap.TryGet("Name", out obj))
                    str = (string) obj;
                if (attributeMap.AttributeType.FullName == "System.Xml.Serialization.XmlTypeAttribute" && str == null &&
                    attributeMap.TryGet("TypeName", out obj))
                    str = (string) obj;
            }
            if (!Helpers.IsNullOrEmpty(str))
                this.Name = str;
            if (implicitMode != ImplicitFields.None)
                contractFamily &= MetaType.AttributeFamily.ProtoBuf;
            MethodInfo[] callbacks = (MethodInfo[]) null;
            BasicList members = new BasicList();
            foreach (
                MemberInfo member in
                    this.type.GetMembers(isEnum
                        ? BindingFlags.Static | BindingFlags.Public
                        : BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!(member.DeclaringType != this.type) &&
                    !member.IsDefined(this.model.MapType(typeof (ProtoIgnoreAttribute)), true) &&
                    (basicList == null || !basicList.Contains((object) member.Name)))
                {
                    bool forced = false;
                    PropertyInfo property;
                    Type effectiveType;
                    if ((property = member as PropertyInfo) != (PropertyInfo) null)
                    {
                        if (!isEnum)
                        {
                            effectiveType = property.PropertyType;
                            bool isPublic = Helpers.GetGetMethod(property, false, false) != (MethodInfo) null;
                            bool isField = false;
                            MetaType.ApplyDefaultBehaviour_AddMembers((TypeModel) this.model, contractFamily, isEnum,
                                partialMembers, dataMemberOffset, inferTagByName, implicitMode, members, member,
                                ref forced, isPublic, isField, ref effectiveType);
                        }
                    }
                    else
                    {
                        FieldInfo fieldInfo;
                        if ((fieldInfo = member as FieldInfo) != (FieldInfo) null)
                        {
                            effectiveType = fieldInfo.FieldType;
                            bool isPublic = fieldInfo.IsPublic;
                            bool isField = true;
                            if (!isEnum || fieldInfo.IsStatic)
                                MetaType.ApplyDefaultBehaviour_AddMembers((TypeModel) this.model, contractFamily, isEnum,
                                    partialMembers, dataMemberOffset, inferTagByName, implicitMode, members, member,
                                    ref forced, isPublic, isField, ref effectiveType);
                        }
                        else
                        {
                            MethodInfo method;
                            if ((method = member as MethodInfo) != (MethodInfo) null && !isEnum)
                            {
                                AttributeMap[] attributes2 = AttributeMap.Create((TypeModel) this.model,
                                    (MemberInfo) method, false);
                                if (attributes2 != null && attributes2.Length > 0)
                                {
                                    MetaType.CheckForCallback(method, attributes2,
                                        "ProtoBuf.ProtoBeforeSerializationAttribute", ref callbacks, 0);
                                    MetaType.CheckForCallback(method, attributes2,
                                        "ProtoBuf.ProtoAfterSerializationAttribute", ref callbacks, 1);
                                    MetaType.CheckForCallback(method, attributes2,
                                        "ProtoBuf.ProtoBeforeDeserializationAttribute", ref callbacks, 2);
                                    MetaType.CheckForCallback(method, attributes2,
                                        "ProtoBuf.ProtoAfterDeserializationAttribute", ref callbacks, 3);
                                    MetaType.CheckForCallback(method, attributes2,
                                        "System.Runtime.Serialization.OnSerializingAttribute", ref callbacks, 4);
                                    MetaType.CheckForCallback(method, attributes2,
                                        "System.Runtime.Serialization.OnSerializedAttribute", ref callbacks, 5);
                                    MetaType.CheckForCallback(method, attributes2,
                                        "System.Runtime.Serialization.OnDeserializingAttribute", ref callbacks, 6);
                                    MetaType.CheckForCallback(method, attributes2,
                                        "System.Runtime.Serialization.OnDeserializedAttribute", ref callbacks, 7);
                                }
                            }
                        }
                    }
                }
            }
            ProtoMemberAttribute[] array = new ProtoMemberAttribute[members.Count];
            members.CopyTo((Array) array, 0);
            if (inferTagByName || implicitMode != ImplicitFields.None)
            {
                Array.Sort<ProtoMemberAttribute>(array);
                int num2 = num1;
                foreach (ProtoMemberAttribute protoMemberAttribute in array)
                {
                    if (!protoMemberAttribute.TagIsPinned)
                        protoMemberAttribute.Rebase(num2++);
                }
            }
            foreach (ProtoMemberAttribute normalizedAttribute in array)
            {
                ValueMember member = this.ApplyDefaultBehaviour(isEnum, normalizedAttribute);
                if (member != null)
                    this.Add(member);
            }
            if (callbacks == null)
                return;
            this.SetCallbacks(MetaType.Coalesce(callbacks, 0, 4), MetaType.Coalesce(callbacks, 1, 5),
                MetaType.Coalesce(callbacks, 2, 6), MetaType.Coalesce(callbacks, 3, 7));
        }

        private static void ApplyDefaultBehaviour_AddMembers(TypeModel model, MetaType.AttributeFamily family,
            bool isEnum, BasicList partialMembers, int dataMemberOffset, bool inferTagByName,
            ImplicitFields implicitMode, BasicList members, MemberInfo member, ref bool forced, bool isPublic,
            bool isField, ref Type effectiveType)
        {
            switch (implicitMode)
            {
                case ImplicitFields.AllPublic:
                    if (isPublic)
                    {
                        forced = true;
                        break;
                    }
                    else
                        break;
                case ImplicitFields.AllFields:
                    if (isField)
                    {
                        forced = true;
                        break;
                    }
                    else
                        break;
            }
            if (effectiveType.IsSubclassOf(model.MapType(typeof (Delegate))))
                effectiveType = (Type) null;
            if (!(effectiveType != (Type) null))
                return;
            ProtoMemberAttribute protoMemberAttribute = MetaType.NormalizeProtoMember(model, member, family, forced,
                isEnum, partialMembers, dataMemberOffset, inferTagByName);
            if (protoMemberAttribute == null)
                return;
            members.Add((object) protoMemberAttribute);
        }

        private static MethodInfo Coalesce(MethodInfo[] arr, int x, int y)
        {
            MethodInfo methodInfo = arr[x];
            if (methodInfo == (MethodInfo) null)
                methodInfo = arr[y];
            return methodInfo;
        }

        internal static MetaType.AttributeFamily GetContractFamily(RuntimeTypeModel model, Type type,
            AttributeMap[] attributes)
        {
            MetaType.AttributeFamily attributeFamily = MetaType.AttributeFamily.None;
            if (attributes == null)
                attributes = AttributeMap.Create((TypeModel) model, type, false);
            for (int index = 0; index < attributes.Length; ++index)
            {
                switch (attributes[index].AttributeType.FullName)
                {
                    case "ProtoBuf.ProtoContractAttribute":
                        bool flag = false;
                        MetaType.GetFieldBoolean(ref flag, attributes[index], "UseProtoMembersOnly");
                        if (flag)
                            return MetaType.AttributeFamily.ProtoBuf;
                        attributeFamily |= MetaType.AttributeFamily.ProtoBuf;
                        break;
                    case "System.Xml.Serialization.XmlTypeAttribute":
                        if (!model.AutoAddProtoContractTypesOnly)
                        {
                            attributeFamily |= MetaType.AttributeFamily.XmlSerializer;
                            break;
                        }
                        else
                            break;
                    case "System.Runtime.Serialization.DataContractAttribute":
                        if (!model.AutoAddProtoContractTypesOnly)
                        {
                            attributeFamily |= MetaType.AttributeFamily.DataContractSerialier;
                            break;
                        }
                        else
                            break;
                }
            }
            MemberInfo[] mappedMembers;
            if (attributeFamily == MetaType.AttributeFamily.None &&
                MetaType.ResolveTupleConstructor(type, out mappedMembers) != (ConstructorInfo) null)
                attributeFamily |= MetaType.AttributeFamily.AutoTuple;
            return attributeFamily;
        }

        internal static ConstructorInfo ResolveTupleConstructor(Type type, out MemberInfo[] mappedMembers)
        {
            mappedMembers = (MemberInfo[]) null;
            if (type == (Type) null)
                throw new ArgumentNullException("type");
            if (type.IsAbstract)
                return (ConstructorInfo) null;
            ConstructorInfo[] constructors = Helpers.GetConstructors(type, false);
            if (constructors.Length == 0 || constructors.Length == 1 && constructors[0].GetParameters().Length == 0)
                return (ConstructorInfo) null;
            MemberInfo[] fieldsAndProperties = Helpers.GetInstanceFieldsAndProperties(type, true);
            BasicList basicList = new BasicList();
            for (int index = 0; index < fieldsAndProperties.Length; ++index)
            {
                PropertyInfo property = fieldsAndProperties[index] as PropertyInfo;
                if (property != (PropertyInfo) null)
                {
                    if (!property.CanRead)
                        return (ConstructorInfo) null;
                    if (property.CanWrite && Helpers.GetSetMethod(property, false, false) != (MethodInfo) null)
                        return (ConstructorInfo) null;
                    basicList.Add((object) property);
                }
                else
                {
                    FieldInfo fieldInfo = fieldsAndProperties[index] as FieldInfo;
                    if (fieldInfo != (FieldInfo) null)
                    {
                        if (!fieldInfo.IsInitOnly)
                            return (ConstructorInfo) null;
                        basicList.Add((object) fieldInfo);
                    }
                }
            }
            if (basicList.Count == 0)
                return (ConstructorInfo) null;
            MemberInfo[] memberInfoArray = new MemberInfo[basicList.Count];
            basicList.CopyTo((Array) memberInfoArray, 0);
            int[] numArray = new int[memberInfoArray.Length];
            int num = 0;
            ConstructorInfo constructorInfo = (ConstructorInfo) null;
            mappedMembers = new MemberInfo[numArray.Length];
            for (int index1 = 0; index1 < constructors.Length; ++index1)
            {
                ParameterInfo[] parameters = constructors[index1].GetParameters();
                if (parameters.Length == memberInfoArray.Length)
                {
                    for (int index2 = 0; index2 < numArray.Length; ++index2)
                        numArray[index2] = -1;
                    for (int index2 = 0; index2 < parameters.Length; ++index2)
                    {
                        string str = parameters[index2].Name.ToLower();
                        for (int index3 = 0; index3 < memberInfoArray.Length; ++index3)
                        {
                            if (!(memberInfoArray[index3].Name.ToLower() != str) &&
                                !(Helpers.GetMemberType(memberInfoArray[index3]) != parameters[index2].ParameterType))
                                numArray[index2] = index3;
                        }
                    }
                    bool flag = false;
                    for (int index2 = 0; index2 < numArray.Length; ++index2)
                    {
                        if (numArray[index2] < 0)
                        {
                            flag = true;
                            break;
                        }
                        else
                            mappedMembers[index2] = memberInfoArray[numArray[index2]];
                    }
                    if (!flag)
                    {
                        ++num;
                        constructorInfo = constructors[index1];
                    }
                }
            }
            if (num != 1)
                return (ConstructorInfo) null;
            else
                return constructorInfo;
        }

        private static void CheckForCallback(MethodInfo method, AttributeMap[] attributes, string callbackTypeName,
            ref MethodInfo[] callbacks, int index)
        {
            for (int index1 = 0; index1 < attributes.Length; ++index1)
            {
                if (attributes[index1].AttributeType.FullName == callbackTypeName)
                {
                    if (callbacks == null)
                        callbacks = new MethodInfo[8];
                    else if (callbacks[index] != (MethodInfo) null)
                    {
                        Type reflectedType = method.ReflectedType;
                        throw new ProtoException("Duplicate " + callbackTypeName + " callbacks on " +
                                                 reflectedType.FullName);
                    }
                    callbacks[index] = method;
                }
            }
        }

        private static bool HasFamily(MetaType.AttributeFamily value, MetaType.AttributeFamily required)
        {
            return (value & required) == required;
        }

        private static ProtoMemberAttribute NormalizeProtoMember(TypeModel model, MemberInfo member,
            MetaType.AttributeFamily family, bool forced, bool isEnum, BasicList partialMembers, int dataMemberOffset,
            bool inferByTagName)
        {
            if (member == (MemberInfo) null || family == MetaType.AttributeFamily.None && !isEnum)
                return (ProtoMemberAttribute) null;
            int tag = int.MinValue;
            int num1 = inferByTagName ? -1 : 1;
            string name = (string) null;
            bool flag1 = false;
            bool ignore = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            bool flag6 = false;
            bool flag7 = false;
            bool flag8 = false;
            DataFormat dataFormat = DataFormat.Default;
            if (isEnum)
                forced = true;
            AttributeMap[] attribs = AttributeMap.Create(model, member, true);
            if (isEnum)
            {
                if (MetaType.GetAttribute(attribs, "ProtoBuf.ProtoIgnoreAttribute") != null)
                {
                    ignore = true;
                }
                else
                {
                    AttributeMap attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoEnumAttribute");
                    tag = Convert.ToInt32(((FieldInfo) member).GetRawConstantValue());
                    if (attribute != null)
                    {
                        MetaType.GetFieldName(ref name, attribute, "Name");
                        object obj;
                        if (
                            (bool)
                                Helpers.GetInstanceMethod(attribute.AttributeType, "HasValue")
                                    .Invoke(attribute.Target, (object[]) null) && attribute.TryGet("Value", out obj))
                            tag = (int) obj;
                    }
                }
                flag2 = true;
            }
            if (!ignore && !flag2)
            {
                AttributeMap attribute = MetaType.GetAttribute(attribs, "ProtoBuf.ProtoMemberAttribute");
                MetaType.GetIgnore(ref ignore, attribute, attribs, "ProtoBuf.ProtoIgnoreAttribute");
                if (!ignore && attribute != null)
                {
                    MetaType.GetFieldNumber(ref tag, attribute, "Tag");
                    MetaType.GetFieldName(ref name, attribute, "Name");
                    MetaType.GetFieldBoolean(ref flag3, attribute, "IsRequired");
                    MetaType.GetFieldBoolean(ref flag1, attribute, "IsPacked");
                    MetaType.GetFieldBoolean(ref flag8, attribute, "OverwriteList");
                    MetaType.GetDataFormat(ref dataFormat, attribute, "DataFormat");
                    MetaType.GetFieldBoolean(ref flag5, attribute, "AsReferenceHasValue", false);
                    if (flag5)
                        flag5 = MetaType.GetFieldBoolean(ref flag4, attribute, "AsReference", true);
                    MetaType.GetFieldBoolean(ref flag6, attribute, "DynamicType");
                    flag2 = flag7 = tag > 0;
                }
                if (!flag2 && partialMembers != null)
                {
                    foreach (AttributeMap attrib in partialMembers)
                    {
                        object obj;
                        if (attrib.TryGet("MemberName", out obj) && (string) obj == member.Name)
                        {
                            MetaType.GetFieldNumber(ref tag, attrib, "Tag");
                            MetaType.GetFieldName(ref name, attrib, "Name");
                            MetaType.GetFieldBoolean(ref flag3, attrib, "IsRequired");
                            MetaType.GetFieldBoolean(ref flag1, attrib, "IsPacked");
                            MetaType.GetFieldBoolean(ref flag8, attribute, "OverwriteList");
                            MetaType.GetDataFormat(ref dataFormat, attrib, "DataFormat");
                            MetaType.GetFieldBoolean(ref flag5, attribute, "AsReferenceHasValue", false);
                            if (flag5)
                                flag5 = MetaType.GetFieldBoolean(ref flag4, attrib, "AsReference", true);
                            MetaType.GetFieldBoolean(ref flag6, attrib, "DynamicType");
                            int num2;
                            flag7 = (num2 = tag > 0 ? 1 : 0) != 0;
                            flag2 = num2 != 0;
                            if (num2 != 0)
                                break;
                        }
                    }
                }
            }
            if (!ignore && !flag2 && MetaType.HasFamily(family, MetaType.AttributeFamily.DataContractSerialier))
            {
                AttributeMap attribute = MetaType.GetAttribute(attribs,
                    "System.Runtime.Serialization.DataMemberAttribute");
                if (attribute != null)
                {
                    MetaType.GetFieldNumber(ref tag, attribute, "Order");
                    MetaType.GetFieldName(ref name, attribute, "Name");
                    MetaType.GetFieldBoolean(ref flag3, attribute, "IsRequired");
                    flag2 = tag >= num1;
                    if (flag2)
                        tag += dataMemberOffset;
                }
            }
            if (!ignore && !flag2 && MetaType.HasFamily(family, MetaType.AttributeFamily.XmlSerializer))
            {
                AttributeMap attrib = MetaType.GetAttribute(attribs, "System.Xml.Serialization.XmlElementAttribute") ??
                                      MetaType.GetAttribute(attribs, "System.Xml.Serialization.XmlArrayAttribute");
                MetaType.GetIgnore(ref ignore, attrib, attribs, "System.Xml.Serialization.XmlIgnoreAttribute");
                if (attrib != null && !ignore)
                {
                    MetaType.GetFieldNumber(ref tag, attrib, "Order");
                    MetaType.GetFieldName(ref name, attrib, "ElementName");
                    flag2 = tag >= num1;
                }
            }
            if (!ignore && !flag2 && MetaType.GetAttribute(attribs, "System.NonSerializedAttribute") != null)
                ignore = true;
            if (ignore || tag < num1 && !forced)
                return (ProtoMemberAttribute) null;
            return new ProtoMemberAttribute(tag, forced || inferByTagName)
            {
                AsReference = flag4,
                AsReferenceHasValue = flag5,
                DataFormat = dataFormat,
                DynamicType = flag6,
                IsPacked = flag1,
                OverwriteList = flag8,
                IsRequired = flag3,
                Name = Helpers.IsNullOrEmpty(name) ? member.Name : name,
                Member = member,
                TagIsPinned = flag7
            };
        }

        private ValueMember ApplyDefaultBehaviour(bool isEnum, ProtoMemberAttribute normalizedAttribute)
        {
            MemberInfo member;
            if (normalizedAttribute == null || (member = normalizedAttribute.Member) == (MemberInfo) null)
                return (ValueMember) null;
            Type memberType = Helpers.GetMemberType(member);
            Type itemType = (Type) null;
            Type defaultType = (Type) null;
            MetaType.ResolveListTypes((TypeModel) this.model, memberType, ref itemType, ref defaultType);
            if (itemType != (Type) null && this.model.FindOrAddAuto(memberType, false, true, false) >= 0 &&
                this.model[memberType].IgnoreListHandling)
            {
                itemType = (Type) null;
                defaultType = (Type) null;
            }
            AttributeMap[] attribs = AttributeMap.Create((TypeModel) this.model, member, true);
            object defaultValue = (object) null;
            if (this.model.UseImplicitZeroDefaults)
            {
                switch (Helpers.GetTypeCode(memberType))
                {
                    case ProtoTypeCode.Boolean:
                        defaultValue = (object) false;
                        break;
                    case ProtoTypeCode.Char:
                        defaultValue = (object) char.MinValue;
                        break;
                    case ProtoTypeCode.SByte:
                        defaultValue = (object) 0;
                        break;
                    case ProtoTypeCode.Byte:
                        defaultValue = (object) 0;
                        break;
                    case ProtoTypeCode.Int16:
                        defaultValue = (object) 0;
                        break;
                    case ProtoTypeCode.UInt16:
                        defaultValue = (object) 0;
                        break;
                    case ProtoTypeCode.Int32:
                        defaultValue = (object) 0;
                        break;
                    case ProtoTypeCode.UInt32:
                        defaultValue = (object) 0;
                        break;
                    case ProtoTypeCode.Int64:
                        defaultValue = (object) 0;
                        break;
                    case ProtoTypeCode.UInt64:
                        defaultValue = (object) 0;
                        break;
                    case ProtoTypeCode.Single:
                        defaultValue = (object) 0.0f;
                        break;
                    case ProtoTypeCode.Double:
                        defaultValue = (object) 0.0;
                        break;
                    case ProtoTypeCode.Decimal:
                        defaultValue = (object) new Decimal(0);
                        break;
                    case ProtoTypeCode.TimeSpan:
                        defaultValue = (object) TimeSpan.Zero;
                        break;
                    case ProtoTypeCode.Guid:
                        defaultValue = (object) Guid.Empty;
                        break;
                }
            }
            AttributeMap attribute;
            object obj;
            if ((attribute = MetaType.GetAttribute(attribs, "System.ComponentModel.DefaultValueAttribute")) != null &&
                attribute.TryGet("Value", out obj))
                defaultValue = obj;
            ValueMember valueMember = isEnum || normalizedAttribute.Tag > 0
                ? new ValueMember(this.model, this.type, normalizedAttribute.Tag, member, memberType, itemType,
                    defaultType, normalizedAttribute.DataFormat, defaultValue)
                : (ValueMember) null;
            if (valueMember != null)
            {
                Type type = this.type;
                PropertyInfo property = Helpers.GetProperty(type, member.Name + "Specified", true);
                MethodInfo getMethod = Helpers.GetGetMethod(property, true, true);
                if (getMethod == (MethodInfo) null || getMethod.IsStatic)
                    property = (PropertyInfo) null;
                if (property != (PropertyInfo) null)
                {
                    valueMember.SetSpecified(getMethod, Helpers.GetSetMethod(property, true, true));
                }
                else
                {
                    MethodInfo instanceMethod = Helpers.GetInstanceMethod(type, "ShouldSerialize" + member.Name,
                        Helpers.EmptyTypes);
                    if (instanceMethod != (MethodInfo) null &&
                        instanceMethod.ReturnType == this.model.MapType(typeof (bool)))
                        valueMember.SetSpecified(instanceMethod, (MethodInfo) null);
                }
                if (!Helpers.IsNullOrEmpty(normalizedAttribute.Name))
                    valueMember.SetName(normalizedAttribute.Name);
                valueMember.IsPacked = normalizedAttribute.IsPacked;
                valueMember.IsRequired = normalizedAttribute.IsRequired;
                valueMember.OverwriteList = normalizedAttribute.OverwriteList;
                if (normalizedAttribute.AsReferenceHasValue)
                    valueMember.AsReference = normalizedAttribute.AsReference;
                valueMember.DynamicType = normalizedAttribute.DynamicType;
            }
            return valueMember;
        }

        private static void GetDataFormat(ref DataFormat value, AttributeMap attrib, string memberName)
        {
            object obj;
            if (attrib == null || value != DataFormat.Default || (!attrib.TryGet(memberName, out obj) || obj == null))
                return;
            value = (DataFormat) obj;
        }

        private static void GetIgnore(ref bool ignore, AttributeMap attrib, AttributeMap[] attribs, string fullName)
        {
            if (ignore || attrib == null)
                return;
            ignore = MetaType.GetAttribute(attribs, fullName) != null;
        }

        private static void GetFieldBoolean(ref bool value, AttributeMap attrib, string memberName)
        {
            MetaType.GetFieldBoolean(ref value, attrib, memberName, true);
        }

        private static bool GetFieldBoolean(ref bool value, AttributeMap attrib, string memberName, bool publicOnly)
        {
            if (attrib == null)
                return false;
            if (value)
                return true;
            object obj;
            if (!attrib.TryGet(memberName, publicOnly, out obj) || obj == null)
                return false;
            value = (bool) obj;
            return true;
        }

        private static void GetFieldNumber(ref int value, AttributeMap attrib, string memberName)
        {
            object obj;
            if (attrib == null || value > 0 || (!attrib.TryGet(memberName, out obj) || obj == null))
                return;
            value = (int) obj;
        }

        private static void GetFieldName(ref string name, AttributeMap attrib, string memberName)
        {
            object obj;
            if (attrib == null || !Helpers.IsNullOrEmpty(name) || (!attrib.TryGet(memberName, out obj) || obj == null))
                return;
            name = (string) obj;
        }

        private static AttributeMap GetAttribute(AttributeMap[] attribs, string fullName)
        {
            for (int index = 0; index < attribs.Length; ++index)
            {
                AttributeMap attributeMap = attribs[index];
                if (attributeMap != null && attributeMap.AttributeType.FullName == fullName)
                    return attributeMap;
            }
            return (AttributeMap) null;
        }

        public MetaType Add(int fieldNumber, string memberName)
        {
            this.AddField(fieldNumber, memberName, (Type) null, (Type) null, (object) null);
            return this;
        }

        public ValueMember AddField(int fieldNumber, string memberName)
        {
            return this.AddField(fieldNumber, memberName, (Type) null, (Type) null, (object) null);
        }

        public MetaType Add(string memberName)
        {
            this.Add(this.GetNextFieldNumber(), memberName);
            return this;
        }

        public void SetSurrogate(Type surrogateType)
        {
            if (surrogateType == this.type)
                surrogateType = (Type) null;
            if (surrogateType != (Type) null && surrogateType != (Type) null &&
                Helpers.IsAssignableFrom(this.model.MapType(typeof (IEnumerable)), surrogateType))
                throw new ArgumentException(
                    "Repeated data (a list, collection, etc) has inbuilt behaviour and cannot be used as a surrogate");
            this.ThrowIfFrozen();
            this.surrogate = surrogateType;
        }

        internal MetaType GetSurrogateOrSelf()
        {
            if (this.surrogate != (Type) null)
                return this.model[this.surrogate];
            else
                return this;
        }

        internal MetaType GetSurrogateOrBaseOrSelf(bool deep)
        {
            if (this.surrogate != (Type) null)
                return this.model[this.surrogate];
            MetaType metaType1 = this.baseType;
            if (metaType1 == null)
                return this;
            if (!deep)
                return metaType1;
            MetaType metaType2;
            do
            {
                metaType2 = metaType1;
                metaType1 = metaType1.baseType;
            } while (metaType1 != null);
            return metaType2;
        }

        private int GetNextFieldNumber()
        {
            int num = 0;
            foreach (ValueMember valueMember in this.fields)
            {
                if (valueMember.FieldNumber > num)
                    num = valueMember.FieldNumber;
            }
            if (this.subTypes != null)
            {
                foreach (SubType subType in this.subTypes)
                {
                    if (subType.FieldNumber > num)
                        num = subType.FieldNumber;
                }
            }
            return num + 1;
        }

        public MetaType Add(params string[] memberNames)
        {
            int nextFieldNumber = this.GetNextFieldNumber();
            for (int index = 0; index < memberNames.Length; ++index)
                this.Add(nextFieldNumber++, memberNames[index]);
            return this;
        }

        public MetaType Add(int fieldNumber, string memberName, object defaultValue)
        {
            this.AddField(fieldNumber, memberName, (Type) null, (Type) null, defaultValue);
            return this;
        }

        public MetaType Add(int fieldNumber, string memberName, Type itemType, Type defaultType)
        {
            this.AddField(fieldNumber, memberName, itemType, defaultType, (object) null);
            return this;
        }

        public ValueMember AddField(int fieldNumber, string memberName, Type itemType, Type defaultType)
        {
            return this.AddField(fieldNumber, memberName, itemType, defaultType, (object) null);
        }

        private ValueMember AddField(int fieldNumber, string memberName, Type itemType, Type defaultType,
            object defaultValue)
        {
            MemberInfo member1 = (MemberInfo) null;
            MemberInfo[] member2 = this.type.GetMember(memberName,
                Helpers.IsEnum(this.type)
                    ? BindingFlags.Static | BindingFlags.Public
                    : BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (member2 != null && member2.Length == 1)
                member1 = member2[0];
            if (member1 == (MemberInfo) null)
                throw new ArgumentException("Unable to determine member: " + memberName, "memberName");
            Type type;
            switch (member1.MemberType)
            {
                case MemberTypes.Field:
                    type = ((FieldInfo) member1).FieldType;
                    break;
                case MemberTypes.Property:
                    type = ((PropertyInfo) member1).PropertyType;
                    break;
                default:
                    throw new NotSupportedException(((object) member1.MemberType).ToString());
            }
            MetaType.ResolveListTypes((TypeModel) this.model, type, ref itemType, ref defaultType);
            ValueMember member3 = new ValueMember(this.model, this.type, fieldNumber, member1, type, itemType,
                defaultType, DataFormat.Default, defaultValue);
            this.Add(member3);
            return member3;
        }

        internal static void ResolveListTypes(TypeModel model, Type type, ref Type itemType, ref Type defaultType)
        {
            if (type == (Type) null)
                return;
            if (type.IsArray)
            {
                if (type.GetArrayRank() != 1)
                    throw new NotSupportedException("Multi-dimension arrays are supported");
                itemType = type.GetElementType();
                defaultType = !(itemType == model.MapType(typeof (byte))) ? type : (itemType = (Type) null);
            }
            if (itemType == (Type) null)
                itemType = TypeModel.GetListItemType(model, type);
            if (itemType != (Type) null)
            {
                Type itemType1 = (Type) null;
                Type defaultType1 = (Type) null;
                MetaType.ResolveListTypes(model, itemType, ref itemType1, ref defaultType1);
                if (itemType1 != (Type) null)
                    throw TypeModel.CreateNestedListsNotSupported();
            }
            if (!(itemType != (Type) null) || !(defaultType == (Type) null))
                return;
            if (type.IsClass && !type.IsAbstract &&
                Helpers.GetConstructor(type, Helpers.EmptyTypes, true) != (ConstructorInfo) null)
                defaultType = type;
            if (defaultType == (Type) null && type.IsInterface)
            {
                Type[] genericArguments;
                if (type.IsGenericType && type.GetGenericTypeDefinition() == model.MapType(typeof (IDictionary<,>)) &&
                    itemType ==
                    model.MapType(typeof (KeyValuePair<,>))
                        .MakeGenericType(genericArguments = type.GetGenericArguments()))
                    defaultType = model.MapType(typeof (Dictionary<,>)).MakeGenericType(genericArguments);
                else
                    defaultType = model.MapType(typeof (List<>)).MakeGenericType(itemType);
            }
            if (!(defaultType != (Type) null) || Helpers.IsAssignableFrom(type, defaultType))
                return;
            defaultType = (Type) null;
        }

        private void Add(ValueMember member)
        {
            int opaqueToken = 0;
            try
            {
                this.model.TakeLock(ref opaqueToken);
                this.ThrowIfFrozen();
                this.fields.Add((object) member);
            }
            finally
            {
                this.model.ReleaseLock(opaqueToken);
            }
        }

        public ValueMember[] GetFields()
        {
            ValueMember[] array = new ValueMember[this.fields.Count];
            this.fields.CopyTo((Array) array, 0);
            Array.Sort<ValueMember>(array, (IComparer<ValueMember>) ValueMember.Comparer.Default);
            return array;
        }

        public SubType[] GetSubtypes()
        {
            if (this.subTypes == null || this.subTypes.Count == 0)
                return new SubType[0];
            SubType[] array = new SubType[this.subTypes.Count];
            this.subTypes.CopyTo((Array) array, 0);
            Array.Sort<SubType>(array, (IComparer<SubType>) SubType.Comparer.Default);
            return array;
        }

        public void CompileInPlace()
        {
            this.serializer = (IProtoTypeSerializer) CompiledSerializer.Wrap(this.Serializer, (TypeModel) this.model);
        }

        internal bool IsDefined(int fieldNumber)
        {
            foreach (ValueMember valueMember in this.fields)
            {
                if (valueMember.FieldNumber == fieldNumber)
                    return true;
            }
            return false;
        }

        internal int GetKey(bool demand, bool getBaseKey)
        {
            return this.model.GetKey(this.type, demand, getBaseKey);
        }

        internal EnumSerializer.EnumPair[] GetEnumMap()
        {
            if (this.HasFlag((byte) 2))
                return (EnumSerializer.EnumPair[]) null;
            EnumSerializer.EnumPair[] enumPairArray = new EnumSerializer.EnumPair[this.fields.Count];
            for (int index = 0; index < enumPairArray.Length; ++index)
            {
                ValueMember valueMember = (ValueMember) this.fields[index];
                int fieldNumber = valueMember.FieldNumber;
                object rawEnumValue = valueMember.GetRawEnumValue();
                enumPairArray[index] = new EnumSerializer.EnumPair(fieldNumber, rawEnumValue, valueMember.MemberType);
            }
            return enumPairArray;
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

        internal static MetaType GetRootType(MetaType source)
        {
            // removed because trying to fix the errors is pointless
            return default(MetaType);
        }

        internal bool IsPrepared()
        {
            return this.serializer is CompiledSerializer;
        }

        internal static StringBuilder NewLine(StringBuilder builder, int indent)
        {
            return Helpers.AppendLine(builder).Append(' ', indent*3);
        }

        internal void WriteSchema(StringBuilder builder, int indent, ref bool requiresBclImport)
        {
            if (this.surrogate != (Type) null)
                return;
            ValueMember[] array1 = new ValueMember[this.fields.Count];
            this.fields.CopyTo((Array) array1, 0);
            Array.Sort<ValueMember>(array1, (IComparer<ValueMember>) ValueMember.Comparer.Default);
            if (this.IsList)
            {
                string schemaTypeName =
                    this.model.GetSchemaTypeName(TypeModel.GetListItemType((TypeModel) this.model, this.type),
                        DataFormat.Default, false, false, ref requiresBclImport);
                MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
                MetaType.NewLine(builder, indent + 1).Append("repeated ").Append(schemaTypeName).Append(" items = 1;");
                MetaType.NewLine(builder, indent).Append('}');
            }
            else if (this.IsAutoTuple)
            {
                MemberInfo[] mappedMembers;
                if (!(MetaType.ResolveTupleConstructor(this.type, out mappedMembers) != (ConstructorInfo) null))
                    return;
                MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
                for (int index = 0; index < mappedMembers.Length; ++index)
                {
                    Type effectiveType;
                    if (mappedMembers[index] is PropertyInfo)
                    {
                        effectiveType = ((PropertyInfo) mappedMembers[index]).PropertyType;
                    }
                    else
                    {
                        if (!(mappedMembers[index] is FieldInfo))
                            throw new NotSupportedException("Unknown member type: " +
                                                            mappedMembers[index].GetType().Name);
                        effectiveType = ((FieldInfo) mappedMembers[index]).FieldType;
                    }
                    MetaType.NewLine(builder, indent + 1)
                        .Append("optional ")
                        .Append(
                            this.model.GetSchemaTypeName(effectiveType, DataFormat.Default, false, false,
                                ref requiresBclImport).Replace('.', '_'))
                        .Append(' ')
                        .Append(mappedMembers[index].Name)
                        .Append(" = ")
                        .Append(index + 1)
                        .Append(';');
                }
                MetaType.NewLine(builder, indent).Append('}');
            }
            else if (Helpers.IsEnum(this.type))
            {
                MetaType.NewLine(builder, indent).Append("enum ").Append(this.GetSchemaTypeName()).Append(" {");
                if (array1.Length == 0 && this.EnumPassthru)
                {
                    if (this.type.IsDefined(this.model.MapType(typeof (FlagsAttribute)), false))
                        MetaType.NewLine(builder, indent + 1).Append("// this is a composite/flags enumeration");
                    else
                        MetaType.NewLine(builder, indent + 1)
                            .Append("// this enumeration will be passed as a raw value");
                    foreach (FieldInfo fieldInfo in this.type.GetFields())
                    {
                        if (fieldInfo.IsStatic && fieldInfo.IsLiteral)
                        {
                            object rawConstantValue = fieldInfo.GetRawConstantValue();
                            MetaType.NewLine(builder, indent + 1)
                                .Append(fieldInfo.Name)
                                .Append(" = ")
                                .Append(rawConstantValue)
                                .Append(";");
                        }
                    }
                }
                else
                {
                    foreach (ValueMember valueMember in array1)
                        MetaType.NewLine(builder, indent + 1)
                            .Append(valueMember.Name)
                            .Append(" = ")
                            .Append(valueMember.FieldNumber)
                            .Append(';');
                }
                MetaType.NewLine(builder, indent).Append('}');
            }
            else
            {
                MetaType.NewLine(builder, indent).Append("message ").Append(this.GetSchemaTypeName()).Append(" {");
                foreach (ValueMember valueMember in array1)
                {
                    string str = valueMember.ItemType != (Type) null
                        ? "repeated"
                        : (valueMember.IsRequired ? "required" : "optional");
                    MetaType.NewLine(builder, indent + 1).Append(str).Append(' ');
                    if (valueMember.DataFormat == DataFormat.Group)
                        builder.Append("group ");
                    string schemaTypeName = valueMember.GetSchemaTypeName(true, ref requiresBclImport);
                    builder.Append(schemaTypeName)
                        .Append(" ")
                        .Append(valueMember.Name)
                        .Append(" = ")
                        .Append(valueMember.FieldNumber);
                    if (valueMember.DefaultValue != null)
                    {
                        if (valueMember.DefaultValue is string)
                            builder.Append(" [default = \"").Append(valueMember.DefaultValue).Append("\"]");
                        else if (valueMember.DefaultValue is bool)
                            builder.Append((bool) valueMember.DefaultValue ? " [default = true]" : " [default = false]");
                        else
                            builder.Append(" [default = ").Append(valueMember.DefaultValue).Append(']');
                    }
                    if (valueMember.ItemType != (Type) null && valueMember.IsPacked)
                        builder.Append(" [packed=true]");
                    builder.Append(';');
                    if (schemaTypeName == "bcl.NetObjectProxy" && valueMember.AsReference && !valueMember.DynamicType)
                        builder.Append(" // reference-tracked ")
                            .Append(valueMember.GetSchemaTypeName(false, ref requiresBclImport));
                }
                if (this.subTypes != null && this.subTypes.Count != 0)
                {
                    MetaType.NewLine(builder, indent + 1)
                        .Append("// the following represent sub-types; at most 1 should have a value");
                    SubType[] array2 = new SubType[this.subTypes.Count];
                    this.subTypes.CopyTo((Array) array2, 0);
                    Array.Sort<SubType>(array2, (IComparer<SubType>) SubType.Comparer.Default);
                    foreach (SubType subType in array2)
                    {
                        string schemaTypeName = subType.DerivedType.GetSchemaTypeName();
                        MetaType.NewLine(builder, indent + 1)
                            .Append("optional ")
                            .Append(schemaTypeName)
                            .Append(" ")
                            .Append(schemaTypeName)
                            .Append(" = ")
                            .Append(subType.FieldNumber)
                            .Append(';');
                    }
                }
                MetaType.NewLine(builder, indent).Append('}');
            }
        }

        internal class Comparer : IComparer, IComparer<MetaType>
        {
            public static readonly MetaType.Comparer Default = new MetaType.Comparer();

            public int Compare(object x, object y)
            {
                return this.Compare(x as MetaType, y as MetaType);
            }

            public int Compare(MetaType x, MetaType y)
            {
                if (object.ReferenceEquals((object) x, (object) y))
                    return 0;
                if (x == null)
                    return -1;
                if (y == null)
                    return 1;
                else
                    return string.Compare(x.GetSchemaTypeName(), y.GetSchemaTypeName(), StringComparison.Ordinal);
            }
        }

        [Flags]
        internal enum AttributeFamily
        {
            None = 0,
            ProtoBuf = 1,
            DataContractSerialier = 2,
            XmlSerializer = 4,
            AutoTuple = 8,
        }
    }
}