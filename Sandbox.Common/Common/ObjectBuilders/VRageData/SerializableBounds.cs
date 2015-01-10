// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.VRageData.SerializableBounds
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.VRageData
{
  [ProtoContract]
  public struct SerializableBounds
  {
    [XmlAttribute]
    [ProtoMember(1)]
    public float Min;
    [ProtoMember(2)]
    [XmlAttribute]
    public float Max;
    [ProtoMember(3)]
    [XmlAttribute]
    public float Default;

    public SerializableBounds(float min, float max, float def)
    {
      this.Min = min;
      this.Max = max;
      this.Default = def;
    }

    public static implicit operator MyBounds(SerializableBounds v)
    {
      return new MyBounds(v.Min, v.Max, v.Default);
    }

    public static implicit operator SerializableBounds(MyBounds v)
    {
      return new SerializableBounds(v.Min, v.Max, v.Default);
    }
  }
}
