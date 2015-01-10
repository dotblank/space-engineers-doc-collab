// Decompiled with JetBrains decompiler
// Type: VRageMath.BoundingFrustumD
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Globalization;

namespace VRageMath
{
  [Serializable]
  public class BoundingFrustumD : IEquatable<BoundingFrustumD>
  {
    private PlaneD[] planes = new PlaneD[6];
    internal Vector3D[] cornerArray = new Vector3D[8];
    public const int CornerCount = 8;
    private const int NearPlaneIndex = 0;
    private const int FarPlaneIndex = 1;
    private const int LeftPlaneIndex = 2;
    private const int RightPlaneIndex = 3;
    private const int TopPlaneIndex = 4;
    private const int BottomPlaneIndex = 5;
    private const int NumPlanes = 6;
    private MatrixD matrix;
    private GjkD gjk;

    public PlaneD this[int index]
    {
      get
      {
        return this.planes[index];
      }
    }

    public PlaneD Near
    {
      get
      {
        return this.planes[0];
      }
    }

    public PlaneD Far
    {
      get
      {
        return this.planes[1];
      }
    }

    public PlaneD Left
    {
      get
      {
        return this.planes[2];
      }
    }

    public PlaneD Right
    {
      get
      {
        return this.planes[3];
      }
    }

    public PlaneD Top
    {
      get
      {
        return this.planes[4];
      }
    }

    public PlaneD Bottom
    {
      get
      {
        return this.planes[5];
      }
    }

    public MatrixD Matrix
    {
      get
      {
        return this.matrix;
      }
      set
      {
        this.SetMatrix(ref value);
      }
    }

    private BoundingFrustumD()
    {
    }

    public BoundingFrustumD(MatrixD value)
    {
      this.SetMatrix(ref value);
    }

    public static bool operator ==(BoundingFrustumD a, BoundingFrustumD b)
    {
      return object.Equals((object) a, (object) b);
    }

    public static bool operator !=(BoundingFrustumD a, BoundingFrustumD b)
    {
      return !object.Equals((object) a, (object) b);
    }

    public Vector3D[] GetCorners()
    {
      return (Vector3D[]) this.cornerArray.Clone();
    }

    public void GetCorners(Vector3D[] corners)
    {
      this.cornerArray.CopyTo((Array) corners, 0);
    }

    public bool Equals(BoundingFrustumD other)
    {
      if (other == (BoundingFrustumD) null)
        return false;
      else
        return this.matrix == other.matrix;
    }

    public override bool Equals(object obj)
    {
      bool flag = false;
      BoundingFrustumD boundingFrustumD = obj as BoundingFrustumD;
      if (boundingFrustumD != (BoundingFrustumD) null)
        flag = this.matrix == boundingFrustumD.matrix;
      return flag;
    }

    public override int GetHashCode()
    {
      return this.matrix.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{{Near:{0} Far:{1} Left:{2} Right:{3} Top:{4} Bottom:{5}}}", (object) this.Near.ToString(), (object) this.Far.ToString(), (object) this.Left.ToString(), (object) this.Right.ToString(), (object) this.Top.ToString(), (object) this.Bottom.ToString());
    }

