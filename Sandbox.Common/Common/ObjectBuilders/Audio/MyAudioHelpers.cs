// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Audio.MyAudioHelpers
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Audio
{
  public class MyAudioHelpers
  {
    public enum CurveType
    {
      Linear,
      Quadratic,
      Poly2,
      Custom_1,
    }

    public enum Dimensions
    {
      D2,
      D3,
    }

    [XmlType("Wave")]
    [ProtoContract]
    public class Wave
    {
      [XmlAttribute]
      [ProtoMember(1)]
      public MyAudioHelpers.Dimensions Type;
      [DefaultValue("")]
      [ProtoMember(2)]
      [ModdableContentFile("xwm")]
      public string Start;
      [DefaultValue("")]
      [ModdableContentFile("xwm")]
      [ProtoMember(3)]
      public string Loop;
      [ProtoMember(4)]
      [DefaultValue("")]
      [ModdableContentFile("xwm")]
      public string End;
    }
  }
}
