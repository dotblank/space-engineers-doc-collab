// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.VRageData.SerializableBlockOrientation
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.VRageData
{
  [ProtoContract]
  public struct SerializableBlockOrientation
  {
    public static readonly SerializableBlockOrientation Identity = new SerializableBlockOrientation(Base6Directions.Direction.Forward, Base6Directions.Direction.Up);
    [XmlAttribute]
    [ProtoMember(1)]
    public Base6Directions.Direction Forward;
    [ProtoMember(2)]
    [XmlAttribute]
    public Base6Directions.Direction Up;

    public SerializableBlockOrientation(Base6Directions.Direction forward, Base6Directions.Direction up)
    {
      this.Forward = forward;
      this.Up = up;
    }

    public SerializableBlockOrientation(ref Quaternion q)
    {
      this.Forward = Base6Directions.GetForward(q);
      this.Up = Base6Directions.GetUp(q);
    }

    public static implicit operator MyBlockOrientation(SerializableBlockOrientation v)
    {
      if (Base6Directions.IsValidBlockOrientation(v.Forward, v.Up))
        return new MyBlockOrientation(v.Forward, v.Up);
      if (v.Up == Base6Directions.Direction.Forward)
        return new MyBlockOrientation(v.Forward, Base6Directions.Direction.Up);
      else
        return MyBlockOrientation.Identity;
    }

    public static implicit operator SerializableBlockOrientation(MyBlockOrientation v)
    {
      return new SerializableBlockOrientation(v.Forward, v.Up);
    }
  }
}
