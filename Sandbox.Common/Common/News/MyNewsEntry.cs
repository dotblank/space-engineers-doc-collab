// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.News.MyNewsEntry
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Xml.Serialization;

namespace Sandbox.Common.News
{
  public class MyNewsEntry
  {
    [XmlAttribute(AttributeName = "public")]
    public bool Public = true;
    [XmlAttribute(AttributeName = "title")]
    public string Title;
    [XmlAttribute(AttributeName = "date")]
    public string Date;
    [XmlAttribute(AttributeName = "version")]
    public string Version;
    [XmlText]
    public string Text;
  }
}
