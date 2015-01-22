// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.News.MyNews
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sandbox.Common.News
{
    [XmlRoot(ElementName = "News")]
    public class MyNews
    {
        [XmlElement("Entry")] public List<MyNewsEntry> Entry;
    }
}