    private void SetMatrix(ref MatrixD value)
    {
      this.matrix = value;
      this.planes[2].Normal.X = -value.M14 - value.M11;
      this.planes[2].Normal.Y = -value.M24 - value.M21;
      this.planes[2].Normal.Z = -value.M34 - value.M31;
      this.planes[2].D = -value.M44 - value.M41;
      this.planes[3].Normal.X = -value.M14 + value.M11;
      this.planes[3].Normal.Y = -value.M24 + value.M21;
      this.planes[3].Normal.Z = -value.M34 + value.M31;
      this.planes[3].D = -value.M44 + value.M41;
      this.planes[4].Normal.X = -value.M14 + value.M12;
      this.planes[4].Normal.Y = -value.M24 + value.M22;
      this.planes[4].Normal.Z = -value.M34 + value.M32;
      this.planes[4].D = -value.M44 + value.M42;
      this.planes[5].Normal.X = -value.M14 - value.M12;
      this.planes[5].Normal.Y = -value.M24 - value.M22;
      this.planes[5].Normal.Z = -value.M34 - value.M32;
      this.planes[5].D = -value.M44 - value.M42;
      this.planes[0].Normal.X = -value.M13;
      this.planes[0].Normal.Y = -value.M23;
      this.planes[0].Normal.Z = -value.M33;
      this.planes[0].D = -value.M43;
      this.planes[1].Normal.X = -value.M14 + value.M13;
      this.planes[1].Normal.Y = -value.M24 + value.M23;
      this.planes[1].Normal.Z = -value.M34 + value.M33;
      this.planes[1].D = -value.M44 + value.M43;
      for (int index = 0; index < 6; ++index)
      {
        double num = this.planes[index].Normal.Length();
        this.planes[index].Normal = this.planes[index].Normal / num;
        this.planes[index].D = this.planes[index].D / num;
      }
      RayD intersectionLine1 = BoundingFrustumD.ComputeIntersectionLine(ref this.planes[0], ref this.planes[2]);
      this.cornerArray[0] = BoundingFrustumD.ComputeIntersection(ref this.planes[4], ref intersectionLine1);
      this.cornerArray[3] = BoundingFrustumD.ComputeIntersection(ref this.planes[5], ref intersectionLine1);
      RayD intersectionLine2 = BoundingFrustumD.ComputeIntersectionLine(ref this.planes[3], ref this.planes[0]);
      this.cornerArray[1] = BoundingFrustumD.ComputeIntersection(ref this.planes[4], ref intersectionLine2);
      this.cornerArray[2] = BoundingFrustumD.ComputeIntersection(ref this.planes[5], ref intersectionLine2);
      intersectionLine2 = BoundingFrustumD.ComputeIntersectionLine(ref this.planes[2], ref this.planes[1]);
      this.cornerArray[4] = BoundingFrustumD.ComputeIntersection(ref this.planes[4], ref intersectionLine2);
      this.cornerArray[7] = BoundingFrustumD.ComputeIntersection(ref this.planes[5], ref intersectionLine2);
      intersectionLine2 = BoundingFrustumD.ComputeIntersectionLine(ref this.planes[1], ref this.planes[3]);
      this.cornerArray[5] = BoundingFrustumD.ComputeIntersection(ref this.planes[4], ref intersectionLine2);
      this.cornerArray[6] = BoundingFrustumD.ComputeIntersection(ref this.planes[5], ref intersectionLine2);
    }

    private static RayD ComputeIntersectionLine(ref PlaneD p1, ref PlaneD p2)
    {
      RayD rayD = new RayD();
      rayD.Direction = Vector3D.Cross(p1.Normal, p2.Normal);
      double num = rayD.Direction.LengthSquared();
      rayD.Position = Vector3D.Cross(-p1.D * p2.Normal + p2.D * p1.Normal, rayD.Direction) / num;
      return rayD;
    }

    private static Vector3D ComputeIntersection(ref PlaneD plane, ref RayD ray)
    {
      double num = (-plane.D - Vector3D.Dot(plane.Normal, ray.Position)) / Vector3D.Dot(plane.Normal, ray.Direction);
      return ray.Position + ray.Direction * num;
    }

    public bool Intersects(BoundingBoxD box)
    {
      bool result;
      this.Intersects(ref box, out result);
      return result;
    }

    public void Intersects(ref BoundingBoxD box, out bool result)
    {
      if (this.gjk == null)
        this.gjk = new GjkD();
      this.gjk.Reset();
      Vector3D result1;
      Vector3D.Subtract(ref this.cornerArray[0], ref box.Min, out result1);
      if (result1.LengthSquared() < 9.99999974737875E-06)
        Vector3D.Subtract(ref this.cornerArray[0], ref box.Max, out result1);
      double num1 = double.MaxValue;
      result = false;
      double num2;
      do
      {
        Vector3D v;
        v.X = -result1.X;
        v.Y = -result1.Y;
        v.Z = -result1.Z;
        Vector3D result2;
        this.SupportMapping(ref v, out result2);
        Vector3D result3;
        box.SupportMapping(ref result1, out result3);
        Vector3D result4;
        Vector3D.Subtract(ref result2, ref result3, out result4);
        if (result1.X * result4.X + result1.Y * result4.Y + result1.Z * result4.Z > 0.0)
          return;
        this.gjk.AddSupportPoint(ref result4);
        result1 = this.gjk.ClosestPoint;
        double num3 = num1;
        num1 = result1.LengthSquared();
        if (num3 - num1 <= 9.99999974737875E-06 * num3)
          return;
        num2 = 3.9999998989515E-05 * this.gjk.MaxLengthSquared;
      }
      while (!this.gjk.FullSimplex && num1 >= num2);
      result = true;
    }

