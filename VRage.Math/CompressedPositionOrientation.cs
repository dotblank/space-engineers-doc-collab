// Decompiled with JetBrains decompiler
// Type: VRageMath.CompressedPositionOrientation
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using VRageMath.PackedVector;

namespace VRageMath
{
  public struct CompressedPositionOrientation
  {
    public Vector3 Position;
    public HalfVector4 Orientation;

    public Matrix Matrix
    {
      get
      {
        Matrix result;
        this.ToMatrix(out result);
        return result;
      }
      set
      {
        this.FromMatrix(ref value);
      }
    }

    public CompressedPositionOrientation(ref Matrix matrix)
    {
      this.Position = matrix.Translation;
      Quaternion result;
      Quaternion.CreateFromRotationMatrix(ref matrix, out result);
      this.Orientation = new HalfVector4(result.ToVector4());
    }

    public void FromMatrix(ref Matrix matrix)
    {
      this.Position = matrix.Translation;
      Quaternion result;
      Quaternion.CreateFromRotationMatrix(ref matrix, out result);
      this.Orientation = new HalfVector4(result.ToVector4());
    }

    public void ToMatrix(out Matrix result)
    {
      Quaternion quaternion = Quaternion.FromVector4(this.Orientation.ToVector4());
      Matrix.CreateFromQuaternion(ref quaternion, out result);
      result.Translation = this.Position;
    }
  }
}
