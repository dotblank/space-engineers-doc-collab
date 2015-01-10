// Decompiled with JetBrains decompiler
// Type: VRageMath.Vector4UByte
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;
using System;

namespace VRageMath
{
  [ProtoContract]
  public struct Vector4UByte
  {
    [ProtoMember(1)]
    public byte X;
    [ProtoMember(2)]
    public byte Y;
    [ProtoMember(3)]
    public byte Z;
    [ProtoMember(4)]
    public byte W;

    public Vector4UByte(byte x, byte y, byte z, byte w)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
      this.W = w;
    }

    public override string ToString()
    {
      return (string) (object) this.X + (object) ", " + (string) (object) this.Y + ", " + (string) (object) this.Z + ", " + (string) (object) this.W;
    }

    public static Vector4UByte Round(Vector3 vec)
    {
      return Vector4UByte.Round(new Vector4(vec.X, vec.Y, vec.Z, 0.0f));
    }

    public static Vector4UByte Round(Vector4 vec)
    {
      return new Vector4UByte((byte) Math.Round((double) vec.X), (byte) Math.Round((double) vec.Y), (byte) Math.Round((double) vec.Z), (byte) 0);
    }

    public static Vector4UByte Normalize(Vector3 vec, float range)
    {
      return Vector4UByte.Round((vec / range / 2f + new Vector3(0.5f)) * (float) byte.MaxValue);
    }
  }
}
