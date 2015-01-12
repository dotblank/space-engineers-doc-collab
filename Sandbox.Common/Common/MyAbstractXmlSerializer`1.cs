// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyAbstractXmlSerializer`1
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders.Serializer;
using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using VRage.Common.Generics;

namespace Sandbox.Common
{
    public class MyAbstractXmlSerializer<TAbstractBase> : IXmlSerializable
    {
        [ThreadStatic] private static MyObjectsPool<CustomRootReader> m_readerPool;
        [ThreadStatic] private static MyObjectsPool<CustomRootWriter> m_writerPool;
        private TAbstractBase m_data;

        private static MyObjectsPool<CustomRootReader> ReaderPool
        {
            get
            {
                if (MyAbstractXmlSerializer<TAbstractBase>.m_readerPool == null)
                    MyAbstractXmlSerializer<TAbstractBase>.m_readerPool = new MyObjectsPool<CustomRootReader>(2);
                return MyAbstractXmlSerializer<TAbstractBase>.m_readerPool;
            }
        }

        private static MyObjectsPool<CustomRootWriter> WriterPool
        {
            get
            {
                if (MyAbstractXmlSerializer<TAbstractBase>.m_writerPool == null)
                    MyAbstractXmlSerializer<TAbstractBase>.m_writerPool = new MyObjectsPool<CustomRootWriter>(2);
                return MyAbstractXmlSerializer<TAbstractBase>.m_writerPool;
            }
        }

        public TAbstractBase Data
        {
            get { return this.m_data; }
            set { this.m_data = value; }
        }

        public MyAbstractXmlSerializer()
        {
        }

        public MyAbstractXmlSerializer(TAbstractBase data)
        {
            this.m_data = data;
        }

        public static implicit operator TAbstractBase(MyAbstractXmlSerializer<TAbstractBase> o)
        {
            return o.Data;
        }

        public static implicit operator MyAbstractXmlSerializer<TAbstractBase>(TAbstractBase o)
        {
            if ((object) o != null)
                return new MyAbstractXmlSerializer<TAbstractBase>(o);
            else
                return (MyAbstractXmlSerializer<TAbstractBase>) null;
        }

        public XmlSchema GetSchema()
        {
            return (XmlSchema) null;
        }

        public void ReadXml(XmlReader reader)
        {
            string str = reader.GetAttribute("xsi:type") ??
                         MyObjectBuilderSerializer.GetSerializedName(typeof (TAbstractBase));
            CustomRootReader customRootReader;
            MyAbstractXmlSerializer<TAbstractBase>.ReaderPool.AllocateOrCreate(out customRootReader);
            customRootReader.Init(str, reader);
            this.Data =
                (TAbstractBase) MyObjectBuilderSerializer.GetSerializer(str).Deserialize((XmlReader) customRootReader);
            customRootReader.Release();
            MyAbstractXmlSerializer<TAbstractBase>.ReaderPool.Deallocate(customRootReader);
        }

        public void WriteXml(XmlWriter writer)
        {
            Type type = this.m_data.GetType();
            CustomRootWriter customRootWriter;
            MyAbstractXmlSerializer<TAbstractBase>.WriterPool.AllocateOrCreate(out customRootWriter);
            string serializedName = MyObjectBuilderSerializer.GetSerializedName(type);
            customRootWriter.Init(serializedName, writer);
            MyObjectBuilderSerializer.GetSerializer(type).Serialize((XmlWriter) customRootWriter, (object) this.m_data);
            customRootWriter.Release();
            MyAbstractXmlSerializer<TAbstractBase>.WriterPool.Deallocate(customRootWriter);
        }
    }
}