// Decompiled with JetBrains decompiler
// Type: VRageMath.Vector3UByte
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;
using System;
using System.Collections.Generic;

namespace VRageMath
{
  [ProtoContract]
  public struct Vector3UByte
  {
    public static readonly Vector3UByte.EqualityComparer Comparer = new Vector3UByte.EqualityComparer();
    [ProtoMember(1)]
    public byte X;
    [ProtoMember(2)]
    public byte Y;
    [ProtoMember(3)]
    public byte Z;

    public Vector3UByte(byte x, byte y, byte z)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
    }

    public Vector3UByte(Vector3I vec)
    {
      this.X = (byte) vec.X;
      this.Y = (byte) vec.Y;
      this.Z = (byte) vec.Z;
    }

    public static implicit operator Vector3I(Vector3UByte vec)
    {
      return new Vector3I((int) vec.X, (int) vec.Y, (int) vec.Z);
    }

    public override string ToString()
    {
      return (string) (object) this.X + (object) ", " + (string) (object) this.Y + ", " + (string) (object) this.Z;
    }

    public static Vector3UByte Round(Vector3 vec)
    {
      return new Vector3UByte((byte) Math.Round((double) vec.X), (byte) Math.Round((double) vec.Y), (byte) Math.Round((double) vec.Z));
    }

    public int LengthSquared()
    {
      return (int) this.X * (int) this.X + (int) this.Y * (int) this.Y + (int) this.Z * (int) this.Z;
    }

    public static bool IsMiddle(Vector3UByte vec)
    {
      if ((int) vec.X == (int) sbyte.MaxValue && (int) vec.Y == (int) sbyte.MaxValue)
        return (int) vec.Z == (int) sbyte.MaxValue;
      else
        return false;
    }

    public static Vector3UByte Normalize(Vector3 vec, float range)
    {
      Vector3 vector3 = (vec / range / 2f + new Vector3(0.5f)) * 255.9999f;
      return new Vector3UByte((byte) vector3.X, (byte) vector3.Y, (byte) vector3.Z);
    }

    public static Vector3 Denormalize(Vector3UByte vec, float range)
    {
      float num = 0.001960784f;
      return (new Vector3((float) vec.X, (float) vec.Y, (float) vec.Z) / (float) byte.MaxValue - new Vector3(0.5f - num)) * 2f * range;
    }

    public class EqualityComparer : IEqualityComparer<Vector3UByte>, IComparer<Vector3UByte>
    {
      public bool Equals(Vector3UByte x, Vector3UByte y)
      {
        return (int) x.X == (int) y.X & (int) x.Y == (int) y.Y & (int) x.Z == (int) y.Z;
      }

      public int GetHashCode(Vector3UByte obj)
      {
        return ((int) obj.X * 397 ^ (int) obj.Y) * 397 ^ (int) obj.Z;
      }

      public int Compare(Vector3UByte a, Vector3UByte b)
      {
        int num1 = (int) a.X - (int) b.X;
        int num2 = (int) a.Y - (int) b.Y;
        int num3 = (int) a.Z - (int) b.Z;
        if (num1 != 0)
          return num1;
        if (num2 == 0)
          return num3;
        else
          return num2;
      }
    }
  }
}
