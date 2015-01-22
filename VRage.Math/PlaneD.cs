// Decompiled with JetBrains decompiler
// Type: VRageMath.PlaneD
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Globalization;

namespace VRageMath
{
  [Serializable]
  public struct PlaneD : IEquatable<PlaneD>
  {
    public Vector3D Normal;
    public double D;
    private static Random _random;

    public PlaneD(double a, double b, double c, double d)
    {
      this.Normal.X = a;
      this.Normal.Y = b;
      this.Normal.Z = c;
      this.D = d;
    }

    public PlaneD(Vector3D normal, double d)
    {
      this.Normal = normal;
      this.D = d;
    }

    public PlaneD(Vector3D position, Vector3D normal)
    {
      this.Normal = normal;
      this.D = -Vector3D.Dot(position, normal);
    }

    public PlaneD(Vector3D position, Vector3 normal)
    {
      this.Normal = (Vector3D) normal;
      this.D = -Vector3D.Dot(position, normal);
    }

    public PlaneD(Vector4 value)
    {
      this.Normal.X = (double) value.X;
      this.Normal.Y = (double) value.Y;
      this.Normal.Z = (double) value.Z;
      this.D = (double) value.W;
    }

    public PlaneD(Vector3D point1, Vector3D point2, Vector3D point3)
    {
      double num1 = point2.X - point1.X;
      double num2 = point2.Y - point1.Y;
      double num3 = point2.Z - point1.Z;
      double num4 = point3.X - point1.X;
      double num5 = point3.Y - point1.Y;
      double num6 = point3.Z - point1.Z;
      double num7 = num2 * num6 - num3 * num5;
      double num8 = num3 * num4 - num1 * num6;
      double num9 = num1 * num5 - num2 * num4;
      double num10 = 1.0 / Math.Sqrt(num7 * num7 + num8 * num8 + num9 * num9);
      this.Normal.X = num7 * num10;
      this.Normal.Y = num8 * num10;
      this.Normal.Z = num9 * num10;
      this.D = -(this.Normal.X * point1.X + this.Normal.Y * point1.Y + this.Normal.Z * point1.Z);
    }

    public static bool operator ==(PlaneD lhs, PlaneD rhs)
    {
      return lhs.Equals(rhs);
    }

    public static bool operator !=(PlaneD lhs, PlaneD rhs)
    {
      if (lhs.Normal.X == rhs.Normal.X && lhs.Normal.Y == rhs.Normal.Y && lhs.Normal.Z == rhs.Normal.Z)
        return lhs.D != rhs.D;
      else
        return true;
    }

    public bool Equals(PlaneD other)
    {
      if (this.Normal.X == other.Normal.X && this.Normal.Y == other.Normal.Y && this.Normal.Z == other.Normal.Z)
        return this.D == other.D;
      else
        return false;
    }

    public override bool Equals(object obj)
    {
      bool flag = false;
      if (obj is PlaneD)
        flag = this.Equals((PlaneD) obj);
      return flag;
    }

    public override int GetHashCode()
    {
      return this.Normal.GetHashCode() + this.D.GetHashCode();
    }

    public override string ToString()
    {
      CultureInfo currentCulture = CultureInfo.CurrentCulture;
      return string.Format((IFormatProvider) currentCulture, "{{Normal:{0} D:{1}}}", new object[2]
      {
        (object) this.Normal.ToString(),
        (object) this.D.ToString((IFormatProvider) currentCulture)
      });
    }

    public void Normalize()
    {
      double d = this.Normal.X * this.Normal.X + this.Normal.Y * this.Normal.Y + this.Normal.Z * this.Normal.Z;
      if (Math.Abs(d - 1.0) < 1.19209289550781E-07)
        return;
      double num = 1.0 / Math.Sqrt(d);
      this.Normal.X *= num;
      this.Normal.Y *= num;
      this.Normal.Z *= num;
      this.D *= num;
    }

