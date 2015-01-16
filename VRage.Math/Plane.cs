// Decompiled with JetBrains decompiler
// Type: VRageMath.Plane
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Globalization;

namespace VRageMath
{
    [Serializable]
    public struct Plane : IEquatable<Plane>
    {
        public Vector3 Normal;
        public float D;
        private static Random _random;

        public Plane(float a, float b, float c, float d)
        {
            this.Normal.X = a;
            this.Normal.Y = b;
            this.Normal.Z = c;
            this.D = d;
        }

        public Plane(Vector3 normal, float d)
        {
            this.Normal = normal;
            this.D = d;
        }

        public Plane(Vector4 value)
        {
            this.Normal.X = value.X;
            this.Normal.Y = value.Y;
            this.Normal.Z = value.Z;
            this.D = value.W;
        }

        public Plane(Vector3 point1, Vector3 point2, Vector3 point3)
        {
            float num1 = point2.X - point1.X;
            float num2 = point2.Y - point1.Y;
            float num3 = point2.Z - point1.Z;
            float num4 = point3.X - point1.X;
            float num5 = point3.Y - point1.Y;
            float num6 = point3.Z - point1.Z;
            float num7 = (float) ((double) num2*(double) num6 - (double) num3*(double) num5);
            float num8 = (float) ((double) num3*(double) num4 - (double) num1*(double) num6);
            float num9 = (float) ((double) num1*(double) num5 - (double) num2*(double) num4);
            float num10 = 1f/
                          (float)
                              Math.Sqrt((double) num7*(double) num7 + (double) num8*(double) num8 +
                                        (double) num9*(double) num9);
            this.Normal.X = num7*num10;
            this.Normal.Y = num8*num10;
            this.Normal.Z = num9*num10;
            this.D =
                (float)
                    -((double) this.Normal.X*(double) point1.X + (double) this.Normal.Y*(double) point1.Y +
                      (double) this.Normal.Z*(double) point1.Z);
        }

        public static bool operator ==(Plane lhs, Plane rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Plane lhs, Plane rhs)
        {
            if ((double) lhs.Normal.X == (double) rhs.Normal.X && (double) lhs.Normal.Y == (double) rhs.Normal.Y &&
                (double) lhs.Normal.Z == (double) rhs.Normal.Z)
                return (double) lhs.D != (double) rhs.D;
            else
                return true;
        }

        public bool Equals(Plane other)
        {
            if ((double) this.Normal.X == (double) other.Normal.X && (double) this.Normal.Y == (double) other.Normal.Y &&
                (double) this.Normal.Z == (double) other.Normal.Z)
                return (double) this.D == (double) other.D;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Plane)
                flag = this.Equals((Plane) obj);
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
            float num1 =
                (float)
                    ((double) this.Normal.X*(double) this.Normal.X + (double) this.Normal.Y*(double) this.Normal.Y +
                     (double) this.Normal.Z*(double) this.Normal.Z);
            if ((double) Math.Abs(num1 - 1f) < 1.19209289550781E-07)
                return;
            float num2 = 1f/(float) Math.Sqrt((double) num1);
            this.Normal.X *= num2;
            this.Normal.Y *= num2;
            this.Normal.Z *= num2;
            this.D *= num2;
        }

        public static Plane Normalize(Plane value)
        {
            float num1 =
                (float)
                    ((double) value.Normal.X*(double) value.Normal.X + (double) value.Normal.Y*(double) value.Normal.Y +
                     (double) value.Normal.Z*(double) value.Normal.Z);
            if ((double) Math.Abs(num1 - 1f) < 1.19209289550781E-07)
            {
                Plane plane;
                plane.Normal = value.Normal;
                plane.D = value.D;
                return plane;
            }
            else
            {
                float num2 = 1f/(float) Math.Sqrt((double) num1);
                Plane plane;
                plane.Normal.X = value.Normal.X*num2;
                plane.Normal.Y = value.Normal.Y*num2;
                plane.Normal.Z = value.Normal.Z*num2;
                plane.D = value.D*num2;
                return plane;
            }
        }

