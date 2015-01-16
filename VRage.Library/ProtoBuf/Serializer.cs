// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Serializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf.Meta;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace ProtoBuf
{
    public static class Serializer
    {
        private const string ProtoBinaryField = "proto";
        public const int ListItemTag = 1;

        public static string GetProto<T>()
        {
            return RuntimeTypeModel.Default.GetSchema(RuntimeTypeModel.Default.MapType(typeof (T)));
        }

        public static T DeepClone<T>(T instance)
        {
            if ((object) instance != null)
                return (T) RuntimeTypeModel.Default.DeepClone((object) instance);
            else
                return instance;
        }

        public static T Merge<T>(Stream source, T instance)
        {
            return
                (T)
                    ((ProtoBuf.Meta.TypeModel) RuntimeTypeModel.Default).Deserialize(source, (object) instance,
                        typeof (T));
        }

        public static T Deserialize<T>(Stream source)
        {
            return
                (T) ((ProtoBuf.Meta.TypeModel) RuntimeTypeModel.Default).Deserialize(source, (object) null, typeof (T));
        }

        public static void Serialize<T>(Stream destination, T instance)
        {
            if ((object) instance == null)
                return;
            ((ProtoBuf.Meta.TypeModel) RuntimeTypeModel.Default).Serialize(destination, (object) instance);
        }

        public static TTo ChangeType<TFrom, TTo>(TFrom instance)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize<TFrom>((Stream) memoryStream, instance);
                memoryStream.Position = 0L;
                return Serializer.Deserialize<TTo>((Stream) memoryStream);
            }
        }

        public static void Serialize<T>(SerializationInfo info, T instance) where T : class, ISerializable
        {
            Serializer.Serialize<T>(info, new StreamingContext(StreamingContextStates.Persistence), instance);
        }

        public static void Serialize<T>(SerializationInfo info, StreamingContext context, T instance)
            where T : class, ISerializable
        {
            if (info == null)
                throw new ArgumentNullException("info");
            if ((object) instance == null)
                throw new ArgumentNullException("instance");
            if (instance.GetType() != typeof (T))
                throw new ArgumentException("Incorrect type", "instance");
            using (MemoryStream memoryStream = new MemoryStream())
            {
                ((ProtoBuf.Meta.TypeModel) RuntimeTypeModel.Default).Serialize((Stream) memoryStream, (object) instance,
                    (SerializationContext) context);
                info.AddValue("proto", (object) memoryStream.ToArray());
            }
        }

        public static void Serialize<T>(XmlWriter writer, T instance) where T : IXmlSerializable
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if ((object) instance == null)
                throw new ArgumentNullException("instance");
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Serializer.Serialize<T>((Stream) memoryStream, instance);
                writer.WriteBase64(memoryStream.GetBuffer(), 0, (int) memoryStream.Length);
            }
        }

        public static void Merge<T>(XmlReader reader, T instance) where T : IXmlSerializable
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if ((object) instance == null)
                throw new ArgumentNullException("instance");
            byte[] buffer = new byte[4096];
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int depth = reader.Depth;
                while (reader.Read() && reader.Depth > depth)
                {
                    if (reader.NodeType == XmlNodeType.Text)
                    {
                        int count;
                        while ((count = reader.ReadContentAsBase64(buffer, 0, 4096)) > 0)
                            memoryStream.Write(buffer, 0, count);
                        if (reader.Depth <= depth)
                            break;
                    }
                }
                memoryStream.Position = 0L;
                Serializer.Merge<T>((Stream) memoryStream, instance);
            }
        }

        public static void Merge<T>(SerializationInfo info, T instance) where T : class, ISerializable
        {
            Serializer.Merge<T>(info, new StreamingContext(StreamingContextStates.Persistence), instance);
        }

        public static void Merge<T>(SerializationInfo info, StreamingContext context, T instance)
            where T : class, ISerializable
        {
            if (info == null)
                throw new ArgumentNullException("info");
            if ((object) instance == null)
                throw new ArgumentNullException("instance");
            if (instance.GetType() != typeof (T))
                throw new ArgumentException("Incorrect type", "instance");
            using (MemoryStream memoryStream = new MemoryStream((byte[]) info.GetValue("proto", typeof (byte[]))))
            {
                if (
                    !object.ReferenceEquals(
                        (object)
                            (T)
                                ((ProtoBuf.Meta.TypeModel) RuntimeTypeModel.Default).Deserialize((Stream) memoryStream,
                                    (object) instance, typeof (T), (SerializationContext) context), (object) instance))
                    throw new ProtoException("Deserialization changed the instance; cannot succeed.");
            }
        }

        public static void PrepareSerializer<T>()
        {
            RuntimeTypeModel @default = RuntimeTypeModel.Default;
            @default[@default.MapType(typeof (T))].CompileInPlace();
        }

        public static IFormatter CreateFormatter<T>()
        {
            return RuntimeTypeModel.Default.CreateFormatter(typeof (T));
        }

        public static IEnumerable<T> DeserializeItems<T>(Stream source, PrefixStyle style, int fieldNumber)
        {
            return RuntimeTypeModel.Default.DeserializeItems<T>(source, style, fieldNumber);
        }

        public static T DeserializeWithLengthPrefix<T>(Stream source, PrefixStyle style)
        {
            return Serializer.DeserializeWithLengthPrefix<T>(source, style, 0);
        }

        public static T DeserializeWithLengthPrefix<T>(Stream source, PrefixStyle style, int fieldNumber)
        {
            RuntimeTypeModel @default = RuntimeTypeModel.Default;
            return
                (T)
                    @default.DeserializeWithLengthPrefix(source, (object) null, @default.MapType(typeof (T)), style,
                        fieldNumber);
        }

        public static T MergeWithLengthPrefix<T>(Stream source, T instance, PrefixStyle style)
        {
            RuntimeTypeModel @default = RuntimeTypeModel.Default;
            return
                (T)
                    @default.DeserializeWithLengthPrefix(source, (object) instance, @default.MapType(typeof (T)), style,
                        0);
        }

        public static void SerializeWithLengthPrefix<T>(Stream destination, T instance, PrefixStyle style)
        {
            Serializer.SerializeWithLengthPrefix<T>(destination, instance, style, 0);
        }

        public static void SerializeWithLengthPrefix<T>(Stream destination, T instance, PrefixStyle style,
            int fieldNumber)
        {
            RuntimeTypeModel @default = RuntimeTypeModel.Default;
            @default.SerializeWithLengthPrefix(destination, (object) instance, @default.MapType(typeof (T)), style,
                fieldNumber);
        }

        public static bool TryReadLengthPrefix(Stream source, PrefixStyle style, out int length)
        {
            int fieldNumber;
            int bytesRead;
            length = ProtoReader.ReadLengthPrefix(source, false, style, out fieldNumber, out bytesRead);
            return bytesRead > 0;
        }

        public static bool TryReadLengthPrefix(byte[] buffer, int index, int count, PrefixStyle style, out int length)
        {
            using (Stream source = (Stream) new MemoryStream(buffer, index, count))
                return Serializer.TryReadLengthPrefix(source, style, out length);
        }

        public static void FlushPool()
        {
            BufferPool.Flush();
        }

        public static class NonGeneric
        {
            public static object DeepClone(object instance)
            {
                if (instance != null)
                    return RuntimeTypeModel.Default.DeepClone(instance);
                else
                    return (object) null;
            }

            public static void Serialize(Stream dest, object instance)
            {
                if (instance == null)
                    return;
                ((ProtoBuf.Meta.TypeModel) RuntimeTypeModel.Default).Serialize(dest, instance);
            }

            public static object Deserialize(Type type, Stream source)
            {
                return ((ProtoBuf.Meta.TypeModel) RuntimeTypeModel.Default).Deserialize(source, (object) null, type);
            }

            public static object Merge(Stream source, object instance)
            {
                if (instance == null)
                    throw new ArgumentNullException("instance");
                else
                    return ((ProtoBuf.Meta.TypeModel) RuntimeTypeModel.Default).Deserialize(source, instance,
                        instance.GetType(), (SerializationContext) null);
            }

            public static void SerializeWithLengthPrefix(Stream destination, object instance, PrefixStyle style,
                int fieldNumber)
            {
                RuntimeTypeModel @default = RuntimeTypeModel.Default;
                @default.SerializeWithLengthPrefix(destination, instance, @default.MapType(instance.GetType()), style,
                    fieldNumber);
            }

            public static bool TryDeserializeWithLengthPrefix(Stream source, PrefixStyle style,
                Serializer.TypeResolver resolver, out object value)
            {
                value = RuntimeTypeModel.Default.DeserializeWithLengthPrefix(source, (object) null, (Type) null, style,
                    0, resolver);
                return value != null;
            }

            public static bool CanSerialize(Type type)
            {
                return ((ProtoBuf.Meta.TypeModel) RuntimeTypeModel.Default).IsDefined(type);
            }
        }

        public static class GlobalOptions
        {
            [Obsolete("Please use RuntimeTypeModel.Default.InferTagFromNameDefault instead (or on a per-model basis)",
                false)]
            public static bool InferTagFromName
            {
                get { return RuntimeTypeModel.Default.InferTagFromNameDefault; }
                set { RuntimeTypeModel.Default.InferTagFromNameDefault = value; }
            }
        }

        public delegate Type TypeResolver(int fieldNumber);
    }
}