    public static PlaneD Normalize(PlaneD value)
    {
      double d = value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y + value.Normal.Z * value.Normal.Z;
      if (Math.Abs(d - 1.0) < 1.19209289550781E-07)
      {
        PlaneD planeD;
        planeD.Normal = value.Normal;
        planeD.D = value.D;
        return planeD;
      }
      else
      {
        double num = 1.0 / Math.Sqrt(d);
        PlaneD planeD;
        planeD.Normal.X = value.Normal.X * num;
        planeD.Normal.Y = value.Normal.Y * num;
        planeD.Normal.Z = value.Normal.Z * num;
        planeD.D = value.D * num;
        return planeD;
      }
    }

    public static void Normalize(ref PlaneD value, out PlaneD result)
    {
      double d = value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y + value.Normal.Z * value.Normal.Z;
      if (Math.Abs(d - 1.0) < 1.19209289550781E-07)
      {
        result.Normal = value.Normal;
        result.D = value.D;
      }
      else
      {
        double num = 1.0 / Math.Sqrt(d);
        result.Normal.X = value.Normal.X * num;
        result.Normal.Y = value.Normal.Y * num;
        result.Normal.Z = value.Normal.Z * num;
        result.D = value.D * num;
      }
    }

    public static PlaneD Transform(PlaneD PlaneD, Matrix matrix)
    {
      Matrix result;
      Matrix.Invert(ref matrix, out result);
      double num1 = PlaneD.Normal.X;
      double num2 = PlaneD.Normal.Y;
      double num3 = PlaneD.Normal.Z;
      double num4 = PlaneD.D;
      PlaneD planeD;
      planeD.Normal.X = num1 * (double) result.M11 + num2 * (double) result.M12 + num3 * (double) result.M13 + num4 * (double) result.M14;
      planeD.Normal.Y = num1 * (double) result.M21 + num2 * (double) result.M22 + num3 * (double) result.M23 + num4 * (double) result.M24;
      planeD.Normal.Z = num1 * (double) result.M31 + num2 * (double) result.M32 + num3 * (double) result.M33 + num4 * (double) result.M34;
      planeD.D = num1 * (double) result.M41 + num2 * (double) result.M42 + num3 * (double) result.M43 + num4 * (double) result.M44;
      return planeD;
    }

    public static void Transform(ref PlaneD PlaneD, ref Matrix matrix, out PlaneD result)
    {
      Matrix result1;
      Matrix.Invert(ref matrix, out result1);
      double num1 = PlaneD.Normal.X;
      double num2 = PlaneD.Normal.Y;
      double num3 = PlaneD.Normal.Z;
      double num4 = PlaneD.D;
      result.Normal.X = num1 * (double) result1.M11 + num2 * (double) result1.M12 + num3 * (double) result1.M13 + num4 * (double) result1.M14;
      result.Normal.Y = num1 * (double) result1.M21 + num2 * (double) result1.M22 + num3 * (double) result1.M23 + num4 * (double) result1.M24;
      result.Normal.Z = num1 * (double) result1.M31 + num2 * (double) result1.M32 + num3 * (double) result1.M33 + num4 * (double) result1.M34;
      result.D = num1 * (double) result1.M41 + num2 * (double) result1.M42 + num3 * (double) result1.M43 + num4 * (double) result1.M44;
    }

