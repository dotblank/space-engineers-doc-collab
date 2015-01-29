// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Audio.MyAudioHelpers
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
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
      [ProtoMember(2)]
      [DefaultValue("")]
      [ModdableContentFile("xwm")]
      public string Start;
      [ProtoMember(3)]
      [ModdableContentFile("xwm")]
      [DefaultValue("")]
      public string Loop;
      [ModdableContentFile("xwm")]
      [ProtoMember(4)]
      [DefaultValue("")]
      public string End;
    }
  }
}
