// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Audio.MyAudioHelpers
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
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

    [ProtoContract]
    [XmlType("Wave")]
    public class Wave
    {
      [ProtoMember(1)]
      [XmlAttribute]
      public MyAudioHelpers.Dimensions Type;
      [DefaultValue("")]
      [ProtoMember(2)]
      [ModdableContentFile("xwm")]
      public string Start;
      [DefaultValue("")]
      [ProtoMember(3)]
      [ModdableContentFile("xwm")]
      public string Loop;
      [ModdableContentFile("xwm")]
      [ProtoMember(4)]
      [DefaultValue("")]
      public string End;
    }
  }
}
