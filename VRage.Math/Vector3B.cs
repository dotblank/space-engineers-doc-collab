// Decompiled with JetBrains decompiler
// Type: VRageMath.Vector3B
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;
using System;

namespace VRageMath
{
  [ProtoContract]
  public struct Vector3B
  {
    public static readonly Vector3B Zero = new Vector3B();
    [ProtoMember(1)]
    public sbyte X;
    [ProtoMember(2)]
    public sbyte Y;
    [ProtoMember(3)]
    public sbyte Z;

    public Vector3B(sbyte x, sbyte y, sbyte z)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
    }

    public Vector3B(Vector3I vec)
    {
      this.X = (sbyte) vec.X;
      this.Y = (sbyte) vec.Y;
      this.Z = (sbyte) vec.Z;
    }

    public static implicit operator Vector3I(Vector3B vec)
    {
      return new Vector3I((int) vec.X, (int) vec.Y, (int) vec.Z);
    }

    public static Vector3 operator *(Vector3 vector, Vector3B shortVector)
    {
      return shortVector * vector;
    }

    public static Vector3 operator *(Vector3B shortVector, Vector3 vector)
    {
      return new Vector3((float) shortVector.X * vector.X, (float) shortVector.Y * vector.Y, (float) shortVector.Z * vector.Z);
    }

    public static bool operator ==(Vector3B a, Vector3B b)
    {
      if ((int) a.X == (int) b.X && (int) a.Y == (int) b.Y)
        return (int) a.Z == (int) b.Z;
      else
        return false;
    }

    public static bool operator !=(Vector3B a, Vector3B b)
    {
      return !(a == b);
    }

    public override string ToString()
    {
      return (string) (object) this.X + (object) ", " + (string) (object) this.Y + ", " + (string) (object) this.Z;
    }

    public static Vector3B Round(Vector3 vec)
    {
      return new Vector3B((sbyte) Math.Round((double) vec.X), (sbyte) Math.Round((double) vec.Y), (sbyte) Math.Round((double) vec.Z));
    }

    public static Vector3B Fit(Vector3 vec, float range)
    {
      return Vector3B.Round(vec / range * 128f);
    }
  }
}
