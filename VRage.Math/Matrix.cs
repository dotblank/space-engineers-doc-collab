// Decompiled with JetBrains decompiler
// Type: VRageMath.Matrix
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace VRageMath
{
  [Serializable]
  [StructLayout(LayoutKind.Explicit)]
  public struct Matrix : IEquatable<Matrix>
  {
    public static Matrix Identity = new Matrix(1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f, 0.0f, 0.0f, 0.0f, 0.0f, 1f);
    public static Matrix Zero = new Matrix(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
    [FieldOffset(0)]
    private Matrix.F16 M;
    [FieldOffset(0)]
    public float M11;
    [FieldOffset(4)]
    public float M12;
    [FieldOffset(8)]
    public float M13;
    [FieldOffset(12)]
    public float M14;
    [FieldOffset(16)]
    public float M21;
    [FieldOffset(20)]
    public float M22;
    [FieldOffset(24)]
    public float M23;
    [FieldOffset(28)]
    public float M24;
    [FieldOffset(32)]
    public float M31;
    [FieldOffset(36)]
    public float M32;
    [FieldOffset(40)]
    public float M33;
    [FieldOffset(44)]
    public float M34;
    [FieldOffset(48)]
    public float M41;
    [FieldOffset(52)]
    public float M42;
    [FieldOffset(56)]
    public float M43;
    [FieldOffset(60)]
    public float M44;

    public Vector3 Up
    {
      get
      {
        Vector3 vector3;
        vector3.X = this.M21;
        vector3.Y = this.M22;
        vector3.Z = this.M23;
        return vector3;
      }
      set
      {
        this.M21 = value.X;
        this.M22 = value.Y;
        this.M23 = value.Z;
      }
    }

    public Vector3 Down
    {
      get
      {
        Vector3 vector3;
        vector3.X = -this.M21;
        vector3.Y = -this.M22;
        vector3.Z = -this.M23;
        return vector3;
      }
      set
      {
        this.M21 = -value.X;
        this.M22 = -value.Y;
        this.M23 = -value.Z;
      }
    }

    public Vector3 Right
    {
      get
      {
        Vector3 vector3;
        vector3.X = this.M11;
        vector3.Y = this.M12;
        vector3.Z = this.M13;
        return vector3;
      }
      set
      {
        this.M11 = value.X;
        this.M12 = value.Y;
        this.M13 = value.Z;
      }
    }

    public Vector3 Col0
    {
      get
      {
        Vector3 vector3;
        vector3.X = this.M11;
        vector3.Y = this.M21;
        vector3.Z = this.M31;
        return vector3;
      }
    }

    public Vector3 Col1
    {
      get
      {
        Vector3 vector3;
        vector3.X = this.M12;
        vector3.Y = this.M22;
        vector3.Z = this.M32;
        return vector3;
      }
    }

    public Vector3 Col2
    {
      get
      {
        Vector3 vector3;
        vector3.X = this.M13;
        vector3.Y = this.M23;
        vector3.Z = this.M33;
        return vector3;
      }
    }

    public Vector3 Left
    {
      get
      {
        Vector3 vector3;
        vector3.X = -this.M11;
        vector3.Y = -this.M12;
        vector3.Z = -this.M13;
        return vector3;
      }
      set
      {
        this.M11 = -value.X;
        this.M12 = -value.Y;
        this.M13 = -value.Z;
      }
    }

    public Vector3 Forward
    {
      get
      {
        Vector3 vector3;
        vector3.X = -this.M31;
        vector3.Y = -this.M32;
        vector3.Z = -this.M33;
        return vector3;
      }
      set
      {
        this.M31 = -value.X;
        this.M32 = -value.Y;
        this.M33 = -value.Z;
      }
    }

    public Vector3 Backward
    {
      get
      {
        Vector3 vector3;
        vector3.X = this.M31;
        vector3.Y = this.M32;
        vector3.Z = this.M33;
        return vector3;
      }
      set
      {
        this.M31 = value.X;
        this.M32 = value.Y;
        this.M33 = value.Z;
      }
    }

    public Vector3 Scale
    {
      get
      {
        return new Vector3(this.Col0.Length(), this.Col1.Length(), this.Col2.Length());
      }
    }

    public Vector3 Translation
    {
      get
      {
        Vector3 vector3;
        vector3.X = this.M41;
        vector3.Y = this.M42;
        vector3.Z = this.M43;
        return vector3;
      }
      set
      {
        this.M41 = value.X;
        this.M42 = value.Y;
        this.M43 = value.Z;
      }
    }

    public unsafe float this[int row, int column]
    {
      get
      {
          throw new NotImplementedException();
      }
      set
      {
          throw new NotImplementedException();
      }
    }

    public Matrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
    {
      this.M11 = m11;
      this.M12 = m12;
      this.M13 = m13;
      this.M14 = m14;
      this.M21 = m21;
      this.M22 = m22;
      this.M23 = m23;
      this.M24 = m24;
      this.M31 = m31;
      this.M32 = m32;
      this.M33 = m33;
      this.M34 = m34;
      this.M41 = m41;
      this.M42 = m42;
      this.M43 = m43;
      this.M44 = m44;
    }

    public Matrix(float m11, float m12, float m13, float m21, float m22, float m23, float m31, float m32, float m33)
    {
      this.M11 = m11;
      this.M12 = m12;
      this.M13 = m13;
      this.M14 = 0.0f;
      this.M21 = m21;
      this.M22 = m22;
      this.M23 = m23;
      this.M24 = 0.0f;
      this.M31 = m31;
      this.M32 = m32;
      this.M33 = m33;
      this.M34 = 0.0f;
      this.M41 = 0.0f;
      this.M42 = 0.0f;
      this.M43 = 0.0f;
      this.M44 = 1f;
    }

    public static Matrix operator -(Matrix matrix1)
    {
      Matrix matrix;
      matrix.M11 = -matrix1.M11;
      matrix.M12 = -matrix1.M12;
      matrix.M13 = -matrix1.M13;
      matrix.M14 = -matrix1.M14;
      matrix.M21 = -matrix1.M21;
      matrix.M22 = -matrix1.M22;
      matrix.M23 = -matrix1.M23;
      matrix.M24 = -matrix1.M24;
      matrix.M31 = -matrix1.M31;
      matrix.M32 = -matrix1.M32;
      matrix.M33 = -matrix1.M33;
      matrix.M34 = -matrix1.M34;
      matrix.M41 = -matrix1.M41;
      matrix.M42 = -matrix1.M42;
      matrix.M43 = -matrix1.M43;
      matrix.M44 = -matrix1.M44;
      return matrix;
    }

    public static bool operator ==(Matrix matrix1, Matrix matrix2)
    {
      if ((double) matrix1.M11 == (double) matrix2.M11 && (double) matrix1.M22 == (double) matrix2.M22 && ((double) matrix1.M33 == (double) matrix2.M33 && (double) matrix1.M44 == (double) matrix2.M44) && ((double) matrix1.M12 == (double) matrix2.M12 && (double) matrix1.M13 == (double) matrix2.M13 && ((double) matrix1.M14 == (double) matrix2.M14 && (double) matrix1.M21 == (double) matrix2.M21)) && ((double) matrix1.M23 == (double) matrix2.M23 && (double) matrix1.M24 == (double) matrix2.M24 && ((double) matrix1.M31 == (double) matrix2.M31 && (double) matrix1.M32 == (double) matrix2.M32) && ((double) matrix1.M34 == (double) matrix2.M34 && (double) matrix1.M41 == (double) matrix2.M41 && (double) matrix1.M42 == (double) matrix2.M42)))
        return (double) matrix1.M43 == (double) matrix2.M43;
      else
        return false;
    }

    public static bool operator !=(Matrix matrix1, Matrix matrix2)
    {
      if ((double) matrix1.M11 == (double) matrix2.M11 && (double) matrix1.M12 == (double) matrix2.M12 && ((double) matrix1.M13 == (double) matrix2.M13 && (double) matrix1.M14 == (double) matrix2.M14) && ((double) matrix1.M21 == (double) matrix2.M21 && (double) matrix1.M22 == (double) matrix2.M22 && ((double) matrix1.M23 == (double) matrix2.M23 && (double) matrix1.M24 == (double) matrix2.M24)) && ((double) matrix1.M31 == (double) matrix2.M31 && (double) matrix1.M32 == (double) matrix2.M32 && ((double) matrix1.M33 == (double) matrix2.M33 && (double) matrix1.M34 == (double) matrix2.M34) && ((double) matrix1.M41 == (double) matrix2.M41 && (double) matrix1.M42 == (double) matrix2.M42 && (double) matrix1.M43 == (double) matrix2.M43)))
        return (double) matrix1.M44 != (double) matrix2.M44;
      else
        return true;
    }

    public static Matrix operator +(Matrix matrix1, Matrix matrix2)
    {
      Matrix matrix;
      matrix.M11 = matrix1.M11 + matrix2.M11;
      matrix.M12 = matrix1.M12 + matrix2.M12;
      matrix.M13 = matrix1.M13 + matrix2.M13;
      matrix.M14 = matrix1.M14 + matrix2.M14;
      matrix.M21 = matrix1.M21 + matrix2.M21;
      matrix.M22 = matrix1.M22 + matrix2.M22;
      matrix.M23 = matrix1.M23 + matrix2.M23;
      matrix.M24 = matrix1.M24 + matrix2.M24;
      matrix.M31 = matrix1.M31 + matrix2.M31;
      matrix.M32 = matrix1.M32 + matrix2.M32;
      matrix.M33 = matrix1.M33 + matrix2.M33;
      matrix.M34 = matrix1.M34 + matrix2.M34;
      matrix.M41 = matrix1.M41 + matrix2.M41;
      matrix.M42 = matrix1.M42 + matrix2.M42;
      matrix.M43 = matrix1.M43 + matrix2.M43;
      matrix.M44 = matrix1.M44 + matrix2.M44;
      return matrix;
    }

    public static Matrix operator -(Matrix matrix1, Matrix matrix2)
    {
      Matrix matrix;
      matrix.M11 = matrix1.M11 - matrix2.M11;
      matrix.M12 = matrix1.M12 - matrix2.M12;
      matrix.M13 = matrix1.M13 - matrix2.M13;
      matrix.M14 = matrix1.M14 - matrix2.M14;
      matrix.M21 = matrix1.M21 - matrix2.M21;
      matrix.M22 = matrix1.M22 - matrix2.M22;
      matrix.M23 = matrix1.M23 - matrix2.M23;
      matrix.M24 = matrix1.M24 - matrix2.M24;
      matrix.M31 = matrix1.M31 - matrix2.M31;
      matrix.M32 = matrix1.M32 - matrix2.M32;
      matrix.M33 = matrix1.M33 - matrix2.M33;
      matrix.M34 = matrix1.M34 - matrix2.M34;
      matrix.M41 = matrix1.M41 - matrix2.M41;
      matrix.M42 = matrix1.M42 - matrix2.M42;
      matrix.M43 = matrix1.M43 - matrix2.M43;
      matrix.M44 = matrix1.M44 - matrix2.M44;
      return matrix;
    }

    public static Matrix operator *(Matrix matrix1, Matrix matrix2)
    {
      Matrix matrix;
      matrix.M11 = (float) ((double) matrix1.M11 * (double) matrix2.M11 + (double) matrix1.M12 * (double) matrix2.M21 + (double) matrix1.M13 * (double) matrix2.M31 + (double) matrix1.M14 * (double) matrix2.M41);
      matrix.M12 = (float) ((double) matrix1.M11 * (double) matrix2.M12 + (double) matrix1.M12 * (double) matrix2.M22 + (double) matrix1.M13 * (double) matrix2.M32 + (double) matrix1.M14 * (double) matrix2.M42);
      matrix.M13 = (float) ((double) matrix1.M11 * (double) matrix2.M13 + (double) matrix1.M12 * (double) matrix2.M23 + (double) matrix1.M13 * (double) matrix2.M33 + (double) matrix1.M14 * (double) matrix2.M43);
      matrix.M14 = (float) ((double) matrix1.M11 * (double) matrix2.M14 + (double) matrix1.M12 * (double) matrix2.M24 + (double) matrix1.M13 * (double) matrix2.M34 + (double) matrix1.M14 * (double) matrix2.M44);
      matrix.M21 = (float) ((double) matrix1.M21 * (double) matrix2.M11 + (double) matrix1.M22 * (double) matrix2.M21 + (double) matrix1.M23 * (double) matrix2.M31 + (double) matrix1.M24 * (double) matrix2.M41);
      matrix.M22 = (float) ((double) matrix1.M21 * (double) matrix2.M12 + (double) matrix1.M22 * (double) matrix2.M22 + (double) matrix1.M23 * (double) matrix2.M32 + (double) matrix1.M24 * (double) matrix2.M42);
      matrix.M23 = (float) ((double) matrix1.M21 * (double) matrix2.M13 + (double) matrix1.M22 * (double) matrix2.M23 + (double) matrix1.M23 * (double) matrix2.M33 + (double) matrix1.M24 * (double) matrix2.M43);
      matrix.M24 = (float) ((double) matrix1.M21 * (double) matrix2.M14 + (double) matrix1.M22 * (double) matrix2.M24 + (double) matrix1.M23 * (double) matrix2.M34 + (double) matrix1.M24 * (double) matrix2.M44);
      matrix.M31 = (float) ((double) matrix1.M31 * (double) matrix2.M11 + (double) matrix1.M32 * (double) matrix2.M21 + (double) matrix1.M33 * (double) matrix2.M31 + (double) matrix1.M34 * (double) matrix2.M41);
      matrix.M32 = (float) ((double) matrix1.M31 * (double) matrix2.M12 + (double) matrix1.M32 * (double) matrix2.M22 + (double) matrix1.M33 * (double) matrix2.M32 + (double) matrix1.M34 * (double) matrix2.M42);
      matrix.M33 = (float) ((double) matrix1.M31 * (double) matrix2.M13 + (double) matrix1.M32 * (double) matrix2.M23 + (double) matrix1.M33 * (double) matrix2.M33 + (double) matrix1.M34 * (double) matrix2.M43);
      matrix.M34 = (float) ((double) matrix1.M31 * (double) matrix2.M14 + (double) matrix1.M32 * (double) matrix2.M24 + (double) matrix1.M33 * (double) matrix2.M34 + (double) matrix1.M34 * (double) matrix2.M44);
      matrix.M41 = (float) ((double) matrix1.M41 * (double) matrix2.M11 + (double) matrix1.M42 * (double) matrix2.M21 + (double) matrix1.M43 * (double) matrix2.M31 + (double) matrix1.M44 * (double) matrix2.M41);
      matrix.M42 = (float) ((double) matrix1.M41 * (double) matrix2.M12 + (double) matrix1.M42 * (double) matrix2.M22 + (double) matrix1.M43 * (double) matrix2.M32 + (double) matrix1.M44 * (double) matrix2.M42);
      matrix.M43 = (float) ((double) matrix1.M41 * (double) matrix2.M13 + (double) matrix1.M42 * (double) matrix2.M23 + (double) matrix1.M43 * (double) matrix2.M33 + (double) matrix1.M44 * (double) matrix2.M43);
      matrix.M44 = (float) ((double) matrix1.M41 * (double) matrix2.M14 + (double) matrix1.M42 * (double) matrix2.M24 + (double) matrix1.M43 * (double) matrix2.M34 + (double) matrix1.M44 * (double) matrix2.M44);
      return matrix;
    }

    public static Matrix operator *(Matrix matrix, float scaleFactor)
    {
      float num = scaleFactor;
      Matrix matrix1;
      matrix1.M11 = matrix.M11 * num;
      matrix1.M12 = matrix.M12 * num;
      matrix1.M13 = matrix.M13 * num;
      matrix1.M14 = matrix.M14 * num;
      matrix1.M21 = matrix.M21 * num;
      matrix1.M22 = matrix.M22 * num;
      matrix1.M23 = matrix.M23 * num;
      matrix1.M24 = matrix.M24 * num;
      matrix1.M31 = matrix.M31 * num;
      matrix1.M32 = matrix.M32 * num;
      matrix1.M33 = matrix.M33 * num;
      matrix1.M34 = matrix.M34 * num;
      matrix1.M41 = matrix.M41 * num;
      matrix1.M42 = matrix.M42 * num;
      matrix1.M43 = matrix.M43 * num;
      matrix1.M44 = matrix.M44 * num;
      return matrix1;
    }

    public static Matrix operator *(float scaleFactor, Matrix matrix)
    {
      float num = scaleFactor;
      Matrix matrix1;
      matrix1.M11 = matrix.M11 * num;
      matrix1.M12 = matrix.M12 * num;
      matrix1.M13 = matrix.M13 * num;
      matrix1.M14 = matrix.M14 * num;
      matrix1.M21 = matrix.M21 * num;
      matrix1.M22 = matrix.M22 * num;
      matrix1.M23 = matrix.M23 * num;
      matrix1.M24 = matrix.M24 * num;
      matrix1.M31 = matrix.M31 * num;
      matrix1.M32 = matrix.M32 * num;
      matrix1.M33 = matrix.M33 * num;
      matrix1.M34 = matrix.M34 * num;
      matrix1.M41 = matrix.M41 * num;
      matrix1.M42 = matrix.M42 * num;
      matrix1.M43 = matrix.M43 * num;
      matrix1.M44 = matrix.M44 * num;
      return matrix1;
    }

    public static Matrix operator /(Matrix matrix1, Matrix matrix2)
    {
      Matrix matrix;
      matrix.M11 = matrix1.M11 / matrix2.M11;
      matrix.M12 = matrix1.M12 / matrix2.M12;
      matrix.M13 = matrix1.M13 / matrix2.M13;
      matrix.M14 = matrix1.M14 / matrix2.M14;
      matrix.M21 = matrix1.M21 / matrix2.M21;
      matrix.M22 = matrix1.M22 / matrix2.M22;
      matrix.M23 = matrix1.M23 / matrix2.M23;
      matrix.M24 = matrix1.M24 / matrix2.M24;
      matrix.M31 = matrix1.M31 / matrix2.M31;
      matrix.M32 = matrix1.M32 / matrix2.M32;
      matrix.M33 = matrix1.M33 / matrix2.M33;
      matrix.M34 = matrix1.M34 / matrix2.M34;
      matrix.M41 = matrix1.M41 / matrix2.M41;
      matrix.M42 = matrix1.M42 / matrix2.M42;
      matrix.M43 = matrix1.M43 / matrix2.M43;
      matrix.M44 = matrix1.M44 / matrix2.M44;
      return matrix;
    }

    public static Matrix operator /(Matrix matrix1, float divider)
    {
      float num = 1f / divider;
      Matrix matrix;
      matrix.M11 = matrix1.M11 * num;
      matrix.M12 = matrix1.M12 * num;
      matrix.M13 = matrix1.M13 * num;
      matrix.M14 = matrix1.M14 * num;
      matrix.M21 = matrix1.M21 * num;
      matrix.M22 = matrix1.M22 * num;
      matrix.M23 = matrix1.M23 * num;
      matrix.M24 = matrix1.M24 * num;
      matrix.M31 = matrix1.M31 * num;
      matrix.M32 = matrix1.M32 * num;
      matrix.M33 = matrix1.M33 * num;
      matrix.M34 = matrix1.M34 * num;
      matrix.M41 = matrix1.M41 * num;
      matrix.M42 = matrix1.M42 * num;
      matrix.M43 = matrix1.M43 * num;
      matrix.M44 = matrix1.M44 * num;
      return matrix;
    }

    public Vector3 GetDirectionVector(Base6Directions.Direction direction)
    {
      switch (direction)
      {
        case Base6Directions.Direction.Forward:
          return this.Forward;
        case Base6Directions.Direction.Backward:
          return this.Backward;
        case Base6Directions.Direction.Left:
          return this.Left;
        case Base6Directions.Direction.Right:
          return this.Right;
        case Base6Directions.Direction.Up:
          return this.Up;
        case Base6Directions.Direction.Down:
          return this.Down;
        default:
          return Vector3.Zero;
      }
    }

    public void SetDirectionVector(Base6Directions.Direction direction, Vector3 newValue)
    {
      switch (direction)
      {
        case Base6Directions.Direction.Forward:
          this.Forward = newValue;
          break;
        case Base6Directions.Direction.Backward:
          this.Backward = newValue;
          break;
        case Base6Directions.Direction.Left:
          this.Left = newValue;
          break;
        case Base6Directions.Direction.Right:
          this.Right = newValue;
          break;
        case Base6Directions.Direction.Up:
          this.Up = newValue;
          break;
        case Base6Directions.Direction.Down:
          this.Down = newValue;
          break;
      }
    }

    public Base6Directions.Direction GetClosestDirection(Vector3 referenceVector)
    {
      return this.GetClosestDirection(ref referenceVector);
    }

    public Base6Directions.Direction GetClosestDirection(ref Vector3 referenceVector)
    {
      float num1 = Vector3.Dot(referenceVector, this.Right);
      float num2 = Vector3.Dot(referenceVector, this.Up);
      float num3 = Vector3.Dot(referenceVector, this.Backward);
      float num4 = Math.Abs(num1);
      float num5 = Math.Abs(num2);
      float num6 = Math.Abs(num3);
      if ((double) num4 > (double) num5)
      {
        if ((double) num4 > (double) num6)
          return (double) num1 > 0.0 ? Base6Directions.Direction.Right : Base6Directions.Direction.Left;
        else
          return (double) num3 > 0.0 ? Base6Directions.Direction.Backward : Base6Directions.Direction.Forward;
      }
      else if ((double) num5 > (double) num6)
        return (double) num2 > 0.0 ? Base6Directions.Direction.Up : Base6Directions.Direction.Down;
      else
        return (double) num3 > 0.0 ? Base6Directions.Direction.Backward : Base6Directions.Direction.Forward;
    }

    public static void Rescale(ref Matrix matrix, float scale)
    {
      matrix.M11 *= scale;
      matrix.M12 *= scale;
      matrix.M13 *= scale;
      matrix.M21 *= scale;
      matrix.M22 *= scale;
      matrix.M23 *= scale;
      matrix.M31 *= scale;
      matrix.M32 *= scale;
      matrix.M33 *= scale;
    }

    public static void Rescale(ref Matrix matrix, ref Vector3 scale)
    {
      matrix.M11 *= scale.X;
      matrix.M12 *= scale.X;
      matrix.M13 *= scale.X;
      matrix.M21 *= scale.Y;
      matrix.M22 *= scale.Y;
      matrix.M23 *= scale.Y;
      matrix.M31 *= scale.Z;
      matrix.M32 *= scale.Z;
      matrix.M33 *= scale.Z;
    }

    public static Matrix Rescale(Matrix matrix, float scale)
    {
      Matrix.Rescale(ref matrix, scale);
      return matrix;
    }

    public static Matrix Rescale(Matrix matrix, Vector3 scale)
    {
      Matrix.Rescale(ref matrix, ref scale);
      return matrix;
    }

    public static Matrix CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3? cameraForwardVector)
    {
      Vector3 result1;
      result1.X = objectPosition.X - cameraPosition.X;
      result1.Y = objectPosition.Y - cameraPosition.Y;
      result1.Z = objectPosition.Z - cameraPosition.Z;
      float num1 = result1.LengthSquared();
      if ((double) num1 < 9.99999974737875E-05)
        result1 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
      else
        Vector3.Multiply(ref result1, 1f / (float) Math.Sqrt((double) num1), out result1);
      Vector3 result2;
      Vector3.Cross(ref cameraUpVector, ref result1, out result2);
      double num2 = (double) result2.Normalize();
      Vector3 result3;
      Vector3.Cross(ref result1, ref result2, out result3);
      Matrix matrix;
      matrix.M11 = result2.X;
      matrix.M12 = result2.Y;
      matrix.M13 = result2.Z;
      matrix.M14 = 0.0f;
      matrix.M21 = result3.X;
      matrix.M22 = result3.Y;
      matrix.M23 = result3.Z;
      matrix.M24 = 0.0f;
      matrix.M31 = result1.X;
      matrix.M32 = result1.Y;
      matrix.M33 = result1.Z;
      matrix.M34 = 0.0f;
      matrix.M41 = objectPosition.X;
      matrix.M42 = objectPosition.Y;
      matrix.M43 = objectPosition.Z;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix result)
    {
      Vector3 result1;
      result1.X = objectPosition.X - cameraPosition.X;
      result1.Y = objectPosition.Y - cameraPosition.Y;
      result1.Z = objectPosition.Z - cameraPosition.Z;
      float num1 = result1.LengthSquared();
      if ((double) num1 < 9.99999974737875E-05)
        result1 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
      else
        Vector3.Multiply(ref result1, 1f / (float) Math.Sqrt((double) num1), out result1);
      Vector3 result2;
      Vector3.Cross(ref cameraUpVector, ref result1, out result2);
      double num2 = (double) result2.Normalize();
      Vector3 result3;
      Vector3.Cross(ref result1, ref result2, out result3);
      result.M11 = result2.X;
      result.M12 = result2.Y;
      result.M13 = result2.Z;
      result.M14 = 0.0f;
      result.M21 = result3.X;
      result.M22 = result3.Y;
      result.M23 = result3.Z;
      result.M24 = 0.0f;
      result.M31 = result1.X;
      result.M32 = result1.Y;
      result.M33 = result1.Z;
      result.M34 = 0.0f;
      result.M41 = objectPosition.X;
      result.M42 = objectPosition.Y;
      result.M43 = objectPosition.Z;
      result.M44 = 1f;
    }

    public static Matrix CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector)
    {
      Vector3 result1;
      result1.X = objectPosition.X - cameraPosition.X;
      result1.Y = objectPosition.Y - cameraPosition.Y;
      result1.Z = objectPosition.Z - cameraPosition.Z;
      float num1 = result1.LengthSquared();
      if ((double) num1 < 9.99999974737875E-05)
        result1 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
      else
        Vector3.Multiply(ref result1, 1f / (float) Math.Sqrt((double) num1), out result1);
      Vector3 vector2 = rotateAxis;
      float result2;
      Vector3.Dot(ref rotateAxis, ref result1, out result2);
      Vector3 result3;
      Vector3 result4;
      if ((double) Math.Abs(result2) > 0.998254656791687)
      {
        if (objectForwardVector.HasValue)
        {
          result3 = objectForwardVector.Value;
          Vector3.Dot(ref rotateAxis, ref result3, out result2);
          if ((double) Math.Abs(result2) > 0.998254656791687)
            result3 = (double) Math.Abs((float) ((double) rotateAxis.X * (double) Vector3.Forward.X + (double) rotateAxis.Y * (double) Vector3.Forward.Y + (double) rotateAxis.Z * (double) Vector3.Forward.Z)) > 0.998254656791687 ? Vector3.Right : Vector3.Forward;
        }
        else
          result3 = (double) Math.Abs((float) ((double) rotateAxis.X * (double) Vector3.Forward.X + (double) rotateAxis.Y * (double) Vector3.Forward.Y + (double) rotateAxis.Z * (double) Vector3.Forward.Z)) > 0.998254656791687 ? Vector3.Right : Vector3.Forward;
        Vector3.Cross(ref rotateAxis, ref result3, out result4);
        double num2 = (double) result4.Normalize();
        Vector3.Cross(ref result4, ref rotateAxis, out result3);
        double num3 = (double) result3.Normalize();
      }
      else
      {
        Vector3.Cross(ref rotateAxis, ref result1, out result4);
        double num2 = (double) result4.Normalize();
        Vector3.Cross(ref result4, ref vector2, out result3);
        double num3 = (double) result3.Normalize();
      }
      Matrix matrix;
      matrix.M11 = result4.X;
      matrix.M12 = result4.Y;
      matrix.M13 = result4.Z;
      matrix.M14 = 0.0f;
      matrix.M21 = vector2.X;
      matrix.M22 = vector2.Y;
      matrix.M23 = vector2.Z;
      matrix.M24 = 0.0f;
      matrix.M31 = result3.X;
      matrix.M32 = result3.Y;
      matrix.M33 = result3.Z;
      matrix.M34 = 0.0f;
      matrix.M41 = objectPosition.X;
      matrix.M42 = objectPosition.Y;
      matrix.M43 = objectPosition.Z;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateConstrainedBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix result)
    {
      Vector3 result1;
      result1.X = objectPosition.X - cameraPosition.X;
      result1.Y = objectPosition.Y - cameraPosition.Y;
      result1.Z = objectPosition.Z - cameraPosition.Z;
      float num1 = result1.LengthSquared();
      if ((double) num1 < 9.99999974737875E-05)
        result1 = cameraForwardVector.HasValue ? -cameraForwardVector.Value : Vector3.Forward;
      else
        Vector3.Multiply(ref result1, 1f / (float) Math.Sqrt((double) num1), out result1);
      Vector3 vector2 = rotateAxis;
      float result2;
      Vector3.Dot(ref rotateAxis, ref result1, out result2);
      Vector3 result3;
      Vector3 result4;
      if ((double) Math.Abs(result2) > 0.998254656791687)
      {
        if (objectForwardVector.HasValue)
        {
          result3 = objectForwardVector.Value;
          Vector3.Dot(ref rotateAxis, ref result3, out result2);
          if ((double) Math.Abs(result2) > 0.998254656791687)
            result3 = (double) Math.Abs((float) ((double) rotateAxis.X * (double) Vector3.Forward.X + (double) rotateAxis.Y * (double) Vector3.Forward.Y + (double) rotateAxis.Z * (double) Vector3.Forward.Z)) > 0.998254656791687 ? Vector3.Right : Vector3.Forward;
        }
        else
          result3 = (double) Math.Abs((float) ((double) rotateAxis.X * (double) Vector3.Forward.X + (double) rotateAxis.Y * (double) Vector3.Forward.Y + (double) rotateAxis.Z * (double) Vector3.Forward.Z)) > 0.998254656791687 ? Vector3.Right : Vector3.Forward;
        Vector3.Cross(ref rotateAxis, ref result3, out result4);
        double num2 = (double) result4.Normalize();
        Vector3.Cross(ref result4, ref rotateAxis, out result3);
        double num3 = (double) result3.Normalize();
      }
      else
      {
        Vector3.Cross(ref rotateAxis, ref result1, out result4);
        double num2 = (double) result4.Normalize();
        Vector3.Cross(ref result4, ref vector2, out result3);
        double num3 = (double) result3.Normalize();
      }
      result.M11 = result4.X;
      result.M12 = result4.Y;
      result.M13 = result4.Z;
      result.M14 = 0.0f;
      result.M21 = vector2.X;
      result.M22 = vector2.Y;
      result.M23 = vector2.Z;
      result.M24 = 0.0f;
      result.M31 = result3.X;
      result.M32 = result3.Y;
      result.M33 = result3.Z;
      result.M34 = 0.0f;
      result.M41 = objectPosition.X;
      result.M42 = objectPosition.Y;
      result.M43 = objectPosition.Z;
      result.M44 = 1f;
    }

    public static Matrix CreateTranslation(Vector3 position)
    {
      Matrix matrix;
      matrix.M11 = 1f;
      matrix.M12 = 0.0f;
      matrix.M13 = 0.0f;
      matrix.M14 = 0.0f;
      matrix.M21 = 0.0f;
      matrix.M22 = 1f;
      matrix.M23 = 0.0f;
      matrix.M24 = 0.0f;
      matrix.M31 = 0.0f;
      matrix.M32 = 0.0f;
      matrix.M33 = 1f;
      matrix.M34 = 0.0f;
      matrix.M41 = position.X;
      matrix.M42 = position.Y;
      matrix.M43 = position.Z;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateTranslation(ref Vector3 position, out Matrix result)
    {
      result.M11 = 1f;
      result.M12 = 0.0f;
      result.M13 = 0.0f;
      result.M14 = 0.0f;
      result.M21 = 0.0f;
      result.M22 = 1f;
      result.M23 = 0.0f;
      result.M24 = 0.0f;
      result.M31 = 0.0f;
      result.M32 = 0.0f;
      result.M33 = 1f;
      result.M34 = 0.0f;
      result.M41 = position.X;
      result.M42 = position.Y;
      result.M43 = position.Z;
      result.M44 = 1f;
    }

    public static Matrix CreateTranslation(float xPosition, float yPosition, float zPosition)
    {
      Matrix matrix;
      matrix.M11 = 1f;
      matrix.M12 = 0.0f;
      matrix.M13 = 0.0f;
      matrix.M14 = 0.0f;
      matrix.M21 = 0.0f;
      matrix.M22 = 1f;
      matrix.M23 = 0.0f;
      matrix.M24 = 0.0f;
      matrix.M31 = 0.0f;
      matrix.M32 = 0.0f;
      matrix.M33 = 1f;
      matrix.M34 = 0.0f;
      matrix.M41 = xPosition;
      matrix.M42 = yPosition;
      matrix.M43 = zPosition;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateTranslation(float xPosition, float yPosition, float zPosition, out Matrix result)
    {
      result.M11 = 1f;
      result.M12 = 0.0f;
      result.M13 = 0.0f;
      result.M14 = 0.0f;
      result.M21 = 0.0f;
      result.M22 = 1f;
      result.M23 = 0.0f;
      result.M24 = 0.0f;
      result.M31 = 0.0f;
      result.M32 = 0.0f;
      result.M33 = 1f;
      result.M34 = 0.0f;
      result.M41 = xPosition;
      result.M42 = yPosition;
      result.M43 = zPosition;
      result.M44 = 1f;
    }

    public static Matrix CreateScale(float xScale, float yScale, float zScale)
    {
      float num1 = xScale;
      float num2 = yScale;
      float num3 = zScale;
      Matrix matrix;
      matrix.M11 = num1;
      matrix.M12 = 0.0f;
      matrix.M13 = 0.0f;
      matrix.M14 = 0.0f;
      matrix.M21 = 0.0f;
      matrix.M22 = num2;
      matrix.M23 = 0.0f;
      matrix.M24 = 0.0f;
      matrix.M31 = 0.0f;
      matrix.M32 = 0.0f;
      matrix.M33 = num3;
      matrix.M34 = 0.0f;
      matrix.M41 = 0.0f;
      matrix.M42 = 0.0f;
      matrix.M43 = 0.0f;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateScale(float xScale, float yScale, float zScale, out Matrix result)
    {
      float num1 = xScale;
      float num2 = yScale;
      float num3 = zScale;
      result.M11 = num1;
      result.M12 = 0.0f;
      result.M13 = 0.0f;
      result.M14 = 0.0f;
      result.M21 = 0.0f;
      result.M22 = num2;
      result.M23 = 0.0f;
      result.M24 = 0.0f;
      result.M31 = 0.0f;
      result.M32 = 0.0f;
      result.M33 = num3;
      result.M34 = 0.0f;
      result.M41 = 0.0f;
      result.M42 = 0.0f;
      result.M43 = 0.0f;
      result.M44 = 1f;
    }

    public static Matrix CreateScale(Vector3 scales)
    {
      float num1 = scales.X;
      float num2 = scales.Y;
      float num3 = scales.Z;
      Matrix matrix;
      matrix.M11 = num1;
      matrix.M12 = 0.0f;
      matrix.M13 = 0.0f;
      matrix.M14 = 0.0f;
      matrix.M21 = 0.0f;
      matrix.M22 = num2;
      matrix.M23 = 0.0f;
      matrix.M24 = 0.0f;
      matrix.M31 = 0.0f;
      matrix.M32 = 0.0f;
      matrix.M33 = num3;
      matrix.M34 = 0.0f;
      matrix.M41 = 0.0f;
      matrix.M42 = 0.0f;
      matrix.M43 = 0.0f;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateScale(ref Vector3 scales, out Matrix result)
    {
      float num1 = scales.X;
      float num2 = scales.Y;
      float num3 = scales.Z;
      result.M11 = num1;
      result.M12 = 0.0f;
      result.M13 = 0.0f;
      result.M14 = 0.0f;
      result.M21 = 0.0f;
      result.M22 = num2;
      result.M23 = 0.0f;
      result.M24 = 0.0f;
      result.M31 = 0.0f;
      result.M32 = 0.0f;
      result.M33 = num3;
      result.M34 = 0.0f;
      result.M41 = 0.0f;
      result.M42 = 0.0f;
      result.M43 = 0.0f;
      result.M44 = 1f;
    }

    public static Matrix CreateScale(float scale)
    {
      float num = scale;
      Matrix matrix;
      matrix.M11 = num;
      matrix.M12 = 0.0f;
      matrix.M13 = 0.0f;
      matrix.M14 = 0.0f;
      matrix.M21 = 0.0f;
      matrix.M22 = num;
      matrix.M23 = 0.0f;
      matrix.M24 = 0.0f;
      matrix.M31 = 0.0f;
      matrix.M32 = 0.0f;
      matrix.M33 = num;
      matrix.M34 = 0.0f;
      matrix.M41 = 0.0f;
      matrix.M42 = 0.0f;
      matrix.M43 = 0.0f;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateScale(float scale, out Matrix result)
    {
      float num = scale;
      result.M11 = num;
      result.M12 = 0.0f;
      result.M13 = 0.0f;
      result.M14 = 0.0f;
      result.M21 = 0.0f;
      result.M22 = num;
      result.M23 = 0.0f;
      result.M24 = 0.0f;
      result.M31 = 0.0f;
      result.M32 = 0.0f;
      result.M33 = num;
      result.M34 = 0.0f;
      result.M41 = 0.0f;
      result.M42 = 0.0f;
      result.M43 = 0.0f;
      result.M44 = 1f;
    }

    public static Matrix CreateRotationX(float radians)
    {
      float num1 = (float) Math.Cos((double) radians);
      float num2 = (float) Math.Sin((double) radians);
      Matrix matrix;
      matrix.M11 = 1f;
      matrix.M12 = 0.0f;
      matrix.M13 = 0.0f;
      matrix.M14 = 0.0f;
      matrix.M21 = 0.0f;
      matrix.M22 = num1;
      matrix.M23 = num2;
      matrix.M24 = 0.0f;
      matrix.M31 = 0.0f;
      matrix.M32 = -num2;
      matrix.M33 = num1;
      matrix.M34 = 0.0f;
      matrix.M41 = 0.0f;
      matrix.M42 = 0.0f;
      matrix.M43 = 0.0f;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateRotationX(float radians, out Matrix result)
    {
      float num1 = (float) Math.Cos((double) radians);
      float num2 = (float) Math.Sin((double) radians);
      result.M11 = 1f;
      result.M12 = 0.0f;
      result.M13 = 0.0f;
      result.M14 = 0.0f;
      result.M21 = 0.0f;
      result.M22 = num1;
      result.M23 = num2;
      result.M24 = 0.0f;
      result.M31 = 0.0f;
      result.M32 = -num2;
      result.M33 = num1;
      result.M34 = 0.0f;
      result.M41 = 0.0f;
      result.M42 = 0.0f;
      result.M43 = 0.0f;
      result.M44 = 1f;
    }

    public static Matrix CreateRotationY(float radians)
    {
      float num1 = (float) Math.Cos((double) radians);
      float num2 = (float) Math.Sin((double) radians);
      Matrix matrix;
      matrix.M11 = num1;
      matrix.M12 = 0.0f;
      matrix.M13 = -num2;
      matrix.M14 = 0.0f;
      matrix.M21 = 0.0f;
      matrix.M22 = 1f;
      matrix.M23 = 0.0f;
      matrix.M24 = 0.0f;
      matrix.M31 = num2;
      matrix.M32 = 0.0f;
      matrix.M33 = num1;
      matrix.M34 = 0.0f;
      matrix.M41 = 0.0f;
      matrix.M42 = 0.0f;
      matrix.M43 = 0.0f;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateRotationY(float radians, out Matrix result)
    {
      float num1 = (float) Math.Cos((double) radians);
      float num2 = (float) Math.Sin((double) radians);
      result.M11 = num1;
      result.M12 = 0.0f;
      result.M13 = -num2;
      result.M14 = 0.0f;
      result.M21 = 0.0f;
      result.M22 = 1f;
      result.M23 = 0.0f;
      result.M24 = 0.0f;
      result.M31 = num2;
      result.M32 = 0.0f;
      result.M33 = num1;
      result.M34 = 0.0f;
      result.M41 = 0.0f;
      result.M42 = 0.0f;
      result.M43 = 0.0f;
      result.M44 = 1f;
    }

    public static Matrix CreateRotationZ(float radians)
    {
      float num1 = (float) Math.Cos((double) radians);
      float num2 = (float) Math.Sin((double) radians);
      Matrix matrix;
      matrix.M11 = num1;
      matrix.M12 = num2;
      matrix.M13 = 0.0f;
      matrix.M14 = 0.0f;
      matrix.M21 = -num2;
      matrix.M22 = num1;
      matrix.M23 = 0.0f;
      matrix.M24 = 0.0f;
      matrix.M31 = 0.0f;
      matrix.M32 = 0.0f;
      matrix.M33 = 1f;
      matrix.M34 = 0.0f;
      matrix.M41 = 0.0f;
      matrix.M42 = 0.0f;
      matrix.M43 = 0.0f;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateRotationZ(float radians, out Matrix result)
    {
      float num1 = (float) Math.Cos((double) radians);
      float num2 = (float) Math.Sin((double) radians);
      result.M11 = num1;
      result.M12 = num2;
      result.M13 = 0.0f;
      result.M14 = 0.0f;
      result.M21 = -num2;
      result.M22 = num1;
      result.M23 = 0.0f;
      result.M24 = 0.0f;
      result.M31 = 0.0f;
      result.M32 = 0.0f;
      result.M33 = 1f;
      result.M34 = 0.0f;
      result.M41 = 0.0f;
      result.M42 = 0.0f;
      result.M43 = 0.0f;
      result.M44 = 1f;
    }

    public static Matrix CreateFromAxisAngle(Vector3 axis, float angle)
    {
      float num1 = axis.X;
      float num2 = axis.Y;
      float num3 = axis.Z;
      float num4 = (float) Math.Sin((double) angle);
      float num5 = (float) Math.Cos((double) angle);
      float num6 = num1 * num1;
      float num7 = num2 * num2;
      float num8 = num3 * num3;
      float num9 = num1 * num2;
      float num10 = num1 * num3;
      float num11 = num2 * num3;
      Matrix matrix;
      matrix.M11 = num6 + num5 * (1f - num6);
      matrix.M12 = (float) ((double) num9 - (double) num5 * (double) num9 + (double) num4 * (double) num3);
      matrix.M13 = (float) ((double) num10 - (double) num5 * (double) num10 - (double) num4 * (double) num2);
      matrix.M14 = 0.0f;
      matrix.M21 = (float) ((double) num9 - (double) num5 * (double) num9 - (double) num4 * (double) num3);
      matrix.M22 = num7 + num5 * (1f - num7);
      matrix.M23 = (float) ((double) num11 - (double) num5 * (double) num11 + (double) num4 * (double) num1);
      matrix.M24 = 0.0f;
      matrix.M31 = (float) ((double) num10 - (double) num5 * (double) num10 + (double) num4 * (double) num2);
      matrix.M32 = (float) ((double) num11 - (double) num5 * (double) num11 - (double) num4 * (double) num1);
      matrix.M33 = num8 + num5 * (1f - num8);
      matrix.M34 = 0.0f;
      matrix.M41 = 0.0f;
      matrix.M42 = 0.0f;
      matrix.M43 = 0.0f;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Matrix result)
    {
      float num1 = axis.X;
      float num2 = axis.Y;
      float num3 = axis.Z;
      float num4 = (float) Math.Sin((double) angle);
      float num5 = (float) Math.Cos((double) angle);
      float num6 = num1 * num1;
      float num7 = num2 * num2;
      float num8 = num3 * num3;
      float num9 = num1 * num2;
      float num10 = num1 * num3;
      float num11 = num2 * num3;
      result.M11 = num6 + num5 * (1f - num6);
      result.M12 = (float) ((double) num9 - (double) num5 * (double) num9 + (double) num4 * (double) num3);
      result.M13 = (float) ((double) num10 - (double) num5 * (double) num10 - (double) num4 * (double) num2);
      result.M14 = 0.0f;
      result.M21 = (float) ((double) num9 - (double) num5 * (double) num9 - (double) num4 * (double) num3);
      result.M22 = num7 + num5 * (1f - num7);
      result.M23 = (float) ((double) num11 - (double) num5 * (double) num11 + (double) num4 * (double) num1);
      result.M24 = 0.0f;
      result.M31 = (float) ((double) num10 - (double) num5 * (double) num10 + (double) num4 * (double) num2);
      result.M32 = (float) ((double) num11 - (double) num5 * (double) num11 - (double) num4 * (double) num1);
      result.M33 = num8 + num5 * (1f - num8);
      result.M34 = 0.0f;
      result.M41 = 0.0f;
      result.M42 = 0.0f;
      result.M43 = 0.0f;
      result.M44 = 1f;
    }

    public static Matrix CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
    {
      if ((double) fieldOfView <= 0.0 || (double) fieldOfView >= 3.14159274101257)
        throw new ArgumentOutOfRangeException("fieldOfView", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OutRangeFieldOfView", new object[1]
        {
          (object) "fieldOfView"
        }));
      else if ((double) nearPlaneDistance <= 0.0)
        throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "nearPlaneDistance"
        }));
      else if ((double) farPlaneDistance <= 0.0)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "farPlaneDistance"
        }));
      }
      else
      {
        if ((double) nearPlaneDistance >= (double) farPlaneDistance)
          throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
        float num1 = 1f / (float) Math.Tan((double) fieldOfView * 0.5);
        float num2 = num1 / aspectRatio;
        Matrix matrix;
        matrix.M11 = num2;
        matrix.M12 = matrix.M13 = matrix.M14 = 0.0f;
        matrix.M22 = num1;
        matrix.M21 = matrix.M23 = matrix.M24 = 0.0f;
        matrix.M31 = matrix.M32 = 0.0f;
        matrix.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        matrix.M34 = -1f;
        matrix.M41 = matrix.M42 = matrix.M44 = 0.0f;
        matrix.M43 = (float) ((double) nearPlaneDistance * (double) farPlaneDistance / ((double) nearPlaneDistance - (double) farPlaneDistance));
        return matrix;
      }
    }

    public static Matrix CreateFromPerspectiveFieldOfView(ref Matrix proj, float nearPlaneDistance, float farPlaneDistance)
    {
      Matrix matrix = proj;
      matrix.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
      matrix.M43 = (float) ((double) nearPlaneDistance * (double) farPlaneDistance / ((double) nearPlaneDistance - (double) farPlaneDistance));
      return matrix;
    }

    public static void CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
    {
      if ((double) fieldOfView <= 0.0 || (double) fieldOfView >= 3.14159274101257)
        throw new ArgumentOutOfRangeException("fieldOfView", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "OutRangeFieldOfView", new object[1]
        {
          (object) "fieldOfView"
        }));
      else if ((double) nearPlaneDistance <= 0.0)
        throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "nearPlaneDistance"
        }));
      else if ((double) farPlaneDistance <= 0.0)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "farPlaneDistance"
        }));
      }
      else
      {
        if ((double) nearPlaneDistance >= (double) farPlaneDistance)
          throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
        float num1 = 1f / (float) Math.Tan((double) fieldOfView * 0.5);
        float num2 = num1 / aspectRatio;
        result.M11 = num2;
        result.M12 = result.M13 = result.M14 = 0.0f;
        result.M22 = num1;
        result.M21 = result.M23 = result.M24 = 0.0f;
        result.M31 = result.M32 = 0.0f;
        result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        result.M34 = -1f;
        result.M41 = result.M42 = result.M44 = 0.0f;
        result.M43 = (float) ((double) nearPlaneDistance * (double) farPlaneDistance / ((double) nearPlaneDistance - (double) farPlaneDistance));
      }
    }

    public static Matrix CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
    {
      if ((double) nearPlaneDistance <= 0.0)
        throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "nearPlaneDistance"
        }));
      else if ((double) farPlaneDistance <= 0.0)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "farPlaneDistance"
        }));
      }
      else
      {
        if ((double) nearPlaneDistance >= (double) farPlaneDistance)
          throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
        Matrix matrix;
        matrix.M11 = 2f * nearPlaneDistance / width;
        matrix.M12 = matrix.M13 = matrix.M14 = 0.0f;
        matrix.M22 = 2f * nearPlaneDistance / height;
        matrix.M21 = matrix.M23 = matrix.M24 = 0.0f;
        matrix.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        matrix.M31 = matrix.M32 = 0.0f;
        matrix.M34 = -1f;
        matrix.M41 = matrix.M42 = matrix.M44 = 0.0f;
        matrix.M43 = (float) ((double) nearPlaneDistance * (double) farPlaneDistance / ((double) nearPlaneDistance - (double) farPlaneDistance));
        return matrix;
      }
    }

    public static void CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
    {
      if ((double) nearPlaneDistance <= 0.0)
        throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "nearPlaneDistance"
        }));
      else if ((double) farPlaneDistance <= 0.0)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "farPlaneDistance"
        }));
      }
      else
      {
        if ((double) nearPlaneDistance >= (double) farPlaneDistance)
          throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
        result.M11 = 2f * nearPlaneDistance / width;
        result.M12 = result.M13 = result.M14 = 0.0f;
        result.M22 = 2f * nearPlaneDistance / height;
        result.M21 = result.M23 = result.M24 = 0.0f;
        result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        result.M31 = result.M32 = 0.0f;
        result.M34 = -1f;
        result.M41 = result.M42 = result.M44 = 0.0f;
        result.M43 = (float) ((double) nearPlaneDistance * (double) farPlaneDistance / ((double) nearPlaneDistance - (double) farPlaneDistance));
      }
    }

    public static Matrix CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
    {
      if ((double) nearPlaneDistance <= 0.0)
        throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "nearPlaneDistance"
        }));
      else if ((double) farPlaneDistance <= 0.0)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "farPlaneDistance"
        }));
      }
      else
      {
        if ((double) nearPlaneDistance >= (double) farPlaneDistance)
          throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
        Matrix matrix;
        matrix.M11 = (float) (2.0 * (double) nearPlaneDistance / ((double) right - (double) left));
        matrix.M12 = matrix.M13 = matrix.M14 = 0.0f;
        matrix.M22 = (float) (2.0 * (double) nearPlaneDistance / ((double) top - (double) bottom));
        matrix.M21 = matrix.M23 = matrix.M24 = 0.0f;
        matrix.M31 = (float) (((double) left + (double) right) / ((double) right - (double) left));
        matrix.M32 = (float) (((double) top + (double) bottom) / ((double) top - (double) bottom));
        matrix.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        matrix.M34 = -1f;
        matrix.M43 = (float) ((double) nearPlaneDistance * (double) farPlaneDistance / ((double) nearPlaneDistance - (double) farPlaneDistance));
        matrix.M41 = matrix.M42 = matrix.M44 = 0.0f;
        return matrix;
      }
    }

    public static void CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
    {
      if ((double) nearPlaneDistance <= 0.0)
        throw new ArgumentOutOfRangeException("nearPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "nearPlaneDistance"
        }));
      else if ((double) farPlaneDistance <= 0.0)
      {
        throw new ArgumentOutOfRangeException("farPlaneDistance", string.Format((IFormatProvider) CultureInfo.CurrentCulture, "NegativePlaneDistance", new object[1]
        {
          (object) "farPlaneDistance"
        }));
      }
      else
      {
        if ((double) nearPlaneDistance >= (double) farPlaneDistance)
          throw new ArgumentOutOfRangeException("nearPlaneDistance", "OppositePlanes");
        result.M11 = (float) (2.0 * (double) nearPlaneDistance / ((double) right - (double) left));
        result.M12 = result.M13 = result.M14 = 0.0f;
        result.M22 = (float) (2.0 * (double) nearPlaneDistance / ((double) top - (double) bottom));
        result.M21 = result.M23 = result.M24 = 0.0f;
        result.M31 = (float) (((double) left + (double) right) / ((double) right - (double) left));
        result.M32 = (float) (((double) top + (double) bottom) / ((double) top - (double) bottom));
        result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
        result.M34 = -1f;
        result.M43 = (float) ((double) nearPlaneDistance * (double) farPlaneDistance / ((double) nearPlaneDistance - (double) farPlaneDistance));
        result.M41 = result.M42 = result.M44 = 0.0f;
      }
    }

    public static Matrix CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane)
    {
      Matrix matrix;
      matrix.M11 = 2f / width;
      matrix.M12 = matrix.M13 = matrix.M14 = 0.0f;
      matrix.M22 = 2f / height;
      matrix.M21 = matrix.M23 = matrix.M24 = 0.0f;
      matrix.M33 = (float) (1.0 / ((double) zNearPlane - (double) zFarPlane));
      matrix.M31 = matrix.M32 = matrix.M34 = 0.0f;
      matrix.M41 = matrix.M42 = 0.0f;
      matrix.M43 = zNearPlane / (zNearPlane - zFarPlane);
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane, out Matrix result)
    {
      result.M11 = 2f / width;
      result.M12 = result.M13 = result.M14 = 0.0f;
      result.M22 = 2f / height;
      result.M21 = result.M23 = result.M24 = 0.0f;
      result.M33 = (float) (1.0 / ((double) zNearPlane - (double) zFarPlane));
      result.M31 = result.M32 = result.M34 = 0.0f;
      result.M41 = result.M42 = 0.0f;
      result.M43 = zNearPlane / (zNearPlane - zFarPlane);
      result.M44 = 1f;
    }

    public static Matrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane)
    {
      Matrix matrix;
      matrix.M11 = (float) (2.0 / ((double) right - (double) left));
      matrix.M12 = matrix.M13 = matrix.M14 = 0.0f;
      matrix.M22 = (float) (2.0 / ((double) top - (double) bottom));
      matrix.M21 = matrix.M23 = matrix.M24 = 0.0f;
      matrix.M33 = (float) (1.0 / ((double) zNearPlane - (double) zFarPlane));
      matrix.M31 = matrix.M32 = matrix.M34 = 0.0f;
      matrix.M41 = (float) (((double) left + (double) right) / ((double) left - (double) right));
      matrix.M42 = (float) (((double) top + (double) bottom) / ((double) bottom - (double) top));
      matrix.M43 = zNearPlane / (zNearPlane - zFarPlane);
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane, out Matrix result)
    {
      result.M11 = (float) (2.0 / ((double) right - (double) left));
      result.M12 = result.M13 = result.M14 = 0.0f;
      result.M22 = (float) (2.0 / ((double) top - (double) bottom));
      result.M21 = result.M23 = result.M24 = 0.0f;
      result.M33 = (float) (1.0 / ((double) zNearPlane - (double) zFarPlane));
      result.M31 = result.M32 = result.M34 = 0.0f;
      result.M41 = (float) (((double) left + (double) right) / ((double) left - (double) right));
      result.M42 = (float) (((double) top + (double) bottom) / ((double) bottom - (double) top));
      result.M43 = zNearPlane / (zNearPlane - zFarPlane);
      result.M44 = 1f;
    }

    public static Matrix CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
    {
      Vector3 vector3_1 = Vector3.Normalize(cameraPosition - cameraTarget);
      Vector3 vector3_2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector3_1));
      Vector3 vector1 = Vector3.Cross(vector3_1, vector3_2);
      Matrix matrix;
      matrix.M11 = vector3_2.X;
      matrix.M12 = vector1.X;
      matrix.M13 = vector3_1.X;
      matrix.M14 = 0.0f;
      matrix.M21 = vector3_2.Y;
      matrix.M22 = vector1.Y;
      matrix.M23 = vector3_1.Y;
      matrix.M24 = 0.0f;
      matrix.M31 = vector3_2.Z;
      matrix.M32 = vector1.Z;
      matrix.M33 = vector3_1.Z;
      matrix.M34 = 0.0f;
      matrix.M41 = -Vector3.Dot(vector3_2, cameraPosition);
      matrix.M42 = -Vector3.Dot(vector1, cameraPosition);
      matrix.M43 = -Vector3.Dot(vector3_1, cameraPosition);
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateLookAt(ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix result)
    {
      Vector3 vector3_1 = Vector3.Normalize(cameraPosition - cameraTarget);
      Vector3 vector3_2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector3_1));
      Vector3 vector1 = Vector3.Cross(vector3_1, vector3_2);
      result.M11 = vector3_2.X;
      result.M12 = vector1.X;
      result.M13 = vector3_1.X;
      result.M14 = 0.0f;
      result.M21 = vector3_2.Y;
      result.M22 = vector1.Y;
      result.M23 = vector3_1.Y;
      result.M24 = 0.0f;
      result.M31 = vector3_2.Z;
      result.M32 = vector1.Z;
      result.M33 = vector3_1.Z;
      result.M34 = 0.0f;
      result.M41 = -Vector3.Dot(vector3_2, cameraPosition);
      result.M42 = -Vector3.Dot(vector1, cameraPosition);
      result.M43 = -Vector3.Dot(vector3_1, cameraPosition);
      result.M44 = 1f;
    }

    public static Matrix CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
    {
      Vector3 vector3_1 = Vector3.Normalize(-forward);
      Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector3_1));
      Vector3 vector3_2 = Vector3.Cross(vector3_1, vector2);
      Matrix matrix;
      matrix.M11 = vector2.X;
      matrix.M12 = vector2.Y;
      matrix.M13 = vector2.Z;
      matrix.M14 = 0.0f;
      matrix.M21 = vector3_2.X;
      matrix.M22 = vector3_2.Y;
      matrix.M23 = vector3_2.Z;
      matrix.M24 = 0.0f;
      matrix.M31 = vector3_1.X;
      matrix.M32 = vector3_1.Y;
      matrix.M33 = vector3_1.Z;
      matrix.M34 = 0.0f;
      matrix.M41 = position.X;
      matrix.M42 = position.Y;
      matrix.M43 = position.Z;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateWorld(ref Vector3 position, ref Vector3 forward, ref Vector3 up, out Matrix result)
    {
      Vector3 vector3_1 = Vector3.Normalize(-forward);
      Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector3_1));
      Vector3 vector3_2 = Vector3.Cross(vector3_1, vector2);
      result.M11 = vector2.X;
      result.M12 = vector2.Y;
      result.M13 = vector2.Z;
      result.M14 = 0.0f;
      result.M21 = vector3_2.X;
      result.M22 = vector3_2.Y;
      result.M23 = vector3_2.Z;
      result.M24 = 0.0f;
      result.M31 = vector3_1.X;
      result.M32 = vector3_1.Y;
      result.M33 = vector3_1.Z;
      result.M34 = 0.0f;
      result.M41 = position.X;
      result.M42 = position.Y;
      result.M43 = position.Z;
      result.M44 = 1f;
    }

    public static Matrix CreateFromQuaternion(Quaternion quaternion)
    {
      float num1 = quaternion.X * quaternion.X;
      float num2 = quaternion.Y * quaternion.Y;
      float num3 = quaternion.Z * quaternion.Z;
      float num4 = quaternion.X * quaternion.Y;
      float num5 = quaternion.Z * quaternion.W;
      float num6 = quaternion.Z * quaternion.X;
      float num7 = quaternion.Y * quaternion.W;
      float num8 = quaternion.Y * quaternion.Z;
      float num9 = quaternion.X * quaternion.W;
      Matrix matrix;
      matrix.M11 = (float) (1.0 - 2.0 * ((double) num2 + (double) num3));
      matrix.M12 = (float) (2.0 * ((double) num4 + (double) num5));
      matrix.M13 = (float) (2.0 * ((double) num6 - (double) num7));
      matrix.M14 = 0.0f;
      matrix.M21 = (float) (2.0 * ((double) num4 - (double) num5));
      matrix.M22 = (float) (1.0 - 2.0 * ((double) num3 + (double) num1));
      matrix.M23 = (float) (2.0 * ((double) num8 + (double) num9));
      matrix.M24 = 0.0f;
      matrix.M31 = (float) (2.0 * ((double) num6 + (double) num7));
      matrix.M32 = (float) (2.0 * ((double) num8 - (double) num9));
      matrix.M33 = (float) (1.0 - 2.0 * ((double) num2 + (double) num1));
      matrix.M34 = 0.0f;
      matrix.M41 = 0.0f;
      matrix.M42 = 0.0f;
      matrix.M43 = 0.0f;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateFromQuaternion(ref Quaternion quaternion, out Matrix result)
    {
      float num1 = quaternion.X * quaternion.X;
      float num2 = quaternion.Y * quaternion.Y;
      float num3 = quaternion.Z * quaternion.Z;
      float num4 = quaternion.X * quaternion.Y;
      float num5 = quaternion.Z * quaternion.W;
      float num6 = quaternion.Z * quaternion.X;
      float num7 = quaternion.Y * quaternion.W;
      float num8 = quaternion.Y * quaternion.Z;
      float num9 = quaternion.X * quaternion.W;
      result.M11 = (float) (1.0 - 2.0 * ((double) num2 + (double) num3));
      result.M12 = (float) (2.0 * ((double) num4 + (double) num5));
      result.M13 = (float) (2.0 * ((double) num6 - (double) num7));
      result.M14 = 0.0f;
      result.M21 = (float) (2.0 * ((double) num4 - (double) num5));
      result.M22 = (float) (1.0 - 2.0 * ((double) num3 + (double) num1));
      result.M23 = (float) (2.0 * ((double) num8 + (double) num9));
      result.M24 = 0.0f;
      result.M31 = (float) (2.0 * ((double) num6 + (double) num7));
      result.M32 = (float) (2.0 * ((double) num8 - (double) num9));
      result.M33 = (float) (1.0 - 2.0 * ((double) num2 + (double) num1));
      result.M34 = 0.0f;
      result.M41 = 0.0f;
      result.M42 = 0.0f;
      result.M43 = 0.0f;
      result.M44 = 1f;
    }

    public static Matrix CreateFromYawPitchRoll(float yaw, float pitch, float roll)
    {
      Quaternion result1;
      Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out result1);
      Matrix result2;
      Matrix.CreateFromQuaternion(ref result1, out result2);
      return result2;
    }

    public static void CreateFromYawPitchRoll(float yaw, float pitch, float roll, out Matrix result)
    {
      Quaternion result1;
      Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, out result1);
      Matrix.CreateFromQuaternion(ref result1, out result);
    }

    public static Matrix CreateFromTransformScale(Quaternion orientation, Vector3 position, Vector3 scale)
    {
      Matrix fromQuaternion = Matrix.CreateFromQuaternion(orientation);
      fromQuaternion.Translation = position;
      Matrix.Rescale(ref fromQuaternion, ref scale);
      return fromQuaternion;
    }

    public static Matrix CreateShadow(Vector3 lightDirection, Plane plane)
    {
      Plane result;
      Plane.Normalize(ref plane, out result);
      float num1 = (float) ((double) result.Normal.X * (double) lightDirection.X + (double) result.Normal.Y * (double) lightDirection.Y + (double) result.Normal.Z * (double) lightDirection.Z);
      float num2 = -result.Normal.X;
      float num3 = -result.Normal.Y;
      float num4 = -result.Normal.Z;
      float num5 = -result.D;
      Matrix matrix;
      matrix.M11 = num2 * lightDirection.X + num1;
      matrix.M21 = num3 * lightDirection.X;
      matrix.M31 = num4 * lightDirection.X;
      matrix.M41 = num5 * lightDirection.X;
      matrix.M12 = num2 * lightDirection.Y;
      matrix.M22 = num3 * lightDirection.Y + num1;
      matrix.M32 = num4 * lightDirection.Y;
      matrix.M42 = num5 * lightDirection.Y;
      matrix.M13 = num2 * lightDirection.Z;
      matrix.M23 = num3 * lightDirection.Z;
      matrix.M33 = num4 * lightDirection.Z + num1;
      matrix.M43 = num5 * lightDirection.Z;
      matrix.M14 = 0.0f;
      matrix.M24 = 0.0f;
      matrix.M34 = 0.0f;
      matrix.M44 = num1;
      return matrix;
    }

    public static void CreateShadow(ref Vector3 lightDirection, ref Plane plane, out Matrix result)
    {
      Plane result1;
      Plane.Normalize(ref plane, out result1);
      float num1 = (float) ((double) result1.Normal.X * (double) lightDirection.X + (double) result1.Normal.Y * (double) lightDirection.Y + (double) result1.Normal.Z * (double) lightDirection.Z);
      float num2 = -result1.Normal.X;
      float num3 = -result1.Normal.Y;
      float num4 = -result1.Normal.Z;
      float num5 = -result1.D;
      result.M11 = num2 * lightDirection.X + num1;
      result.M21 = num3 * lightDirection.X;
      result.M31 = num4 * lightDirection.X;
      result.M41 = num5 * lightDirection.X;
      result.M12 = num2 * lightDirection.Y;
      result.M22 = num3 * lightDirection.Y + num1;
      result.M32 = num4 * lightDirection.Y;
      result.M42 = num5 * lightDirection.Y;
      result.M13 = num2 * lightDirection.Z;
      result.M23 = num3 * lightDirection.Z;
      result.M33 = num4 * lightDirection.Z + num1;
      result.M43 = num5 * lightDirection.Z;
      result.M14 = 0.0f;
      result.M24 = 0.0f;
      result.M34 = 0.0f;
      result.M44 = num1;
    }

    public static Matrix CreateReflection(Plane value)
    {
      value.Normalize();
      float num1 = value.Normal.X;
      float num2 = value.Normal.Y;
      float num3 = value.Normal.Z;
      float num4 = -2f * num1;
      float num5 = -2f * num2;
      float num6 = -2f * num3;
      Matrix matrix;
      matrix.M11 = (float) ((double) num4 * (double) num1 + 1.0);
      matrix.M12 = num5 * num1;
      matrix.M13 = num6 * num1;
      matrix.M14 = 0.0f;
      matrix.M21 = num4 * num2;
      matrix.M22 = (float) ((double) num5 * (double) num2 + 1.0);
      matrix.M23 = num6 * num2;
      matrix.M24 = 0.0f;
      matrix.M31 = num4 * num3;
      matrix.M32 = num5 * num3;
      matrix.M33 = (float) ((double) num6 * (double) num3 + 1.0);
      matrix.M34 = 0.0f;
      matrix.M41 = num4 * value.D;
      matrix.M42 = num5 * value.D;
      matrix.M43 = num6 * value.D;
      matrix.M44 = 1f;
      return matrix;
    }

    public static void CreateReflection(ref Plane value, out Matrix result)
    {
      Plane result1;
      Plane.Normalize(ref value, out result1);
      value.Normalize();
      float num1 = result1.Normal.X;
      float num2 = result1.Normal.Y;
      float num3 = result1.Normal.Z;
      float num4 = -2f * num1;
      float num5 = -2f * num2;
      float num6 = -2f * num3;
      result.M11 = (float) ((double) num4 * (double) num1 + 1.0);
      result.M12 = num5 * num1;
      result.M13 = num6 * num1;
      result.M14 = 0.0f;
      result.M21 = num4 * num2;
      result.M22 = (float) ((double) num5 * (double) num2 + 1.0);
      result.M23 = num6 * num2;
      result.M24 = 0.0f;
      result.M31 = num4 * num3;
      result.M32 = num5 * num3;
      result.M33 = (float) ((double) num6 * (double) num3 + 1.0);
      result.M34 = 0.0f;
      result.M41 = num4 * result1.D;
      result.M42 = num5 * result1.D;
      result.M43 = num6 * result1.D;
      result.M44 = 1f;
    }

    public static Matrix Transform(Matrix value, Quaternion rotation)
    {
      float num1 = rotation.X + rotation.X;
      float num2 = rotation.Y + rotation.Y;
      float num3 = rotation.Z + rotation.Z;
      float num4 = rotation.W * num1;
      float num5 = rotation.W * num2;
      float num6 = rotation.W * num3;
      float num7 = rotation.X * num1;
      float num8 = rotation.X * num2;
      float num9 = rotation.X * num3;
      float num10 = rotation.Y * num2;
      float num11 = rotation.Y * num3;
      float num12 = rotation.Z * num3;
      float num13 = 1f - num10 - num12;
      float num14 = num8 - num6;
      float num15 = num9 + num5;
      float num16 = num8 + num6;
      float num17 = 1f - num7 - num12;
      float num18 = num11 - num4;
      float num19 = num9 - num5;
      float num20 = num11 + num4;
      float num21 = 1f - num7 - num10;
      Matrix matrix;
      matrix.M11 = (float) ((double) value.M11 * (double) num13 + (double) value.M12 * (double) num14 + (double) value.M13 * (double) num15);
      matrix.M12 = (float) ((double) value.M11 * (double) num16 + (double) value.M12 * (double) num17 + (double) value.M13 * (double) num18);
      matrix.M13 = (float) ((double) value.M11 * (double) num19 + (double) value.M12 * (double) num20 + (double) value.M13 * (double) num21);
      matrix.M14 = value.M14;
      matrix.M21 = (float) ((double) value.M21 * (double) num13 + (double) value.M22 * (double) num14 + (double) value.M23 * (double) num15);
      matrix.M22 = (float) ((double) value.M21 * (double) num16 + (double) value.M22 * (double) num17 + (double) value.M23 * (double) num18);
      matrix.M23 = (float) ((double) value.M21 * (double) num19 + (double) value.M22 * (double) num20 + (double) value.M23 * (double) num21);
      matrix.M24 = value.M24;
      matrix.M31 = (float) ((double) value.M31 * (double) num13 + (double) value.M32 * (double) num14 + (double) value.M33 * (double) num15);
      matrix.M32 = (float) ((double) value.M31 * (double) num16 + (double) value.M32 * (double) num17 + (double) value.M33 * (double) num18);
      matrix.M33 = (float) ((double) value.M31 * (double) num19 + (double) value.M32 * (double) num20 + (double) value.M33 * (double) num21);
      matrix.M34 = value.M34;
      matrix.M41 = (float) ((double) value.M41 * (double) num13 + (double) value.M42 * (double) num14 + (double) value.M43 * (double) num15);
      matrix.M42 = (float) ((double) value.M41 * (double) num16 + (double) value.M42 * (double) num17 + (double) value.M43 * (double) num18);
      matrix.M43 = (float) ((double) value.M41 * (double) num19 + (double) value.M42 * (double) num20 + (double) value.M43 * (double) num21);
      matrix.M44 = value.M44;
      return matrix;
    }

    public static void Transform(ref Matrix value, ref Quaternion rotation, out Matrix result)
    {
      float num1 = rotation.X + rotation.X;
      float num2 = rotation.Y + rotation.Y;
      float num3 = rotation.Z + rotation.Z;
      float num4 = rotation.W * num1;
      float num5 = rotation.W * num2;
      float num6 = rotation.W * num3;
      float num7 = rotation.X * num1;
      float num8 = rotation.X * num2;
      float num9 = rotation.X * num3;
      float num10 = rotation.Y * num2;
      float num11 = rotation.Y * num3;
      float num12 = rotation.Z * num3;
      float num13 = 1f - num10 - num12;
      float num14 = num8 - num6;
      float num15 = num9 + num5;
      float num16 = num8 + num6;
      float num17 = 1f - num7 - num12;
      float num18 = num11 - num4;
      float num19 = num9 - num5;
      float num20 = num11 + num4;
      float num21 = 1f - num7 - num10;
      float num22 = (float) ((double) value.M11 * (double) num13 + (double) value.M12 * (double) num14 + (double) value.M13 * (double) num15);
      float num23 = (float) ((double) value.M11 * (double) num16 + (double) value.M12 * (double) num17 + (double) value.M13 * (double) num18);
      float num24 = (float) ((double) value.M11 * (double) num19 + (double) value.M12 * (double) num20 + (double) value.M13 * (double) num21);
      float num25 = value.M14;
      float num26 = (float) ((double) value.M21 * (double) num13 + (double) value.M22 * (double) num14 + (double) value.M23 * (double) num15);
      float num27 = (float) ((double) value.M21 * (double) num16 + (double) value.M22 * (double) num17 + (double) value.M23 * (double) num18);
      float num28 = (float) ((double) value.M21 * (double) num19 + (double) value.M22 * (double) num20 + (double) value.M23 * (double) num21);
      float num29 = value.M24;
      float num30 = (float) ((double) value.M31 * (double) num13 + (double) value.M32 * (double) num14 + (double) value.M33 * (double) num15);
      float num31 = (float) ((double) value.M31 * (double) num16 + (double) value.M32 * (double) num17 + (double) value.M33 * (double) num18);
      float num32 = (float) ((double) value.M31 * (double) num19 + (double) value.M32 * (double) num20 + (double) value.M33 * (double) num21);
      float num33 = value.M34;
      float num34 = (float) ((double) value.M41 * (double) num13 + (double) value.M42 * (double) num14 + (double) value.M43 * (double) num15);
      float num35 = (float) ((double) value.M41 * (double) num16 + (double) value.M42 * (double) num17 + (double) value.M43 * (double) num18);
      float num36 = (float) ((double) value.M41 * (double) num19 + (double) value.M42 * (double) num20 + (double) value.M43 * (double) num21);
      float num37 = value.M44;
      result.M11 = num22;
      result.M12 = num23;
      result.M13 = num24;
      result.M14 = num25;
      result.M21 = num26;
      result.M22 = num27;
      result.M23 = num28;
      result.M24 = num29;
      result.M31 = num30;
      result.M32 = num31;
      result.M33 = num32;
      result.M34 = num33;
      result.M41 = num34;
      result.M42 = num35;
      result.M43 = num36;
      result.M44 = num37;
    }

    public unsafe Vector4 GetRow(int row)
    {
        throw new System.NotImplementedException();
    }

    public unsafe void SetRow(int row, Vector4 value)
    {
        throw new System.NotImplementedException();
    }

    public override string ToString()
    {
      CultureInfo currentCulture = CultureInfo.CurrentCulture;
      return "{ " + string.Format((IFormatProvider) currentCulture, "{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", (object) this.M11.ToString((IFormatProvider) currentCulture), (object) this.M12.ToString((IFormatProvider) currentCulture), (object) this.M13.ToString((IFormatProvider) currentCulture), (object) this.M14.ToString((IFormatProvider) currentCulture)) + string.Format((IFormatProvider) currentCulture, "{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", (object) this.M21.ToString((IFormatProvider) currentCulture), (object) this.M22.ToString((IFormatProvider) currentCulture), (object) this.M23.ToString((IFormatProvider) currentCulture), (object) this.M24.ToString((IFormatProvider) currentCulture)) + string.Format((IFormatProvider) currentCulture, "{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", (object) this.M31.ToString((IFormatProvider) currentCulture), (object) this.M32.ToString((IFormatProvider) currentCulture), (object) this.M33.ToString((IFormatProvider) currentCulture), (object) this.M34.ToString((IFormatProvider) currentCulture)) + string.Format((IFormatProvider) currentCulture, "{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", (object) this.M41.ToString((IFormatProvider) currentCulture), (object) this.M42.ToString((IFormatProvider) currentCulture), (object) this.M43.ToString((IFormatProvider) currentCulture), (object) this.M44.ToString((IFormatProvider) currentCulture)) + "}";
    }

    public bool Equals(Matrix other)
    {
      if ((double) this.M11 == (double) other.M11 && (double) this.M22 == (double) other.M22 && ((double) this.M33 == (double) other.M33 && (double) this.M44 == (double) other.M44) && ((double) this.M12 == (double) other.M12 && (double) this.M13 == (double) other.M13 && ((double) this.M14 == (double) other.M14 && (double) this.M21 == (double) other.M21)) && ((double) this.M23 == (double) other.M23 && (double) this.M24 == (double) other.M24 && ((double) this.M31 == (double) other.M31 && (double) this.M32 == (double) other.M32) && ((double) this.M34 == (double) other.M34 && (double) this.M41 == (double) other.M41 && (double) this.M42 == (double) other.M42)))
        return (double) this.M43 == (double) other.M43;
      else
        return false;
    }

    public bool EqualsFast(ref Matrix other, float epsilon = 0.0001f)
    {
      float num1 = this.M21 - other.M21;
      float num2 = this.M22 - other.M22;
      float num3 = this.M23 - other.M23;
      float num4 = this.M31 - other.M31;
      float num5 = this.M32 - other.M32;
      float num6 = this.M33 - other.M33;
      float num7 = this.M41 - other.M41;
      float num8 = this.M42 - other.M42;
      float num9 = this.M43 - other.M43;
      float num10 = epsilon * epsilon;
      return (double) num1 * (double) num1 + (double) num2 * (double) num2 + (double) num3 * (double) num3 < (double) num10 & (double) num4 * (double) num4 + (double) num5 * (double) num5 + (double) num6 * (double) num6 < (double) num10 & (double) num7 * (double) num7 + (double) num8 * (double) num8 + (double) num9 * (double) num9 < (double) num10;
    }

    public override bool Equals(object obj)
    {
      bool flag = false;
      if (obj is Matrix)
        flag = this.Equals((Matrix) obj);
      return flag;
    }

    public override int GetHashCode()
    {
      return this.M11.GetHashCode() + this.M12.GetHashCode() + this.M13.GetHashCode() + this.M14.GetHashCode() + this.M21.GetHashCode() + this.M22.GetHashCode() + this.M23.GetHashCode() + this.M24.GetHashCode() + this.M31.GetHashCode() + this.M32.GetHashCode() + this.M33.GetHashCode() + this.M34.GetHashCode() + this.M41.GetHashCode() + this.M42.GetHashCode() + this.M43.GetHashCode() + this.M44.GetHashCode();
    }

    public static Matrix Transpose(Matrix matrix)
    {
      Matrix matrix1;
      matrix1.M11 = matrix.M11;
      matrix1.M12 = matrix.M21;
      matrix1.M13 = matrix.M31;
      matrix1.M14 = matrix.M41;
      matrix1.M21 = matrix.M12;
      matrix1.M22 = matrix.M22;
      matrix1.M23 = matrix.M32;
      matrix1.M24 = matrix.M42;
      matrix1.M31 = matrix.M13;
      matrix1.M32 = matrix.M23;
      matrix1.M33 = matrix.M33;
      matrix1.M34 = matrix.M43;
      matrix1.M41 = matrix.M14;
      matrix1.M42 = matrix.M24;
      matrix1.M43 = matrix.M34;
      matrix1.M44 = matrix.M44;
      return matrix1;
    }

    public static void Transpose(ref Matrix matrix, out Matrix result)
    {
      float num1 = matrix.M11;
      float num2 = matrix.M12;
      float num3 = matrix.M13;
      float num4 = matrix.M14;
      float num5 = matrix.M21;
      float num6 = matrix.M22;
      float num7 = matrix.M23;
      float num8 = matrix.M24;
      float num9 = matrix.M31;
      float num10 = matrix.M32;
      float num11 = matrix.M33;
      float num12 = matrix.M34;
      float num13 = matrix.M41;
      float num14 = matrix.M42;
      float num15 = matrix.M43;
      float num16 = matrix.M44;
      result.M11 = num1;
      result.M12 = num5;
      result.M13 = num9;
      result.M14 = num13;
      result.M21 = num2;
      result.M22 = num6;
      result.M23 = num10;
      result.M24 = num14;
      result.M31 = num3;
      result.M32 = num7;
      result.M33 = num11;
      result.M34 = num15;
      result.M41 = num4;
      result.M42 = num8;
      result.M43 = num12;
      result.M44 = num16;
    }

    public float Determinant()
    {
      float num1 = this.M11;
      float num2 = this.M12;
      float num3 = this.M13;
      float num4 = this.M14;
      float num5 = this.M21;
      float num6 = this.M22;
      float num7 = this.M23;
      float num8 = this.M24;
      float num9 = this.M31;
      float num10 = this.M32;
      float num11 = this.M33;
      float num12 = this.M34;
      float num13 = this.M41;
      float num14 = this.M42;
      float num15 = this.M43;
      float num16 = this.M44;
      float num17 = (float) ((double) num11 * (double) num16 - (double) num12 * (double) num15);
      float num18 = (float) ((double) num10 * (double) num16 - (double) num12 * (double) num14);
      float num19 = (float) ((double) num10 * (double) num15 - (double) num11 * (double) num14);
      float num20 = (float) ((double) num9 * (double) num16 - (double) num12 * (double) num13);
      float num21 = (float) ((double) num9 * (double) num15 - (double) num11 * (double) num13);
      float num22 = (float) ((double) num9 * (double) num14 - (double) num10 * (double) num13);
      return (float) ((double) num1 * ((double) num6 * (double) num17 - (double) num7 * (double) num18 + (double) num8 * (double) num19) - (double) num2 * ((double) num5 * (double) num17 - (double) num7 * (double) num20 + (double) num8 * (double) num21) + (double) num3 * ((double) num5 * (double) num18 - (double) num6 * (double) num20 + (double) num8 * (double) num22) - (double) num4 * ((double) num5 * (double) num19 - (double) num6 * (double) num21 + (double) num7 * (double) num22));
    }

    public static Matrix Invert(Matrix matrix)
    {
      float num1 = matrix.M11;
      float num2 = matrix.M12;
      float num3 = matrix.M13;
      float num4 = matrix.M14;
      float num5 = matrix.M21;
      float num6 = matrix.M22;
      float num7 = matrix.M23;
      float num8 = matrix.M24;
      float num9 = matrix.M31;
      float num10 = matrix.M32;
      float num11 = matrix.M33;
      float num12 = matrix.M34;
      float num13 = matrix.M41;
      float num14 = matrix.M42;
      float num15 = matrix.M43;
      float num16 = matrix.M44;
      float num17 = (float) ((double) num11 * (double) num16 - (double) num12 * (double) num15);
      float num18 = (float) ((double) num10 * (double) num16 - (double) num12 * (double) num14);
      float num19 = (float) ((double) num10 * (double) num15 - (double) num11 * (double) num14);
      float num20 = (float) ((double) num9 * (double) num16 - (double) num12 * (double) num13);
      float num21 = (float) ((double) num9 * (double) num15 - (double) num11 * (double) num13);
      float num22 = (float) ((double) num9 * (double) num14 - (double) num10 * (double) num13);
      float num23 = (float) ((double) num6 * (double) num17 - (double) num7 * (double) num18 + (double) num8 * (double) num19);
      float num24 = (float) -((double) num5 * (double) num17 - (double) num7 * (double) num20 + (double) num8 * (double) num21);
      float num25 = (float) ((double) num5 * (double) num18 - (double) num6 * (double) num20 + (double) num8 * (double) num22);
      float num26 = (float) -((double) num5 * (double) num19 - (double) num6 * (double) num21 + (double) num7 * (double) num22);
      float num27 = (float) (1.0 / ((double) num1 * (double) num23 + (double) num2 * (double) num24 + (double) num3 * (double) num25 + (double) num4 * (double) num26));
      Matrix matrix1;
      matrix1.M11 = num23 * num27;
      matrix1.M21 = num24 * num27;
      matrix1.M31 = num25 * num27;
      matrix1.M41 = num26 * num27;
      matrix1.M12 = (float) -((double) num2 * (double) num17 - (double) num3 * (double) num18 + (double) num4 * (double) num19) * num27;
      matrix1.M22 = (float) ((double) num1 * (double) num17 - (double) num3 * (double) num20 + (double) num4 * (double) num21) * num27;
      matrix1.M32 = (float) -((double) num1 * (double) num18 - (double) num2 * (double) num20 + (double) num4 * (double) num22) * num27;
      matrix1.M42 = (float) ((double) num1 * (double) num19 - (double) num2 * (double) num21 + (double) num3 * (double) num22) * num27;
      float num28 = (float) ((double) num7 * (double) num16 - (double) num8 * (double) num15);
      float num29 = (float) ((double) num6 * (double) num16 - (double) num8 * (double) num14);
      float num30 = (float) ((double) num6 * (double) num15 - (double) num7 * (double) num14);
      float num31 = (float) ((double) num5 * (double) num16 - (double) num8 * (double) num13);
      float num32 = (float) ((double) num5 * (double) num15 - (double) num7 * (double) num13);
      float num33 = (float) ((double) num5 * (double) num14 - (double) num6 * (double) num13);
      matrix1.M13 = (float) ((double) num2 * (double) num28 - (double) num3 * (double) num29 + (double) num4 * (double) num30) * num27;
      matrix1.M23 = (float) -((double) num1 * (double) num28 - (double) num3 * (double) num31 + (double) num4 * (double) num32) * num27;
      matrix1.M33 = (float) ((double) num1 * (double) num29 - (double) num2 * (double) num31 + (double) num4 * (double) num33) * num27;
      matrix1.M43 = (float) -((double) num1 * (double) num30 - (double) num2 * (double) num32 + (double) num3 * (double) num33) * num27;
      float num34 = (float) ((double) num7 * (double) num12 - (double) num8 * (double) num11);
      float num35 = (float) ((double) num6 * (double) num12 - (double) num8 * (double) num10);
      float num36 = (float) ((double) num6 * (double) num11 - (double) num7 * (double) num10);
      float num37 = (float) ((double) num5 * (double) num12 - (double) num8 * (double) num9);
      float num38 = (float) ((double) num5 * (double) num11 - (double) num7 * (double) num9);
      float num39 = (float) ((double) num5 * (double) num10 - (double) num6 * (double) num9);
      matrix1.M14 = (float) -((double) num2 * (double) num34 - (double) num3 * (double) num35 + (double) num4 * (double) num36) * num27;
      matrix1.M24 = (float) ((double) num1 * (double) num34 - (double) num3 * (double) num37 + (double) num4 * (double) num38) * num27;
      matrix1.M34 = (float) -((double) num1 * (double) num35 - (double) num2 * (double) num37 + (double) num4 * (double) num39) * num27;
      matrix1.M44 = (float) ((double) num1 * (double) num36 - (double) num2 * (double) num38 + (double) num3 * (double) num39) * num27;
      return matrix1;
    }

    public static void Invert(ref Matrix matrix, out Matrix result)
    {
      float num1 = matrix.M11;
      float num2 = matrix.M12;
      float num3 = matrix.M13;
      float num4 = matrix.M14;
      float num5 = matrix.M21;
      float num6 = matrix.M22;
      float num7 = matrix.M23;
      float num8 = matrix.M24;
      float num9 = matrix.M31;
      float num10 = matrix.M32;
      float num11 = matrix.M33;
      float num12 = matrix.M34;
      float num13 = matrix.M41;
      float num14 = matrix.M42;
      float num15 = matrix.M43;
      float num16 = matrix.M44;
      float num17 = (float) ((double) num11 * (double) num16 - (double) num12 * (double) num15);
      float num18 = (float) ((double) num10 * (double) num16 - (double) num12 * (double) num14);
      float num19 = (float) ((double) num10 * (double) num15 - (double) num11 * (double) num14);
      float num20 = (float) ((double) num9 * (double) num16 - (double) num12 * (double) num13);
      float num21 = (float) ((double) num9 * (double) num15 - (double) num11 * (double) num13);
      float num22 = (float) ((double) num9 * (double) num14 - (double) num10 * (double) num13);
      float num23 = (float) ((double) num6 * (double) num17 - (double) num7 * (double) num18 + (double) num8 * (double) num19);
      float num24 = (float) -((double) num5 * (double) num17 - (double) num7 * (double) num20 + (double) num8 * (double) num21);
      float num25 = (float) ((double) num5 * (double) num18 - (double) num6 * (double) num20 + (double) num8 * (double) num22);
      float num26 = (float) -((double) num5 * (double) num19 - (double) num6 * (double) num21 + (double) num7 * (double) num22);
      float num27 = (float) (1.0 / ((double) num1 * (double) num23 + (double) num2 * (double) num24 + (double) num3 * (double) num25 + (double) num4 * (double) num26));
      result.M11 = num23 * num27;
      result.M21 = num24 * num27;
      result.M31 = num25 * num27;
      result.M41 = num26 * num27;
      result.M12 = (float) -((double) num2 * (double) num17 - (double) num3 * (double) num18 + (double) num4 * (double) num19) * num27;
      result.M22 = (float) ((double) num1 * (double) num17 - (double) num3 * (double) num20 + (double) num4 * (double) num21) * num27;
      result.M32 = (float) -((double) num1 * (double) num18 - (double) num2 * (double) num20 + (double) num4 * (double) num22) * num27;
      result.M42 = (float) ((double) num1 * (double) num19 - (double) num2 * (double) num21 + (double) num3 * (double) num22) * num27;
      float num28 = (float) ((double) num7 * (double) num16 - (double) num8 * (double) num15);
      float num29 = (float) ((double) num6 * (double) num16 - (double) num8 * (double) num14);
      float num30 = (float) ((double) num6 * (double) num15 - (double) num7 * (double) num14);
      float num31 = (float) ((double) num5 * (double) num16 - (double) num8 * (double) num13);
      float num32 = (float) ((double) num5 * (double) num15 - (double) num7 * (double) num13);
      float num33 = (float) ((double) num5 * (double) num14 - (double) num6 * (double) num13);
      result.M13 = (float) ((double) num2 * (double) num28 - (double) num3 * (double) num29 + (double) num4 * (double) num30) * num27;
      result.M23 = (float) -((double) num1 * (double) num28 - (double) num3 * (double) num31 + (double) num4 * (double) num32) * num27;
      result.M33 = (float) ((double) num1 * (double) num29 - (double) num2 * (double) num31 + (double) num4 * (double) num33) * num27;
      result.M43 = (float) -((double) num1 * (double) num30 - (double) num2 * (double) num32 + (double) num3 * (double) num33) * num27;
      float num34 = (float) ((double) num7 * (double) num12 - (double) num8 * (double) num11);
      float num35 = (float) ((double) num6 * (double) num12 - (double) num8 * (double) num10);
      float num36 = (float) ((double) num6 * (double) num11 - (double) num7 * (double) num10);
      float num37 = (float) ((double) num5 * (double) num12 - (double) num8 * (double) num9);
      float num38 = (float) ((double) num5 * (double) num11 - (double) num7 * (double) num9);
      float num39 = (float) ((double) num5 * (double) num10 - (double) num6 * (double) num9);
      result.M14 = (float) -((double) num2 * (double) num34 - (double) num3 * (double) num35 + (double) num4 * (double) num36) * num27;
      result.M24 = (float) ((double) num1 * (double) num34 - (double) num3 * (double) num37 + (double) num4 * (double) num38) * num27;
      result.M34 = (float) -((double) num1 * (double) num35 - (double) num2 * (double) num37 + (double) num4 * (double) num39) * num27;
      result.M44 = (float) ((double) num1 * (double) num36 - (double) num2 * (double) num38 + (double) num3 * (double) num39) * num27;
    }

    public static Matrix Lerp(Matrix matrix1, Matrix matrix2, float amount)
    {
      Matrix matrix;
      matrix.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
      matrix.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
      matrix.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
      matrix.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
      matrix.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
      matrix.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
      matrix.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
      matrix.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
      matrix.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
      matrix.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
      matrix.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
      matrix.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
      matrix.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
      matrix.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
      matrix.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
      matrix.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
      return matrix;
    }

    public static void Lerp(ref Matrix matrix1, ref Matrix matrix2, float amount, out Matrix result)
    {
      result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
      result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
      result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
      result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
      result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
      result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
      result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
      result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
      result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
      result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
      result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
      result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
      result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
      result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
      result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
      result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
    }

    public static void Slerp(ref Matrix matrix1, ref Matrix matrix2, float amount, out Matrix result)
    {
      Quaternion result1;
      Quaternion.CreateFromRotationMatrix(ref matrix1, out result1);
      Quaternion result2;
      Quaternion.CreateFromRotationMatrix(ref matrix2, out result2);
      Quaternion result3;
      Quaternion.Slerp(ref result1, ref result2, amount, out result3);
      Matrix.CreateFromQuaternion(ref result3, out result);
      result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
      result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
      result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
    }

    public static void SlerpScale(ref Matrix matrix1, ref Matrix matrix2, float amount, out Matrix result)
    {
      Vector3 scale1 = matrix1.Scale;
      Vector3 scale2 = matrix2.Scale;
      if ((double) scale1.LengthSquared() < 9.99999997475243E-07 || (double) scale2.LengthSquared() < 9.99999997475243E-07)
      {
        result = Matrix.Zero;
      }
      else
      {
        Matrix matrix3 = Matrix.Normalize(matrix1);
        Matrix matrix4 = Matrix.Normalize(matrix2);
        Quaternion result1;
        Quaternion.CreateFromRotationMatrix(ref matrix3, out result1);
        Quaternion result2;
        Quaternion.CreateFromRotationMatrix(ref matrix4, out result2);
        Quaternion result3;
        Quaternion.Slerp(ref result1, ref result2, amount, out result3);
        Matrix.CreateFromQuaternion(ref result3, out result);
        Vector3 scale3 = Vector3.Lerp(scale1, scale2, amount);
        Matrix.Rescale(ref result, ref scale3);
        result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
        result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
        result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
      }
    }

    public static void Slerp(Matrix matrix1, Matrix matrix2, float amount, out Matrix result)
    {
      Matrix.Slerp(ref matrix1, ref matrix2, amount, out result);
    }

    public static Matrix Slerp(Matrix matrix1, Matrix matrix2, float amount)
    {
      Matrix result;
      Matrix.Slerp(ref matrix1, ref matrix2, amount, out result);
      return result;
    }

    public static void SlerpScale(Matrix matrix1, Matrix matrix2, float amount, out Matrix result)
    {
      Matrix.SlerpScale(ref matrix1, ref matrix2, amount, out result);
    }

    public static Matrix SlerpScale(Matrix matrix1, Matrix matrix2, float amount)
    {
      Matrix result;
      Matrix.SlerpScale(ref matrix1, ref matrix2, amount, out result);
      return result;
    }

    public static Matrix Negate(Matrix matrix)
    {
      Matrix matrix1;
      matrix1.M11 = -matrix.M11;
      matrix1.M12 = -matrix.M12;
      matrix1.M13 = -matrix.M13;
      matrix1.M14 = -matrix.M14;
      matrix1.M21 = -matrix.M21;
      matrix1.M22 = -matrix.M22;
      matrix1.M23 = -matrix.M23;
      matrix1.M24 = -matrix.M24;
      matrix1.M31 = -matrix.M31;
      matrix1.M32 = -matrix.M32;
      matrix1.M33 = -matrix.M33;
      matrix1.M34 = -matrix.M34;
      matrix1.M41 = -matrix.M41;
      matrix1.M42 = -matrix.M42;
      matrix1.M43 = -matrix.M43;
      matrix1.M44 = -matrix.M44;
      return matrix1;
    }

    public static void Negate(ref Matrix matrix, out Matrix result)
    {
      result.M11 = -matrix.M11;
      result.M12 = -matrix.M12;
      result.M13 = -matrix.M13;
      result.M14 = -matrix.M14;
      result.M21 = -matrix.M21;
      result.M22 = -matrix.M22;
      result.M23 = -matrix.M23;
      result.M24 = -matrix.M24;
      result.M31 = -matrix.M31;
      result.M32 = -matrix.M32;
      result.M33 = -matrix.M33;
      result.M34 = -matrix.M34;
      result.M41 = -matrix.M41;
      result.M42 = -matrix.M42;
      result.M43 = -matrix.M43;
      result.M44 = -matrix.M44;
    }

    public static Matrix Add(Matrix matrix1, Matrix matrix2)
    {
      Matrix matrix;
      matrix.M11 = matrix1.M11 + matrix2.M11;
      matrix.M12 = matrix1.M12 + matrix2.M12;
      matrix.M13 = matrix1.M13 + matrix2.M13;
      matrix.M14 = matrix1.M14 + matrix2.M14;
      matrix.M21 = matrix1.M21 + matrix2.M21;
      matrix.M22 = matrix1.M22 + matrix2.M22;
      matrix.M23 = matrix1.M23 + matrix2.M23;
      matrix.M24 = matrix1.M24 + matrix2.M24;
      matrix.M31 = matrix1.M31 + matrix2.M31;
      matrix.M32 = matrix1.M32 + matrix2.M32;
      matrix.M33 = matrix1.M33 + matrix2.M33;
      matrix.M34 = matrix1.M34 + matrix2.M34;
      matrix.M41 = matrix1.M41 + matrix2.M41;
      matrix.M42 = matrix1.M42 + matrix2.M42;
      matrix.M43 = matrix1.M43 + matrix2.M43;
      matrix.M44 = matrix1.M44 + matrix2.M44;
      return matrix;
    }

    public static void Add(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
    {
      result.M11 = matrix1.M11 + matrix2.M11;
      result.M12 = matrix1.M12 + matrix2.M12;
      result.M13 = matrix1.M13 + matrix2.M13;
      result.M14 = matrix1.M14 + matrix2.M14;
      result.M21 = matrix1.M21 + matrix2.M21;
      result.M22 = matrix1.M22 + matrix2.M22;
      result.M23 = matrix1.M23 + matrix2.M23;
      result.M24 = matrix1.M24 + matrix2.M24;
      result.M31 = matrix1.M31 + matrix2.M31;
      result.M32 = matrix1.M32 + matrix2.M32;
      result.M33 = matrix1.M33 + matrix2.M33;
      result.M34 = matrix1.M34 + matrix2.M34;
      result.M41 = matrix1.M41 + matrix2.M41;
      result.M42 = matrix1.M42 + matrix2.M42;
      result.M43 = matrix1.M43 + matrix2.M43;
      result.M44 = matrix1.M44 + matrix2.M44;
    }

    public static Matrix Subtract(Matrix matrix1, Matrix matrix2)
    {
      Matrix matrix;
      matrix.M11 = matrix1.M11 - matrix2.M11;
      matrix.M12 = matrix1.M12 - matrix2.M12;
      matrix.M13 = matrix1.M13 - matrix2.M13;
      matrix.M14 = matrix1.M14 - matrix2.M14;
      matrix.M21 = matrix1.M21 - matrix2.M21;
      matrix.M22 = matrix1.M22 - matrix2.M22;
      matrix.M23 = matrix1.M23 - matrix2.M23;
      matrix.M24 = matrix1.M24 - matrix2.M24;
      matrix.M31 = matrix1.M31 - matrix2.M31;
      matrix.M32 = matrix1.M32 - matrix2.M32;
      matrix.M33 = matrix1.M33 - matrix2.M33;
      matrix.M34 = matrix1.M34 - matrix2.M34;
      matrix.M41 = matrix1.M41 - matrix2.M41;
      matrix.M42 = matrix1.M42 - matrix2.M42;
      matrix.M43 = matrix1.M43 - matrix2.M43;
      matrix.M44 = matrix1.M44 - matrix2.M44;
      return matrix;
    }

    public static void Subtract(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
    {
      result.M11 = matrix1.M11 - matrix2.M11;
      result.M12 = matrix1.M12 - matrix2.M12;
      result.M13 = matrix1.M13 - matrix2.M13;
      result.M14 = matrix1.M14 - matrix2.M14;
      result.M21 = matrix1.M21 - matrix2.M21;
      result.M22 = matrix1.M22 - matrix2.M22;
      result.M23 = matrix1.M23 - matrix2.M23;
      result.M24 = matrix1.M24 - matrix2.M24;
      result.M31 = matrix1.M31 - matrix2.M31;
      result.M32 = matrix1.M32 - matrix2.M32;
      result.M33 = matrix1.M33 - matrix2.M33;
      result.M34 = matrix1.M34 - matrix2.M34;
      result.M41 = matrix1.M41 - matrix2.M41;
      result.M42 = matrix1.M42 - matrix2.M42;
      result.M43 = matrix1.M43 - matrix2.M43;
      result.M44 = matrix1.M44 - matrix2.M44;
    }

    public static Matrix Multiply(Matrix matrix1, Matrix matrix2)
    {
      Matrix matrix;
      matrix.M11 = (float) ((double) matrix1.M11 * (double) matrix2.M11 + (double) matrix1.M12 * (double) matrix2.M21 + (double) matrix1.M13 * (double) matrix2.M31 + (double) matrix1.M14 * (double) matrix2.M41);
      matrix.M12 = (float) ((double) matrix1.M11 * (double) matrix2.M12 + (double) matrix1.M12 * (double) matrix2.M22 + (double) matrix1.M13 * (double) matrix2.M32 + (double) matrix1.M14 * (double) matrix2.M42);
      matrix.M13 = (float) ((double) matrix1.M11 * (double) matrix2.M13 + (double) matrix1.M12 * (double) matrix2.M23 + (double) matrix1.M13 * (double) matrix2.M33 + (double) matrix1.M14 * (double) matrix2.M43);
      matrix.M14 = (float) ((double) matrix1.M11 * (double) matrix2.M14 + (double) matrix1.M12 * (double) matrix2.M24 + (double) matrix1.M13 * (double) matrix2.M34 + (double) matrix1.M14 * (double) matrix2.M44);
      matrix.M21 = (float) ((double) matrix1.M21 * (double) matrix2.M11 + (double) matrix1.M22 * (double) matrix2.M21 + (double) matrix1.M23 * (double) matrix2.M31 + (double) matrix1.M24 * (double) matrix2.M41);
      matrix.M22 = (float) ((double) matrix1.M21 * (double) matrix2.M12 + (double) matrix1.M22 * (double) matrix2.M22 + (double) matrix1.M23 * (double) matrix2.M32 + (double) matrix1.M24 * (double) matrix2.M42);
      matrix.M23 = (float) ((double) matrix1.M21 * (double) matrix2.M13 + (double) matrix1.M22 * (double) matrix2.M23 + (double) matrix1.M23 * (double) matrix2.M33 + (double) matrix1.M24 * (double) matrix2.M43);
      matrix.M24 = (float) ((double) matrix1.M21 * (double) matrix2.M14 + (double) matrix1.M22 * (double) matrix2.M24 + (double) matrix1.M23 * (double) matrix2.M34 + (double) matrix1.M24 * (double) matrix2.M44);
      matrix.M31 = (float) ((double) matrix1.M31 * (double) matrix2.M11 + (double) matrix1.M32 * (double) matrix2.M21 + (double) matrix1.M33 * (double) matrix2.M31 + (double) matrix1.M34 * (double) matrix2.M41);
      matrix.M32 = (float) ((double) matrix1.M31 * (double) matrix2.M12 + (double) matrix1.M32 * (double) matrix2.M22 + (double) matrix1.M33 * (double) matrix2.M32 + (double) matrix1.M34 * (double) matrix2.M42);
      matrix.M33 = (float) ((double) matrix1.M31 * (double) matrix2.M13 + (double) matrix1.M32 * (double) matrix2.M23 + (double) matrix1.M33 * (double) matrix2.M33 + (double) matrix1.M34 * (double) matrix2.M43);
      matrix.M34 = (float) ((double) matrix1.M31 * (double) matrix2.M14 + (double) matrix1.M32 * (double) matrix2.M24 + (double) matrix1.M33 * (double) matrix2.M34 + (double) matrix1.M34 * (double) matrix2.M44);
      matrix.M41 = (float) ((double) matrix1.M41 * (double) matrix2.M11 + (double) matrix1.M42 * (double) matrix2.M21 + (double) matrix1.M43 * (double) matrix2.M31 + (double) matrix1.M44 * (double) matrix2.M41);
      matrix.M42 = (float) ((double) matrix1.M41 * (double) matrix2.M12 + (double) matrix1.M42 * (double) matrix2.M22 + (double) matrix1.M43 * (double) matrix2.M32 + (double) matrix1.M44 * (double) matrix2.M42);
      matrix.M43 = (float) ((double) matrix1.M41 * (double) matrix2.M13 + (double) matrix1.M42 * (double) matrix2.M23 + (double) matrix1.M43 * (double) matrix2.M33 + (double) matrix1.M44 * (double) matrix2.M43);
      matrix.M44 = (float) ((double) matrix1.M41 * (double) matrix2.M14 + (double) matrix1.M42 * (double) matrix2.M24 + (double) matrix1.M43 * (double) matrix2.M34 + (double) matrix1.M44 * (double) matrix2.M44);
      return matrix;
    }

    [SuppressUnmanagedCodeSecurity]
    [DllImport("d3dx9_43.dll", EntryPoint = "D3DXMatrixMultiply", CallingConvention = CallingConvention.StdCall)]
    private static extern unsafe Matrix* D3DXMatrixMultiply_([Out] Matrix* pOut, [In] Matrix* pM1, [In] Matrix* pM2);

    public static unsafe void Multiply_Native(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
    {
      fixed (Matrix* pOut = &result)
        fixed (Matrix* pM1 = &matrix1)
          fixed (Matrix* pM2 = &matrix2)
            Matrix.D3DXMatrixMultiply_(pOut, pM1, pM2);
    }

    public static void Multiply(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
    {
      float num1 = (float) ((double) matrix1.M11 * (double) matrix2.M11 + (double) matrix1.M12 * (double) matrix2.M21 + (double) matrix1.M13 * (double) matrix2.M31 + (double) matrix1.M14 * (double) matrix2.M41);
      float num2 = (float) ((double) matrix1.M11 * (double) matrix2.M12 + (double) matrix1.M12 * (double) matrix2.M22 + (double) matrix1.M13 * (double) matrix2.M32 + (double) matrix1.M14 * (double) matrix2.M42);
      float num3 = (float) ((double) matrix1.M11 * (double) matrix2.M13 + (double) matrix1.M12 * (double) matrix2.M23 + (double) matrix1.M13 * (double) matrix2.M33 + (double) matrix1.M14 * (double) matrix2.M43);
      float num4 = (float) ((double) matrix1.M11 * (double) matrix2.M14 + (double) matrix1.M12 * (double) matrix2.M24 + (double) matrix1.M13 * (double) matrix2.M34 + (double) matrix1.M14 * (double) matrix2.M44);
      float num5 = (float) ((double) matrix1.M21 * (double) matrix2.M11 + (double) matrix1.M22 * (double) matrix2.M21 + (double) matrix1.M23 * (double) matrix2.M31 + (double) matrix1.M24 * (double) matrix2.M41);
      float num6 = (float) ((double) matrix1.M21 * (double) matrix2.M12 + (double) matrix1.M22 * (double) matrix2.M22 + (double) matrix1.M23 * (double) matrix2.M32 + (double) matrix1.M24 * (double) matrix2.M42);
      float num7 = (float) ((double) matrix1.M21 * (double) matrix2.M13 + (double) matrix1.M22 * (double) matrix2.M23 + (double) matrix1.M23 * (double) matrix2.M33 + (double) matrix1.M24 * (double) matrix2.M43);
      float num8 = (float) ((double) matrix1.M21 * (double) matrix2.M14 + (double) matrix1.M22 * (double) matrix2.M24 + (double) matrix1.M23 * (double) matrix2.M34 + (double) matrix1.M24 * (double) matrix2.M44);
      float num9 = (float) ((double) matrix1.M31 * (double) matrix2.M11 + (double) matrix1.M32 * (double) matrix2.M21 + (double) matrix1.M33 * (double) matrix2.M31 + (double) matrix1.M34 * (double) matrix2.M41);
      float num10 = (float) ((double) matrix1.M31 * (double) matrix2.M12 + (double) matrix1.M32 * (double) matrix2.M22 + (double) matrix1.M33 * (double) matrix2.M32 + (double) matrix1.M34 * (double) matrix2.M42);
      float num11 = (float) ((double) matrix1.M31 * (double) matrix2.M13 + (double) matrix1.M32 * (double) matrix2.M23 + (double) matrix1.M33 * (double) matrix2.M33 + (double) matrix1.M34 * (double) matrix2.M43);
      float num12 = (float) ((double) matrix1.M31 * (double) matrix2.M14 + (double) matrix1.M32 * (double) matrix2.M24 + (double) matrix1.M33 * (double) matrix2.M34 + (double) matrix1.M34 * (double) matrix2.M44);
      float num13 = (float) ((double) matrix1.M41 * (double) matrix2.M11 + (double) matrix1.M42 * (double) matrix2.M21 + (double) matrix1.M43 * (double) matrix2.M31 + (double) matrix1.M44 * (double) matrix2.M41);
      float num14 = (float) ((double) matrix1.M41 * (double) matrix2.M12 + (double) matrix1.M42 * (double) matrix2.M22 + (double) matrix1.M43 * (double) matrix2.M32 + (double) matrix1.M44 * (double) matrix2.M42);
      float num15 = (float) ((double) matrix1.M41 * (double) matrix2.M13 + (double) matrix1.M42 * (double) matrix2.M23 + (double) matrix1.M43 * (double) matrix2.M33 + (double) matrix1.M44 * (double) matrix2.M43);
      float num16 = (float) ((double) matrix1.M41 * (double) matrix2.M14 + (double) matrix1.M42 * (double) matrix2.M24 + (double) matrix1.M43 * (double) matrix2.M34 + (double) matrix1.M44 * (double) matrix2.M44);
      result.M11 = num1;
      result.M12 = num2;
      result.M13 = num3;
      result.M14 = num4;
      result.M21 = num5;
      result.M22 = num6;
      result.M23 = num7;
      result.M24 = num8;
      result.M31 = num9;
      result.M32 = num10;
      result.M33 = num11;
      result.M34 = num12;
      result.M41 = num13;
      result.M42 = num14;
      result.M43 = num15;
      result.M44 = num16;
    }

    public static Matrix Multiply(Matrix matrix1, float scaleFactor)
    {
      float num = scaleFactor;
      Matrix matrix;
      matrix.M11 = matrix1.M11 * num;
      matrix.M12 = matrix1.M12 * num;
      matrix.M13 = matrix1.M13 * num;
      matrix.M14 = matrix1.M14 * num;
      matrix.M21 = matrix1.M21 * num;
      matrix.M22 = matrix1.M22 * num;
      matrix.M23 = matrix1.M23 * num;
      matrix.M24 = matrix1.M24 * num;
      matrix.M31 = matrix1.M31 * num;
      matrix.M32 = matrix1.M32 * num;
      matrix.M33 = matrix1.M33 * num;
      matrix.M34 = matrix1.M34 * num;
      matrix.M41 = matrix1.M41 * num;
      matrix.M42 = matrix1.M42 * num;
      matrix.M43 = matrix1.M43 * num;
      matrix.M44 = matrix1.M44 * num;
      return matrix;
    }

    public static void Multiply(ref Matrix matrix1, float scaleFactor, out Matrix result)
    {
      float num = scaleFactor;
      result.M11 = matrix1.M11 * num;
      result.M12 = matrix1.M12 * num;
      result.M13 = matrix1.M13 * num;
      result.M14 = matrix1.M14 * num;
      result.M21 = matrix1.M21 * num;
      result.M22 = matrix1.M22 * num;
      result.M23 = matrix1.M23 * num;
      result.M24 = matrix1.M24 * num;
      result.M31 = matrix1.M31 * num;
      result.M32 = matrix1.M32 * num;
      result.M33 = matrix1.M33 * num;
      result.M34 = matrix1.M34 * num;
      result.M41 = matrix1.M41 * num;
      result.M42 = matrix1.M42 * num;
      result.M43 = matrix1.M43 * num;
      result.M44 = matrix1.M44 * num;
    }

    public static Matrix Divide(Matrix matrix1, Matrix matrix2)
    {
      Matrix matrix;
      matrix.M11 = matrix1.M11 / matrix2.M11;
      matrix.M12 = matrix1.M12 / matrix2.M12;
      matrix.M13 = matrix1.M13 / matrix2.M13;
      matrix.M14 = matrix1.M14 / matrix2.M14;
      matrix.M21 = matrix1.M21 / matrix2.M21;
      matrix.M22 = matrix1.M22 / matrix2.M22;
      matrix.M23 = matrix1.M23 / matrix2.M23;
      matrix.M24 = matrix1.M24 / matrix2.M24;
      matrix.M31 = matrix1.M31 / matrix2.M31;
      matrix.M32 = matrix1.M32 / matrix2.M32;
      matrix.M33 = matrix1.M33 / matrix2.M33;
      matrix.M34 = matrix1.M34 / matrix2.M34;
      matrix.M41 = matrix1.M41 / matrix2.M41;
      matrix.M42 = matrix1.M42 / matrix2.M42;
      matrix.M43 = matrix1.M43 / matrix2.M43;
      matrix.M44 = matrix1.M44 / matrix2.M44;
      return matrix;
    }

    public static void Divide(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
    {
      result.M11 = matrix1.M11 / matrix2.M11;
      result.M12 = matrix1.M12 / matrix2.M12;
      result.M13 = matrix1.M13 / matrix2.M13;
      result.M14 = matrix1.M14 / matrix2.M14;
      result.M21 = matrix1.M21 / matrix2.M21;
      result.M22 = matrix1.M22 / matrix2.M22;
      result.M23 = matrix1.M23 / matrix2.M23;
      result.M24 = matrix1.M24 / matrix2.M24;
      result.M31 = matrix1.M31 / matrix2.M31;
      result.M32 = matrix1.M32 / matrix2.M32;
      result.M33 = matrix1.M33 / matrix2.M33;
      result.M34 = matrix1.M34 / matrix2.M34;
      result.M41 = matrix1.M41 / matrix2.M41;
      result.M42 = matrix1.M42 / matrix2.M42;
      result.M43 = matrix1.M43 / matrix2.M43;
      result.M44 = matrix1.M44 / matrix2.M44;
    }

    public static Matrix Divide(Matrix matrix1, float divider)
    {
      float num = 1f / divider;
      Matrix matrix;
      matrix.M11 = matrix1.M11 * num;
      matrix.M12 = matrix1.M12 * num;
      matrix.M13 = matrix1.M13 * num;
      matrix.M14 = matrix1.M14 * num;
      matrix.M21 = matrix1.M21 * num;
      matrix.M22 = matrix1.M22 * num;
      matrix.M23 = matrix1.M23 * num;
      matrix.M24 = matrix1.M24 * num;
      matrix.M31 = matrix1.M31 * num;
      matrix.M32 = matrix1.M32 * num;
      matrix.M33 = matrix1.M33 * num;
      matrix.M34 = matrix1.M34 * num;
      matrix.M41 = matrix1.M41 * num;
      matrix.M42 = matrix1.M42 * num;
      matrix.M43 = matrix1.M43 * num;
      matrix.M44 = matrix1.M44 * num;
      return matrix;
    }

    public static void Divide(ref Matrix matrix1, float divider, out Matrix result)
    {
      float num = 1f / divider;
      result.M11 = matrix1.M11 * num;
      result.M12 = matrix1.M12 * num;
      result.M13 = matrix1.M13 * num;
      result.M14 = matrix1.M14 * num;
      result.M21 = matrix1.M21 * num;
      result.M22 = matrix1.M22 * num;
      result.M23 = matrix1.M23 * num;
      result.M24 = matrix1.M24 * num;
      result.M31 = matrix1.M31 * num;
      result.M32 = matrix1.M32 * num;
      result.M33 = matrix1.M33 * num;
      result.M34 = matrix1.M34 * num;
      result.M41 = matrix1.M41 * num;
      result.M42 = matrix1.M42 * num;
      result.M43 = matrix1.M43 * num;
      result.M44 = matrix1.M44 * num;
    }

    public Matrix GetOrientation()
    {
      Matrix matrix = Matrix.Identity;
      matrix.Forward = this.Forward;
      matrix.Up = this.Up;
      matrix.Right = this.Right;
      return matrix;
    }

    public bool IsValid()
    {
      return FloatExtensions.IsValid(this.M11 + this.M12 + this.M13 + this.M14 + this.M21 + this.M22 + this.M23 + this.M24 + this.M31 + this.M32 + this.M33 + this.M34 + this.M41 + this.M42 + this.M43 + this.M44);
    }

    public bool IsNan()
    {
      return float.IsNaN(this.M11 + this.M12 + this.M13 + this.M14 + this.M21 + this.M22 + this.M23 + this.M24 + this.M31 + this.M32 + this.M33 + this.M34 + this.M41 + this.M42 + this.M43 + this.M44);
    }

    public bool IsRotation()
    {
      float num = 0.01f;
      return this.HasNoTranslationOrPerspective() && (double) Math.Abs(this.Right.Dot(this.Up)) <= (double) num && ((double) Math.Abs(this.Right.Dot(this.Backward)) <= (double) num && (double) Math.Abs(this.Up.Dot(this.Backward)) <= (double) num) && ((double) Math.Abs(this.Right.LengthSquared() - 1f) <= (double) num && (double) Math.Abs(this.Up.LengthSquared() - 1f) <= (double) num && (double) Math.Abs(this.Backward.LengthSquared() - 1f) <= (double) num);
    }

    public bool HasNoTranslationOrPerspective()
    {
      float num = 0.0001f;
      return (double) (this.M41 + this.M42 + this.M43 + this.M34 + this.M24 + this.M14) <= (double) num && (double) Math.Abs(this.M44 - 1f) <= (double) num;
    }

    public static Matrix CreateFromDir(Vector3 dir)
    {
      Vector3 vector2 = new Vector3(0.0f, 0.0f, 1f);
      float num = dir.Z;
      Vector3 vector3;
      if ((double) num > -0.99999 && (double) num < 0.99999)
      {
        vector2 = Vector3.Normalize(vector2 - dir * num);
        vector3 = Vector3.Cross(dir, vector2);
      }
      else
      {
        vector2 = new Vector3(dir.Z, 0.0f, -dir.X);
        vector3 = new Vector3(0.0f, 1f, 0.0f);
      }
      Matrix matrix = Matrix.Identity;
      matrix.Right = vector2;
      matrix.Up = vector3;
      matrix.Forward = dir;
      return matrix;
    }

    public static Matrix CreateFromDir(Vector3 dir, Vector3 suggestedUp)
    {
      Vector3 up = Vector3.Cross(Vector3.Cross(dir, suggestedUp), dir);
      return Matrix.CreateWorld(Vector3.Zero, dir, up);
    }

    public static Matrix Normalize(Matrix matrix)
    {
      Matrix matrix1 = matrix;
      matrix1.Right = Vector3.Normalize(matrix1.Right);
      matrix1.Up = Vector3.Normalize(matrix1.Up);
      matrix1.Forward = Vector3.Normalize(matrix1.Forward);
      return matrix1;
    }

    public static Matrix Orthogonalize(Matrix rotationMatrix)
    {
      Matrix matrix = rotationMatrix;
      matrix.Right = Vector3.Normalize(matrix.Right);
      matrix.Up = Vector3.Normalize(matrix.Up - matrix.Right * matrix.Up.Dot(matrix.Right));
      matrix.Backward = Vector3.Normalize(matrix.Backward - matrix.Right * matrix.Backward.Dot(matrix.Right) - matrix.Up * matrix.Backward.Dot(matrix.Up));
      return matrix;
    }

    public static Matrix Round(ref Matrix matrix)
    {
      Matrix matrix1 = matrix;
      matrix1.Right = (Vector3) Vector3I.Round(matrix1.Right);
      matrix1.Up = (Vector3) Vector3I.Round(matrix1.Up);
      matrix1.Forward = (Vector3) Vector3I.Round(matrix1.Forward);
      return matrix1;
    }

    public static Matrix AlignRotationToAxes(ref Matrix toAlign, ref Matrix axisDefinitionMatrix)
    {
      Matrix matrix = Matrix.Identity;
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      float num1 = toAlign.Right.Dot(axisDefinitionMatrix.Right);
      float num2 = toAlign.Right.Dot(axisDefinitionMatrix.Up);
      float num3 = toAlign.Right.Dot(axisDefinitionMatrix.Backward);
      if ((double) Math.Abs(num1) > (double) Math.Abs(num2))
      {
        if ((double) Math.Abs(num1) > (double) Math.Abs(num3))
        {
          matrix.Right = (double) num1 > 0.0 ? axisDefinitionMatrix.Right : axisDefinitionMatrix.Left;
          flag1 = true;
        }
        else
        {
          matrix.Right = (double) num3 > 0.0 ? axisDefinitionMatrix.Backward : axisDefinitionMatrix.Forward;
          flag3 = true;
        }
      }
      else if ((double) Math.Abs(num2) > (double) Math.Abs(num3))
      {
        matrix.Right = (double) num2 > 0.0 ? axisDefinitionMatrix.Up : axisDefinitionMatrix.Down;
        flag2 = true;
      }
      else
      {
        matrix.Right = (double) num3 > 0.0 ? axisDefinitionMatrix.Backward : axisDefinitionMatrix.Forward;
        flag3 = true;
      }
      float num4 = toAlign.Up.Dot(axisDefinitionMatrix.Right);
      float num5 = toAlign.Up.Dot(axisDefinitionMatrix.Up);
      float num6 = toAlign.Up.Dot(axisDefinitionMatrix.Backward);
      bool flag4;
      if (flag2 || (double) Math.Abs(num4) > (double) Math.Abs(num5) && !flag1)
      {
        if ((double) Math.Abs(num4) > (double) Math.Abs(num6) || flag3)
        {
          matrix.Up = (double) num4 > 0.0 ? axisDefinitionMatrix.Right : axisDefinitionMatrix.Left;
          flag1 = true;
        }
        else
        {
          matrix.Up = (double) num6 > 0.0 ? axisDefinitionMatrix.Backward : axisDefinitionMatrix.Forward;
          flag4 = true;
        }
      }
      else if ((double) Math.Abs(num5) > (double) Math.Abs(num6) || flag3)
      {
        matrix.Up = (double) num5 > 0.0 ? axisDefinitionMatrix.Up : axisDefinitionMatrix.Down;
        flag2 = true;
      }
      else
      {
        matrix.Up = (double) num6 > 0.0 ? axisDefinitionMatrix.Backward : axisDefinitionMatrix.Forward;
        flag4 = true;
      }
      if (!flag1)
      {
        float num7 = toAlign.Backward.Dot(axisDefinitionMatrix.Right);
        matrix.Backward = (double) num7 > 0.0 ? axisDefinitionMatrix.Right : axisDefinitionMatrix.Left;
      }
      else if (!flag2)
      {
        float num7 = toAlign.Backward.Dot(axisDefinitionMatrix.Up);
        matrix.Backward = (double) num7 > 0.0 ? axisDefinitionMatrix.Up : axisDefinitionMatrix.Down;
      }
      else
      {
        float num7 = toAlign.Backward.Dot(axisDefinitionMatrix.Backward);
        matrix.Backward = (double) num7 > 0.0 ? axisDefinitionMatrix.Backward : axisDefinitionMatrix.Forward;
      }
      return matrix;
    }

    public static bool GetEulerAnglesXYZ(ref Matrix mat, out Vector3 xyz)
    {
      float num1 = mat.GetRow(0).X;
      float num2 = mat.GetRow(0).Y;
      float num3 = mat.GetRow(0).Z;
      float num4 = mat.GetRow(1).X;
      float num5 = mat.GetRow(1).Y;
      float num6 = mat.GetRow(1).Z;
      double num7 = (double) mat.GetRow(2).X;
      double num8 = (double) mat.GetRow(2).Y;
      float num9 = mat.GetRow(2).Z;
      float num10 = num3;
      if ((double) num10 < 1.0)
      {
        if ((double) num10 > -1.0)
        {
          xyz = new Vector3((float) Math.Atan2(-(double) num6, (double) num9), (float) Math.Asin((double) num3), (float) Math.Atan2(-(double) num2, (double) num1));
          return true;
        }
        else
        {
          xyz = new Vector3((float) -Math.Atan2((double) num4, (double) num5), -1.570796f, 0.0f);
          return false;
        }
      }
      else
      {
        xyz = new Vector3((float) Math.Atan2((double) num4, (double) num5), -1.570796f, 0.0f);
        return false;
      }
    }

    public static Matrix SwapYZCoordinates(Matrix m)
    {
      return m * Matrix.CreateRotationX(MathHelper.ToRadians(-90f));
    }

    public bool IsMirrored()
    {
      return (double) this.Determinant() < 0.0;
    }

    private struct F16
    {
      public unsafe fixed float data[16];
    }
  }
}