    public bool Intersects(BoundingFrustumD frustum)
    {
      if (frustum == (BoundingFrustumD) null)
        throw new ArgumentNullException("frustum");
      if (this.gjk == null)
        this.gjk = new GjkD();
      this.gjk.Reset();
      Vector3D result1;
      Vector3D.Subtract(ref this.cornerArray[0], ref frustum.cornerArray[0], out result1);
      if (result1.LengthSquared() < 9.99999974737875E-06)
        Vector3D.Subtract(ref this.cornerArray[0], ref frustum.cornerArray[1], out result1);
      double num1 = double.MaxValue;
      double num2;
      do
      {
        Vector3D v;
        v.X = -result1.X;
        v.Y = -result1.Y;
        v.Z = -result1.Z;
        Vector3D result2;
        this.SupportMapping(ref v, out result2);
        Vector3D result3;
        frustum.SupportMapping(ref result1, out result3);
        Vector3D result4;
        Vector3D.Subtract(ref result2, ref result3, out result4);
        if (result1.X * result4.X + result1.Y * result4.Y + result1.Z * result4.Z > 0.0)
          return false;
        this.gjk.AddSupportPoint(ref result4);
        result1 = this.gjk.ClosestPoint;
        double num3 = num1;
        num1 = result1.LengthSquared();
        num2 = 3.9999998989515E-05 * this.gjk.MaxLengthSquared;
        if (num3 - num1 <= 9.99999974737875E-06 * num3)
          return false;
      }
      while (!this.gjk.FullSimplex && num1 >= num2);
      return true;
    }

    public PlaneIntersectionType Intersects(PlaneD plane)
    {
      int num = 0;
      for (int index = 0; index < 8; ++index)
      {
        double result;
        Vector3D.Dot(ref this.cornerArray[index], ref plane.Normal, out result);
        if (result + plane.D > 0.0)
          num |= 1;
        else
          num |= 2;
        if (num == 3)
          return PlaneIntersectionType.Intersecting;
      }
      return num == 1 ? PlaneIntersectionType.Front : PlaneIntersectionType.Back;
    }

    public void Intersects(ref PlaneD plane, out PlaneIntersectionType result)
    {
      int num = 0;
      for (int index = 0; index < 8; ++index)
      {
        double result1;
        Vector3D.Dot(ref this.cornerArray[index], ref plane.Normal, out result1);
        if (result1 + plane.D > 0.0)
          num |= 1;
        else
          num |= 2;
        if (num == 3)
        {
          result = PlaneIntersectionType.Intersecting;
          return;
        }
      }
      result = num == 1 ? PlaneIntersectionType.Front : PlaneIntersectionType.Back;
    }

    public double? Intersects(RayD ray)
    {
      double? result;
      this.Intersects(ref ray, out result);
      return result;
    }

    public void Intersects(ref RayD ray, out double? result)
    {
      ContainmentType result1;
      this.Contains(ref ray.Position, out result1);
      if (result1 == ContainmentType.Contains)
      {
        result = new double?(0.0);
      }
      else
      {
        double num1 = double.MinValue;
        double num2 = double.MaxValue;
        result = new double?();
        foreach (PlaneD planeD in this.planes)
        {
          Vector3D vector2 = planeD.Normal;
          double result2;
          Vector3D.Dot(ref ray.Direction, ref vector2, out result2);
          double result3;
          Vector3D.Dot(ref ray.Position, ref vector2, out result3);
          result3 += planeD.D;
          if (Math.Abs(result2) < 9.99999974737875E-06)
          {
            if (result3 > 0.0)
              return;
          }
          else
          {
            double num3 = -result3 / result2;
            if (result2 < 0.0)
            {
              if (num3 > num2)
                return;
              if (num3 > num1)
                num1 = num3;
            }
            else
            {
              if (num3 < num1)
                return;
              if (num3 < num2)
                num2 = num3;
            }
          }
        }
        double num4 = num1 >= 0.0 ? num1 : num2;
        if (num4 < 0.0)
          return;
        result = new double?(num4);
      }
    }

    public bool Intersects(BoundingSphereD sphere)
    {
      bool result;
      this.Intersects(ref sphere, out result);
      return result;
    }

    public void Intersects(ref BoundingSphereD sphere, out bool result)
    {
      if (this.gjk == null)
        this.gjk = new GjkD();
      this.gjk.Reset();
      Vector3D result1;
      Vector3D.Subtract(ref this.cornerArray[0], ref sphere.Center, out result1);
      if (result1.LengthSquared() < 9.99999974737875E-06)
        result1 = Vector3D.UnitX;
      double num1 = double.MaxValue;
      result = false;
      double num2;
      do
      {
        Vector3D v;
        v.X = -result1.X;
        v.Y = -result1.Y;
        v.Z = -result1.Z;
        Vector3D result2;
        this.SupportMapping(ref v, out result2);
        Vector3D result3;
        sphere.SupportMapping(ref result1, out result3);
        Vector3D result4;
        Vector3D.Subtract(ref result2, ref result3, out result4);
        if (result1.X * result4.X + result1.Y * result4.Y + result1.Z * result4.Z > 0.0)
          return;
        this.gjk.AddSupportPoint(ref result4);
        result1 = this.gjk.ClosestPoint;
        double num3 = num1;
        num1 = result1.LengthSquared();
        if (num3 - num1 <= 9.99999974737875E-06 * num3)
          return;
        num2 = 3.9999998989515E-05 * this.gjk.MaxLengthSquared;
      }
      while (!this.gjk.FullSimplex && num1 >= num2);
      result = true;
    }

