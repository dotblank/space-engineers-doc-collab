// Decompiled with JetBrains decompiler
// Type: VRageMath.MyTransform
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

namespace VRageMath
{
  public struct MyTransform
  {
    public Quaternion Rotation;
    public Vector3 Position;

    public Matrix TransformMatrix
    {
      get
      {
        Matrix fromQuaternion = Matrix.CreateFromQuaternion(this.Rotation);
        fromQuaternion.Translation = this.Position;
        return fromQuaternion;
      }
    }

    public MyTransform(Vector3 position)
    {
      this = new MyTransform(ref position);
    }

    public MyTransform(Matrix matrix)
    {
      this = new MyTransform(ref matrix);
    }

    public MyTransform(ref Vector3 position)
    {
      this.Rotation = Quaternion.Identity;
      this.Position = position;
    }

    public MyTransform(ref Matrix matrix)
    {
      Quaternion.CreateFromRotationMatrix(ref matrix, out this.Rotation);
      this.Position = matrix.Translation;
    }

    public static MyTransform Transform(ref MyTransform t1, ref MyTransform t2)
    {
      MyTransform result;
      MyTransform.Transform(ref t1, ref t2, out result);
      return result;
    }

    public static void Transform(ref MyTransform t1, ref MyTransform t2, out MyTransform result)
    {
      Vector3 result1;
      Vector3.Transform(ref t1.Position, ref t2.Rotation, out result1);
      Vector3 vector3 = result1 + t2.Position;
      Quaternion result2;
      Quaternion.Multiply(ref t1.Rotation, ref t2.Rotation, out result2);
      result.Position = vector3;
      result.Rotation = result2;
    }

    public static Vector3 Transform(ref Vector3 v, ref MyTransform t2)
    {
      Vector3 result;
      MyTransform.Transform(ref v, ref t2, out result);
      return result;
    }

    public static void Transform(ref Vector3 v, ref MyTransform t2, out Vector3 result)
    {
      Vector3.Transform(ref v, ref t2.Rotation, out result);
      result += t2.Position;
    }
  }
}
