// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.News.MyNewsEntry
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Xml.Serialization;

namespace Sandbox.Common.News
{
    public class MyNewsEntry
    {
        [XmlAttribute(AttributeName = "public")] public bool Public = true;
        [XmlAttribute(AttributeName = "title")] public string Title;
        [XmlAttribute(AttributeName = "date")] public string Date;
        [XmlAttribute(AttributeName = "version")] public string Version;
        [XmlText] public string Text;
    }
}