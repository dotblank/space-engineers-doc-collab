// Decompiled with JetBrains decompiler
// Type: VRageMath.BoundingSphereD
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Collections.Generic;
using System.Globalization;

namespace VRageMath
{
  [Serializable]
  public struct BoundingSphereD : IEquatable<BoundingSphereD>
  {
    public Vector3D Center;
    public double Radius;

    public BoundingSphereD(Vector3D center, double radius)
    {
      this.Center = center;
      this.Radius = radius;
    }

    public static implicit operator BoundingSphereD(BoundingSphere b)
    {
      return new BoundingSphereD((Vector3D) b.Center, (double) b.Radius);
    }

    public static implicit operator BoundingSphere(BoundingSphereD b)
    {
      return new BoundingSphere((Vector3) b.Center, (float) b.Radius);
    }

    public static bool operator ==(BoundingSphereD a, BoundingSphereD b)
    {
      return a.Equals(b);
    }

    public static bool operator !=(BoundingSphereD a, BoundingSphereD b)
    {
      if (!(a.Center != b.Center))
        return a.Radius != b.Radius;
      else
        return true;
    }

    public bool Equals(BoundingSphereD other)
    {
      if (this.Center == other.Center)
        return this.Radius == other.Radius;
      else
        return false;
    }

    public override bool Equals(object obj)
    {
      bool flag = false;
      if (obj is BoundingSphereD)
        flag = this.Equals((BoundingSphereD) obj);
      return flag;
    }

    public override int GetHashCode()
    {
      return this.Center.GetHashCode() + this.Radius.GetHashCode();
    }

    public override string ToString()
    {
      CultureInfo currentCulture = CultureInfo.CurrentCulture;
      return string.Format((IFormatProvider) currentCulture, "{{Center:{0} Radius:{1}}}", new object[2]
      {
        (object) this.Center.ToString(),
        (object) this.Radius.ToString((IFormatProvider) currentCulture)
      });
    }

    public static BoundingSphereD CreateMerged(BoundingSphereD original, BoundingSphereD additional)
    {
      Vector3D result;
      Vector3D.Subtract(ref additional.Center, ref original.Center, out result);
      double num1 = result.Length();
      double num2 = original.Radius;
      double num3 = additional.Radius;
      if (num2 + num3 >= num1)
      {
        if (num2 - num3 >= num1)
          return original;
        if (num3 - num2 >= num1)
          return additional;
      }
      Vector3D vector3D = result * (1.0 / num1);
      double num4 = MathHelper.Min(-num2, num1 - num3);
      double num5 = (MathHelper.Max(num2, num1 + num3) - num4) * 0.5;
      BoundingSphereD boundingSphereD;
      boundingSphereD.Center = original.Center + vector3D * (num5 + num4);
      boundingSphereD.Radius = num5;
      return boundingSphereD;
    }

    public static void CreateMerged(ref BoundingSphereD original, ref BoundingSphereD additional, out BoundingSphereD result)
    {
      Vector3D result1;
      Vector3D.Subtract(ref additional.Center, ref original.Center, out result1);
      double num1 = result1.Length();
      double num2 = original.Radius;
      double num3 = additional.Radius;
      if (num2 + num3 >= num1)
      {
        if (num2 - num3 >= num1)
        {
          result = original;
          return;
        }
        else if (num3 - num2 >= num1)
        {
          result = additional;
          return;
        }
      }
      Vector3D vector3D = result1 * (1.0 / num1);
      double num4 = MathHelper.Min(-num2, num1 - num3);
      double num5 = (MathHelper.Max(num2, num1 + num3) - num4) * 0.5;
      result.Center = original.Center + vector3D * (num5 + num4);
      result.Radius = num5;
    }

