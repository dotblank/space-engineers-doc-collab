// Decompiled with JetBrains decompiler
// Type: VRageMath.BoundingBoxD
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace VRageMath
{
    [ProtoContract]
    [Serializable]
    public struct BoundingBoxD : IEquatable<BoundingBoxD>
    {
        private static Vector3D[] m_frustumPoints = (Vector3D[]) null;
        public static readonly BoundingBoxD.ComparerType Comparer = new BoundingBoxD.ComparerType();
        public const int CornerCount = 8;
        [ProtoMember(1)] public Vector3D Min;
        [ProtoMember(2)] public Vector3D Max;

        public Vector3D Center
        {
            get { return (this.Min + this.Max)/2.0; }
        }

        public Vector3D HalfExtents
        {
            get { return (this.Max - this.Min)/2.0; }
        }

        public MatrixD Matrix
        {
            get
            {
                Vector3D center = this.Center;
                Vector3D scale = this.Size();
                MatrixD result;
                MatrixD.CreateTranslation(ref center, out result);
                MatrixD.Rescale(ref result, ref scale);
                return result;
            }
        }

        public BoundingBoxD(Vector3D min, Vector3D max)
        {
            this.Min = min;
            this.Max = max;
        }

        public static explicit operator BoundingBoxD(BoundingBox b)
        {
            return new BoundingBoxD((Vector3D) b.Min, (Vector3D) b.Max);
        }

        public static explicit operator BoundingBox(BoundingBoxD b)
        {
            return new BoundingBox((Vector3) b.Min, (Vector3) b.Max);
        }

        public static bool operator ==(BoundingBoxD a, BoundingBoxD b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(BoundingBoxD a, BoundingBoxD b)
        {
            if (!(a.Min != b.Min))
                return a.Max != b.Max;
            else
                return true;
        }

        public Vector3D[] GetCorners()
        {
            return new Vector3D[8]
            {
                new Vector3D(this.Min.X, this.Max.Y, this.Max.Z),
                new Vector3D(this.Max.X, this.Max.Y, this.Max.Z),
                new Vector3D(this.Max.X, this.Min.Y, this.Max.Z),
                new Vector3D(this.Min.X, this.Min.Y, this.Max.Z),
                new Vector3D(this.Min.X, this.Max.Y, this.Min.Z),
                new Vector3D(this.Max.X, this.Max.Y, this.Min.Z),
                new Vector3D(this.Max.X, this.Min.Y, this.Min.Z),
                new Vector3D(this.Min.X, this.Min.Y, this.Min.Z)
            };
        }

        public void GetCorners(Vector3D[] corners)
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

        public unsafe void GetCornersUnsafe(Vector3D* corners)
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

        public bool Equals(BoundingBoxD other)
        {
            if (this.Min == other.Min)
                return this.Max == other.Max;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is BoundingBoxD)
                flag = this.Equals((BoundingBoxD) obj);
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

        public static BoundingBoxD CreateMerged(BoundingBoxD original, BoundingBoxD additional)
        {
            BoundingBoxD boundingBoxD;
            Vector3D.Min(ref original.Min, ref additional.Min, out boundingBoxD.Min);
            Vector3D.Max(ref original.Max, ref additional.Max, out boundingBoxD.Max);
            return boundingBoxD;
        }

        public static void CreateMerged(ref BoundingBoxD original, ref BoundingBoxD additional, out BoundingBoxD result)
        {
            Vector3D result1;
            Vector3D.Min(ref original.Min, ref additional.Min, out result1);
            Vector3D result2;
            Vector3D.Max(ref original.Max, ref additional.Max, out result2);
            result.Min = result1;
            result.Max = result2;
        }

        public static BoundingBoxD CreateFromSphere(BoundingSphereD sphere)
        {
            BoundingBoxD boundingBoxD;
            boundingBoxD.Min.X = sphere.Center.X - sphere.Radius;
            boundingBoxD.Min.Y = sphere.Center.Y - sphere.Radius;
            boundingBoxD.Min.Z = sphere.Center.Z - sphere.Radius;
            boundingBoxD.Max.X = sphere.Center.X + sphere.Radius;
            boundingBoxD.Max.Y = sphere.Center.Y + sphere.Radius;
            boundingBoxD.Max.Z = sphere.Center.Z + sphere.Radius;
            return boundingBoxD;
        }

        public static void CreateFromSphere(ref BoundingSphereD sphere, out BoundingBoxD result)
        {
            result.Min.X = sphere.Center.X - sphere.Radius;
            result.Min.Y = sphere.Center.Y - sphere.Radius;
            result.Min.Z = sphere.Center.Z - sphere.Radius;
            result.Max.X = sphere.Center.X + sphere.Radius;
            result.Max.Y = sphere.Center.Y + sphere.Radius;
            result.Max.Z = sphere.Center.Z + sphere.Radius;
        }

        public static BoundingBoxD CreateFromPoints(IEnumerable<Vector3D> points)
        {
            return default(BoundingBoxD);
        }

        public BoundingBoxD Intersect(BoundingBoxD box)
        {
            BoundingBoxD boundingBoxD;
            boundingBoxD.Min.X = Math.Max(this.Min.X, box.Min.X);
            boundingBoxD.Min.Y = Math.Max(this.Min.Y, box.Min.Y);
            boundingBoxD.Min.Z = Math.Max(this.Min.Z, box.Min.Z);
            boundingBoxD.Max.X = Math.Min(this.Max.X, box.Max.X);
            boundingBoxD.Max.Y = Math.Min(this.Max.Y, box.Max.Y);
            boundingBoxD.Max.Z = Math.Min(this.Max.Z, box.Max.Z);
            return boundingBoxD;
        }

        public bool Intersects(BoundingBoxD box)
        {
            return this.Intersects(ref box);
        }

        public bool Intersects(ref BoundingBoxD box)
        {
            if (this.Max.X >= box.Min.X && this.Min.X <= box.Max.X &&
                (this.Max.Y >= box.Min.Y && this.Min.Y <= box.Max.Y) && this.Max.Z >= box.Min.Z)
                return this.Min.Z <= box.Max.Z;
            else
                return false;
        }

        public void Intersects(ref BoundingBoxD box, out bool result)
        {
            result = false;
            if (this.Max.X < box.Min.X || this.Min.X > box.Max.X || (this.Max.Y < box.Min.Y || this.Min.Y > box.Max.Y) ||
                (this.Max.Z < box.Min.Z || this.Min.Z > box.Max.Z))
                return;
            result = true;
        }

        public void Intersects(ref BoundingBox box, out bool result)
        {
            result = false;
            if (this.Max.X < (double) box.Min.X || this.Min.X > (double) box.Max.X ||
                (this.Max.Y < (double) box.Min.Y || this.Min.Y > (double) box.Max.Y) ||
                (this.Max.Z < (double) box.Min.Z || this.Min.Z > (double) box.Max.Z))
                return;
            result = true;
        }

        public bool IntersectsTriangle(Vector3D v0, Vector3D v1, Vector3D v2)
        {
            return this.IntersectsTriangle(ref v0, ref v1, ref v2);
        }

        public bool IntersectsTriangle(ref Vector3D v0, ref Vector3D v1, ref Vector3D v2)
        {
            Vector3D result1;
            Vector3D.Min(ref v0, ref v1, out result1);
            Vector3D.Min(ref result1, ref v2, out result1);
            Vector3D result2;
            Vector3D.Max(ref v0, ref v1, out result2);
            Vector3D.Max(ref result2, ref v2, out result2);
            if (result1.X > this.Max.X || result2.X < this.Min.X || (result1.Y > this.Max.Y || result2.Y < this.Min.Y) ||
                (result1.Z > this.Max.Z || result2.Z < this.Min.Z))
                return false;
            Vector3D vector1 = v1 - v0;
            Vector3D vector2 = v2 - v1;
            Vector3D result3;
            Vector3D.Cross(ref vector1, ref vector2, out result3);
            double result4;
            Vector3D.Dot(ref v0, ref result3, out result4);
            PlaneD plane = new PlaneD(result3, result4);
            PlaneIntersectionType result5;
            this.Intersects(ref plane, out result5);
            if (result5 == PlaneIntersectionType.Back || result5 == PlaneIntersectionType.Front)
                return false;
            Vector3D center = this.Center;
            Vector3D halfExtents = new BoundingBoxD(this.Min - center, this.Max - center).HalfExtents;
            Vector3D vector3D = v0 - v2;
            double num1 = halfExtents.Y*Math.Abs(vector1.Z) + halfExtents.Z*Math.Abs(vector1.Y);
            double val1_1 = v0.Z*v1.Y - v0.Y*v1.Z;
            double val2_1 = v2.Z*vector1.Y - v2.Y*vector1.Z;
            if (Math.Min(val1_1, val2_1) > num1 || Math.Max(val1_1, val2_1) < -num1)
                return false;
            double num2 = halfExtents.X*Math.Abs(vector1.Z) + halfExtents.Z*Math.Abs(vector1.X);
            double val1_2 = v0.X*v1.Z - v0.Z*v1.X;
            double val2_2 = v2.X*vector1.Z - v2.Z*vector1.X;
            if (Math.Min(val1_2, val2_2) > num2 || Math.Max(val1_2, val2_2) < -num2)
                return false;
            double num3 = halfExtents.X*Math.Abs(vector1.Y) + halfExtents.Y*Math.Abs(vector1.X);
            double val1_3 = v0.Y*v1.X - v0.X*v1.Y;
            double val2_3 = v2.Y*vector1.X - v2.X*vector1.Y;
            if (Math.Min(val1_3, val2_3) > num3 || Math.Max(val1_3, val2_3) < -num3)
                return false;
            double num4 = halfExtents.Y*Math.Abs(vector2.Z) + halfExtents.Z*Math.Abs(vector2.Y);
            double val1_4 = v1.Z*v2.Y - v1.Y*v2.Z;
            double val2_4 = v0.Z*vector2.Y - v0.Y*vector2.Z;
            if (Math.Min(val1_4, val2_4) > num4 || Math.Max(val1_4, val2_4) < -num4)
                return false;
            double num5 = halfExtents.X*Math.Abs(vector2.Z) + halfExtents.Z*Math.Abs(vector2.X);
            double val1_5 = v1.X*v2.Z - v1.Z*v2.X;
            double val2_5 = v0.X*vector2.Z - v0.Z*vector2.X;
            if (Math.Min(val1_5, val2_5) > num5 || Math.Max(val1_5, val2_5) < -num5)
                return false;
            double num6 = halfExtents.X*Math.Abs(vector2.Y) + halfExtents.Y*Math.Abs(vector2.X);
            double val1_6 = v1.Y*v2.X - v1.X*v2.Y;
            double val2_6 = v0.Y*vector2.X - v0.X*vector2.Y;
            if (Math.Min(val1_6, val2_6) > num6 || Math.Max(val1_6, val2_6) < -num6)
                return false;
            double num7 = halfExtents.Y*Math.Abs(vector3D.Z) + halfExtents.Z*Math.Abs(vector3D.Y);
            double val1_7 = v2.Z*v0.Y - v2.Y*v0.Z;
            double val2_7 = v1.Z*vector3D.Y - v1.Y*vector3D.Z;
            if (Math.Min(val1_7, val2_7) > num7 || Math.Max(val1_7, val2_7) < -num7)
                return false;
            double num8 = halfExtents.X*Math.Abs(vector3D.Z) + halfExtents.Z*Math.Abs(vector3D.X);
            double val1_8 = v2.X*v0.Z - v2.Z*v0.X;
            double val2_8 = v1.X*vector3D.Z - v1.Z*vector3D.X;
            if (Math.Min(val1_8, val2_8) > num8 || Math.Max(val1_8, val2_8) < -num8)
                return false;
            double num9 = halfExtents.X*Math.Abs(vector3D.Y) + halfExtents.Y*Math.Abs(vector3D.X);
            double val1_9 = v2.Y*v0.X - v2.X*v0.Y;
            double val2_9 = v1.Y*vector3D.X - v1.X*vector3D.Y;
            return Math.Min(val1_9, val2_9) <= num9 && Math.Max(val1_9, val2_9) >= -num9;
        }

        public bool Intersects(BoundingFrustumD frustum)
        {
            if ((BoundingFrustumD) null == frustum)
                throw new ArgumentNullException("frustum");
            else
                return frustum.Intersects(this);
        }

        public PlaneIntersectionType Intersects(PlaneD plane)
        {
            Vector3D vector3D1;
            vector3D1.X = plane.Normal.X >= 0.0 ? this.Min.X : this.Max.X;
            vector3D1.Y = plane.Normal.Y >= 0.0 ? this.Min.Y : this.Max.Y;
            vector3D1.Z = plane.Normal.Z >= 0.0 ? this.Min.Z : this.Max.Z;
            Vector3D vector3D2;
            vector3D2.X = plane.Normal.X >= 0.0 ? this.Max.X : this.Min.X;
            vector3D2.Y = plane.Normal.Y >= 0.0 ? this.Max.Y : this.Min.Y;
            vector3D2.Z = plane.Normal.Z >= 0.0 ? this.Max.Z : this.Min.Z;
            if (plane.Normal.X*vector3D1.X + plane.Normal.Y*vector3D1.Y + plane.Normal.Z*vector3D1.Z + plane.D > 0.0)
                return PlaneIntersectionType.Front;
            return plane.Normal.X*vector3D2.X + plane.Normal.Y*vector3D2.Y + plane.Normal.Z*vector3D2.Z + plane.D >= 0.0
                ? PlaneIntersectionType.Intersecting
                : PlaneIntersectionType.Back;
        }

        public void Intersects(ref PlaneD plane, out PlaneIntersectionType result)
        {
            Vector3D vector3D1;
            vector3D1.X = plane.Normal.X >= 0.0 ? this.Min.X : this.Max.X;
            vector3D1.Y = plane.Normal.Y >= 0.0 ? this.Min.Y : this.Max.Y;
            vector3D1.Z = plane.Normal.Z >= 0.0 ? this.Min.Z : this.Max.Z;
            Vector3D vector3D2;
            vector3D2.X = plane.Normal.X >= 0.0 ? this.Max.X : this.Min.X;
            vector3D2.Y = plane.Normal.Y >= 0.0 ? this.Max.Y : this.Min.Y;
            vector3D2.Z = plane.Normal.Z >= 0.0 ? this.Max.Z : this.Min.Z;
            if (plane.Normal.X*vector3D1.X + plane.Normal.Y*vector3D1.Y + plane.Normal.Z*vector3D1.Z + plane.D > 0.0)
                result = PlaneIntersectionType.Front;
            else if (plane.Normal.X*vector3D2.X + plane.Normal.Y*vector3D2.Y + plane.Normal.Z*vector3D2.Z + plane.D < 0.0)
                result = PlaneIntersectionType.Back;
            else
                result = PlaneIntersectionType.Intersecting;
        }

        public bool Intersects(LineD line, out double distance)
        {
            distance = 0.0;
            double? nullable = this.Intersects(new RayD(line.From, line.Direction));
            if (!nullable.HasValue || nullable.Value < 0.0 || nullable.Value > line.Length)
                return false;
            distance = nullable.Value;
            return true;
        }

        public double? Intersects(Ray ray)
        {
            return this.Intersects(new RayD((Vector3D) ray.Position, (Vector3D) ray.Direction));
        }

        public double? Intersects(RayD ray)
        {
            double num1 = 0.0;
            double num2 = double.MaxValue;
            if (Math.Abs(ray.Direction.X) < 9.99999997475243E-07)
            {
                if (ray.Position.X < this.Min.X || ray.Position.X > this.Max.X)
                    return new double?();
            }
            else
            {
                double num3 = 1.0/ray.Direction.X;
                double num4 = (this.Min.X - ray.Position.X)*num3;
                double num5 = (this.Max.X - ray.Position.X)*num3;
                if (num4 > num5)
                {
                    double num6 = num4;
                    num4 = num5;
                    num5 = num6;
                }
                num1 = MathHelper.Max(num4, num1);
                num2 = MathHelper.Min(num5, num2);
                if (num1 > num2)
                    return new double?();
            }
            if (Math.Abs(ray.Direction.Y) < 9.99999997475243E-07)
            {
                if (ray.Position.Y < this.Min.Y || ray.Position.Y > this.Max.Y)
                    return new double?();
            }
            else
            {
                double num3 = 1.0/ray.Direction.Y;
                double num4 = (this.Min.Y - ray.Position.Y)*num3;
                double num5 = (this.Max.Y - ray.Position.Y)*num3;
                if (num4 > num5)
                {
                    double num6 = num4;
                    num4 = num5;
                    num5 = num6;
                }
                num1 = MathHelper.Max(num4, num1);
                num2 = MathHelper.Min(num5, num2);
                if (num1 > num2)
                    return new double?();
            }
            if (Math.Abs(ray.Direction.Z) < 9.99999997475243E-07)
            {
                if (ray.Position.Z < this.Min.Z || ray.Position.Z > this.Max.Z)
                    return new double?();
            }
            else
            {
                double num3 = 1.0/ray.Direction.Z;
                double num4 = (this.Min.Z - ray.Position.Z)*num3;
                double num5 = (this.Max.Z - ray.Position.Z)*num3;
                if (num4 > num5)
                {
                    double num6 = num4;
                    num4 = num5;
                    num5 = num6;
                }
                num1 = MathHelper.Max(num4, num1);
                double num7 = MathHelper.Min(num5, num2);
                if (num1 > num7)
                    return new double?();
            }
            return new double?(num1);
        }

        public void Intersects(ref RayD ray, out double? result)
        {
            result = new double?();
            double num1 = 0.0;
            double num2 = double.MaxValue;
            if (Math.Abs(ray.Direction.X) < 9.99999997475243E-07)
            {
                if (ray.Position.X < this.Min.X || ray.Position.X > this.Max.X)
                    return;
            }
            else
            {
                double num3 = 1.0/ray.Direction.X;
                double num4 = (this.Min.X - ray.Position.X)*num3;
                double num5 = (this.Max.X - ray.Position.X)*num3;
                if (num4 > num5)
                {
                    double num6 = num4;
                    num4 = num5;
                    num5 = num6;
                }
                num1 = MathHelper.Max(num4, num1);
                num2 = MathHelper.Min(num5, num2);
                if (num1 > num2)
                    return;
            }
            if (Math.Abs(ray.Direction.Y) < 9.99999997475243E-07)
            {
                if (ray.Position.Y < this.Min.Y || ray.Position.Y > this.Max.Y)
                    return;
            }
            else
            {
                double num3 = 1.0/ray.Direction.Y;
                double num4 = (this.Min.Y - ray.Position.Y)*num3;
                double num5 = (this.Max.Y - ray.Position.Y)*num3;
                if (num4 > num5)
                {
                    double num6 = num4;
                    num4 = num5;
                    num5 = num6;
                }
                num1 = MathHelper.Max(num4, num1);
                num2 = MathHelper.Min(num5, num2);
                if (num1 > num2)
                    return;
            }
            if (Math.Abs(ray.Direction.Z) < 9.99999997475243E-07)
            {
                if (ray.Position.Z < this.Min.Z || ray.Position.Z > this.Max.Z)
                    return;
            }
            else
            {
                double num3 = 1.0/ray.Direction.Z;
                double num4 = (this.Min.Z - ray.Position.Z)*num3;
                double num5 = (this.Max.Z - ray.Position.Z)*num3;
                if (num4 > num5)
                {
                    double num6 = num4;
                    num4 = num5;
                    num5 = num6;
                }
                num1 = MathHelper.Max(num4, num1);
                double num7 = MathHelper.Min(num5, num2);
                if (num1 > num7)
                    return;
            }
            result = new double?(num1);
        }

        public bool Intersects(BoundingSphereD sphere)
        {
            return this.Intersects(ref sphere);
        }

        public void Intersects(ref BoundingSphereD sphere, out bool result)
        {
            Vector3D result1;
            Vector3D.Clamp(ref sphere.Center, ref this.Min, ref this.Max, out result1);
            double result2;
            Vector3D.DistanceSquared(ref sphere.Center, ref result1, out result2);
            result = result2 <= sphere.Radius*sphere.Radius;
        }

        public bool Intersects(ref BoundingSphereD sphere)
        {
            Vector3D result1;
            Vector3D.Clamp(ref sphere.Center, ref this.Min, ref this.Max, out result1);
            double result2;
            Vector3D.DistanceSquared(ref sphere.Center, ref result1, out result2);
            return result2 <= sphere.Radius*sphere.Radius;
        }

        public double Distance(Vector3D point)
        {
            return Vector3D.Distance(Vector3D.Clamp(point, this.Min, this.Max), point);
        }

        public ContainmentType Contains(BoundingBoxD box)
        {
            if (this.Max.X < box.Min.X || this.Min.X > box.Max.X || (this.Max.Y < box.Min.Y || this.Min.Y > box.Max.Y) ||
                (this.Max.Z < box.Min.Z || this.Min.Z > box.Max.Z))
                return ContainmentType.Disjoint;
            return this.Min.X <= box.Min.X && box.Max.X <= this.Max.X &&
                   (this.Min.Y <= box.Min.Y && box.Max.Y <= this.Max.Y) &&
                   (this.Min.Z <= box.Min.Z && box.Max.Z <= this.Max.Z)
                ? ContainmentType.Contains
                : ContainmentType.Intersects;
        }

        public void Contains(ref BoundingBoxD box, out ContainmentType result)
        {
            result = ContainmentType.Disjoint;
            if (this.Max.X < box.Min.X || this.Min.X > box.Max.X || (this.Max.Y < box.Min.Y || this.Min.Y > box.Max.Y) ||
                (this.Max.Z < box.Min.Z || this.Min.Z > box.Max.Z))
                return;
            result = this.Min.X > box.Min.X || box.Max.X > this.Max.X ||
                     (this.Min.Y > box.Min.Y || box.Max.Y > this.Max.Y) ||
                     (this.Min.Z > box.Min.Z || box.Max.Z > this.Max.Z)
                ? ContainmentType.Intersects
                : ContainmentType.Contains;
        }

        public ContainmentType Contains(BoundingFrustumD frustum)
        {
            if (!frustum.Intersects(this))
                return ContainmentType.Disjoint;
            foreach (Vector3D point in frustum.cornerArray)
            {
                if (this.Contains(point) == ContainmentType.Disjoint)
                    return ContainmentType.Intersects;
            }
            return ContainmentType.Contains;
        }

        public ContainmentType Contains(Vector3D point)
        {
            return this.Min.X <= point.X && point.X <= this.Max.X && (this.Min.Y <= point.Y && point.Y <= this.Max.Y) &&
                   (this.Min.Z <= point.Z && point.Z <= this.Max.Z)
                ? ContainmentType.Contains
                : ContainmentType.Disjoint;
        }

        public void Contains(ref Vector3D point, out ContainmentType result)
        {
            result = this.Min.X > point.X || point.X > this.Max.X || (this.Min.Y > point.Y || point.Y > this.Max.Y) ||
                     (this.Min.Z > point.Z || point.Z > this.Max.Z)
                ? ContainmentType.Disjoint
                : ContainmentType.Contains;
        }

        public ContainmentType Contains(BoundingSphereD sphere)
        {
            Vector3D result1;
            Vector3D.Clamp(ref sphere.Center, ref this.Min, ref this.Max, out result1);
            double result2;
            Vector3D.DistanceSquared(ref sphere.Center, ref result1, out result2);
            double num = sphere.Radius;
            if (result2 > num*num)
                return ContainmentType.Disjoint;
            return this.Min.X + num <= sphere.Center.X && sphere.Center.X <= this.Max.X - num &&
                   (this.Max.X - this.Min.X > num && this.Min.Y + num <= sphere.Center.Y) &&
                   (sphere.Center.Y <= this.Max.Y - num && this.Max.Y - this.Min.Y > num &&
                    (this.Min.Z + num <= sphere.Center.Z && sphere.Center.Z <= this.Max.Z - num)) &&
                   this.Max.X - this.Min.X > num
                ? ContainmentType.Contains
                : ContainmentType.Intersects;
        }

        public void Contains(ref BoundingSphereD sphere, out ContainmentType result)
        {
            Vector3D result1;
            Vector3D.Clamp(ref sphere.Center, ref this.Min, ref this.Max, out result1);
            double result2;
            Vector3D.DistanceSquared(ref sphere.Center, ref result1, out result2);
            double num = sphere.Radius;
            if (result2 > num*num)
                result = ContainmentType.Disjoint;
            else
                result = this.Min.X + num > sphere.Center.X || sphere.Center.X > this.Max.X - num ||
                         (this.Max.X - this.Min.X <= num || this.Min.Y + num > sphere.Center.Y) ||
                         (sphere.Center.Y > this.Max.Y - num || this.Max.Y - this.Min.Y <= num ||
                          (this.Min.Z + num > sphere.Center.Z || sphere.Center.Z > this.Max.Z - num)) ||
                         this.Max.X - this.Min.X <= num
                    ? ContainmentType.Intersects
                    : ContainmentType.Contains;
        }

        internal void SupportMapping(ref Vector3D v, out Vector3D result)
        {
            result.X = v.X >= 0.0 ? this.Max.X : this.Min.X;
            result.Y = v.Y >= 0.0 ? this.Max.Y : this.Min.Y;
            result.Z = v.Z >= 0.0 ? this.Max.Z : this.Min.Z;
        }

        public BoundingBoxD Translate(MatrixD worldMatrix)
        {
            this.Min += worldMatrix.Translation;
            this.Max += worldMatrix.Translation;
            return this;
        }

        public BoundingBoxD Translate(Vector3D vctTranlsation)
        {
            this.Min += vctTranlsation;
            this.Max += vctTranlsation;
            return this;
        }

        public Vector3D Size()
        {
            return this.Max - this.Min;
        }

        public BoundingBoxD Transform(MatrixD worldMatrix)
        {
            return this.Transform(ref worldMatrix);
        }

        public unsafe BoundingBoxD Transform(ref MatrixD worldMatrix)
        {
            BoundingBoxD boundingBoxD = BoundingBoxD.CreateInvalid();
            Vector3D* corners = stackalloc Vector3D[8];
            this.GetCornersUnsafe(corners);
            for (int index = 0; index < 8; ++index)
            {
                Vector3D point = Vector3D.Transform(corners[index], worldMatrix);
                boundingBoxD = boundingBoxD.Include(ref point);
            }
            return boundingBoxD;
        }

        public BoundingBoxD Include(ref Vector3D point)
        {
            if (point.X < this.Min.X)
                this.Min.X = point.X;
            if (point.Y < this.Min.Y)
                this.Min.Y = point.Y;
            if (point.Z < this.Min.Z)
                this.Min.Z = point.Z;
            if (point.X > this.Max.X)
                this.Max.X = point.X;
            if (point.Y > this.Max.Y)
                this.Max.Y = point.Y;
            if (point.Z > this.Max.Z)
                this.Max.Z = point.Z;
            return this;
        }

        public BoundingBoxD Include(Vector3D point)
        {
            return this.Include(ref point);
        }

        public BoundingBoxD Include(Vector3D p0, Vector3D p1, Vector3D p2)
        {
            return this.Include(ref p0, ref p1, ref p2);
        }

        public BoundingBoxD Include(ref Vector3D p0, ref Vector3D p1, ref Vector3D p2)
        {
            this.Include(ref p0);
            this.Include(ref p1);
            this.Include(ref p2);
            return this;
        }

        public BoundingBoxD Include(ref BoundingBoxD box)
        {
            this.Min = Vector3D.Min(this.Min, box.Min);
            this.Max = Vector3D.Max(this.Max, box.Max);
            return this;
        }

        public BoundingBoxD Include(BoundingBoxD box)
        {
            return this.Include(ref box);
        }

        public void Include(ref LineD line)
        {
            this.Include(ref line.From);
            this.Include(ref line.To);
        }

        public BoundingBoxD Include(BoundingSphereD sphere)
        {
            return this.Include(ref sphere);
        }

        public BoundingBoxD Include(ref BoundingSphereD sphere)
        {
            Vector3D vector3D = new Vector3D(sphere.Radius);
            Vector3D result1 = sphere.Center;
            Vector3D result2 = sphere.Center;
            Vector3D.Subtract(ref result1, ref vector3D, out result1);
            Vector3D.Add(ref result2, ref vector3D, out result2);
            this.Include(ref result1);
            this.Include(ref result2);
            return this;
        }

        public BoundingBoxD Include(ref BoundingFrustumD frustum)
        {
            if (BoundingBoxD.m_frustumPoints == null)
                BoundingBoxD.m_frustumPoints = new Vector3D[8];
            frustum.GetCorners(BoundingBoxD.m_frustumPoints);
            this.Include(ref BoundingBoxD.m_frustumPoints[0]);
            this.Include(ref BoundingBoxD.m_frustumPoints[1]);
            this.Include(ref BoundingBoxD.m_frustumPoints[2]);
            this.Include(ref BoundingBoxD.m_frustumPoints[3]);
            this.Include(ref BoundingBoxD.m_frustumPoints[4]);
            this.Include(ref BoundingBoxD.m_frustumPoints[5]);
            this.Include(ref BoundingBoxD.m_frustumPoints[6]);
            this.Include(ref BoundingBoxD.m_frustumPoints[7]);
            return this;
        }

        public static BoundingBoxD CreateInvalid()
        {
            BoundingBoxD boundingBoxD = new BoundingBoxD();
            Vector3D vector3D1 = new Vector3D(double.MaxValue, double.MaxValue, double.MaxValue);
            Vector3D vector3D2 = new Vector3D(double.MinValue, double.MinValue, double.MinValue);
            boundingBoxD.Min = vector3D1;
            boundingBoxD.Max = vector3D2;
            return boundingBoxD;
        }

        public double SurfaceArea()
        {
            Vector3D vector3D = this.Max - this.Min;
            return 2.0*(vector3D.X*vector3D.Y + vector3D.X*vector3D.Z + vector3D.Y*vector3D.Z);
        }

        public double Volume()
        {
            Vector3D vector3D = this.Max - this.Min;
            return vector3D.X*vector3D.Y*vector3D.Z;
        }

        public double Perimeter()
        {
            return 4.0*(this.Max.X - this.Min.X + (this.Max.Y - this.Min.Y) + (this.Max.Z - this.Min.Z));
        }

        public BoundingBoxD Inflate(double size)
        {
            this.Max += new Vector3D(size);
            this.Min -= new Vector3D(size);
            return this;
        }

        public BoundingBoxD Inflate(Vector3 size)
        {
            this.Max += size;
            this.Min -= size;
            return this;
        }

        public BoundingBoxD GetInflated(double size)
        {
            BoundingBoxD boundingBoxD = this;
            boundingBoxD.Inflate(size);
            return boundingBoxD;
        }

        public BoundingBoxD GetInflated(Vector3 size)
        {
            BoundingBoxD boundingBoxD = this;
            boundingBoxD.Inflate(size);
            return boundingBoxD;
        }

        public void InflateToMinimum(Vector3D minimumSize)
        {
            Vector3D center = this.Center;
            if (this.Size().X < minimumSize.X)
            {
                this.Min.X = center.X - minimumSize.X/2.0;
                this.Max.X = center.X + minimumSize.X/2.0;
            }
            if (this.Size().Y < minimumSize.Y)
            {
                this.Min.Y = center.Y - minimumSize.Y/2.0;
                this.Max.Y = center.Y + minimumSize.Y/2.0;
            }
            if (this.Size().Z >= minimumSize.Z)
                return;
            this.Min.Z = center.Z - minimumSize.Z/2.0;
            this.Max.Z = center.Z + minimumSize.Z/2.0;
        }

        public class ComparerType : IEqualityComparer<BoundingBox>
        {
            public bool Equals(BoundingBox x, BoundingBox y)
            {
                if (x.Min == y.Min)
                    return x.Max == y.Max;
                else
                    return false;
            }

            public int GetHashCode(BoundingBox obj)
            {
                return obj.Min.GetHashCode() ^ obj.Max.GetHashCode();
            }
        }
    }
}