// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.TypeModel
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace ProtoBuf.Meta
{
    public abstract class TypeModel
    {
        private static readonly Type ilist = typeof (IList);

        public event TypeFormatEventHandler DynamicTypeFormatting;

        protected internal Type MapType(Type type)
        {
            return this.MapType(type, true);
        }

        protected internal virtual Type MapType(Type type, bool demand)
        {
            return type;
        }

        private WireType GetWireType(ProtoTypeCode code, DataFormat format, ref Type type, out int modelKey)
        {
            modelKey = -1;
            if (Helpers.IsEnum(type))
            {
                modelKey = this.GetKey(ref type);
                return WireType.Variant;
            }
            else
            {
                switch (code)
                {
                    case ProtoTypeCode.Boolean:
                    case ProtoTypeCode.Char:
                    case ProtoTypeCode.SByte:
                    case ProtoTypeCode.Byte:
                    case ProtoTypeCode.Int16:
                    case ProtoTypeCode.UInt16:
                    case ProtoTypeCode.Int32:
                    case ProtoTypeCode.UInt32:
                        return format != DataFormat.FixedSize ? WireType.Variant : WireType.Fixed32;
                    case ProtoTypeCode.Int64:
                    case ProtoTypeCode.UInt64:
                        return format != DataFormat.FixedSize ? WireType.Variant : WireType.Fixed64;
                    case ProtoTypeCode.Single:
                        return WireType.Fixed32;
                    case ProtoTypeCode.Double:
                        return WireType.Fixed64;
                    case ProtoTypeCode.Decimal:
                    case ProtoTypeCode.DateTime:
                    case ProtoTypeCode.String:
                    case ProtoTypeCode.TimeSpan:
                    case ProtoTypeCode.ByteArray:
                    case ProtoTypeCode.Guid:
                    case ProtoTypeCode.Uri:
                        return WireType.String;
                    default:
                        return (modelKey = this.GetKey(ref type)) >= 0 ? WireType.String : WireType.None;
                }
            }
        }

        internal bool TrySerializeAuxiliaryType(ProtoWriter writer, Type type, DataFormat format, int tag, object value,
            bool isInsideList)
        {
            if (type == (Type) null)
                type = value.GetType();
            ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
            int modelKey;
            WireType wireType = this.GetWireType(typeCode, format, ref type, out modelKey);
            if (modelKey >= 0)
            {
                if (Helpers.IsEnum(type))
                {
                    this.Serialize(modelKey, value, writer);
                    return true;
                }
                else
                {
                    ProtoWriter.WriteFieldHeader(tag, wireType, writer);
                    switch (wireType)
                    {
                        case WireType.None:
                            throw ProtoWriter.CreateException(writer);
                        case WireType.String:
                        case WireType.StartGroup:
                            SubItemToken token = ProtoWriter.StartSubItem(value, writer);
                            this.Serialize(modelKey, value, writer);
                            ProtoWriter.EndSubItem(token, writer);
                            return true;
                        default:
                            this.Serialize(modelKey, value, writer);
                            return true;
                    }
                }
            }
            else
            {
                if (wireType != WireType.None)
                    ProtoWriter.WriteFieldHeader(tag, wireType, writer);
                switch (typeCode)
                {
                    case ProtoTypeCode.Boolean:
                        ProtoWriter.WriteBoolean((bool) value, writer);
                        return true;
                    case ProtoTypeCode.Char:
                        ProtoWriter.WriteUInt16((ushort) (char) value, writer);
                        return true;
                    case ProtoTypeCode.SByte:
                        ProtoWriter.WriteSByte((sbyte) value, writer);
                        return true;
                    case ProtoTypeCode.Byte:
                        ProtoWriter.WriteByte((byte) value, writer);
                        return true;
                    case ProtoTypeCode.Int16:
                        ProtoWriter.WriteInt16((short) value, writer);
                        return true;
                    case ProtoTypeCode.UInt16:
                        ProtoWriter.WriteUInt16((ushort) value, writer);
                        return true;
                    case ProtoTypeCode.Int32:
                        ProtoWriter.WriteInt32((int) value, writer);
                        return true;
                    case ProtoTypeCode.UInt32:
                        ProtoWriter.WriteUInt32((uint) value, writer);
                        return true;
                    case ProtoTypeCode.Int64:
                        ProtoWriter.WriteInt64((long) value, writer);
                        return true;
                    case ProtoTypeCode.UInt64:
                        ProtoWriter.WriteUInt64((ulong) value, writer);
                        return true;
                    case ProtoTypeCode.Single:
                        ProtoWriter.WriteSingle((float) value, writer);
                        return true;
                    case ProtoTypeCode.Double:
                        ProtoWriter.WriteDouble((double) value, writer);
                        return true;
                    case ProtoTypeCode.Decimal:
                        BclHelpers.WriteDecimal((Decimal) value, writer);
                        return true;
                    case ProtoTypeCode.DateTime:
                        BclHelpers.WriteDateTime((DateTime) value, writer);
                        return true;
                    case ProtoTypeCode.String:
                        ProtoWriter.WriteString((string) value, writer);
                        return true;
                    case ProtoTypeCode.TimeSpan:
                        BclHelpers.WriteTimeSpan((TimeSpan) value, writer);
                        return true;
                    case ProtoTypeCode.ByteArray:
                        ProtoWriter.WriteBytes((byte[]) value, writer);
                        return true;
                    case ProtoTypeCode.Guid:
                        BclHelpers.WriteGuid((Guid) value, writer);
                        return true;
                    case ProtoTypeCode.Uri:
                        ProtoWriter.WriteString(((Uri) value).AbsoluteUri, writer);
                        return true;
                    default:
                        IEnumerable enumerable = value as IEnumerable;
                        if (enumerable == null)
                            return false;
                        if (isInsideList)
                            throw TypeModel.CreateNestedListsNotSupported();
                        foreach (object obj in enumerable)
                        {
                            if (obj == null)
                                throw new NullReferenceException();
                            if (!this.TrySerializeAuxiliaryType(writer, (Type) null, format, tag, obj, true))
                                TypeModel.ThrowUnexpectedType(obj.GetType());
                        }
                        return true;
                }
            }
        }

        private void SerializeCore(ProtoWriter writer, object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            Type type = value.GetType();
            int key = this.GetKey(ref type);
            if (key >= 0)
            {
                this.Serialize(key, value, writer);
            }
            else
            {
                if (this.TrySerializeAuxiliaryType(writer, type, DataFormat.Default, 1, value, false))
                    return;
                TypeModel.ThrowUnexpectedType(type);
            }
        }

        public void Serialize(Stream dest, object value)
        {
            this.Serialize(dest, value, (SerializationContext) null);
        }

        public void Serialize(Stream dest, object value, SerializationContext context)
        {
            using (ProtoWriter writer = new ProtoWriter(dest, this, context))
            {
                writer.SetRootObject(value);
                this.SerializeCore(writer, value);
                writer.Close();
            }
        }

        public void Serialize(ProtoWriter dest, object value)
        {
            dest.CheckDepthFlushlock();
            dest.SetRootObject(value);
            this.SerializeCore(dest, value);
            dest.CheckDepthFlushlock();
            ProtoWriter.Flush(dest);
        }

        public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style,
            int fieldNumber)
        {
            int bytesRead;
            return this.DeserializeWithLengthPrefix(source, value, type, style, fieldNumber,
                (Serializer.TypeResolver) null, out bytesRead);
        }

        public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style,
            int expectedField, Serializer.TypeResolver resolver)
        {
            int bytesRead;
            return this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out bytesRead);
        }

        public object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style,
            int expectedField, Serializer.TypeResolver resolver, out int bytesRead)
        {
            bool haveObject;
            return this.DeserializeWithLengthPrefix(source, value, type, style, expectedField, resolver, out bytesRead,
                out haveObject, (SerializationContext) null);
        }

        private object DeserializeWithLengthPrefix(Stream source, object value, Type type, PrefixStyle style,
            int expectedField, Serializer.TypeResolver resolver, out int bytesRead, out bool haveObject,
            SerializationContext context)
        {
            haveObject = false;
            bytesRead = 0;
            if (type == (Type) null && (style != PrefixStyle.Base128 || resolver == null))
                throw new InvalidOperationException(
                    "A type must be provided unless base-128 prefixing is being used in combination with a resolver");
            int num;
            bool flag;
            do
            {
                bool expectHeader = expectedField > 0 || resolver != null;
                int fieldNumber;
                int bytesRead1;
                num = ProtoReader.ReadLengthPrefix(source, expectHeader, style, out fieldNumber, out bytesRead1);
                if (bytesRead1 == 0)
                    return value;
                bytesRead += bytesRead1;
                if (num < 0)
                    return value;
                if (style == PrefixStyle.Base128)
                {
                    if (expectHeader && expectedField == 0 && (type == (Type) null && resolver != null))
                    {
                        type = resolver(fieldNumber);
                        flag = type == (Type) null;
                    }
                    else
                        flag = expectedField != fieldNumber;
                }
                else
                    flag = false;
                if (flag)
                {
                    if (num == int.MaxValue)
                        throw new InvalidOperationException();
                    ProtoReader.Seek(source, num, (byte[]) null);
                    bytesRead += num;
                }
            } while (flag);
            using (ProtoReader protoReader = new ProtoReader(source, this, context, num))
            {
                int key = this.GetKey(ref type);
                if (key >= 0)
                    value = this.Deserialize(key, value, protoReader);
                else if (
                    !this.TryDeserializeAuxiliaryType(protoReader, DataFormat.Default, 1, type, ref value, true,
                        false, true, false) && num != 0)
                    TypeModel.ThrowUnexpectedType(type);
                bytesRead += protoReader.Position;
                haveObject = true;
                return value;
            }
        }

        public IEnumerable DeserializeItems(Stream source, Type type, PrefixStyle style, int expectedField,
            Serializer.TypeResolver resolver)
        {
            return this.DeserializeItems(source, type, style, expectedField, resolver, (SerializationContext) null);
        }

        public IEnumerable DeserializeItems(Stream source, Type type, PrefixStyle style, int expectedField,
            Serializer.TypeResolver resolver, SerializationContext context)
        {
            return
                (IEnumerable)
                    new TypeModel.DeserializeItemsIterator(this, source, type, style, expectedField, resolver, context);
        }

        public IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int expectedField)
        {
            return this.DeserializeItems<T>(source, style, expectedField, (SerializationContext) null);
        }

        public IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int expectedField,
            SerializationContext context)
        {
            return
                (IEnumerable<T>) new TypeModel.DeserializeItemsIterator<T>(this, source, style, expectedField, context);
        }

        public void SerializeWithLengthPrefix(Stream dest, object value, Type type, PrefixStyle style, int fieldNumber)
        {
            this.SerializeWithLengthPrefix(dest, value, type, style, fieldNumber, (SerializationContext) null);
        }

        public void SerializeWithLengthPrefix(Stream dest, object value, Type type, PrefixStyle style, int fieldNumber,
            SerializationContext context)
        {
            if (type == (Type) null)
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                type = this.MapType(value.GetType());
            }
            int key = this.GetKey(ref type);
            using (ProtoWriter protoWriter = new ProtoWriter(dest, this, context))
            {
                switch (style)
                {
                    case PrefixStyle.None:
                        this.Serialize(key, value, protoWriter);
                        break;
                    case PrefixStyle.Base128:
                    case PrefixStyle.Fixed32:
                    case PrefixStyle.Fixed32BigEndian:
                        ProtoWriter.WriteObject(value, key, protoWriter, style, fieldNumber);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("style");
                }
                protoWriter.Close();
            }
        }

        public object Deserialize(Stream source, object value, Type type)
        {
            return this.Deserialize(source, value, type, (SerializationContext) null);
        }

        public object Deserialize(Stream source, object value, Type type, SerializationContext context)
        {
            bool noAutoCreate = this.PrepareDeserialize(value, ref type);
            using (ProtoReader reader = new ProtoReader(source, this, context))
            {
                if (value != null)
                    reader.SetRootObject(value);
                object obj = this.DeserializeCore(reader, type, value, noAutoCreate);
                reader.CheckFullyConsumed();
                return obj;
            }
        }

        private bool PrepareDeserialize(object value, ref Type type)
        {
            if (type == (Type) null)
            {
                if (value == null)
                    throw new ArgumentNullException("type");
                type = this.MapType(value.GetType());
            }
            bool flag = true;
            Type underlyingType = Helpers.GetUnderlyingType(type);
            if (underlyingType != (Type) null)
            {
                type = underlyingType;
                flag = false;
            }
            return flag;
        }

        public object Deserialize(Stream source, object value, Type type, int length)
        {
            return this.Deserialize(source, value, type, length, (SerializationContext) null);
        }

        public object Deserialize(Stream source, object value, Type type, int length, SerializationContext context)
        {
            bool noAutoCreate = this.PrepareDeserialize(value, ref type);
            using (ProtoReader reader = new ProtoReader(source, this, context, length))
            {
                if (value != null)
                    reader.SetRootObject(value);
                object obj = this.DeserializeCore(reader, type, value, noAutoCreate);
                reader.CheckFullyConsumed();
                return obj;
            }
        }

        public object Deserialize(ProtoReader source, object value, Type type)
        {
            bool noAutoCreate = this.PrepareDeserialize(value, ref type);
            if (value != null)
                source.SetRootObject(value);
            object obj = this.DeserializeCore(source, type, value, noAutoCreate);
            source.CheckFullyConsumed();
            return obj;
        }

        private object DeserializeCore(ProtoReader reader, Type type, object value, bool noAutoCreate)
        {
            int key = this.GetKey(ref type);
            if (key >= 0 && !Helpers.IsEnum(type))
                return this.Deserialize(key, value, reader);
            this.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false, noAutoCreate,
                false);
            return value;
        }

        internal static MethodInfo ResolveListAdd(TypeModel model, Type listType, Type itemType, out bool isList)
        {
            Type type = listType;
            isList = model.MapType(TypeModel.ilist).IsAssignableFrom(type);
            Type[] types = new Type[1]
            {
                itemType
            };
            MethodInfo instanceMethod = Helpers.GetInstanceMethod(type, "Add", types);
            if (instanceMethod == (MethodInfo) null)
            {
                Type declaringType = model.MapType(typeof (ICollection<>)).MakeGenericType(types);
                if (declaringType.IsAssignableFrom(type))
                    instanceMethod = Helpers.GetInstanceMethod(declaringType, "Add", types);
            }
            if (instanceMethod == (MethodInfo) null)
            {
                foreach (Type declaringType in type.GetInterfaces())
                {
                    if (declaringType.Name == "IProducerConsumerCollection`1" && declaringType.IsGenericType &&
                        declaringType.GetGenericTypeDefinition().FullName ==
                        "System.Collections.Concurrent.IProducerConsumerCollection`1")
                    {
                        instanceMethod = Helpers.GetInstanceMethod(declaringType, "TryAdd", types);
                        if (instanceMethod != (MethodInfo) null)
                            break;
                    }
                }
            }
            if (instanceMethod == (MethodInfo) null)
            {
                types[0] = model.MapType(typeof (object));
                instanceMethod = Helpers.GetInstanceMethod(type, "Add", types);
            }
            if (instanceMethod == (MethodInfo) null && isList)
                instanceMethod = Helpers.GetInstanceMethod(model.MapType(TypeModel.ilist), "Add", types);
            return instanceMethod;
        }

        internal static Type GetListItemType(TypeModel model, Type listType)
        {
            if (listType == model.MapType(typeof (string)) || listType.IsArray ||
                !model.MapType(typeof (IEnumerable)).IsAssignableFrom(listType))
                return (Type) null;
            BasicList basicList = new BasicList();
            foreach (MethodInfo methodInfo in listType.GetMethods())
            {
                if (!methodInfo.IsStatic && !(methodInfo.Name != "Add"))
                {
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    if (parameters.Length == 1 && !basicList.Contains((object) parameters[0].ParameterType))
                        basicList.Add((object) parameters[0].ParameterType);
                }
            }
            string name = listType.Name;
            if (name == null || !name.Contains("Queue") && !name.Contains("Stack"))
            {
                foreach (Type type in listType.GetInterfaces())
                {
                    if (type.IsGenericType)
                    {
                        Type genericTypeDefinition = type.GetGenericTypeDefinition();
                        if (genericTypeDefinition == model.MapType(typeof (ICollection<>)) ||
                            genericTypeDefinition.FullName ==
                            "System.Collections.Concurrent.IProducerConsumerCollection`1")
                        {
                            Type[] genericArguments = type.GetGenericArguments();
                            if (!basicList.Contains((object) genericArguments[0]))
                                basicList.Add((object) genericArguments[0]);
                        }
                    }
                }
            }
            foreach (
                PropertyInfo propertyInfo in
                    listType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!(propertyInfo.Name != "Item") && !basicList.Contains((object) propertyInfo.PropertyType))
                {
                    ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
                    if (indexParameters.Length == 1 &&
                        !(indexParameters[0].ParameterType != model.MapType(typeof (int))))
                        basicList.Add((object) propertyInfo.PropertyType);
                }
            }
            switch (basicList.Count)
            {
                case 0:
                    return (Type) null;
                case 1:
                    return (Type) basicList[0];
                case 2:
                    if (TypeModel.CheckDictionaryAccessors(model, (Type) basicList[0], (Type) basicList[1]))
                        return (Type) basicList[0];
                    if (TypeModel.CheckDictionaryAccessors(model, (Type) basicList[1], (Type) basicList[0]))
                        return (Type) basicList[1];
                    else
                        break;
            }
            return (Type) null;
        }

        private static bool CheckDictionaryAccessors(TypeModel model, Type pair, Type value)
        {
            if (pair.IsGenericType && pair.GetGenericTypeDefinition() == model.MapType(typeof (KeyValuePair<,>)))
                return pair.GetGenericArguments()[1] == value;
            else
                return false;
        }

        private bool TryDeserializeList(TypeModel model, ProtoReader reader, DataFormat format, int tag, Type listType,
            Type itemType, ref object value)
        {
            bool isList;
            MethodInfo methodInfo = TypeModel.ResolveListAdd(model, listType, itemType, out isList);
            if (methodInfo == (MethodInfo) null)
                throw new NotSupportedException("Unknown list variant: " + listType.FullName);
            bool flag = false;
            object obj = (object) null;
            IList list = value as IList;
            object[] parameters = isList ? (object[]) null : new object[1];
            BasicList basicList = listType.IsArray ? new BasicList() : (BasicList) null;
            for (;
                this.TryDeserializeAuxiliaryType(reader, format, tag, itemType, ref obj, true, true, true, true);
                obj = (object) null)
            {
                flag = true;
                if (value == null && basicList == null)
                {
                    value = TypeModel.CreateListInstance(listType, itemType);
                    list = value as IList;
                }
                if (list != null)
                    list.Add(obj);
                else if (basicList != null)
                {
                    basicList.Add(obj);
                }
                else
                {
                    parameters[0] = obj;
                    methodInfo.Invoke(value, parameters);
                }
            }
            if (basicList != null)
            {
                if (value != null)
                {
                    if (basicList.Count != 0)
                    {
                        Array sourceArray = (Array) value;
                        Array instance = Array.CreateInstance(itemType, sourceArray.Length + basicList.Count);
                        Array.Copy(sourceArray, instance, sourceArray.Length);
                        basicList.CopyTo(instance, sourceArray.Length);
                        value = (object) instance;
                    }
                }
                else
                {
                    Array instance = Array.CreateInstance(itemType, basicList.Count);
                    basicList.CopyTo(instance, 0);
                    value = (object) instance;
                }
            }
            return flag;
        }

        private static object CreateListInstance(Type listType, Type itemType)
        {
            Type type = listType;
            if (listType.IsArray)
                return (object) Array.CreateInstance(itemType, 0);
            if (!listType.IsClass || listType.IsAbstract ||
                Helpers.GetConstructor(listType, Helpers.EmptyTypes, true) == (ConstructorInfo) null)
            {
                bool flag = false;
                string fullName;
                if (listType.IsInterface && (fullName = listType.FullName) != null &&
                    fullName.IndexOf("Dictionary") >= 0)
                {
                    if (listType.IsGenericType && listType.GetGenericTypeDefinition() == typeof (IDictionary<,>))
                    {
                        type = typeof (Dictionary<,>).MakeGenericType(listType.GetGenericArguments());
                        flag = true;
                    }
                    if (!flag && listType == typeof (IDictionary))
                    {
                        type = typeof (Hashtable);
                        flag = true;
                    }
                }
                if (!flag)
                {
                    type = typeof (List<>).MakeGenericType(itemType);
                    flag = true;
                }
                if (!flag)
                    type = typeof (ArrayList);
            }
            return Activator.CreateInstance(type);
        }

        internal bool TryDeserializeAuxiliaryType(ProtoReader reader, DataFormat format, int tag, Type type,
            ref object value, bool skipOtherFields, bool asListItem, bool autoCreate, bool insideList)
        {
            if (type == (Type) null)
                throw new ArgumentNullException("type");
            ProtoTypeCode typeCode = Helpers.GetTypeCode(type);
            int modelKey;
            WireType wireType = this.GetWireType(typeCode, format, ref type, out modelKey);
            bool flag1 = false;
            if (wireType == WireType.None)
            {
                Type itemType = TypeModel.GetListItemType(this, type);
                if (itemType == (Type) null && type.IsArray && (type.GetArrayRank() == 1 && type != typeof (byte[])))
                    itemType = type.GetElementType();
                if (itemType != (Type) null)
                {
                    if (insideList)
                        throw TypeModel.CreateNestedListsNotSupported();
                    bool flag2 = this.TryDeserializeList(this, reader, format, tag, type, itemType, ref value);
                    if (!flag2 && autoCreate)
                        value = TypeModel.CreateListInstance(type, itemType);
                    return flag2;
                }
                else
                    TypeModel.ThrowUnexpectedType(type);
            }
            while (!flag1 || !asListItem)
            {
                int num = reader.ReadFieldHeader();
                if (num > 0)
                {
                    if (num != tag)
                    {
                        if (skipOtherFields)
                            reader.SkipField();
                        else
                            throw ProtoReader.AddErrorData(
                                (Exception) new InvalidOperationException(string.Concat(new object[4]
                                {
                                    (object) "Expected field ",
                                    (object) tag,
                                    (object) ", but found ",
                                    (object) num
                                })), reader);
                    }
                    else
                    {
                        flag1 = true;
                        reader.Hint(wireType);
                        if (modelKey >= 0)
                        {
                            switch (wireType)
                            {
                                case WireType.String:
                                case WireType.StartGroup:
                                    SubItemToken token = ProtoReader.StartSubItem(reader);
                                    value = this.Deserialize(modelKey, value, reader);
                                    ProtoReader.EndSubItem(token, reader);
                                    continue;
                                default:
                                    value = this.Deserialize(modelKey, value, reader);
                                    continue;
                            }
                        }
                        else
                        {
                            switch (typeCode)
                            {
                                case ProtoTypeCode.Boolean:
                                    value = (object) (reader.ReadBoolean() ? 1 : 0);
                                    continue;
                                case ProtoTypeCode.Char:
                                    value = (object) (char) reader.ReadUInt16();
                                    continue;
                                case ProtoTypeCode.SByte:
                                    value = (object) reader.ReadSByte();
                                    continue;
                                case ProtoTypeCode.Byte:
                                    value = (object) reader.ReadByte();
                                    continue;
                                case ProtoTypeCode.Int16:
                                    value = (object) reader.ReadInt16();
                                    continue;
                                case ProtoTypeCode.UInt16:
                                    value = (object) reader.ReadUInt16();
                                    continue;
                                case ProtoTypeCode.Int32:
                                    value = (object) reader.ReadInt32();
                                    continue;
                                case ProtoTypeCode.UInt32:
                                    value = (object) reader.ReadUInt32();
                                    continue;
                                case ProtoTypeCode.Int64:
                                    value = (object) reader.ReadInt64();
                                    continue;
                                case ProtoTypeCode.UInt64:
                                    value = (object) reader.ReadUInt64();
                                    continue;
                                case ProtoTypeCode.Single:
                                    value = (object) reader.ReadSingle();
                                    continue;
                                case ProtoTypeCode.Double:
                                    value = (object) reader.ReadDouble();
                                    continue;
                                case ProtoTypeCode.Decimal:
                                    value = (object) BclHelpers.ReadDecimal(reader);
                                    continue;
                                case ProtoTypeCode.DateTime:
                                    value = (object) BclHelpers.ReadDateTime(reader);
                                    continue;
                                case ProtoTypeCode.String:
                                    value = (object) reader.ReadString();
                                    continue;
                                case ProtoTypeCode.TimeSpan:
                                    value = (object) BclHelpers.ReadTimeSpan(reader);
                                    continue;
                                case ProtoTypeCode.ByteArray:
                                    value = (object) ProtoReader.AppendBytes((byte[]) value, reader);
                                    continue;
                                case ProtoTypeCode.Guid:
                                    value = (object) BclHelpers.ReadGuid(reader);
                                    continue;
                                case ProtoTypeCode.Uri:
                                    value = (object) new Uri(reader.ReadString());
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                }
                else
                    break;
            }
            if (!flag1 && !asListItem && (autoCreate && type != typeof (string)))
                value = Activator.CreateInstance(type);
            return flag1;
        }

        public static RuntimeTypeModel Create()
        {
            return new RuntimeTypeModel(false);
        }

        protected internal static Type ResolveProxies(Type type)
        {
            if (type == (Type) null)
                return (Type) null;
            if (type.IsGenericParameter)
                return (Type) null;
            Type underlyingType = Helpers.GetUnderlyingType(type);
            if (underlyingType != (Type) null)
                return underlyingType;
            string fullName = type.FullName;
            if (fullName != null && fullName.StartsWith("System.Data.Entity.DynamicProxies."))
                return type.BaseType;
            foreach (Type type1 in type.GetInterfaces())
            {
                switch (type1.FullName)
                {
                    case "NHibernate.Proxy.INHibernateProxy":
                    case "NHibernate.Proxy.DynamicProxy.IProxy":
                    case "NHibernate.Intercept.IFieldInterceptorAccessor":
                        return type.BaseType;
                    default:
                        goto default;
                }
            }
            return (Type) null;
        }

        public bool IsDefined(Type type)
        {
            return this.GetKey(ref type) >= 0;
        }

        protected internal int GetKey(ref Type type)
        {
            int keyImpl = this.GetKeyImpl(type);
            if (keyImpl < 0)
            {
                Type type1 = TypeModel.ResolveProxies(type);
                if (type1 != (Type) null)
                {
                    type = type1;
                    keyImpl = this.GetKeyImpl(type);
                }
            }
            return keyImpl;
        }

        protected abstract int GetKeyImpl(Type type);

        protected internal abstract void Serialize(int key, object value, ProtoWriter dest);

        protected internal abstract object Deserialize(int key, object value, ProtoReader source);

        public object DeepClone(object value)
        {
            if (value == null)
                return (object) null;
            Type type = value.GetType();
            int key = this.GetKey(ref type);
            if (key >= 0 && !Helpers.IsEnum(type))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (ProtoWriter dest = new ProtoWriter((Stream) memoryStream, this, (SerializationContext) null))
                    {
                        dest.SetRootObject(value);
                        this.Serialize(key, value, dest);
                        dest.Close();
                    }
                    memoryStream.Position = 0L;
                    using (
                        ProtoReader source = new ProtoReader((Stream) memoryStream, this, (SerializationContext) null))
                        return this.Deserialize(key, (object) null, source);
                }
            }
            else if (type == typeof (byte[]))
            {
                byte[] from = (byte[]) value;
                byte[] to = new byte[from.Length];
                Helpers.BlockCopy(from, 0, to, 0, from.Length);
                return (object) to;
            }
            else
            {
                int modelKey;
                if (this.GetWireType(Helpers.GetTypeCode(type), DataFormat.Default, ref type, out modelKey) !=
                    WireType.None && modelKey < 0)
                    return value;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (
                        ProtoWriter writer = new ProtoWriter((Stream) memoryStream, this, (SerializationContext) null))
                    {
                        if (!this.TrySerializeAuxiliaryType(writer, type, DataFormat.Default, 1, value, false))
                            TypeModel.ThrowUnexpectedType(type);
                        writer.Close();
                    }
                    memoryStream.Position = 0L;
                    using (
                        ProtoReader reader = new ProtoReader((Stream) memoryStream, this, (SerializationContext) null))
                    {
                        value = (object) null;
                        this.TryDeserializeAuxiliaryType(reader, DataFormat.Default, 1, type, ref value, true, false,
                            true, false);
                        return value;
                    }
                }
            }
        }

        protected internal static void ThrowUnexpectedSubtype(Type expected, Type actual)
        {
            if (expected != TypeModel.ResolveProxies(actual))
                throw new InvalidOperationException("Unexpected sub-type: " + actual.FullName);
        }

        protected internal static void ThrowUnexpectedType(Type type)
        {
            string str = type == (Type) null ? "(unknown)" : type.FullName;
            if (type != (Type) null)
            {
                Type baseType = type.BaseType;
                if (baseType != (Type) null && baseType.IsGenericType &&
                    baseType.GetGenericTypeDefinition().Name == "GeneratedMessage`2")
                    throw new InvalidOperationException(
                        "Are you mixing protobuf-net and protobuf-csharp-port? See http://stackoverflow.com/q/11564914; type: " +
                        str);
            }
            throw new InvalidOperationException("Type is not expected, and no contract can be inferred: " + str);
        }

        internal static Exception CreateNestedListsNotSupported()
        {
            return (Exception) new NotSupportedException("Nested or jagged lists and arrays are not supported");
        }

        public static void ThrowCannotCreateInstance(Type type)
        {
            throw new ProtoException("No parameterless constructor found for " + type.Name);
        }

        internal static string SerializeType(TypeModel model, Type type)
        {
            TypeFormatEventHandler formatEventHandler;
            if (model != null && (formatEventHandler = model.DynamicTypeFormatting) != null)
            {
                TypeFormatEventArgs args = new TypeFormatEventArgs(type);
                formatEventHandler((object) model, args);
                if (!Helpers.IsNullOrEmpty(args.FormattedName))
                    return args.FormattedName;
            }
            return type.AssemblyQualifiedName;
        }

        internal static Type DeserializeType(TypeModel model, string value)
        {
            TypeFormatEventHandler formatEventHandler;
            if (model != null && (formatEventHandler = model.DynamicTypeFormatting) != null)
            {
                TypeFormatEventArgs args = new TypeFormatEventArgs(value);
                formatEventHandler((object) model, args);
                if (args.Type != (Type) null)
                    return args.Type;
            }
            return Type.GetType(value);
        }

        public bool CanSerializeContractType(Type type)
        {
            return this.CanSerialize(type, false, true, true);
        }

        public bool CanSerialize(Type type)
        {
            return this.CanSerialize(type, true, true, true);
        }

        public bool CanSerializeBasicType(Type type)
        {
            return this.CanSerialize(type, true, false, true);
        }

        private bool CanSerialize(Type type, bool allowBasic, bool allowContract, bool allowLists)
        {
            if (type == (Type) null)
                throw new ArgumentNullException("type");
            Type underlyingType = Helpers.GetUnderlyingType(type);
            if (underlyingType != (Type) null)
                type = underlyingType;
            switch (Helpers.GetTypeCode(type))
            {
                case ProtoTypeCode.Empty:
                case ProtoTypeCode.Unknown:
                    if (this.GetKey(ref type) >= 0)
                        return allowContract;
                    if (allowLists)
                    {
                        Type type1 = (Type) null;
                        if (type.IsArray)
                        {
                            if (type.GetArrayRank() == 1)
                                type1 = type.GetElementType();
                        }
                        else
                            type1 = TypeModel.GetListItemType(this, type);
                        if (type1 != (Type) null)
                            return this.CanSerialize(type1, allowBasic, allowContract, false);
                    }
                    return false;
                default:
                    return allowBasic;
            }
        }

        public virtual string GetSchema(Type type)
        {
            throw new NotSupportedException();
        }

        public IFormatter CreateFormatter(Type type)
        {
            return (IFormatter) new TypeModel.Formatter(this, type);
        }

        internal virtual Type GetType(string fullName, Assembly context)
        {
            return TypeModel.ResolveKnownType(fullName, this, context);
        }

        internal static Type ResolveKnownType(string name, TypeModel model, Assembly assembly)
        {
            if (Helpers.IsNullOrEmpty(name))
                return (Type) null;
            try
            {
                Type type = Type.GetType(name);
                if (type != (Type) null)
                    return type;
            }
            catch
            {
            }
            try
            {
                int length = name.IndexOf(',');
                string name1 = (length > 0 ? name.Substring(0, length) : name).Trim();
                if (assembly == (Assembly) null)
                    assembly = Assembly.GetCallingAssembly();
                Type type = assembly == (Assembly) null ? (Type) null : assembly.GetType(name1);
                if (type != (Type) null)
                    return type;
            }
            catch
            {
            }
            return (Type) null;
        }

        private class DeserializeItemsIterator : IEnumerator, IEnumerable
        {
            private bool haveObject;
            private object current;
            private readonly Stream source;
            private readonly Type type;
            private readonly PrefixStyle style;
            private readonly int expectedField;
            private readonly Serializer.TypeResolver resolver;
            private readonly TypeModel model;
            private readonly SerializationContext context;

            public object Current
            {
                get { return this.current; }
            }

            public DeserializeItemsIterator(TypeModel model, Stream source, Type type, PrefixStyle style,
                int expectedField, Serializer.TypeResolver resolver, SerializationContext context)
            {
                this.haveObject = true;
                this.source = source;
                this.type = type;
                this.style = style;
                this.expectedField = expectedField;
                this.resolver = resolver;
                this.model = model;
                this.context = context;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator) this;
            }

            public bool MoveNext()
            {
                if (this.haveObject)
                {
                    int bytesRead;
                    this.current = this.model.DeserializeWithLengthPrefix(this.source, (object) null, this.type,
                        this.style, this.expectedField, this.resolver, out bytesRead, out this.haveObject, this.context);
                }
                return this.haveObject;
            }

            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }
        }

        private class DeserializeItemsIterator<T> : TypeModel.DeserializeItemsIterator, IEnumerator<T>, IDisposable,
            IEnumerator, IEnumerable<T>, IEnumerable
        {
            public T Current
            {
                get { return (T) base.Current; }
            }

            public DeserializeItemsIterator(TypeModel model, Stream source, PrefixStyle style, int expectedField,
                SerializationContext context)
                : base(
                    model, source, model.MapType(typeof (T)), style, expectedField, (Serializer.TypeResolver) null,
                    context)
            {
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return (IEnumerator<T>) this;
            }

            void IDisposable.Dispose()
            {
            }
        }

        protected internal enum CallbackType
        {
            BeforeSerialize,
            AfterSerialize,
            BeforeDeserialize,
            AfterDeserialize,
        }

        internal sealed class Formatter : IFormatter
        {
            private readonly TypeModel model;
            private readonly Type type;
            private SerializationBinder binder;
            private StreamingContext context;
            private ISurrogateSelector surrogateSelector;

            public SerializationBinder Binder
            {
                get { return this.binder; }
                set { this.binder = value; }
            }

            public StreamingContext Context
            {
                get { return this.context; }
                set { this.context = value; }
            }

            public ISurrogateSelector SurrogateSelector
            {
                get { return this.surrogateSelector; }
                set { this.surrogateSelector = value; }
            }

            internal Formatter(TypeModel model, Type type)
            {
                if (model == null)
                    throw new ArgumentNullException("model");
                if (type == (Type) null)
                    throw new ArgumentNullException("type");
                this.model = model;
                this.type = type;
            }

            public object Deserialize(Stream source)
            {
                return this.model.Deserialize(source, (object) null, this.type, -1, (SerializationContext) this.Context);
            }

            public void Serialize(Stream destination, object graph)
            {
                this.model.Serialize(destination, graph, (SerializationContext) this.Context);
            }
        }
    }
}