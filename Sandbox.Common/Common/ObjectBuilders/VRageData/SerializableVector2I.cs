// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.VRageData.SerializableVector2I
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.VRageData
{
  [ProtoContract]
  public struct SerializableVector2I
  {
    public int X;
    public int Y;

    [XmlAttribute]
    [ProtoMember(1)]
    public int x
    {
      get
      {
        return this.X;
      }
      set
      {
        this.X = value;
      }
    }

    [XmlAttribute]
    [ProtoMember(2)]
    public int y
    {
      get
      {
        return this.Y;
      }
      set
      {
        this.Y = value;
      }
    }

    public SerializableVector2I(int x, int y)
    {
      this.X = x;
      this.Y = y;
    }

    public static implicit operator Vector2I(SerializableVector2I v)
    {
      return new Vector2I(v.X, v.Y);
    }

    public static implicit operator SerializableVector2I(Vector2I v)
    {
      return new SerializableVector2I(v.X, v.Y);
    }

    public bool ShouldSerializeX()
    {
      return false;
    }

    public bool ShouldSerializeY()
    {
      return false;
    }
  }
}
