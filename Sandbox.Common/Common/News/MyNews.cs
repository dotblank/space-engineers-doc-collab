﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.News.MyNews
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sandbox.Common.News
{
  [XmlRoot(ElementName = "News")]
  public class MyNews
  {
    [XmlElement("Entry")]
    public List<MyNewsEntry> Entry;
  }
}