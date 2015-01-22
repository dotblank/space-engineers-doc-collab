// Decompiled with JetBrains decompiler
// Type: VRage.Common.Utils.MyChecksums
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
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
      get
      {
        return this.m_publicKey;
      }
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
