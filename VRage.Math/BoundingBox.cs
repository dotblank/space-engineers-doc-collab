// Decompiled with JetBrains decompiler
// Type: VRageMath.BoundingBox
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace VRageMath
{
  [ProtoContract]
  [Serializable]
  public struct BoundingBox : IEquatable<BoundingBox>
  {
    private static Vector3[] m_frustumPoints = (Vector3[]) null;
    public static readonly BoundingBox.ComparerType Comparer = new BoundingBox.ComparerType();
    public const int CornerCount = 8;
    [ProtoMember(1)]
    public Vector3 Min;
    [ProtoMember(2)]
    public Vector3 Max;

    public Vector3 Center
    {
      get
      {
        return (this.Min + this.Max) / 2f;
      }
    }

    public Vector3 HalfExtents
    {
      get
      {
        return (this.Max - this.Min) / 2f;
      }
    }

    public Matrix Matrix
    {
      get
      {
        Vector3 center = this.Center;
        Vector3 scale = this.Size();
        Matrix result;
        Matrix.CreateTranslation(ref center, out result);
        Matrix.Rescale(ref result, ref scale);
        return result;
      }
    }

    public BoundingBox(Vector3 min, Vector3 max)
    {
      this.Min = min;
      this.Max = max;
    }

    public static bool operator ==(BoundingBox a, BoundingBox b)
    {
      return a.Equals(b);
    }

    public static bool operator !=(BoundingBox a, BoundingBox b)
    {
      if (!(a.Min != b.Min))
        return a.Max != b.Max;
      else
        return true;
    }

    public Vector3[] GetCorners()
    {
      return new Vector3[8]
      {
        new Vector3(this.Min.X, this.Max.Y, this.Max.Z),
        new Vector3(this.Max.X, this.Max.Y, this.Max.Z),
        new Vector3(this.Max.X, this.Min.Y, this.Max.Z),
        new Vector3(this.Min.X, this.Min.Y, this.Max.Z),
        new Vector3(this.Min.X, this.Max.Y, this.Min.Z),
        new Vector3(this.Max.X, this.Max.Y, this.Min.Z),
        new Vector3(this.Max.X, this.Min.Y, this.Min.Z),
        new Vector3(this.Min.X, this.Min.Y, this.Min.Z)
      };
    }

    public void GetCorners(Vector3[] corners)
    {
      corners[0].X = this.Min.X;
      corners[0].Y = this.Max.Y;
      corners[0].Z = this.Max.Z;
      corners[1].X = this.Max.X;
      corners[1].Y = this.Max.Y;
      corners[1].Z = this.Max.Z;
      corners[2].X = this.Max.X;
      corners[2].Y = this.Min.Y;
      corners[2].Z = this.Max.Z;
      corners[3].X = this.Min.X;
      corners[3].Y = this.Min.Y;
      corners[3].Z = this.Max.Z;
      corners[4].X = this.Min.X;
      corners[4].Y = this.Max.Y;
      corners[4].Z = this.Min.Z;
      corners[5].X = this.Max.X;
      corners[5].Y = this.Max.Y;
      corners[5].Z = this.Min.Z;
      corners[6].X = this.Max.X;
      corners[6].Y = this.Min.Y;
      corners[6].Z = this.Min.Z;
      corners[7].X = this.Min.X;
      corners[7].Y = this.Min.Y;
      corners[7].Z = this.Min.Z;
    }

    public unsafe void GetCornersUnsafe(Vector3* corners)
    {
      corners->X = this.Min.X;
      corners->Y = this.Max.Y;
      corners->Z = this.Max.Z;
      corners[1].X = this.Max.X;
      corners[1].Y = this.Max.Y;
      corners[1].Z = this.Max.Z;
      corners[2].X = this.Max.X;
      corners[2].Y = this.Min.Y;
      corners[2].Z = this.Max.Z;
      corners[3].X = this.Min.X;
      corners[3].Y = this.Min.Y;
      corners[3].Z = this.Max.Z;
      corners[4].X = this.Min.X;
      corners[4].Y = this.Max.Y;
      corners[4].Z = this.Min.Z;
      corners[5].X = this.Max.X;
      corners[5].Y = this.Max.Y;
      corners[5].Z = this.Min.Z;
      corners[6].X = this.Max.X;
      corners[6].Y = this.Min.Y;
      corners[6].Z = this.Min.Z;
      corners[7].X = this.Min.X;
      corners[7].Y = this.Min.Y;
      corners[7].Z = this.Min.Z;
    }

    public bool Equals(BoundingBox other)
    {
      if (this.Min == other.Min)
        return this.Max == other.Max;
      else
        return false;
    }

    public override bool Equals(object obj)
    {
      bool flag = false;
      if (obj is BoundingBox)
        flag = this.Equals((BoundingBox) obj);
      return flag;
    }

    public override int GetHashCode()
    {
      return this.Min.GetHashCode() + this.Max.GetHashCode();
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "{{Min:{0} Max:{1}}}", new object[2]
      {
        (object) this.Min.ToString(),
        (object) this.Max.ToString()
      });
    }

    public static BoundingBox CreateMerged(BoundingBox original, BoundingBox additional)
    {
      BoundingBox boundingBox;
      Vector3.Min(ref original.Min, ref additional.Min, out boundingBox.Min);
      Vector3.Max(ref original.Max, ref additional.Max, out boundingBox.Max);
      return boundingBox;
    }

    public static void CreateMerged(ref BoundingBox original, ref BoundingBox additional, out BoundingBox result)
    {
      Vector3 result1;
      Vector3.Min(ref original.Min, ref additional.Min, out result1);
      Vector3 result2;
      Vector3.Max(ref original.Max, ref additional.Max, out result2);
      result.Min = result1;
      result.Max = result2;
    }

    public static BoundingBox CreateFromSphere(BoundingSphere sphere)
    {
      BoundingBox boundingBox;
      boundingBox.Min.X = sphere.Center.X - sphere.Radius;
      boundingBox.Min.Y = sphere.Center.Y - sphere.Radius;
      boundingBox.Min.Z = sphere.Center.Z - sphere.Radius;
      boundingBox.Max.X = sphere.Center.X + sphere.Radius;
      boundingBox.Max.Y = sphere.Center.Y + sphere.Radius;
      boundingBox.Max.Z = sphere.Center.Z + sphere.Radius;
      return boundingBox;
    }

    public static void CreateFromSphere(ref BoundingSphere sphere, out BoundingBox result)
    {
      result.Min.X = sphere.Center.X - sphere.Radius;
      result.Min.Y = sphere.Center.Y - sphere.Radius;
      result.Min.Z = sphere.Center.Z - sphere.Radius;
      result.Max.X = sphere.Center.X + sphere.Radius;
      result.Max.Y = sphere.Center.Y + sphere.Radius;
      result.Max.Z = sphere.Center.Z + sphere.Radius;
    }

    public static BoundingBox CreateFromPoints(IEnumerable<Vector3> points)
    {
        return new BoundingBox();
    }

    public BoundingBox Intersect(BoundingBox box)
    {
      BoundingBox boundingBox;
      boundingBox.Min.X = Math.Max(this.Min.X, box.Min.X);
      boundingBox.Min.Y = Math.Max(this.Min.Y, box.Min.Y);
      boundingBox.Min.Z = Math.Max(this.Min.Z, box.Min.Z);
      boundingBox.Max.X = Math.Min(this.Max.X, box.Max.X);
      boundingBox.Max.Y = Math.Min(this.Max.Y, box.Max.Y);
      boundingBox.Max.Z = Math.Min(this.Max.Z, box.Max.Z);
      return boundingBox;
    }

    public bool Intersects(BoundingBox box)
    {
      return this.Intersects(ref box);
    }

    public bool Intersects(ref BoundingBox box)
    {
      if ((double) this.Max.X >= (double) box.Min.X && (double) this.Min.X <= (double) box.Max.X && ((double) this.Max.Y >= (double) box.Min.Y && (double) this.Min.Y <= (double) box.Max.Y) && (double) this.Max.Z >= (double) box.Min.Z)
        return (double) this.Min.Z <= (double) box.Max.Z;
      else
        return false;
    }

    public void Intersects(ref BoundingBox box, out bool result)
    {
      result = false;
      if ((double) this.Max.X < (double) box.Min.X || (double) this.Min.X > (double) box.Max.X || ((double) this.Max.Y < (double) box.Min.Y || (double) this.Min.Y > (double) box.Max.Y) || ((double) this.Max.Z < (double) box.Min.Z || (double) this.Min.Z > (double) box.Max.Z))
        return;
      result = true;
    }

    public bool Intersects(BoundingFrustum frustum)
    {
      if ((BoundingFrustum) null == frustum)
        throw new ArgumentNullException("frustum");
      else
        return frustum.Intersects(this);
    }

    public PlaneIntersectionType Intersects(Plane plane)
    {
      Vector3 vector3_1;
      vector3_1.X = (double) plane.Normal.X >= 0.0 ? this.Min.X : this.Max.X;
      vector3_1.Y = (double) plane.Normal.Y >= 0.0 ? this.Min.Y : this.Max.Y;
      vector3_1.Z = (double) plane.Normal.Z >= 0.0 ? this.Min.Z : this.Max.Z;
      Vector3 vector3_2;
      vector3_2.X = (double) plane.Normal.X >= 0.0 ? this.Max.X : this.Min.X;
      vector3_2.Y = (double) plane.Normal.Y >= 0.0 ? this.Max.Y : this.Min.Y;
      vector3_2.Z = (double) plane.Normal.Z >= 0.0 ? this.Max.Z : this.Min.Z;
      if ((double) plane.Normal.X * (double) vector3_1.X + (double) plane.Normal.Y * (double) vector3_1.Y + (double) plane.Normal.Z * (double) vector3_1.Z + (double) plane.D > 0.0)
        return PlaneIntersectionType.Front;
      return (double) plane.Normal.X * (double) vector3_2.X + (double) plane.Normal.Y * (double) vector3_2.Y + (double) plane.Normal.Z * (double) vector3_2.Z + (double) plane.D >= 0.0 ? PlaneIntersectionType.Intersecting : PlaneIntersectionType.Back;
    }

    public void Intersects(ref Plane plane, out PlaneIntersectionType result)
    {
      Vector3 vector3_1;
      vector3_1.X = (double) plane.Normal.X >= 0.0 ? this.Min.X : this.Max.X;
      vector3_1.Y = (double) plane.Normal.Y >= 0.0 ? this.Min.Y : this.Max.Y;
      vector3_1.Z = (double) plane.Normal.Z >= 0.0 ? this.Min.Z : this.Max.Z;
      Vector3 vector3_2;
      vector3_2.X = (double) plane.Normal.X >= 0.0 ? this.Max.X : this.Min.X;
      vector3_2.Y = (double) plane.Normal.Y >= 0.0 ? this.Max.Y : this.Min.Y;
      vector3_2.Z = (double) plane.Normal.Z >= 0.0 ? this.Max.Z : this.Min.Z;
      if ((double) plane.Normal.X * (double) vector3_1.X + (double) plane.Normal.Y * (double) vector3_1.Y + (double) plane.Normal.Z * (double) vector3_1.Z + (double) plane.D > 0.0)
        result = PlaneIntersectionType.Front;
      else if ((double) plane.Normal.X * (double) vector3_2.X + (double) plane.Normal.Y * (double) vector3_2.Y + (double) plane.Normal.Z * (double) vector3_2.Z + (double) plane.D < 0.0)
        result = PlaneIntersectionType.Back;
      else
        result = PlaneIntersectionType.Intersecting;
    }

    public bool Intersects(Line line, out float distance)
    {
      distance = 0.0f;
      float? nullable = this.Intersects(new Ray(line.From, line.Direction));
      if (!nullable.HasValue || (double) nullable.Value < 0.0 || (double) nullable.Value > (double) line.Length)
        return false;
      distance = nullable.Value;
      return true;
    }

    public float? Intersects(Ray ray)
    {
      float num1 = 0.0f;
      float num2 = float.MaxValue;
      if ((double) Math.Abs(ray.Direction.X) < 9.99999997475243E-07)
      {
        if ((double) ray.Position.X < (double) this.Min.X || (double) ray.Position.X > (double) this.Max.X)
          return new float?();
      }
      else
      {
        float num3 = 1f / ray.Direction.X;
        float num4 = (this.Min.X - ray.Position.X) * num3;
        float num5 = (this.Max.X - ray.Position.X) * num3;
        if ((double) num4 > (double) num5)
        {
          float num6 = num4;
          num4 = num5;
          num5 = num6;
        }
        num1 = MathHelper.Max(num4, num1);
        num2 = MathHelper.Min(num5, num2);
        if ((double) num1 > (double) num2)
          return new float?();
      }
      if ((double) Math.Abs(ray.Direction.Y) < 9.99999997475243E-07)
      {
        if ((double) ray.Position.Y < (double) this.Min.Y || (double) ray.Position.Y > (double) this.Max.Y)
          return new float?();
      }
      else
      {
        float num3 = 1f / ray.Direction.Y;
        float num4 = (this.Min.Y - ray.Position.Y) * num3;
        float num5 = (this.Max.Y - ray.Position.Y) * num3;
        if ((double) num4 > (double) num5)
        {
          float num6 = num4;
          num4 = num5;
          num5 = num6;
        }
        num1 = MathHelper.Max(num4, num1);
        num2 = MathHelper.Min(num5, num2);
        if ((double) num1 > (double) num2)
          return new float?();
      }
      if ((double) Math.Abs(ray.Direction.Z) < 9.99999997475243E-07)
      {
        if ((double) ray.Position.Z < (double) this.Min.Z || (double) ray.Position.Z > (double) this.Max.Z)
          return new float?();
      }
      else
      {
        float num3 = 1f / ray.Direction.Z;
        float num4 = (this.Min.Z - ray.Position.Z) * num3;
        float num5 = (this.Max.Z - ray.Position.Z) * num3;
        if ((double) num4 > (double) num5)
        {
          float num6 = num4;
          num4 = num5;
          num5 = num6;
        }
        num1 = MathHelper.Max(num4, num1);
        float num7 = MathHelper.Min(num5, num2);
        if ((double) num1 > (double) num7)
          return new float?();
      }
      return new float?(num1);
    }

    public void Intersects(ref Ray ray, out float? result)
    {
      result = new float?();
      float num1 = 0.0f;
      float num2 = float.MaxValue;
      if ((double) Math.Abs(ray.Direction.X) < 9.99999997475243E-07)
      {
        if ((double) ray.Position.X < (double) this.Min.X || (double) ray.Position.X > (double) this.Max.X)
          return;
      }
      else
      {
        float num3 = 1f / ray.Direction.X;
        float num4 = (this.Min.X - ray.Position.X) * num3;
        float num5 = (this.Max.X - ray.Position.X) * num3;
        if ((double) num4 > (double) num5)
        {
          float num6 = num4;
          num4 = num5;
          num5 = num6;
        }
        num1 = MathHelper.Max(num4, num1);
        num2 = MathHelper.Min(num5, num2);
        if ((double) num1 > (double) num2)
          return;
      }
      if ((double) Math.Abs(ray.Direction.Y) < 9.99999997475243E-07)
      {
        if ((double) ray.Position.Y < (double) this.Min.Y || (double) ray.Position.Y > (double) this.Max.Y)
          return;
      }
      else
      {
        float num3 = 1f / ray.Direction.Y;
        float num4 = (this.Min.Y - ray.Position.Y) * num3;
        float num5 = (this.Max.Y - ray.Position.Y) * num3;
        if ((double) num4 > (double) num5)
        {
          float num6 = num4;
          num4 = num5;
          num5 = num6;
        }
        num1 = MathHelper.Max(num4, num1);
        num2 = MathHelper.Min(num5, num2);
        if ((double) num1 > (double) num2)
          return;
      }
      if ((double) Math.Abs(ray.Direction.Z) < 9.99999997475243E-07)
      {
        if ((double) ray.Position.Z < (double) this.Min.Z || (double) ray.Position.Z > (double) this.Max.Z)
          return;
      }
      else
      {
        float num3 = 1f / ray.Direction.Z;
        float num4 = (this.Min.Z - ray.Position.Z) * num3;
        float num5 = (this.Max.Z - ray.Position.Z) * num3;
        if ((double) num4 > (double) num5)
        {
          float num6 = num4;
          num4 = num5;
          num5 = num6;
        }
        num1 = MathHelper.Max(num4, num1);
        float num7 = MathHelper.Min(num5, num2);
        if ((double) num1 > (double) num7)
          return;
      }
      result = new float?(num1);
    }

    public bool Intersects(BoundingSphere sphere)
    {
      return this.Intersects(ref sphere);
    }

    public void Intersects(ref BoundingSphere sphere, out bool result)
    {
      Vector3 result1;
      Vector3.Clamp(ref sphere.Center, ref this.Min, ref this.Max, out result1);
      float result2;
      Vector3.DistanceSquared(ref sphere.Center, ref result1, out result2);
      result = (double) result2 <= (double) sphere.Radius * (double) sphere.Radius;
    }

    public bool Intersects(ref BoundingSphere sphere)
    {
      Vector3 result1;
      Vector3.Clamp(ref sphere.Center, ref this.Min, ref this.Max, out result1);
      float result2;
      Vector3.DistanceSquared(ref sphere.Center, ref result1, out result2);
      return (double) result2 <= (double) sphere.Radius * (double) sphere.Radius;
    }

    public bool Intersects(ref BoundingSphereD sphere)
    {
      Vector3 vector3 = (Vector3) sphere.Center;
      Vector3 result1;
      Vector3.Clamp(ref vector3, ref this.Min, ref this.Max, out result1);
      float result2;
      Vector3.DistanceSquared(ref vector3, ref result1, out result2);
      return (double) result2 <= sphere.Radius * sphere.Radius;
    }

    public float Distance(Vector3 point)
    {
      return Vector3.Distance(Vector3.Clamp(point, this.Min, this.Max), point);
    }

    public ContainmentType Contains(BoundingBox box)
    {
      if ((double) this.Max.X < (double) box.Min.X || (double) this.Min.X > (double) box.Max.X || ((double) this.Max.Y < (double) box.Min.Y || (double) this.Min.Y > (double) box.Max.Y) || ((double) this.Max.Z < (double) box.Min.Z || (double) this.Min.Z > (double) box.Max.Z))
        return ContainmentType.Disjoint;
      return (double) this.Min.X <= (double) box.Min.X && (double) box.Max.X <= (double) this.Max.X && ((double) this.Min.Y <= (double) box.Min.Y && (double) box.Max.Y <= (double) this.Max.Y) && ((double) this.Min.Z <= (double) box.Min.Z && (double) box.Max.Z <= (double) this.Max.Z) ? ContainmentType.Contains : ContainmentType.Intersects;
    }

    public void Contains(ref BoundingBox box, out ContainmentType result)
    {
      result = ContainmentType.Disjoint;
      if ((double) this.Max.X < (double) box.Min.X || (double) this.Min.X > (double) box.Max.X || ((double) this.Max.Y < (double) box.Min.Y || (double) this.Min.Y > (double) box.Max.Y) || ((double) this.Max.Z < (double) box.Min.Z || (double) this.Min.Z > (double) box.Max.Z))
        return;
      result = (double) this.Min.X > (double) box.Min.X || (double) box.Max.X > (double) this.Max.X || ((double) this.Min.Y > (double) box.Min.Y || (double) box.Max.Y > (double) this.Max.Y) || ((double) this.Min.Z > (double) box.Min.Z || (double) box.Max.Z > (double) this.Max.Z) ? ContainmentType.Intersects : ContainmentType.Contains;
    }

    public ContainmentType Contains(BoundingFrustum frustum)
    {
      if (!frustum.Intersects(this))
        return ContainmentType.Disjoint;
      foreach (Vector3 point in frustum.cornerArray)
      {
        if (this.Contains(point) == ContainmentType.Disjoint)
          return ContainmentType.Intersects;
      }
      return ContainmentType.Contains;
    }

    public ContainmentType Contains(Vector3 point)
    {
      return (double) this.Min.X <= (double) point.X && (double) point.X <= (double) this.Max.X && ((double) this.Min.Y <= (double) point.Y && (double) point.Y <= (double) this.Max.Y) && ((double) this.Min.Z <= (double) point.Z && (double) point.Z <= (double) this.Max.Z) ? ContainmentType.Contains : ContainmentType.Disjoint;
    }

    public ContainmentType Contains(Vector3D point)
    {
      return (double) this.Min.X <= point.X && point.X <= (double) this.Max.X && ((double) this.Min.Y <= point.Y && point.Y <= (double) this.Max.Y) && ((double) this.Min.Z <= point.Z && point.Z <= (double) this.Max.Z) ? ContainmentType.Contains : ContainmentType.Disjoint;
    }

    public void Contains(ref Vector3 point, out ContainmentType result)
    {
      result = (double) this.Min.X > (double) point.X || (double) point.X > (double) this.Max.X || ((double) this.Min.Y > (double) point.Y || (double) point.Y > (double) this.Max.Y) || ((double) this.Min.Z > (double) point.Z || (double) point.Z > (double) this.Max.Z) ? ContainmentType.Disjoint : ContainmentType.Contains;
    }

    public ContainmentType Contains(BoundingSphere sphere)
    {
      Vector3 result1;
      Vector3.Clamp(ref sphere.Center, ref this.Min, ref this.Max, out result1);
      float result2;
      Vector3.DistanceSquared(ref sphere.Center, ref result1, out result2);
      float num = sphere.Radius;
      if ((double) result2 > (double) num * (double) num)
        return ContainmentType.Disjoint;
      return (double) this.Min.X + (double) num <= (double) sphere.Center.X && (double) sphere.Center.X <= (double) this.Max.X - (double) num && ((double) this.Max.X - (double) this.Min.X > (double) num && (double) this.Min.Y + (double) num <= (double) sphere.Center.Y) && ((double) sphere.Center.Y <= (double) this.Max.Y - (double) num && (double) this.Max.Y - (double) this.Min.Y > (double) num && ((double) this.Min.Z + (double) num <= (double) sphere.Center.Z && (double) sphere.Center.Z <= (double) this.Max.Z - (double) num)) && (double) this.Max.X - (double) this.Min.X > (double) num ? ContainmentType.Contains : ContainmentType.Intersects;
    }

    public void Contains(ref BoundingSphere sphere, out ContainmentType result)
    {
      Vector3 result1;
      Vector3.Clamp(ref sphere.Center, ref this.Min, ref this.Max, out result1);
      float result2;
      Vector3.DistanceSquared(ref sphere.Center, ref result1, out result2);
      float num = sphere.Radius;
      if ((double) result2 > (double) num * (double) num)
        result = ContainmentType.Disjoint;
      else
        result = (double) this.Min.X + (double) num > (double) sphere.Center.X || (double) sphere.Center.X > (double) this.Max.X - (double) num || ((double) this.Max.X - (double) this.Min.X <= (double) num || (double) this.Min.Y + (double) num > (double) sphere.Center.Y) || ((double) sphere.Center.Y > (double) this.Max.Y - (double) num || (double) this.Max.Y - (double) this.Min.Y <= (double) num || ((double) this.Min.Z + (double) num > (double) sphere.Center.Z || (double) sphere.Center.Z > (double) this.Max.Z - (double) num)) || (double) this.Max.X - (double) this.Min.X <= (double) num ? ContainmentType.Intersects : ContainmentType.Contains;
    }

    internal void SupportMapping(ref Vector3 v, out Vector3 result)
    {
      result.X = (double) v.X >= 0.0 ? this.Max.X : this.Min.X;
      result.Y = (double) v.Y >= 0.0 ? this.Max.Y : this.Min.Y;
      result.Z = (double) v.Z >= 0.0 ? this.Max.Z : this.Min.Z;
    }

    public BoundingBox Translate(Matrix worldMatrix)
    {
      this.Min += worldMatrix.Translation;
      this.Max += worldMatrix.Translation;
      return this;
    }

    public BoundingBox Translate(Vector3 vctTranlsation)
    {
      this.Min += vctTranlsation;
      this.Max += vctTranlsation;
      return this;
    }

    public Vector3 Size()
    {
      return this.Max - this.Min;
    }

    public BoundingBox Transform(Matrix worldMatrix)
    {
      return this.Transform(ref worldMatrix);
    }

    public BoundingBoxD Transform(MatrixD worldMatrix)
    {
      return this.Transform(ref worldMatrix);
    }

    public unsafe BoundingBox Transform(ref Matrix worldMatrix)
    {
      BoundingBox boundingBox = BoundingBox.CreateInvalid();
      Vector3* corners = stackalloc Vector3[8];
      this.GetCornersUnsafe(corners);
      for (int index = 0; index < 8; ++index)
      {
        Vector3 point = Vector3.Transform(corners[index], worldMatrix);
        boundingBox = boundingBox.Include(ref point);
      }
      return boundingBox;
    }

    public unsafe BoundingBoxD Transform(ref MatrixD worldMatrix)
    {
      BoundingBoxD boundingBoxD = BoundingBoxD.CreateInvalid();
      Vector3* corners = stackalloc Vector3[8];
      this.GetCornersUnsafe(corners);
      for (int index = 0; index < 8; ++index)
      {
        Vector3D point = Vector3.Transform(corners[index], worldMatrix);
        boundingBoxD = boundingBoxD.Include(ref point);
      }
      return boundingBoxD;
    }

    public BoundingBox Include(ref Vector3 point)
    {
      if ((double) point.X < (double) this.Min.X)
        this.Min.X = point.X;
      if ((double) point.Y < (double) this.Min.Y)
        this.Min.Y = point.Y;
      if ((double) point.Z < (double) this.Min.Z)
        this.Min.Z = point.Z;
      if ((double) point.X > (double) this.Max.X)
        this.Max.X = point.X;
      if ((double) point.Y > (double) this.Max.Y)
        this.Max.Y = point.Y;
      if ((double) point.Z > (double) this.Max.Z)
        this.Max.Z = point.Z;
      return this;
    }

    public BoundingBox GetIncluded(Vector3 point)
    {
      BoundingBox boundingBox = this;
      boundingBox.Include(point);
      return boundingBox;
    }

    public BoundingBox Include(Vector3 point)
    {
      return this.Include(ref point);
    }

    public BoundingBox Include(Vector3 p0, Vector3 p1, Vector3 p2)
    {
      return this.Include(ref p0, ref p1, ref p2);
    }

    public BoundingBox Include(ref Vector3 p0, ref Vector3 p1, ref Vector3 p2)
    {
      this.Include(ref p0);
      this.Include(ref p1);
      this.Include(ref p2);
      return this;
    }

    public BoundingBox Include(ref BoundingBox box)
    {
      this.Min = Vector3.Min(this.Min, box.Min);
      this.Max = Vector3.Max(this.Max, box.Max);
      return this;
    }

    public BoundingBox Include(BoundingBox box)
    {
      return this.Include(ref box);
    }

    public void Include(ref Line line)
    {
      this.Include(ref line.From);
      this.Include(ref line.To);
    }

    public BoundingBox Include(BoundingSphere sphere)
    {
      return this.Include(ref sphere);
    }

    public BoundingBox Include(ref BoundingSphere sphere)
    {
      Vector3 vector3 = new Vector3(sphere.Radius);
      Vector3 result1 = sphere.Center;
      Vector3 result2 = sphere.Center;
      Vector3.Subtract(ref result1, ref vector3, out result1);
      Vector3.Add(ref result2, ref vector3, out result2);
      this.Include(ref result1);
      this.Include(ref result2);
      return this;
    }

    public BoundingBox Include(ref BoundingFrustum frustum)
    {
      if (BoundingBox.m_frustumPoints == null)
        BoundingBox.m_frustumPoints = new Vector3[8];
      frustum.GetCorners(BoundingBox.m_frustumPoints);
      this.Include(ref BoundingBox.m_frustumPoints[0]);
      this.Include(ref BoundingBox.m_frustumPoints[1]);
      this.Include(ref BoundingBox.m_frustumPoints[2]);
      this.Include(ref BoundingBox.m_frustumPoints[3]);
      this.Include(ref BoundingBox.m_frustumPoints[4]);
      this.Include(ref BoundingBox.m_frustumPoints[5]);
      this.Include(ref BoundingBox.m_frustumPoints[6]);
      this.Include(ref BoundingBox.m_frustumPoints[7]);
      return this;
    }

    public static BoundingBox CreateInvalid()
    {
      BoundingBox boundingBox = new BoundingBox();
      Vector3 vector3_1 = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
      Vector3 vector3_2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
      boundingBox.Min = vector3_1;
      boundingBox.Max = vector3_2;
      return boundingBox;
    }

    public float SurfaceArea()
    {
      Vector3 vector3 = this.Max - this.Min;
      return (float) (2.0 * ((double) vector3.X * (double) vector3.Y + (double) vector3.X * (double) vector3.Z + (double) vector3.Y * (double) vector3.Z));
    }

    public float Volume()
    {
      Vector3 vector3 = this.Max - this.Min;
      return vector3.X * vector3.Y * vector3.Z;
    }

    public float ProjectedArea(Vector3 viewDir)
    {
      Vector3 vector3 = this.Max - this.Min;
      Vector3 v = new Vector3(vector3.Y, vector3.Z, vector3.X) * new Vector3(vector3.Z, vector3.X, vector3.Y);
      return Vector3.Abs(viewDir).Dot(v);
    }

    public float Perimeter()
    {
      return (float) (4.0 * ((double) (this.Max.X - this.Min.X) + (double) (this.Max.Y - this.Min.Y) + (double) (this.Max.Z - this.Min.Z)));
    }

    public void Inflate(float size)
    {
      this.Max += new Vector3(size);
      this.Min -= new Vector3(size);
    }

    public class ComparerType : IEqualityComparer<BoundingBoxD>
    {
      public bool Equals(BoundingBoxD x, BoundingBoxD y)
      {
        if (x.Min == y.Min)
          return x.Max == y.Max;
        else
          return false;
      }

      public int GetHashCode(BoundingBoxD obj)
      {
        return obj.Min.GetHashCode() ^ obj.Max.GetHashCode();
      }
    }
  }
}