    public static BoundingSphereD CreateFromBoundingBox(BoundingBoxD box)
    {
      BoundingSphereD boundingSphereD;
      Vector3D.Lerp(ref box.Min, ref box.Max, 0.5, out boundingSphereD.Center);
      double result;
      Vector3D.Distance(ref box.Min, ref box.Max, out result);
      boundingSphereD.Radius = result * 0.5;
      return boundingSphereD;
    }

    public static void CreateFromBoundingBox(ref BoundingBoxD box, out BoundingSphereD result)
    {
      Vector3D.Lerp(ref box.Min, ref box.Max, 0.5, out result.Center);
      double result1;
      Vector3D.Distance(ref box.Min, ref box.Max, out result1);
      result.Radius = result1 * 0.5;
    }

    public static BoundingSphereD CreateFromPoints(IEnumerable<Vector3D> points)
    {
      Vector3D current;
      Vector3D vector3D1 = current = points.GetEnumerator().Current;
      Vector3D vector3D2 = current;
      Vector3D vector3D3 = current;
      Vector3D vector3D4 = current;
      Vector3D vector3D5 = current;
      Vector3D vector3D6 = current;
      foreach (Vector3D vector3D7 in points)
      {
        if (vector3D7.X < vector3D6.X)
          vector3D6 = vector3D7;
        if (vector3D7.X > vector3D5.X)
          vector3D5 = vector3D7;
        if (vector3D7.Y < vector3D4.Y)
          vector3D4 = vector3D7;
        if (vector3D7.Y > vector3D3.Y)
          vector3D3 = vector3D7;
        if (vector3D7.Z < vector3D2.Z)
          vector3D2 = vector3D7;
        if (vector3D7.Z > vector3D1.Z)
          vector3D1 = vector3D7;
      }
      double result1;
      Vector3D.Distance(ref vector3D5, ref vector3D6, out result1);
      double result2;
      Vector3D.Distance(ref vector3D3, ref vector3D4, out result2);
      double result3;
      Vector3D.Distance(ref vector3D1, ref vector3D2, out result3);
      Vector3D result4;
      double num1;
      if (result1 > result2)
      {
        if (result1 > result3)
        {
          Vector3D.Lerp(ref vector3D5, ref vector3D6, 0.5, out result4);
          num1 = result1 * 0.5;
        }
        else
        {
          Vector3D.Lerp(ref vector3D1, ref vector3D2, 0.5, out result4);
          num1 = result3 * 0.5;
        }
      }
      else if (result2 > result3)
      {
        Vector3D.Lerp(ref vector3D3, ref vector3D4, 0.5, out result4);
        num1 = result2 * 0.5;
      }
      else
      {
        Vector3D.Lerp(ref vector3D1, ref vector3D2, 0.5, out result4);
        num1 = result3 * 0.5;
      }
      foreach (Vector3D vector3D7 in points)
      {
        Vector3D vector3D8;
        vector3D8.X = vector3D7.X - result4.X;
        vector3D8.Y = vector3D7.Y - result4.Y;
        vector3D8.Z = vector3D7.Z - result4.Z;
        double num2 = vector3D8.Length();
        if (num2 > num1)
        {
          num1 = (num1 + num2) * 0.5;
          result4 += (1.0 - num1 / num2) * vector3D8;
        }
      }
      BoundingSphereD boundingSphereD;
      boundingSphereD.Center = result4;
      boundingSphereD.Radius = num1;
      return boundingSphereD;
    }

    public static BoundingSphereD CreateFromFrustum(BoundingFrustumD frustum)
    {
      if (frustum == (BoundingFrustumD) null)
        throw new ArgumentNullException("frustum");
      else
        return BoundingSphereD.CreateFromPoints((IEnumerable<Vector3D>) frustum.cornerArray);
    }

    public bool Intersects(BoundingBoxD box)
    {
      Vector3D result1;
      Vector3D.Clamp(ref this.Center, ref box.Min, ref box.Max, out result1);
      double result2;
      Vector3D.DistanceSquared(ref this.Center, ref result1, out result2);
      return result2 <= this.Radius * this.Radius;
    }