        public static void Normalize(ref Plane value, out Plane result)
        {
            float num1 =
                (float)
                    ((double) value.Normal.X*(double) value.Normal.X + (double) value.Normal.Y*(double) value.Normal.Y +
                     (double) value.Normal.Z*(double) value.Normal.Z);
            if ((double) Math.Abs(num1 - 1f) < 1.19209289550781E-07)
            {
                result.Normal = value.Normal;
                result.D = value.D;
            }
            else
            {
                float num2 = 1f/(float) Math.Sqrt((double) num1);
                result.Normal.X = value.Normal.X*num2;
                result.Normal.Y = value.Normal.Y*num2;
                result.Normal.Z = value.Normal.Z*num2;
                result.D = value.D*num2;
            }
        }

        public static Plane Transform(Plane plane, Matrix matrix)
        {
            Matrix result;
            Matrix.Invert(ref matrix, out result);
            float num1 = plane.Normal.X;
            float num2 = plane.Normal.Y;
            float num3 = plane.Normal.Z;
            float num4 = plane.D;
            Plane plane1;
            plane1.Normal.X =
                (float)
                    ((double) num1*(double) result.M11 + (double) num2*(double) result.M12 +
                     (double) num3*(double) result.M13 + (double) num4*(double) result.M14);
            plane1.Normal.Y =
                (float)
                    ((double) num1*(double) result.M21 + (double) num2*(double) result.M22 +
                     (double) num3*(double) result.M23 + (double) num4*(double) result.M24);
            plane1.Normal.Z =
                (float)
                    ((double) num1*(double) result.M31 + (double) num2*(double) result.M32 +
                     (double) num3*(double) result.M33 + (double) num4*(double) result.M34);
            plane1.D =
                (float)
                    ((double) num1*(double) result.M41 + (double) num2*(double) result.M42 +
                     (double) num3*(double) result.M43 + (double) num4*(double) result.M44);
            return plane1;
        }

        public static void Transform(ref Plane plane, ref Matrix matrix, out Plane result)
        {
            Matrix result1;
            Matrix.Invert(ref matrix, out result1);
            float num1 = plane.Normal.X;
            float num2 = plane.Normal.Y;
            float num3 = plane.Normal.Z;
            float num4 = plane.D;
            result.Normal.X =
                (float)
                    ((double) num1*(double) result1.M11 + (double) num2*(double) result1.M12 +
                     (double) num3*(double) result1.M13 + (double) num4*(double) result1.M14);
            result.Normal.Y =
                (float)
                    ((double) num1*(double) result1.M21 + (double) num2*(double) result1.M22 +
                     (double) num3*(double) result1.M23 + (double) num4*(double) result1.M24);
            result.Normal.Z =
                (float)
                    ((double) num1*(double) result1.M31 + (double) num2*(double) result1.M32 +
                     (double) num3*(double) result1.M33 + (double) num4*(double) result1.M34);
            result.D =
                (float)
                    ((double) num1*(double) result1.M41 + (double) num2*(double) result1.M42 +
                     (double) num3*(double) result1.M43 + (double) num4*(double) result1.M44);
        }

        public static Plane Transform(Plane plane, Quaternion rotation)
        {
            float num1 = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W*num1;
            float num5 = rotation.W*num2;
            float num6 = rotation.W*num3;
            float num7 = rotation.X*num1;
            float num8 = rotation.X*num2;
            float num9 = rotation.X*num3;
            float num10 = rotation.Y*num2;
            float num11 = rotation.Y*num3;
            float num12 = rotation.Z*num3;
            float num13 = 1f - num10 - num12;
            float num14 = num8 - num6;
            float num15 = num9 + num5;
            float num16 = num8 + num6;
            float num17 = 1f - num7 - num12;
            float num18 = num11 - num4;
            float num19 = num9 - num5;
            float num20 = num11 + num4;
            float num21 = 1f - num7 - num10;
            float num22 = plane.Normal.X;
            float num23 = plane.Normal.Y;
            float num24 = plane.Normal.Z;
            Plane plane1;
            plane1.Normal.X =
                (float) ((double) num22*(double) num13 + (double) num23*(double) num14 + (double) num24*(double) num15);
            plane1.Normal.Y =
                (float) ((double) num22*(double) num16 + (double) num23*(double) num17 + (double) num24*(double) num18);
            plane1.Normal.Z =
                (float) ((double) num22*(double) num19 + (double) num23*(double) num20 + (double) num24*(double) num21);
            plane1.D = plane.D;
            return plane1;
        }