    public ContainmentType Contains(BoundingBoxD box)
    {
      bool flag = false;
      foreach (PlaneD plane in this.planes)
      {
        switch (box.Intersects(plane))
        {
          case PlaneIntersectionType.Front:
            return ContainmentType.Disjoint;
          case PlaneIntersectionType.Intersecting:
            flag = true;
            break;
        }
      }
      return flag ? ContainmentType.Intersects : ContainmentType.Contains;
    }

    public void Contains(ref BoundingBoxD box, out ContainmentType result)
    {
      bool flag = false;
      foreach (PlaneD plane in this.planes)
      {
        switch (box.Intersects(plane))
        {
          case PlaneIntersectionType.Front:
            result = ContainmentType.Disjoint;
            return;
          case PlaneIntersectionType.Intersecting:
            flag = true;
            break;
        }
      }
      result = flag ? ContainmentType.Intersects : ContainmentType.Contains;
    }

    public ContainmentType Contains(BoundingFrustumD frustum)
    {
      if (frustum == (BoundingFrustumD) null)
        throw new ArgumentNullException("frustum");
      ContainmentType containmentType = ContainmentType.Disjoint;
      if (this.Intersects(frustum))
      {
        containmentType = ContainmentType.Contains;
        for (int index = 0; index < this.cornerArray.Length; ++index)
        {
          if (this.Contains(frustum.cornerArray[index]) == ContainmentType.Disjoint)
          {
            containmentType = ContainmentType.Intersects;
            break;
          }
        }
      }
      return containmentType;
    }

    public ContainmentType Contains(Vector3D point)
    {
      foreach (PlaneD planeD in this.planes)
      {
        if (planeD.Normal.X * point.X + planeD.Normal.Y * point.Y + planeD.Normal.Z * point.Z + planeD.D > 9.99999974737875E-06)
          return ContainmentType.Disjoint;
      }
      return ContainmentType.Contains;
    }

    public void Contains(ref Vector3D point, out ContainmentType result)
    {
      foreach (PlaneD planeD in this.planes)
      {
        if (planeD.Normal.X * point.X + planeD.Normal.Y * point.Y + planeD.Normal.Z * point.Z + planeD.D > 9.99999974737875E-06)
        {
          result = ContainmentType.Disjoint;
          return;
        }
      }
      result = ContainmentType.Contains;
    }

    public ContainmentType Contains(BoundingSphereD sphere)
    {
      Vector3D vector3D = sphere.Center;
      double num1 = sphere.Radius;
      int num2 = 0;
      foreach (PlaneD planeD in this.planes)
      {
        double num3 = planeD.Normal.X * vector3D.X + planeD.Normal.Y * vector3D.Y + planeD.Normal.Z * vector3D.Z + planeD.D;
        if (num3 > num1)
          return ContainmentType.Disjoint;
        if (num3 < -num1)
          ++num2;
      }
      return num2 == 6 ? ContainmentType.Contains : ContainmentType.Intersects;
    }

    public void Contains(ref BoundingSphereD sphere, out ContainmentType result)
    {
      Vector3D vector3D = sphere.Center;
      double num1 = sphere.Radius;
      int num2 = 0;
      foreach (PlaneD planeD in this.planes)
      {
        double num3 = planeD.Normal.X * vector3D.X + planeD.Normal.Y * vector3D.Y + planeD.Normal.Z * vector3D.Z + planeD.D;
        if (num3 > num1)
        {
          result = ContainmentType.Disjoint;
          return;
        }
        else if (num3 < -num1)
          ++num2;
      }
      result = num2 == 6 ? ContainmentType.Contains : ContainmentType.Intersects;
    }

    internal void SupportMapping(ref Vector3D v, out Vector3D result)
    {
      int index1 = 0;
      double result1;
      Vector3D.Dot(ref this.cornerArray[0], ref v, out result1);
      for (int index2 = 1; index2 < this.cornerArray.Length; ++index2)
      {
        double result2;
        Vector3D.Dot(ref this.cornerArray[index2], ref v, out result2);
        if (result2 > result1)
        {
          index1 = index2;
          result1 = result2;
        }
      }
      result = this.cornerArray[index1];
    }
  }
}