    public void Intersects(ref BoundingBoxD box, out bool result)
    {
      Vector3D result1;
      Vector3D.Clamp(ref this.Center, ref box.Min, ref box.Max, out result1);
      double result2;
      Vector3D.DistanceSquared(ref this.Center, ref result1, out result2);
      result = result2 <= this.Radius * this.Radius;
    }

    public double? Intersects(RayD ray)
    {
      return ray.Intersects(this);
    }

    public bool Intersects(BoundingFrustumD frustum)
    {
      bool result;
      frustum.Intersects(ref this, out result);
      return result;
    }

    public bool Intersects(BoundingSphereD sphere)
    {
      double result;
      Vector3D.DistanceSquared(ref this.Center, ref sphere.Center, out result);
      double num1 = this.Radius;
      double num2 = sphere.Radius;
      return num1 * num1 + 2.0 * num1 * num2 + num2 * num2 > result;
    }

    public void Intersects(ref BoundingSphereD sphere, out bool result)
    {
      double result1;
      Vector3D.DistanceSquared(ref this.Center, ref sphere.Center, out result1);
      double num1 = this.Radius;
      double num2 = sphere.Radius;
      result = num1 * num1 + 2.0 * num1 * num2 + num2 * num2 > result1;
    }

    public ContainmentType Contains(BoundingBoxD box)
    {
      if (!box.Intersects(this))
        return ContainmentType.Disjoint;
      double num = this.Radius * this.Radius;
      Vector3D vector3D;
      vector3D.X = this.Center.X - box.Min.X;
      vector3D.Y = this.Center.Y - box.Max.Y;
      vector3D.Z = this.Center.Z - box.Max.Z;
      if (vector3D.LengthSquared() > num)
        return ContainmentType.Intersects;
      vector3D.X = this.Center.X - box.Max.X;
      vector3D.Y = this.Center.Y - box.Max.Y;
      vector3D.Z = this.Center.Z - box.Max.Z;
      if (vector3D.LengthSquared() > num)
        return ContainmentType.Intersects;
      vector3D.X = this.Center.X - box.Max.X;
      vector3D.Y = this.Center.Y - box.Min.Y;
      vector3D.Z = this.Center.Z - box.Max.Z;
      if (vector3D.LengthSquared() > num)
        return ContainmentType.Intersects;
      vector3D.X = this.Center.X - box.Min.X;
      vector3D.Y = this.Center.Y - box.Min.Y;
      vector3D.Z = this.Center.Z - box.Max.Z;
      if (vector3D.LengthSquared() > num)
        return ContainmentType.Intersects;
      vector3D.X = this.Center.X - box.Min.X;
      vector3D.Y = this.Center.Y - box.Max.Y;
      vector3D.Z = this.Center.Z - box.Min.Z;
      if (vector3D.LengthSquared() > num)
        return ContainmentType.Intersects;
      vector3D.X = this.Center.X - box.Max.X;
      vector3D.Y = this.Center.Y - box.Max.Y;
      vector3D.Z = this.Center.Z - box.Min.Z;
      if (vector3D.LengthSquared() > num)
        return ContainmentType.Intersects;
      vector3D.X = this.Center.X - box.Max.X;
      vector3D.Y = this.Center.Y - box.Min.Y;
      vector3D.Z = this.Center.Z - box.Min.Z;
      if (vector3D.LengthSquared() > num)
        return ContainmentType.Intersects;
      vector3D.X = this.Center.X - box.Min.X;
      vector3D.Y = this.Center.Y - box.Min.Y;
      vector3D.Z = this.Center.Z - box.Min.Z;
      return vector3D.LengthSquared() <= num ? ContainmentType.Contains : ContainmentType.Intersects;
    }

