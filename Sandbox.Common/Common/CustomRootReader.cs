// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.CustomRootReader
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Xml;

namespace Sandbox.Common
{
    internal class CustomRootReader : XmlReader
    {
        private XmlReader m_source;
        private string m_customRootName;
        private int m_rootDepth;

        public override int AttributeCount
        {
            get { return this.m_source.AttributeCount; }
        }

        public override string BaseURI
        {
            get { return this.m_source.BaseURI; }
        }

        public override int Depth
        {
            get { return this.m_source.Depth; }
        }

        public override bool EOF
        {
            get { return this.m_source.EOF; }
        }

        public override bool IsEmptyElement
        {
            get { return this.m_source.IsEmptyElement; }
        }

        public override XmlNameTable NameTable
        {
            get { return this.m_source.NameTable; }
        }

        public override XmlNodeType NodeType
        {
            get { return this.m_source.NodeType; }
        }

        public override string Prefix
        {
            get { return this.m_source.Prefix; }
        }

        public override ReadState ReadState
        {
            get { return this.m_source.ReadState; }
        }

        public override string Value
        {
            get { return this.m_source.Value; }
        }

        public override string LocalName
        {
            get
            {
                if (this.m_source.Depth != this.m_rootDepth)
                    return this.m_source.LocalName;
                else
                    return this.m_source.NameTable.Get(this.m_customRootName);
            }
        }

        public override string NamespaceURI
        {
            get
            {
                if (this.m_source.Depth != this.m_rootDepth)
                    return this.m_source.NamespaceURI;
                else
                    return this.m_source.NameTable.Get("");
            }
        }

        internal void Init(string customRootName, XmlReader source)
        {
            this.m_source = source;
            this.m_customRootName = customRootName;
            this.m_rootDepth = source.Depth;
        }

        internal void Release()
        {
            this.m_source = (XmlReader) null;
            this.m_customRootName = (string) null;
            this.m_rootDepth = -1;
        }

        public override void Close()
        {
            this.m_source.Close();
        }

        public override string GetAttribute(int i)
        {
            return this.m_source.GetAttribute(i);
        }

        public override string GetAttribute(string name)
        {
            return this.m_source.GetAttribute(name);
        }

        public override string LookupNamespace(string prefix)
        {
            return this.m_source.LookupNamespace(prefix);
        }

        public override bool MoveToAttribute(string name, string ns)
        {
            return this.m_source.MoveToAttribute(name, ns);
        }

        public override bool MoveToAttribute(string name)
        {
            return this.m_source.MoveToAttribute(name);
        }

        public override bool MoveToElement()
        {
            return this.m_source.MoveToElement();
        }

        public override bool MoveToFirstAttribute()
        {
            return this.m_source.MoveToFirstAttribute();
        }

        public override bool MoveToNextAttribute()
        {
            return this.m_source.MoveToNextAttribute();
        }

        public override bool Read()
        {
            return this.m_source.Read();
        }

        public override bool ReadAttributeValue()
        {
            return this.m_source.ReadAttributeValue();
        }

        public override void ResolveEntity()
        {
            this.m_source.ResolveEntity();
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            if (this.m_source.Depth != this.m_rootDepth)
                return this.m_source.GetAttribute(name, namespaceURI);
            else
                return (string) null;
        }
    }
}