// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.CustomRootWriter
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Xml;

namespace Sandbox.Common
{
    internal class CustomRootWriter : XmlWriter
    {
        private XmlWriter m_target;
        private string m_customRootType;
        private int m_currentDepth;

        public override WriteState WriteState
        {
            get { return this.m_target.WriteState; }
        }

        internal void Init(string customRootType, XmlWriter target)
        {
            this.m_target = target;
            this.m_customRootType = customRootType;
            this.m_target.WriteAttributeString("xsi:type", this.m_customRootType);
            this.m_currentDepth = 0;
        }

        internal void Release()
        {
            this.m_target = (XmlWriter) null;
            this.m_customRootType = (string) null;
        }

        public override void Close()
        {
            this.m_target.Close();
        }

        public override void Flush()
        {
            this.m_target.Flush();
        }

        public override string LookupPrefix(string ns)
        {
            return this.m_target.LookupPrefix(ns);
        }

        public override void WriteBase64(byte[] buffer, int index, int count)
        {
            this.m_target.WriteBase64(buffer, index, count);
        }

        public override void WriteCData(string text)
        {
            this.m_target.WriteCData(text);
        }

        public override void WriteCharEntity(char ch)
        {
            this.m_target.WriteCharEntity(ch);
        }

        public override void WriteChars(char[] buffer, int index, int count)
        {
            this.m_target.WriteChars(buffer, index, count);
        }

        public override void WriteComment(string text)
        {
            this.m_target.WriteComment(text);
        }

        public override void WriteEndAttribute()
        {
            this.m_target.WriteEndAttribute();
        }

        public override void WriteEndElement()
        {
            --this.m_currentDepth;
            if (this.m_currentDepth <= 0)
                return;
            this.m_target.WriteEndElement();
        }

        public override void WriteEntityRef(string name)
        {
            this.m_target.WriteEntityRef(name);
        }

        public override void WriteFullEndElement()
        {
            this.m_target.WriteFullEndElement();
        }

        public override void WriteProcessingInstruction(string name, string text)
        {
            this.m_target.WriteProcessingInstruction(name, text);
        }

        public override void WriteRaw(string data)
        {
            this.m_target.WriteRaw(data);
        }

        public override void WriteRaw(char[] buffer, int index, int count)
        {
            this.m_target.WriteRaw(buffer, index, count);
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            this.m_target.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteString(string text)
        {
            this.m_target.WriteString(text);
        }

        public override void WriteSurrogateCharEntity(char lowChar, char highChar)
        {
            this.m_target.WriteSurrogateCharEntity(lowChar, highChar);
        }

        public override void WriteWhitespace(string ws)
        {
            this.m_target.WriteWhitespace(ws);
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
        }

        public override void WriteStartDocument(bool standalone)
        {
        }

        public override void WriteStartDocument()
        {
        }

        public override void WriteEndDocument()
        {
            while (this.m_currentDepth > 0)
                this.WriteEndElement();
        }

        public override void WriteStartElement(string prefix, string localName, string ns)
        {
            if (this.m_currentDepth > 0)
                this.m_target.WriteStartElement(prefix, localName, ns);
            ++this.m_currentDepth;
        }
    }
}