    public void Contains(ref BoundingBoxD box, out ContainmentType result)
    {
      bool result1;
      box.Intersects(ref this, out result1);
      if (!result1)
      {
        result = ContainmentType.Disjoint;
      }
      else
      {
        double num = this.Radius * this.Radius;
        result = ContainmentType.Intersects;
        Vector3D vector3D;
        vector3D.X = this.Center.X - box.Min.X;
        vector3D.Y = this.Center.Y - box.Max.Y;
        vector3D.Z = this.Center.Z - box.Max.Z;
        if (vector3D.LengthSquared() > num)
          return;
        vector3D.X = this.Center.X - box.Max.X;
        vector3D.Y = this.Center.Y - box.Max.Y;
        vector3D.Z = this.Center.Z - box.Max.Z;
        if (vector3D.LengthSquared() > num)
          return;
        vector3D.X = this.Center.X - box.Max.X;
        vector3D.Y = this.Center.Y - box.Min.Y;
        vector3D.Z = this.Center.Z - box.Max.Z;
        if (vector3D.LengthSquared() > num)
          return;
        vector3D.X = this.Center.X - box.Min.X;
        vector3D.Y = this.Center.Y - box.Min.Y;
        vector3D.Z = this.Center.Z - box.Max.Z;
        if (vector3D.LengthSquared() > num)
          return;
        vector3D.X = this.Center.X - box.Min.X;
        vector3D.Y = this.Center.Y - box.Max.Y;
        vector3D.Z = this.Center.Z - box.Min.Z;
        if (vector3D.LengthSquared() > num)
          return;
        vector3D.X = this.Center.X - box.Max.X;
        vector3D.Y = this.Center.Y - box.Max.Y;
        vector3D.Z = this.Center.Z - box.Min.Z;
        if (vector3D.LengthSquared() > num)
          return;
        vector3D.X = this.Center.X - box.Max.X;
        vector3D.Y = this.Center.Y - box.Min.Y;
        vector3D.Z = this.Center.Z - box.Min.Z;
        if (vector3D.LengthSquared() > num)
          return;
        vector3D.X = this.Center.X - box.Min.X;
        vector3D.Y = this.Center.Y - box.Min.Y;
        vector3D.Z = this.Center.Z - box.Min.Z;
        if (vector3D.LengthSquared() > num)
          return;
        result = ContainmentType.Contains;
      }
    }

    public ContainmentType Contains(BoundingFrustumD frustum)
    {
      if (!frustum.Intersects(this))
        return ContainmentType.Disjoint;
      double num = this.Radius * this.Radius;
      foreach (Vector3D vector3D1 in frustum.cornerArray)
      {
        Vector3D vector3D2;
        vector3D2.X = vector3D1.X - this.Center.X;
        vector3D2.Y = vector3D1.Y - this.Center.Y;
        vector3D2.Z = vector3D1.Z - this.Center.Z;
        if (vector3D2.LengthSquared() > num)
          return ContainmentType.Intersects;
      }
      return ContainmentType.Contains;
    }

    public ContainmentType Contains(Vector3D point)
    {
      return Vector3D.DistanceSquared(point, this.Center) < this.Radius * this.Radius ? ContainmentType.Contains : ContainmentType.Disjoint;
    }

    public void Contains(ref Vector3D point, out ContainmentType result)
    {
      double result1;
      Vector3D.DistanceSquared(ref point, ref this.Center, out result1);
      result = result1 < this.Radius * this.Radius ? ContainmentType.Contains : ContainmentType.Disjoint;
    }

    public ContainmentType Contains(BoundingSphereD sphere)
    {
      double result;
      Vector3D.Distance(ref this.Center, ref sphere.Center, out result);
      double num1 = this.Radius;
      double num2 = sphere.Radius;
      if (num1 + num2 < result)
        return ContainmentType.Disjoint;
      return num1 - num2 >= result ? ContainmentType.Contains : ContainmentType.Intersects;
    }

