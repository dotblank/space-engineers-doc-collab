// Decompiled with JetBrains decompiler
// Type: ProtoBuf.ServiceModel.XmlProtoSerializer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ProtoBuf;
using ProtoBuf.Meta;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace ProtoBuf.ServiceModel
{
    public sealed class XmlProtoSerializer : XmlObjectSerializer
    {
        private const string PROTO_ELEMENT = "proto";
        private readonly TypeModel model;
        private readonly int key;
        private readonly bool isList;
        private readonly Type type;

        internal XmlProtoSerializer(TypeModel model, int key, Type type, bool isList)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (key < 0)
                throw new ArgumentOutOfRangeException("key");
            if (type == (Type) null)
                throw new ArgumentOutOfRangeException("type");
            this.model = model;
            this.key = key;
            this.isList = isList;
            this.type = type;
        }

        public XmlProtoSerializer(TypeModel model, Type type)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (type == (Type) null)
                throw new ArgumentNullException("type");
            this.key = XmlProtoSerializer.GetKey(model, ref type, out this.isList);
            this.model = model;
            this.type = type;
            if (this.key < 0)
                throw new ArgumentOutOfRangeException("type", "Type not recognised by the model: " + type.FullName);
        }

        public static XmlProtoSerializer TryCreate(TypeModel model, Type type)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (type == (Type) null)
                throw new ArgumentNullException("type");
            bool isList;
            int key = XmlProtoSerializer.GetKey(model, ref type, out isList);
            if (key >= 0)
                return new XmlProtoSerializer(model, key, type, isList);
            else
                return (XmlProtoSerializer) null;
        }

        private static int GetKey(TypeModel model, ref Type type, out bool isList)
        {
            if (model != null && type != (Type) null)
            {
                int key1 = model.GetKey(ref type);
                if (key1 >= 0)
                {
                    isList = false;
                    return key1;
                }
                else
                {
                    Type listItemType = TypeModel.GetListItemType(model, type);
                    if (listItemType != (Type) null)
                    {
                        int key2 = model.GetKey(ref listItemType);
                        if (key2 >= 0)
                        {
                            isList = true;
                            return key2;
                        }
                    }
                }
            }
            isList = false;
            return -1;
        }

        public override void WriteEndObject(XmlDictionaryWriter writer)
        {
            writer.WriteEndElement();
        }

        public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
        {
            ((XmlWriter) writer).WriteStartElement("proto");
        }

        public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
        {
            if (graph == null)
            {
                ((XmlWriter) writer).WriteAttributeString("nil", "true");
            }
            else
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    if (this.isList)
                    {
                        this.model.Serialize((Stream) memoryStream, graph, (SerializationContext) null);
                    }
                    else
                    {
                        using (
                            ProtoWriter dest = new ProtoWriter((Stream) memoryStream, this.model,
                                (SerializationContext) null))
                            this.model.Serialize(this.key, graph, dest);
                    }
                    byte[] buffer = memoryStream.GetBuffer();
                    writer.WriteBase64(buffer, 0, (int) memoryStream.Length);
                }
            }
        }

        public override bool IsStartObject(XmlDictionaryReader reader)
        {
            int num = (int) reader.MoveToContent();
            if (reader.NodeType == XmlNodeType.Element)
                return reader.Name == "proto";
            else
                return false;
        }

        public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
        {
            int num = (int) reader.MoveToContent();
            bool isEmptyElement = reader.IsEmptyElement;
            bool flag = ((XmlReader) reader).GetAttribute("nil") == "true";
            ((XmlReader) reader).ReadStartElement("proto");
            if (flag)
            {
                if (!isEmptyElement)
                    reader.ReadEndElement();
                return (object) null;
            }
            else if (isEmptyElement)
            {
                if (this.isList)
                    return this.model.Deserialize(Stream.Null, (object) null, this.type, (SerializationContext) null);
                using (ProtoReader source = new ProtoReader(Stream.Null, this.model, (SerializationContext) null))
                    return this.model.Deserialize(this.key, (object) null, source);
            }
            else
            {
                object obj;
                using (MemoryStream memoryStream = new MemoryStream(reader.ReadContentAsBase64()))
                {
                    if (this.isList)
                    {
                        obj = this.model.Deserialize((Stream) memoryStream, (object) null, this.type,
                            (SerializationContext) null);
                    }
                    else
                    {
                        using (
                            ProtoReader source = new ProtoReader((Stream) memoryStream, this.model,
                                (SerializationContext) null))
                            obj = this.model.Deserialize(this.key, (object) null, source);
                    }
                }
                reader.ReadEndElement();
                return obj;
            }
        }
    }
}