    public static PlaneD Transform(PlaneD PlaneD, Quaternion rotation)
    {
      double num1 = (double) rotation.X + (double) rotation.X;
      double num2 = (double) rotation.Y + (double) rotation.Y;
      double num3 = (double) rotation.Z + (double) rotation.Z;
      double num4 = (double) rotation.W * num1;
      double num5 = (double) rotation.W * num2;
      double num6 = (double) rotation.W * num3;
      double num7 = (double) rotation.X * num1;
      double num8 = (double) rotation.X * num2;
      double num9 = (double) rotation.X * num3;
      double num10 = (double) rotation.Y * num2;
      double num11 = (double) rotation.Y * num3;
      double num12 = (double) rotation.Z * num3;
      double num13 = 1.0 - num10 - num12;
      double num14 = num8 - num6;
      double num15 = num9 + num5;
      double num16 = num8 + num6;
      double num17 = 1.0 - num7 - num12;
      double num18 = num11 - num4;
      double num19 = num9 - num5;
      double num20 = num11 + num4;
      double num21 = 1.0 - num7 - num10;
      double num22 = PlaneD.Normal.X;
      double num23 = PlaneD.Normal.Y;
      double num24 = PlaneD.Normal.Z;
      PlaneD planeD;
      planeD.Normal.X = num22 * num13 + num23 * num14 + num24 * num15;
      planeD.Normal.Y = num22 * num16 + num23 * num17 + num24 * num18;
      planeD.Normal.Z = num22 * num19 + num23 * num20 + num24 * num21;
      planeD.D = PlaneD.D;
      return planeD;
    }

    public static void Transform(ref PlaneD PlaneD, ref Quaternion rotation, out PlaneD result)
    {
      double num1 = (double) rotation.X + (double) rotation.X;
      double num2 = (double) rotation.Y + (double) rotation.Y;
      double num3 = (double) rotation.Z + (double) rotation.Z;
      double num4 = (double) rotation.W * num1;
      double num5 = (double) rotation.W * num2;
      double num6 = (double) rotation.W * num3;
      double num7 = (double) rotation.X * num1;
      double num8 = (double) rotation.X * num2;
      double num9 = (double) rotation.X * num3;
      double num10 = (double) rotation.Y * num2;
      double num11 = (double) rotation.Y * num3;
      double num12 = (double) rotation.Z * num3;
      double num13 = 1.0 - num10 - num12;
      double num14 = num8 - num6;
      double num15 = num9 + num5;
      double num16 = num8 + num6;
      double num17 = 1.0 - num7 - num12;
      double num18 = num11 - num4;
      double num19 = num9 - num5;
      double num20 = num11 + num4;
      double num21 = 1.0 - num7 - num10;
      double num22 = PlaneD.Normal.X;
      double num23 = PlaneD.Normal.Y;
      double num24 = PlaneD.Normal.Z;
      result.Normal.X = num22 * num13 + num23 * num14 + num24 * num15;
      result.Normal.Y = num22 * num16 + num23 * num17 + num24 * num18;
      result.Normal.Z = num22 * num19 + num23 * num20 + num24 * num21;
      result.D = PlaneD.D;
    }

    public double Dot(Vector4 value)
    {
      return this.Normal.X * (double) value.X + this.Normal.Y * (double) value.Y + this.Normal.Z * (double) value.Z + this.D * (double) value.W;
    }

    public void Dot(ref Vector4 value, out double result)
    {
      result = this.Normal.X * (double) value.X + this.Normal.Y * (double) value.Y + this.Normal.Z * (double) value.Z + this.D * (double) value.W;
    }

    public double DotCoordinate(Vector3D value)
    {
      return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D;
    }

    public void DotCoordinate(ref Vector3D value, out double result)
    {
      result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D;
    }

    public double DotNormal(Vector3D value)
    {
      return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z;
    }

    public void DotNormal(ref Vector3D value, out double result)
    {
      result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z;
    }

    public PlaneIntersectionType Intersects(BoundingBoxD box)
    {
      Vector3D vector3D1;
      vector3D1.X = this.Normal.X >= 0.0 ? box.Min.X : box.Max.X;
      vector3D1.Y = this.Normal.Y >= 0.0 ? box.Min.Y : box.Max.Y;
      vector3D1.Z = this.Normal.Z >= 0.0 ? box.Min.Z : box.Max.Z;
      Vector3D vector3D2;
      vector3D2.X = this.Normal.X >= 0.0 ? box.Max.X : box.Min.X;
      vector3D2.Y = this.Normal.Y >= 0.0 ? box.Max.Y : box.Min.Y;
      vector3D2.Z = this.Normal.Z >= 0.0 ? box.Max.Z : box.Min.Z;
      if (this.Normal.X * vector3D1.X + this.Normal.Y * vector3D1.Y + this.Normal.Z * vector3D1.Z + this.D > 0.0)
        return PlaneIntersectionType.Front;
      return this.Normal.X * vector3D2.X + this.Normal.Y * vector3D2.Y + this.Normal.Z * vector3D2.Z + this.D >= 0.0 ? PlaneIntersectionType.Intersecting : PlaneIntersectionType.Back;
    }