    public void Contains(ref BoundingSphereD sphere, out ContainmentType result)
    {
      double result1;
      Vector3D.Distance(ref this.Center, ref sphere.Center, out result1);
      double num1 = this.Radius;
      double num2 = sphere.Radius;
      result = num1 + num2 >= result1 ? (num1 - num2 >= result1 ? ContainmentType.Contains : ContainmentType.Intersects) : ContainmentType.Disjoint;
    }

    internal void SupportMapping(ref Vector3D v, out Vector3D result)
    {
      double num = this.Radius / v.Length();
      result.X = this.Center.X + v.X * num;
      result.Y = this.Center.Y + v.Y * num;
      result.Z = this.Center.Z + v.Z * num;
    }

    public BoundingSphereD Transform(MatrixD matrix)
    {
      BoundingSphereD boundingSphereD = new BoundingSphereD();
      boundingSphereD.Center = Vector3D.Transform(this.Center, matrix);
      double d = Math.Max(matrix.M11 * matrix.M11 + matrix.M12 * matrix.M12 + matrix.M13 * matrix.M13, Math.Max(matrix.M21 * matrix.M21 + matrix.M22 * matrix.M22 + matrix.M23 * matrix.M23, matrix.M31 * matrix.M31 + matrix.M32 * matrix.M32 + matrix.M33 * matrix.M33));
      boundingSphereD.Radius = this.Radius * Math.Sqrt(d);
      return boundingSphereD;
    }

    public void Transform(ref MatrixD matrix, out BoundingSphereD result)
    {
      result.Center = Vector3D.Transform(this.Center, matrix);
      double d = Math.Max(matrix.M11 * matrix.M11 + matrix.M12 * matrix.M12 + matrix.M13 * matrix.M13, Math.Max(matrix.M21 * matrix.M21 + matrix.M22 * matrix.M22 + matrix.M23 * matrix.M23, matrix.M31 * matrix.M31 + matrix.M32 * matrix.M32 + matrix.M33 * matrix.M33));
      result.Radius = this.Radius * Math.Sqrt(d);
    }

    public bool IntersectRaySphere(RayD ray, out double tmin, out double tmax)
    {
      tmin = 0.0;
      tmax = 0.0;
      Vector3D v = ray.Position - this.Center;
      double num1 = ray.Direction.Dot(ray.Direction);
      double num2 = 2.0 * v.Dot(ray.Direction);
      double num3 = v.Dot(v) - this.Radius * this.Radius;
      double d = num2 * num2 - 4.0 * num1 * num3;
      if (d < 0.0)
        return false;
      tmin = (-num2 - Math.Sqrt(d)) / (2.0 * num1);
      tmax = (-num2 + Math.Sqrt(d)) / (2.0 * num1);
      if (tmin > tmax)
      {
        double num4 = tmin;
        tmin = tmax;
        tmax = num4;
      }
      return true;
    }

    public BoundingSphereD Include(BoundingSphereD sphere)
    {
      BoundingSphereD.Include(ref this, ref sphere);
      return this;
    }

    public static void Include(ref BoundingSphereD sphere, ref BoundingSphereD otherSphere)
    {
      if (sphere.Radius == double.MinValue)
      {
        sphere.Center = otherSphere.Center;
        sphere.Radius = otherSphere.Radius;
      }
      else
      {
        double num1 = Vector3D.Distance(sphere.Center, otherSphere.Center);
        if (num1 + otherSphere.Radius <= sphere.Radius)
          return;
        if (num1 + sphere.Radius <= otherSphere.Radius)
        {
          sphere = otherSphere;
        }
        else
        {
          double amount = (num1 + otherSphere.Radius - sphere.Radius) / (2.0 * num1);
          Vector3D vector3D = Vector3D.Lerp(sphere.Center, otherSphere.Center, amount);
          double num2 = (num1 + sphere.Radius + otherSphere.Radius) / 2.0;
          sphere.Center = vector3D;
          sphere.Radius = num2;
        }
      }
    }
  }
}
