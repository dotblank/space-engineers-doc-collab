// Decompiled with JetBrains decompiler
// Type: VRage.Common.Utils.MyChecksums
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Xml.Serialization;
using VRage.Serialization;

namespace VRage.Common.Utils
{
    public sealed class MyChecksums
    {
        private string m_publicKey;

        public string PublicKey
        {
            get { return this.m_publicKey; }
            set
            {
                this.m_publicKey = value;
                this.PublicKeyAsArray = Convert.FromBase64String(this.m_publicKey);
            }
        }

        public SerializableDictionary<string, string> Items { get; set; }

        [XmlIgnore]
        public byte[] PublicKeyAsArray { get; private set; }

        public MyChecksums()
        {
            this.Items = new SerializableDictionary<string, string>();
        }
    }
}