    public void Intersects(ref BoundingBoxD box, out PlaneIntersectionType result)
    {
      Vector3D vector3D1;
      vector3D1.X = this.Normal.X >= 0.0 ? box.Min.X : box.Max.X;
      vector3D1.Y = this.Normal.Y >= 0.0 ? box.Min.Y : box.Max.Y;
      vector3D1.Z = this.Normal.Z >= 0.0 ? box.Min.Z : box.Max.Z;
      Vector3D vector3D2;
      vector3D2.X = this.Normal.X >= 0.0 ? box.Max.X : box.Min.X;
      vector3D2.Y = this.Normal.Y >= 0.0 ? box.Max.Y : box.Min.Y;
      vector3D2.Z = this.Normal.Z >= 0.0 ? box.Max.Z : box.Min.Z;
      if (this.Normal.X * vector3D1.X + this.Normal.Y * vector3D1.Y + this.Normal.Z * vector3D1.Z + this.D > 0.0)
        result = PlaneIntersectionType.Front;
      else if (this.Normal.X * vector3D2.X + this.Normal.Y * vector3D2.Y + this.Normal.Z * vector3D2.Z + this.D < 0.0)
        result = PlaneIntersectionType.Back;
      else
        result = PlaneIntersectionType.Intersecting;
    }

    public PlaneIntersectionType Intersects(BoundingFrustumD frustum)
    {
      return frustum.Intersects(this);
    }

    public PlaneIntersectionType Intersects(BoundingSphereD sphere)
    {
      double num = sphere.Center.X * this.Normal.X + sphere.Center.Y * this.Normal.Y + sphere.Center.Z * this.Normal.Z + this.D;
      if (num > sphere.Radius)
        return PlaneIntersectionType.Front;
      return num >= -sphere.Radius ? PlaneIntersectionType.Intersecting : PlaneIntersectionType.Back;
    }

    public void Intersects(ref BoundingSphere sphere, out PlaneIntersectionType result)
    {
      double num = (double) sphere.Center.X * this.Normal.X + (double) sphere.Center.Y * this.Normal.Y + (double) sphere.Center.Z * this.Normal.Z + this.D;
      if (num > (double) sphere.Radius)
        result = PlaneIntersectionType.Front;
      else if (num < -(double) sphere.Radius)
        result = PlaneIntersectionType.Back;
      else
        result = PlaneIntersectionType.Intersecting;
    }

    public Vector3D RandomPoint()
    {
      if (PlaneD._random == null)
        PlaneD._random = new Random();
      Vector3D vector1 = new Vector3D();
      Vector3D vector3D;
      do
      {
        vector1.X = 2.0 * PlaneD._random.NextDouble() - 1.0;
        vector1.Y = 2.0 * PlaneD._random.NextDouble() - 1.0;
        vector1.Z = 2.0 * PlaneD._random.NextDouble() - 1.0;
        vector3D = Vector3D.Cross(vector1, this.Normal);
      }
      while (vector3D == Vector3D.Zero);
      vector3D.Normalize();
      return vector3D * Math.Sqrt(PlaneD._random.NextDouble());
    }

    public double DistanceToPoint(Vector3D point)
    {
      return Vector3D.Dot(this.Normal, point) - this.D;
    }

    public double DistanceToPoint(ref Vector3D point)
    {
      return Vector3D.Dot(this.Normal, point) - this.D;
    }
  }
}
