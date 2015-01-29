// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.VRageData.SerializableBounds
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.VRageData
{
  [ProtoContract]
  public struct SerializableBounds
  {
    [ProtoMember(1)]
    [XmlAttribute]
    public float Min;
    [XmlAttribute]
    [ProtoMember(2)]
    public float Max;
    [XmlAttribute]
    [ProtoMember(3)]
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