        public static void Transform(ref Plane plane, ref Quaternion rotation, out Plane result)
        {
            float num1 = rotation.X + rotation.X;
            float num2 = rotation.Y + rotation.Y;
            float num3 = rotation.Z + rotation.Z;
            float num4 = rotation.W*num1;
            float num5 = rotation.W*num2;
            float num6 = rotation.W*num3;
            float num7 = rotation.X*num1;
            float num8 = rotation.X*num2;
            float num9 = rotation.X*num3;
            float num10 = rotation.Y*num2;
            float num11 = rotation.Y*num3;
            float num12 = rotation.Z*num3;
            float num13 = 1f - num10 - num12;
            float num14 = num8 - num6;
            float num15 = num9 + num5;
            float num16 = num8 + num6;
            float num17 = 1f - num7 - num12;
            float num18 = num11 - num4;
            float num19 = num9 - num5;
            float num20 = num11 + num4;
            float num21 = 1f - num7 - num10;
            float num22 = plane.Normal.X;
            float num23 = plane.Normal.Y;
            float num24 = plane.Normal.Z;
            result.Normal.X =
                (float) ((double) num22*(double) num13 + (double) num23*(double) num14 + (double) num24*(double) num15);
            result.Normal.Y =
                (float) ((double) num22*(double) num16 + (double) num23*(double) num17 + (double) num24*(double) num18);
            result.Normal.Z =
                (float) ((double) num22*(double) num19 + (double) num23*(double) num20 + (double) num24*(double) num21);
            result.D = plane.D;
        }

        public float Dot(Vector4 value)
        {
            return
                (float)
                    ((double) this.Normal.X*(double) value.X + (double) this.Normal.Y*(double) value.Y +
                     (double) this.Normal.Z*(double) value.Z + (double) this.D*(double) value.W);
        }

        public void Dot(ref Vector4 value, out float result)
        {
            result =
                (float)
                    ((double) this.Normal.X*(double) value.X + (double) this.Normal.Y*(double) value.Y +
                     (double) this.Normal.Z*(double) value.Z + (double) this.D*(double) value.W);
        }

        public float DotCoordinate(Vector3 value)
        {
            return
                (float)
                    ((double) this.Normal.X*(double) value.X + (double) this.Normal.Y*(double) value.Y +
                     (double) this.Normal.Z*(double) value.Z) + this.D;
        }

        public void DotCoordinate(ref Vector3 value, out float result)
        {
            result =
                (float)
                    ((double) this.Normal.X*(double) value.X + (double) this.Normal.Y*(double) value.Y +
                     (double) this.Normal.Z*(double) value.Z) + this.D;
        }

        public float DotNormal(Vector3 value)
        {
            return
                (float)
                    ((double) this.Normal.X*(double) value.X + (double) this.Normal.Y*(double) value.Y +
                     (double) this.Normal.Z*(double) value.Z);
        }

        public void DotNormal(ref Vector3 value, out float result)
        {
            result =
                (float)
                    ((double) this.Normal.X*(double) value.X + (double) this.Normal.Y*(double) value.Y +
                     (double) this.Normal.Z*(double) value.Z);
        }

        public PlaneIntersectionType Intersects(BoundingBox box)
        {
            Vector3 vector3_1;
            vector3_1.X = (double) this.Normal.X >= 0.0 ? box.Min.X : box.Max.X;
            vector3_1.Y = (double) this.Normal.Y >= 0.0 ? box.Min.Y : box.Max.Y;
            vector3_1.Z = (double) this.Normal.Z >= 0.0 ? box.Min.Z : box.Max.Z;
            Vector3 vector3_2;
            vector3_2.X = (double) this.Normal.X >= 0.0 ? box.Max.X : box.Min.X;
            vector3_2.Y = (double) this.Normal.Y >= 0.0 ? box.Max.Y : box.Min.Y;
            vector3_2.Z = (double) this.Normal.Z >= 0.0 ? box.Max.Z : box.Min.Z;
            if ((double) this.Normal.X*(double) vector3_1.X + (double) this.Normal.Y*(double) vector3_1.Y +
                (double) this.Normal.Z*(double) vector3_1.Z + (double) this.D > 0.0)
                return PlaneIntersectionType.Front;
            return (double) this.Normal.X*(double) vector3_2.X + (double) this.Normal.Y*(double) vector3_2.Y +
                   (double) this.Normal.Z*(double) vector3_2.Z + (double) this.D >= 0.0
                ? PlaneIntersectionType.Intersecting
                : PlaneIntersectionType.Back;
        }

