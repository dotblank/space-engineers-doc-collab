// Decompiled with JetBrains decompiler
// Type: VRageMath.PackedVector.HalfVector3
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using VRageMath;

namespace VRageMath.PackedVector
{
  public struct HalfVector3
  {
    private ushort X;
    private ushort Y;
    private ushort Z;

    public HalfVector3(float x, float y, float z)
    {
      this.X = HalfUtils.Pack(x);
      this.Y = HalfUtils.Pack(y);
      this.Z = HalfUtils.Pack(z);
    }

    public HalfVector3(Vector3 vector)
    {
      this = new HalfVector3(vector.X, vector.Y, vector.Z);
    }

    public static implicit operator HalfVector3(Vector3 v)
    {
      return new HalfVector3(v);
    }

    public static implicit operator Vector3(HalfVector3 v)
    {
      return v.ToVector3();
    }

    public Vector3 ToVector3()
    {
      Vector3 vector3;
      vector3.X = HalfUtils.Unpack(this.X);
      vector3.Y = HalfUtils.Unpack(this.Y);
      vector3.Z = HalfUtils.Unpack(this.Z);
      return vector3;
    }

    public override string ToString()
    {
      return this.ToVector3().ToString();
    }
  }
}