        public void Intersects(ref BoundingBox box, out PlaneIntersectionType result)
        {
            Vector3 vector3_1;
            vector3_1.X = (double) this.Normal.X >= 0.0 ? box.Min.X : box.Max.X;
            vector3_1.Y = (double) this.Normal.Y >= 0.0 ? box.Min.Y : box.Max.Y;
            vector3_1.Z = (double) this.Normal.Z >= 0.0 ? box.Min.Z : box.Max.Z;
            Vector3 vector3_2;
            vector3_2.X = (double) this.Normal.X >= 0.0 ? box.Max.X : box.Min.X;
            vector3_2.Y = (double) this.Normal.Y >= 0.0 ? box.Max.Y : box.Min.Y;
            vector3_2.Z = (double) this.Normal.Z >= 0.0 ? box.Max.Z : box.Min.Z;
            if ((double) this.Normal.X*(double) vector3_1.X + (double) this.Normal.Y*(double) vector3_1.Y +
                (double) this.Normal.Z*(double) vector3_1.Z + (double) this.D > 0.0)
                result = PlaneIntersectionType.Front;
            else if ((double) this.Normal.X*(double) vector3_2.X + (double) this.Normal.Y*(double) vector3_2.Y +
                     (double) this.Normal.Z*(double) vector3_2.Z + (double) this.D < 0.0)
                result = PlaneIntersectionType.Back;
            else
                result = PlaneIntersectionType.Intersecting;
        }

        public PlaneIntersectionType Intersects(BoundingFrustum frustum)
        {
            return frustum.Intersects(this);
        }

        public PlaneIntersectionType Intersects(BoundingSphere sphere)
        {
            float num =
                (float)
                    ((double) sphere.Center.X*(double) this.Normal.X + (double) sphere.Center.Y*(double) this.Normal.Y +
                     (double) sphere.Center.Z*(double) this.Normal.Z) + this.D;
            if ((double) num > (double) sphere.Radius)
                return PlaneIntersectionType.Front;
            return (double) num >= -(double) sphere.Radius
                ? PlaneIntersectionType.Intersecting
                : PlaneIntersectionType.Back;
        }

        public void Intersects(ref BoundingSphere sphere, out PlaneIntersectionType result)
        {
            float num =
                (float)
                    ((double) sphere.Center.X*(double) this.Normal.X + (double) sphere.Center.Y*(double) this.Normal.Y +
                     (double) sphere.Center.Z*(double) this.Normal.Z) + this.D;
            if ((double) num > (double) sphere.Radius)
                result = PlaneIntersectionType.Front;
            else if ((double) num < -(double) sphere.Radius)
                result = PlaneIntersectionType.Back;
            else
                result = PlaneIntersectionType.Intersecting;
        }

        public Vector3 RandomPoint()
        {
            if (Plane._random == null)
                Plane._random = new Random();
            Vector3 vector1 = new Vector3();
            Vector3 vector3;
            do
            {
                vector1.X = (float) (2.0*Plane._random.NextDouble() - 1.0);
                vector1.Y = (float) (2.0*Plane._random.NextDouble() - 1.0);
                vector1.Z = (float) (2.0*Plane._random.NextDouble() - 1.0);
                vector3 = Vector3.Cross(vector1, this.Normal);
            } while (vector3 == Vector3.Zero);
            double num = (double) vector3.Normalize();
            return vector3*(float) Math.Sqrt(Plane._random.NextDouble());
        }
    }
}