// Decompiled with JetBrains decompiler
// Type: VRageMath.Vector3
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace VRageMath
{
    [ProtoContract]
    [Serializable]
    public struct Vector3 : IEquatable<Vector3>
    {
        public static Vector3 Zero = new Vector3();
        public static Vector3 One = new Vector3(1f, 1f, 1f);
        public static Vector3 Half = new Vector3(0.5f, 0.5f, 0.5f);
        public static Vector3 PositiveInfinity = new Vector3(float.PositiveInfinity);
        public static Vector3 NegativeInfinity = new Vector3(float.NegativeInfinity);
        public static Vector3 UnitX = new Vector3(1f, 0.0f, 0.0f);
        public static Vector3 UnitY = new Vector3(0.0f, 1f, 0.0f);
        public static Vector3 UnitZ = new Vector3(0.0f, 0.0f, 1f);
        public static Vector3 Up = new Vector3(0.0f, 1f, 0.0f);
        public static Vector3 Down = new Vector3(0.0f, -1f, 0.0f);
        public static Vector3 Right = new Vector3(1f, 0.0f, 0.0f);
        public static Vector3 Left = new Vector3(-1f, 0.0f, 0.0f);
        public static Vector3 Forward = new Vector3(0.0f, 0.0f, -1f);
        public static Vector3 Backward = new Vector3(0.0f, 0.0f, 1f);
        public static Vector3 MaxValue = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        public static Vector3 MinValue = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        [ProtoMember(1)] public float X;
        [ProtoMember(2)] public float Y;
        [ProtoMember(3)] public float Z;

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3(double x, double y, double z)
        {
            this.X = (float) x;
            this.Y = (float) y;
            this.Z = (float) z;
        }

        public Vector3(float value)
        {
            this.X = this.Y = this.Z = value;
        }

        public Vector3(Vector2 value, float z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }

        public Vector3(Vector4 xyz)
        {
            this.X = xyz.X;
            this.Y = xyz.Y;
            this.Z = xyz.Z;
        }

        public Vector3(ref Vector3I value)
        {
            this.X = (float) value.X;
            this.Y = (float) value.Y;
            this.Z = (float) value.Z;
        }

        public Vector3(Vector3I value)
        {
            this.X = (float) value.X;
            this.Y = (float) value.Y;
            this.Z = (float) value.Z;
        }

        public static Vector3 operator -(Vector3 value)
        {
            Vector3 vector3;
            vector3.X = -value.X;
            vector3.Y = -value.Y;
            vector3.Z = -value.Z;
            return vector3;
        }

        public static bool operator ==(Vector3 value1, Vector3 value2)
        {
            if ((double) value1.X == (double) value2.X && (double) value1.Y == (double) value2.Y)
                return (double) value1.Z == (double) value2.Z;
            else
                return false;
        }

        public static bool operator !=(Vector3 value1, Vector3 value2)
        {
            if ((double) value1.X == (double) value2.X && (double) value1.Y == (double) value2.Y)
                return (double) value1.Z != (double) value2.Z;
            else
                return true;
        }

        public static Vector3 operator +(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X + value2.X;
            vector3.Y = value1.Y + value2.Y;
            vector3.Z = value1.Z + value2.Z;
            return vector3;
        }

        public static Vector3 operator +(Vector3 value1, float value2)
        {
            Vector3 vector3;
            vector3.X = value1.X + value2;
            vector3.Y = value1.Y + value2;
            vector3.Z = value1.Z + value2;
            return vector3;
        }

        public static Vector3 operator -(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X - value2.X;
            vector3.Y = value1.Y - value2.Y;
            vector3.Z = value1.Z - value2.Z;
            return vector3;
        }

        public static Vector3 operator -(Vector3 value1, float value2)
        {
            Vector3 vector3;
            vector3.X = value1.X - value2;
            vector3.Y = value1.Y - value2;
            vector3.Z = value1.Z - value2;
            return vector3;
        }

        public static Vector3 operator *(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X*value2.X;
            vector3.Y = value1.Y*value2.Y;
            vector3.Z = value1.Z*value2.Z;
            return vector3;
        }

        public static Vector3 operator *(Vector3 value, float scaleFactor)
        {
            Vector3 vector3;
            vector3.X = value.X*scaleFactor;
            vector3.Y = value.Y*scaleFactor;
            vector3.Z = value.Z*scaleFactor;
            return vector3;
        }

        public static Vector3 operator *(float scaleFactor, Vector3 value)
        {
            Vector3 vector3;
            vector3.X = value.X*scaleFactor;
            vector3.Y = value.Y*scaleFactor;
            vector3.Z = value.Z*scaleFactor;
            return vector3;
        }

        public static Vector3 operator /(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X/value2.X;
            vector3.Y = value1.Y/value2.Y;
            vector3.Z = value1.Z/value2.Z;
            return vector3;
        }

        public static Vector3 operator /(Vector3 value, float divider)
        {
            float num = 1f/divider;
            Vector3 vector3;
            vector3.X = value.X*num;
            vector3.Y = value.Y*num;
            vector3.Z = value.Z*num;
            return vector3;
        }

        public static Vector3 operator /(float value, Vector3 divider)
        {
            Vector3 vector3;
            vector3.X = value/divider.X;
            vector3.Y = value/divider.Y;
            vector3.Z = value/divider.Z;
            return vector3;
        }

        public static Vector3 Abs(Vector3 value)
        {
            return new Vector3((double) value.X < 0.0 ? -value.X : value.X, (double) value.Y < 0.0 ? -value.Y : value.Y,
                (double) value.Z < 0.0 ? -value.Z : value.Z);
        }

        public static Vector3 Sign(Vector3 value)
        {
            return new Vector3((float) Math.Sign(value.X), (float) Math.Sign(value.Y), (float) Math.Sign(value.Z));
        }

        public static Vector3 SignNonZero(Vector3 value)
        {
            return new Vector3((double) value.X < 0.0 ? -1f : 1f, (double) value.Y < 0.0 ? -1f : 1f,
                (double) value.Z < 0.0 ? -1f : 1f);
        }

        public void Interpolate3(Vector3 v0, Vector3 v1, float rt)
        {
            float num = 1f - rt;
            this.X = (float) ((double) num*(double) v0.X + (double) rt*(double) v1.X);
            this.Y = (float) ((double) num*(double) v0.Y + (double) rt*(double) v1.Y);
            this.Z = (float) ((double) num*(double) v0.Z + (double) rt*(double) v1.Z);
        }

        public static bool IsValid(Vector3 value)
        {
            return Vector3.IsValid(ref value);
        }

        public static bool IsValid(ref Vector3 value)
        {
            return FloatExtensions.IsValid(value.X*value.Y*value.Z);
        }

        public static bool IsUnit(ref Vector3 value)
        {
            float num = value.LengthSquared();
            if ((double) num >= 0.999899983406067)
                return (double) num < 1.00010001659393;
            else
                return false;
        }

        public static bool ArePerpendicular(ref Vector3 a, ref Vector3 b)
        {
            float num = a.Dot(b);
            return (double) num*(double) num <
                   9.99999993922529E-09*(double) a.LengthSquared()*(double) b.LengthSquared();
        }

        public static bool IsZero(Vector3 value)
        {
            return Vector3.IsZero(value, 0.0001f);
        }

        public static bool IsZero(Vector3 value, float epsilon)
        {
            if ((double) Math.Abs(value.X) < (double) epsilon && (double) Math.Abs(value.Y) < (double) epsilon)
                return (double) Math.Abs(value.Z) < (double) epsilon;
            else
                return false;
        }

        public static Vector3 IsZeroVector(Vector3 value)
        {
            return new Vector3((double) value.X == 0.0 ? 1f : 0.0f, (double) value.Y == 0.0 ? 1f : 0.0f,
                (double) value.Z == 0.0 ? 1f : 0.0f);
        }

        public static Vector3 IsZeroVector(Vector3 value, float epsilon)
        {
            return new Vector3((double) Math.Abs(value.X) < (double) epsilon ? 1f : 0.0f,
                (double) Math.Abs(value.Y) < (double) epsilon ? 1f : 0.0f,
                (double) Math.Abs(value.Z) < (double) epsilon ? 1f : 0.0f);
        }

        public static Vector3 Step(Vector3 value)
        {
            return new Vector3((double) value.X > 0.0 ? 1f : ((double) value.X < 0.0 ? -1f : 0.0f),
                (double) value.Y > 0.0 ? 1f : ((double) value.Y < 0.0 ? -1f : 0.0f),
                (double) value.Z > 0.0 ? 1f : ((double) value.Z < 0.0 ? -1f : 0.0f));
        }

        public override string ToString()
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format((IFormatProvider) currentCulture, "{{X:{0} Y:{1} Z:{2}}}",
                (object) this.X.ToString((IFormatProvider) currentCulture),
                (object) this.Y.ToString((IFormatProvider) currentCulture),
                (object) this.Z.ToString((IFormatProvider) currentCulture));
        }

        public string ToString(string format)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            return string.Format((IFormatProvider) currentCulture, "{{X:{0} Y:{1} Z:{2}}}",
                (object) this.X.ToString(format, (IFormatProvider) currentCulture),
                (object) this.Y.ToString(format, (IFormatProvider) currentCulture),
                (object) this.Z.ToString(format, (IFormatProvider) currentCulture));
        }

        public bool Equals(Vector3 other)
        {
            if ((double) this.X == (double) other.X && (double) this.Y == (double) other.Y)
                return (double) this.Z == (double) other.Z;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            bool flag = false;
            if (obj is Vector3)
                flag = this.Equals((Vector3) obj);
            return flag;
        }

        public override int GetHashCode()
        {
            return ((int) ((double) this.X*997.0)*397 ^ (int) ((double) this.Y*997.0))*397 ^
                   (int) ((double) this.Z*997.0);
        }

        public long GetHash()
        {
            return ((((((long) Math.Round((double) Math.Abs(this.X*1000f))*397L ^
                        (long) Math.Round((double) Math.Abs(this.Y*1000f)))*397L ^
                       (long) Math.Round((double) Math.Abs(this.Z*1000f)))*397L ^ (long) (Math.Sign(this.X) + 5))*397L ^
                     (long) (Math.Sign(this.Y) + 7))*397L ^ (long) (Math.Sign(this.Z) + 11))*397L ^
                   (long) (2 + 4 + 16 + 256 + 65536 + 1);
        }

        public float Length()
        {
            return
                (float)
                    Math.Sqrt((double) this.X*(double) this.X + (double) this.Y*(double) this.Y +
                              (double) this.Z*(double) this.Z);
        }

        public float LengthSquared()
        {
            return
                (float)
                    ((double) this.X*(double) this.X + (double) this.Y*(double) this.Y + (double) this.Z*(double) this.Z);
        }

        public static float Distance(Vector3 value1, Vector3 value2)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            return
                (float)
                    Math.Sqrt((double) num1*(double) num1 + (double) num2*(double) num2 + (double) num3*(double) num3);
        }

        public static void Distance(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            float num4 =
                (float) ((double) num1*(double) num1 + (double) num2*(double) num2 + (double) num3*(double) num3);
            result = (float) Math.Sqrt((double) num4);
        }

        public static float DistanceSquared(Vector3 value1, Vector3 value2)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            return (float) ((double) num1*(double) num1 + (double) num2*(double) num2 + (double) num3*(double) num3);
        }

        public static void DistanceSquared(ref Vector3 value1, ref Vector3 value2, out float result)
        {
            float num1 = value1.X - value2.X;
            float num2 = value1.Y - value2.Y;
            float num3 = value1.Z - value2.Z;
            result = (float) ((double) num1*(double) num1 + (double) num2*(double) num2 + (double) num3*(double) num3);
        }

        public static float RectangularDistance(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3 = Vector3.Abs(value1 - value2);
            return vector3.X + vector3.Y + vector3.Z;
        }

        public static float RectangularDistance(ref Vector3 value1, ref Vector3 value2)
        {
            Vector3 vector3 = Vector3.Abs(value1 - value2);
            return vector3.X + vector3.Y + vector3.Z;
        }

        public static float Dot(Vector3 vector1, Vector3 vector2)
        {
            return
                (float)
                    ((double) vector1.X*(double) vector2.X + (double) vector1.Y*(double) vector2.Y +
                     (double) vector1.Z*(double) vector2.Z);
        }

        public static void Dot(ref Vector3 vector1, ref Vector3 vector2, out float result)
        {
            result =
                (float)
                    ((double) vector1.X*(double) vector2.X + (double) vector1.Y*(double) vector2.Y +
                     (double) vector1.Z*(double) vector2.Z);
        }

        public float Dot(Vector3 v)
        {
            return Vector3.Dot(this, v);
        }

        public float Dot(ref Vector3 v)
        {
            return (float) ((double) this.X*(double) v.X + (double) this.Y*(double) v.Y + (double) this.Z*(double) v.Z);
        }

        public Vector3 Cross(Vector3 v)
        {
            return Vector3.Cross(this, v);
        }

        public float Normalize()
        {
            float num1 =
                (float)
                    Math.Sqrt((double) this.X*(double) this.X + (double) this.Y*(double) this.Y +
                              (double) this.Z*(double) this.Z);
            float num2 = 1f/num1;
            this.X *= num2;
            this.Y *= num2;
            this.Z *= num2;
            return num1;
        }

        public static Vector3 Normalize(Vector3 value)
        {
            float num = 1f/
                        (float)
                            Math.Sqrt((double) value.X*(double) value.X + (double) value.Y*(double) value.Y +
                                      (double) value.Z*(double) value.Z);
            Vector3 vector3;
            vector3.X = value.X*num;
            vector3.Y = value.Y*num;
            vector3.Z = value.Z*num;
            return vector3;
        }

        public static Vector3 Normalize(Vector3D value)
        {
            float num = 1f/(float) Math.Sqrt(value.X*value.X + value.Y*value.Y + value.Z*value.Z);
            Vector3 vector3;
            vector3.X = (float) value.X*num;
            vector3.Y = (float) value.Y*num;
            vector3.Z = (float) value.Z*num;
            return vector3;
        }

        public static void Normalize(ref Vector3 value, out Vector3 result)
        {
            float num = 1f/
                        (float)
                            Math.Sqrt((double) value.X*(double) value.X + (double) value.Y*(double) value.Y +
                                      (double) value.Z*(double) value.Z);
            result.X = value.X*num;
            result.Y = value.Y*num;
            result.Z = value.Z*num;
        }

        public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
        {
            Vector3 vector3;
            vector3.X = (float) ((double) vector1.Y*(double) vector2.Z - (double) vector1.Z*(double) vector2.Y);
            vector3.Y = (float) ((double) vector1.Z*(double) vector2.X - (double) vector1.X*(double) vector2.Z);
            vector3.Z = (float) ((double) vector1.X*(double) vector2.Y - (double) vector1.Y*(double) vector2.X);
            return vector3;
        }

        public static void Cross(ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
        {
            float num1 = (float) ((double) vector1.Y*(double) vector2.Z - (double) vector1.Z*(double) vector2.Y);
            float num2 = (float) ((double) vector1.Z*(double) vector2.X - (double) vector1.X*(double) vector2.Z);
            float num3 = (float) ((double) vector1.X*(double) vector2.Y - (double) vector1.Y*(double) vector2.X);
            result.X = num1;
            result.Y = num2;
            result.Z = num3;
        }

        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            float num =
                (float)
                    ((double) vector.X*(double) normal.X + (double) vector.Y*(double) normal.Y +
                     (double) vector.Z*(double) normal.Z);
            Vector3 vector3;
            vector3.X = vector.X - 2f*num*normal.X;
            vector3.Y = vector.Y - 2f*num*normal.Y;
            vector3.Z = vector.Z - 2f*num*normal.Z;
            return vector3;
        }

        public static void Reflect(ref Vector3 vector, ref Vector3 normal, out Vector3 result)
        {
            float num =
                (float)
                    ((double) vector.X*(double) normal.X + (double) vector.Y*(double) normal.Y +
                     (double) vector.Z*(double) normal.Z);
            result.X = vector.X - 2f*num*normal.X;
            result.Y = vector.Y - 2f*num*normal.Y;
            result.Z = vector.Z - 2f*num*normal.Z;
        }

        public float Min()
        {
            if ((double) this.X < (double) this.Y)
            {
                if ((double) this.X < (double) this.Z)
                    return this.X;
                else
                    return this.Z;
            }
            else if ((double) this.Y < (double) this.Z)
                return this.Y;
            else
                return this.Z;
        }

        public float AbsMin()
        {
            if ((double) Math.Abs(this.X) < (double) Math.Abs(this.Y))
            {
                if ((double) Math.Abs(this.X) < (double) Math.Abs(this.Z))
                    return Math.Abs(this.X);
                else
                    return Math.Abs(this.Z);
            }
            else if ((double) Math.Abs(this.Y) < (double) Math.Abs(this.Z))
                return Math.Abs(this.Y);
            else
                return Math.Abs(this.Z);
        }

        public float Max()
        {
            if ((double) this.X > (double) this.Y)
            {
                if ((double) this.X > (double) this.Z)
                    return this.X;
                else
                    return this.Z;
            }
            else if ((double) this.Y > (double) this.Z)
                return this.Y;
            else
                return this.Z;
        }

        public float AbsMax()
        {
            if ((double) Math.Abs(this.X) > (double) Math.Abs(this.Y))
            {
                if ((double) Math.Abs(this.X) > (double) Math.Abs(this.Z))
                    return Math.Abs(this.X);
                else
                    return Math.Abs(this.Z);
            }
            else if ((double) Math.Abs(this.Y) > (double) Math.Abs(this.Z))
                return Math.Abs(this.Y);
            else
                return Math.Abs(this.Z);
        }

        public static Vector3 Min(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = (double) value1.X < (double) value2.X ? value1.X : value2.X;
            vector3.Y = (double) value1.Y < (double) value2.Y ? value1.Y : value2.Y;
            vector3.Z = (double) value1.Z < (double) value2.Z ? value1.Z : value2.Z;
            return vector3;
        }

        public static void Min(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = (double) value1.X < (double) value2.X ? value1.X : value2.X;
            result.Y = (double) value1.Y < (double) value2.Y ? value1.Y : value2.Y;
            result.Z = (double) value1.Z < (double) value2.Z ? value1.Z : value2.Z;
        }

        public static Vector3 Max(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = (double) value1.X > (double) value2.X ? value1.X : value2.X;
            vector3.Y = (double) value1.Y > (double) value2.Y ? value1.Y : value2.Y;
            vector3.Z = (double) value1.Z > (double) value2.Z ? value1.Z : value2.Z;
            return vector3;
        }

        public static void Max(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = (double) value1.X > (double) value2.X ? value1.X : value2.X;
            result.Y = (double) value1.Y > (double) value2.Y ? value1.Y : value2.Y;
            result.Z = (double) value1.Z > (double) value2.Z ? value1.Z : value2.Z;
        }

        public static Vector3 DominantAxisProjection(Vector3 value1)
        {
            Vector3 result;
            Vector3.DominantAxisProjection(ref value1, out result);
            return result;
        }

        public static void DominantAxisProjection(ref Vector3 value1, out Vector3 result)
        {
            if ((double) Math.Abs(value1.X) > (double) Math.Abs(value1.Y))
            {
                if ((double) Math.Abs(value1.X) > (double) Math.Abs(value1.Z))
                    result = new Vector3(value1.X, 0.0f, 0.0f);
                else
                    result = new Vector3(0.0f, 0.0f, value1.Z);
            }
            else if ((double) Math.Abs(value1.Y) > (double) Math.Abs(value1.Z))
                result = new Vector3(0.0f, value1.Y, 0.0f);
            else
                result = new Vector3(0.0f, 0.0f, value1.Z);
        }

        public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
        {
            float num1 = value1.X;
            float num2 = (double) num1 > (double) max.X ? max.X : num1;
            float num3 = (double) num2 < (double) min.X ? min.X : num2;
            float num4 = value1.Y;
            float num5 = (double) num4 > (double) max.Y ? max.Y : num4;
            float num6 = (double) num5 < (double) min.Y ? min.Y : num5;
            float num7 = value1.Z;
            float num8 = (double) num7 > (double) max.Z ? max.Z : num7;
            float num9 = (double) num8 < (double) min.Z ? min.Z : num8;
            Vector3 vector3;
            vector3.X = num3;
            vector3.Y = num6;
            vector3.Z = num9;
            return vector3;
        }

        public static void Clamp(ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result)
        {
            float num1 = value1.X;
            float num2 = (double) num1 > (double) max.X ? max.X : num1;
            float num3 = (double) num2 < (double) min.X ? min.X : num2;
            float num4 = value1.Y;
            float num5 = (double) num4 > (double) max.Y ? max.Y : num4;
            float num6 = (double) num5 < (double) min.Y ? min.Y : num5;
            float num7 = value1.Z;
            float num8 = (double) num7 > (double) max.Z ? max.Z : num7;
            float num9 = (double) num8 < (double) min.Z ? min.Z : num8;
            result.X = num3;
            result.Y = num6;
            result.Z = num9;
        }

        public static Vector3 ClampToSphere(Vector3 vector, float radius)
        {
            float num1 = vector.LengthSquared();
            float num2 = radius*radius;
            if ((double) num1 > (double) num2)
                return vector*(float) Math.Sqrt((double) num2/(double) num1);
            else
                return vector;
        }

        public static void ClampToSphere(ref Vector3 vector, float radius)
        {
            float num1 = vector.LengthSquared();
            float num2 = radius*radius;
            if ((double) num1 <= (double) num2)
                return;
            vector *= (float) Math.Sqrt((double) num2/(double) num1);
        }

        public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
        {
            Vector3 vector3;
            vector3.X = value1.X + (value2.X - value1.X)*amount;
            vector3.Y = value1.Y + (value2.Y - value1.Y)*amount;
            vector3.Z = value1.Z + (value2.Z - value1.Z)*amount;
            return vector3;
        }

        public static void Lerp(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
        {
            result.X = value1.X + (value2.X - value1.X)*amount;
            result.Y = value1.Y + (value2.Y - value1.Y)*amount;
            result.Z = value1.Z + (value2.Z - value1.Z)*amount;
        }

        public static Vector3 Barycentric(Vector3 value1, Vector3 value2, Vector3 value3, float amount1, float amount2)
        {
            Vector3 vector3;
            vector3.X =
                (float)
                    ((double) value1.X + (double) amount1*((double) value2.X - (double) value1.X) +
                     (double) amount2*((double) value3.X - (double) value1.X));
            vector3.Y =
                (float)
                    ((double) value1.Y + (double) amount1*((double) value2.Y - (double) value1.Y) +
                     (double) amount2*((double) value3.Y - (double) value1.Y));
            vector3.Z =
                (float)
                    ((double) value1.Z + (double) amount1*((double) value2.Z - (double) value1.Z) +
                     (double) amount2*((double) value3.Z - (double) value1.Z));
            return vector3;
        }

        public static void Barycentric(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, float amount1,
            float amount2, out Vector3 result)
        {
            result.X =
                (float)
                    ((double) value1.X + (double) amount1*((double) value2.X - (double) value1.X) +
                     (double) amount2*((double) value3.X - (double) value1.X));
            result.Y =
                (float)
                    ((double) value1.Y + (double) amount1*((double) value2.Y - (double) value1.Y) +
                     (double) amount2*((double) value3.Y - (double) value1.Y));
            result.Z =
                (float)
                    ((double) value1.Z + (double) amount1*((double) value2.Z - (double) value1.Z) +
                     (double) amount2*((double) value3.Z - (double) value1.Z));
        }

        public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
        {
            amount = (double) amount > 1.0 ? 1f : ((double) amount < 0.0 ? 0.0f : amount);
            amount = (float) ((double) amount*(double) amount*(3.0 - 2.0*(double) amount));
            Vector3 vector3;
            vector3.X = value1.X + (value2.X - value1.X)*amount;
            vector3.Y = value1.Y + (value2.Y - value1.Y)*amount;
            vector3.Z = value1.Z + (value2.Z - value1.Z)*amount;
            return vector3;
        }

        public static void SmoothStep(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
        {
            amount = (double) amount > 1.0 ? 1f : ((double) amount < 0.0 ? 0.0f : amount);
            amount = (float) ((double) amount*(double) amount*(3.0 - 2.0*(double) amount));
            result.X = value1.X + (value2.X - value1.X)*amount;
            result.Y = value1.Y + (value2.Y - value1.Y)*amount;
            result.Z = value1.Z + (value2.Z - value1.Z)*amount;
        }

        public static Vector3 CatmullRom(Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, float amount)
        {
            float num1 = amount*amount;
            float num2 = amount*num1;
            Vector3 vector3;
            vector3.X =
                (float)
                    (0.5*
                     (2.0*(double) value2.X + (-(double) value1.X + (double) value3.X)*(double) amount +
                      (2.0*(double) value1.X - 5.0*(double) value2.X + 4.0*(double) value3.X - (double) value4.X)*
                      (double) num1 +
                      (-(double) value1.X + 3.0*(double) value2.X - 3.0*(double) value3.X + (double) value4.X)*
                      (double) num2));
            vector3.Y =
                (float)
                    (0.5*
                     (2.0*(double) value2.Y + (-(double) value1.Y + (double) value3.Y)*(double) amount +
                      (2.0*(double) value1.Y - 5.0*(double) value2.Y + 4.0*(double) value3.Y - (double) value4.Y)*
                      (double) num1 +
                      (-(double) value1.Y + 3.0*(double) value2.Y - 3.0*(double) value3.Y + (double) value4.Y)*
                      (double) num2));
            vector3.Z =
                (float)
                    (0.5*
                     (2.0*(double) value2.Z + (-(double) value1.Z + (double) value3.Z)*(double) amount +
                      (2.0*(double) value1.Z - 5.0*(double) value2.Z + 4.0*(double) value3.Z - (double) value4.Z)*
                      (double) num1 +
                      (-(double) value1.Z + 3.0*(double) value2.Z - 3.0*(double) value3.Z + (double) value4.Z)*
                      (double) num2));
            return vector3;
        }

        public static void CatmullRom(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4,
            float amount, out Vector3 result)
        {
            float num1 = amount*amount;
            float num2 = amount*num1;
            result.X =
                (float)
                    (0.5*
                     (2.0*(double) value2.X + (-(double) value1.X + (double) value3.X)*(double) amount +
                      (2.0*(double) value1.X - 5.0*(double) value2.X + 4.0*(double) value3.X - (double) value4.X)*
                      (double) num1 +
                      (-(double) value1.X + 3.0*(double) value2.X - 3.0*(double) value3.X + (double) value4.X)*
                      (double) num2));
            result.Y =
                (float)
                    (0.5*
                     (2.0*(double) value2.Y + (-(double) value1.Y + (double) value3.Y)*(double) amount +
                      (2.0*(double) value1.Y - 5.0*(double) value2.Y + 4.0*(double) value3.Y - (double) value4.Y)*
                      (double) num1 +
                      (-(double) value1.Y + 3.0*(double) value2.Y - 3.0*(double) value3.Y + (double) value4.Y)*
                      (double) num2));
            result.Z =
                (float)
                    (0.5*
                     (2.0*(double) value2.Z + (-(double) value1.Z + (double) value3.Z)*(double) amount +
                      (2.0*(double) value1.Z - 5.0*(double) value2.Z + 4.0*(double) value3.Z - (double) value4.Z)*
                      (double) num1 +
                      (-(double) value1.Z + 3.0*(double) value2.Z - 3.0*(double) value3.Z + (double) value4.Z)*
                      (double) num2));
        }

        public static Vector3 Hermite(Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, float amount)
        {
            float num1 = amount*amount;
            float num2 = amount*num1;
            float num3 = (float) (2.0*(double) num2 - 3.0*(double) num1 + 1.0);
            float num4 = (float) (-2.0*(double) num2 + 3.0*(double) num1);
            float num5 = num2 - 2f*num1 + amount;
            float num6 = num2 - num1;
            Vector3 vector3;
            vector3.X =
                (float)
                    ((double) value1.X*(double) num3 + (double) value2.X*(double) num4 +
                     (double) tangent1.X*(double) num5 + (double) tangent2.X*(double) num6);
            vector3.Y =
                (float)
                    ((double) value1.Y*(double) num3 + (double) value2.Y*(double) num4 +
                     (double) tangent1.Y*(double) num5 + (double) tangent2.Y*(double) num6);
            vector3.Z =
                (float)
                    ((double) value1.Z*(double) num3 + (double) value2.Z*(double) num4 +
                     (double) tangent1.Z*(double) num5 + (double) tangent2.Z*(double) num6);
            return vector3;
        }

        public static void Hermite(ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2,
            float amount, out Vector3 result)
        {
            float num1 = amount*amount;
            float num2 = amount*num1;
            float num3 = (float) (2.0*(double) num2 - 3.0*(double) num1 + 1.0);
            float num4 = (float) (-2.0*(double) num2 + 3.0*(double) num1);
            float num5 = num2 - 2f*num1 + amount;
            float num6 = num2 - num1;
            result.X =
                (float)
                    ((double) value1.X*(double) num3 + (double) value2.X*(double) num4 +
                     (double) tangent1.X*(double) num5 + (double) tangent2.X*(double) num6);
            result.Y =
                (float)
                    ((double) value1.Y*(double) num3 + (double) value2.Y*(double) num4 +
                     (double) tangent1.Y*(double) num5 + (double) tangent2.Y*(double) num6);
            result.Z =
                (float)
                    ((double) value1.Z*(double) num3 + (double) value2.Z*(double) num4 +
                     (double) tangent1.Z*(double) num5 + (double) tangent2.Z*(double) num6);
        }

        public static Vector3 Transform(Vector3 position, Matrix matrix)
        {
            float num1 =
                (float)
                    ((double) position.X*(double) matrix.M11 + (double) position.Y*(double) matrix.M21 +
                     (double) position.Z*(double) matrix.M31) + matrix.M41;
            float num2 =
                (float)
                    ((double) position.X*(double) matrix.M12 + (double) position.Y*(double) matrix.M22 +
                     (double) position.Z*(double) matrix.M32) + matrix.M42;
            float num3 =
                (float)
                    ((double) position.X*(double) matrix.M13 + (double) position.Y*(double) matrix.M23 +
                     (double) position.Z*(double) matrix.M33) + matrix.M43;
            float num4 =
                (float)
                    (1.0/
                     ((double) position.X*(double) matrix.M14 + (double) position.Y*(double) matrix.M24 +
                      (double) position.Z*(double) matrix.M34 + (double) matrix.M44));
            Vector3 vector3;
            vector3.X = num1*num4;
            vector3.Y = num2*num4;
            vector3.Z = num3*num4;
            return vector3;
        }

        public static Vector3D Transform(Vector3 position, MatrixD matrix)
        {
            double num1 = (double) position.X*matrix.M11 + (double) position.Y*matrix.M21 +
                          (double) position.Z*matrix.M31 + matrix.M41;
            double num2 = (double) position.X*matrix.M12 + (double) position.Y*matrix.M22 +
                          (double) position.Z*matrix.M32 + matrix.M42;
            double num3 = (double) position.X*matrix.M13 + (double) position.Y*matrix.M23 +
                          (double) position.Z*matrix.M33 + matrix.M43;
            double num4 = 1.0/
                          ((double) position.X*matrix.M14 + (double) position.Y*matrix.M24 +
                           (double) position.Z*matrix.M34 + matrix.M44);
            Vector3D vector3D;
            vector3D.X = num1*num4;
            vector3D.Y = num2*num4;
            vector3D.Z = num3*num4;
            return vector3D;
        }

        public static Vector3 Transform(Vector3 position, ref Matrix matrix)
        {
            Vector3.Transform(ref position, ref matrix, out position);
            return position;
        }

        public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector3 result)
        {
            float num1 =
                (float)
                    ((double) position.X*(double) matrix.M11 + (double) position.Y*(double) matrix.M21 +
                     (double) position.Z*(double) matrix.M31) + matrix.M41;
            float num2 =
                (float)
                    ((double) position.X*(double) matrix.M12 + (double) position.Y*(double) matrix.M22 +
                     (double) position.Z*(double) matrix.M32) + matrix.M42;
            float num3 =
                (float)
                    ((double) position.X*(double) matrix.M13 + (double) position.Y*(double) matrix.M23 +
                     (double) position.Z*(double) matrix.M33) + matrix.M43;
            float num4 =
                (float)
                    (1.0/
                     ((double) position.X*(double) matrix.M14 + (double) position.Y*(double) matrix.M24 +
                      (double) position.Z*(double) matrix.M34 + (double) matrix.M44));
            result.X = num1*num4;
            result.Y = num2*num4;
            result.Z = num3*num4;
        }

        public static void Transform(ref Vector3 position, ref MatrixI matrix, out Vector3 result)
        {
            result = position.X*Base6Directions.GetVector(matrix.Right) +
                     position.Y*Base6Directions.GetVector(matrix.Up) +
                     position.Z*Base6Directions.GetVector(matrix.Backward) + (Vector3) matrix.Translation;
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport("d3dx9_43.dll", EntryPoint = "D3DXVec3TransformCoord", CallingConvention = CallingConvention.StdCall)
        ]
        private static extern unsafe Vector3* D3DXVec3TransformCoord_([Out] Vector3* pOut, [In] Vector3* pV,
            [In] Matrix* pM);

        public static unsafe void Transform_Native(ref Vector3 position, ref Matrix matrix, out Vector3 result)
        {
            fixed (Vector3* pOut = &result)
            fixed (Vector3* pV = &position)
            fixed (Matrix* pM = &matrix)
                Vector3.D3DXVec3TransformCoord_(pOut, pV, pM);
        }

        public static Vector3 TransformNormal(Vector3 normal, Matrix matrix)
        {
            float num1 =
                (float)
                    ((double) normal.X*(double) matrix.M11 + (double) normal.Y*(double) matrix.M21 +
                     (double) normal.Z*(double) matrix.M31);
            float num2 =
                (float)
                    ((double) normal.X*(double) matrix.M12 + (double) normal.Y*(double) matrix.M22 +
                     (double) normal.Z*(double) matrix.M32);
            float num3 =
                (float)
                    ((double) normal.X*(double) matrix.M13 + (double) normal.Y*(double) matrix.M23 +
                     (double) normal.Z*(double) matrix.M33);
            Vector3 vector3;
            vector3.X = num1;
            vector3.Y = num2;
            vector3.Z = num3;
            return vector3;
        }

        public static Vector3 TransformNormal(Vector3 normal, MatrixD matrix)
        {
            float num1 =
                (float) ((double) normal.X*matrix.M11 + (double) normal.Y*matrix.M21 + (double) normal.Z*matrix.M31);
            float num2 =
                (float) ((double) normal.X*matrix.M12 + (double) normal.Y*matrix.M22 + (double) normal.Z*matrix.M32);
            float num3 =
                (float) ((double) normal.X*matrix.M13 + (double) normal.Y*matrix.M23 + (double) normal.Z*matrix.M33);
            Vector3 vector3;
            vector3.X = num1;
            vector3.Y = num2;
            vector3.Z = num3;
            return vector3;
        }

        public static Vector3 TransformNormal(Vector3D normal, Matrix matrix)
        {
            float num1 =
                (float) (normal.X*(double) matrix.M11 + normal.Y*(double) matrix.M21 + normal.Z*(double) matrix.M31);
            float num2 =
                (float) (normal.X*(double) matrix.M12 + normal.Y*(double) matrix.M22 + normal.Z*(double) matrix.M32);
            float num3 =
                (float) (normal.X*(double) matrix.M13 + normal.Y*(double) matrix.M23 + normal.Z*(double) matrix.M33);
            Vector3 vector3;
            vector3.X = num1;
            vector3.Y = num2;
            vector3.Z = num3;
            return vector3;
        }

        public static void TransformNormal(ref Vector3 normal, ref Matrix matrix, out Vector3 result)
        {
            float num1 =
                (float)
                    ((double) normal.X*(double) matrix.M11 + (double) normal.Y*(double) matrix.M21 +
                     (double) normal.Z*(double) matrix.M31);
            float num2 =
                (float)
                    ((double) normal.X*(double) matrix.M12 + (double) normal.Y*(double) matrix.M22 +
                     (double) normal.Z*(double) matrix.M32);
            float num3 =
                (float)
                    ((double) normal.X*(double) matrix.M13 + (double) normal.Y*(double) matrix.M23 +
                     (double) normal.Z*(double) matrix.M33);
            result.X = num1;
            result.Y = num2;
            result.Z = num3;
        }

        public static void TransformNormal(ref Vector3 normal, ref MatrixD matrix, out Vector3 result)
        {
            float num1 =
                (float) ((double) normal.X*matrix.M11 + (double) normal.Y*matrix.M21 + (double) normal.Z*matrix.M31);
            float num2 =
                (float) ((double) normal.X*matrix.M12 + (double) normal.Y*matrix.M22 + (double) normal.Z*matrix.M32);
            float num3 =
                (float) ((double) normal.X*matrix.M13 + (double) normal.Y*matrix.M23 + (double) normal.Z*matrix.M33);
            result.X = num1;
            result.Y = num2;
            result.Z = num3;
        }

        public static void TransformNormal(ref Vector3 normal, ref MatrixI matrix, out Vector3 result)
        {
            result = normal.X*Base6Directions.GetVector(matrix.Right) + normal.Y*Base6Directions.GetVector(matrix.Up) +
                     normal.Z*Base6Directions.GetVector(matrix.Backward);
        }

        public static Vector3 TransformNormal(Vector3 normal, MyBlockOrientation orientation)
        {
            Vector3 result;
            Vector3.TransformNormal(ref normal, orientation, out result);
            return result;
        }

        public static void TransformNormal(ref Vector3 normal, MyBlockOrientation orientation, out Vector3 result)
        {
            result = -normal.X*Base6Directions.GetVector(orientation.Left) +
                     normal.Y*Base6Directions.GetVector(orientation.Up) -
                     normal.Z*Base6Directions.GetVector(orientation.Forward);
        }

        public static Vector3 Transform(Vector3 value, Quaternion rotation)
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
            float num13 =
                (float)
                    ((double) value.X*(1.0 - (double) num10 - (double) num12) +
                     (double) value.Y*((double) num8 - (double) num6) + (double) value.Z*((double) num9 + (double) num5));
            float num14 =
                (float)
                    ((double) value.X*((double) num8 + (double) num6) +
                     (double) value.Y*(1.0 - (double) num7 - (double) num12) +
                     (double) value.Z*((double) num11 - (double) num4));
            float num15 =
                (float)
                    ((double) value.X*((double) num9 - (double) num5) +
                     (double) value.Y*((double) num11 + (double) num4) +
                     (double) value.Z*(1.0 - (double) num7 - (double) num10));
            Vector3 vector3;
            vector3.X = num13;
            vector3.Y = num14;
            vector3.Z = num15;
            return vector3;
        }

        public static void Transform(ref Vector3 value, ref Quaternion rotation, out Vector3 result)
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
            float num13 =
                (float)
                    ((double) value.X*(1.0 - (double) num10 - (double) num12) +
                     (double) value.Y*((double) num8 - (double) num6) + (double) value.Z*((double) num9 + (double) num5));
            float num14 =
                (float)
                    ((double) value.X*((double) num8 + (double) num6) +
                     (double) value.Y*(1.0 - (double) num7 - (double) num12) +
                     (double) value.Z*((double) num11 - (double) num4));
            float num15 =
                (float)
                    ((double) value.X*((double) num9 - (double) num5) +
                     (double) value.Y*((double) num11 + (double) num4) +
                     (double) value.Z*(1.0 - (double) num7 - (double) num10));
            result.X = num13;
            result.Y = num14;
            result.Z = num15;
        }

        public static void Transform(Vector3[] sourceArray, ref Matrix matrix, Vector3[] destinationArray)
        {
            for (int index = 0; index < sourceArray.Length; ++index)
            {
                float num1 = sourceArray[index].X;
                float num2 = sourceArray[index].Y;
                float num3 = sourceArray[index].Z;
                destinationArray[index].X =
                    (float)
                        ((double) num1*(double) matrix.M11 + (double) num2*(double) matrix.M21 +
                         (double) num3*(double) matrix.M31) + matrix.M41;
                destinationArray[index].Y =
                    (float)
                        ((double) num1*(double) matrix.M12 + (double) num2*(double) matrix.M22 +
                         (double) num3*(double) matrix.M32) + matrix.M42;
                destinationArray[index].Z =
                    (float)
                        ((double) num1*(double) matrix.M13 + (double) num2*(double) matrix.M23 +
                         (double) num3*(double) matrix.M33) + matrix.M43;
            }
        }

        public static void Transform(Vector3[] sourceArray, int sourceIndex, ref Matrix matrix,
            Vector3[] destinationArray, int destinationIndex, int length)
        {
            for (; length > 0; --length)
            {
                float num1 = sourceArray[sourceIndex].X;
                float num2 = sourceArray[sourceIndex].Y;
                float num3 = sourceArray[sourceIndex].Z;
                destinationArray[destinationIndex].X =
                    (float)
                        ((double) num1*(double) matrix.M11 + (double) num2*(double) matrix.M21 +
                         (double) num3*(double) matrix.M31) + matrix.M41;
                destinationArray[destinationIndex].Y =
                    (float)
                        ((double) num1*(double) matrix.M12 + (double) num2*(double) matrix.M22 +
                         (double) num3*(double) matrix.M32) + matrix.M42;
                destinationArray[destinationIndex].Z =
                    (float)
                        ((double) num1*(double) matrix.M13 + (double) num2*(double) matrix.M23 +
                         (double) num3*(double) matrix.M33) + matrix.M43;
                ++sourceIndex;
                ++destinationIndex;
            }
        }

        public static void TransformNormal(Vector3[] sourceArray, ref Matrix matrix, Vector3[] destinationArray)
        {
            for (int index = 0; index < sourceArray.Length; ++index)
            {
                float num1 = sourceArray[index].X;
                float num2 = sourceArray[index].Y;
                float num3 = sourceArray[index].Z;
                destinationArray[index].X =
                    (float)
                        ((double) num1*(double) matrix.M11 + (double) num2*(double) matrix.M21 +
                         (double) num3*(double) matrix.M31);
                destinationArray[index].Y =
                    (float)
                        ((double) num1*(double) matrix.M12 + (double) num2*(double) matrix.M22 +
                         (double) num3*(double) matrix.M32);
                destinationArray[index].Z =
                    (float)
                        ((double) num1*(double) matrix.M13 + (double) num2*(double) matrix.M23 +
                         (double) num3*(double) matrix.M33);
            }
        }

        public static void TransformNormal(Vector3[] sourceArray, int sourceIndex, ref Matrix matrix,
            Vector3[] destinationArray, int destinationIndex, int length)
        {
            for (; length > 0; --length)
            {
                float num1 = sourceArray[sourceIndex].X;
                float num2 = sourceArray[sourceIndex].Y;
                float num3 = sourceArray[sourceIndex].Z;
                destinationArray[destinationIndex].X =
                    (float)
                        ((double) num1*(double) matrix.M11 + (double) num2*(double) matrix.M21 +
                         (double) num3*(double) matrix.M31);
                destinationArray[destinationIndex].Y =
                    (float)
                        ((double) num1*(double) matrix.M12 + (double) num2*(double) matrix.M22 +
                         (double) num3*(double) matrix.M32);
                destinationArray[destinationIndex].Z =
                    (float)
                        ((double) num1*(double) matrix.M13 + (double) num2*(double) matrix.M23 +
                         (double) num3*(double) matrix.M33);
                ++sourceIndex;
                ++destinationIndex;
            }
        }

        public static void Transform(Vector3[] sourceArray, ref Quaternion rotation, Vector3[] destinationArray)
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
            for (int index = 0; index < sourceArray.Length; ++index)
            {
                float num22 = sourceArray[index].X;
                float num23 = sourceArray[index].Y;
                float num24 = sourceArray[index].Z;
                destinationArray[index].X =
                    (float)
                        ((double) num22*(double) num13 + (double) num23*(double) num14 + (double) num24*(double) num15);
                destinationArray[index].Y =
                    (float)
                        ((double) num22*(double) num16 + (double) num23*(double) num17 + (double) num24*(double) num18);
                destinationArray[index].Z =
                    (float)
                        ((double) num22*(double) num19 + (double) num23*(double) num20 + (double) num24*(double) num21);
            }
        }

        public static void Transform(Vector3[] sourceArray, int sourceIndex, ref Quaternion rotation,
            Vector3[] destinationArray, int destinationIndex, int length)
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
            for (; length > 0; --length)
            {
                float num22 = sourceArray[sourceIndex].X;
                float num23 = sourceArray[sourceIndex].Y;
                float num24 = sourceArray[sourceIndex].Z;
                destinationArray[destinationIndex].X =
                    (float)
                        ((double) num22*(double) num13 + (double) num23*(double) num14 + (double) num24*(double) num15);
                destinationArray[destinationIndex].Y =
                    (float)
                        ((double) num22*(double) num16 + (double) num23*(double) num17 + (double) num24*(double) num18);
                destinationArray[destinationIndex].Z =
                    (float)
                        ((double) num22*(double) num19 + (double) num23*(double) num20 + (double) num24*(double) num21);
                ++sourceIndex;
                ++destinationIndex;
            }
        }

        public static Vector3 Negate(Vector3 value)
        {
            Vector3 vector3;
            vector3.X = -value.X;
            vector3.Y = -value.Y;
            vector3.Z = -value.Z;
            return vector3;
        }

        public static void Negate(ref Vector3 value, out Vector3 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
        }

        public static Vector3 Add(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X + value2.X;
            vector3.Y = value1.Y + value2.Y;
            vector3.Z = value1.Z + value2.Z;
            return vector3;
        }

        public static void Add(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        public static Vector3 Subtract(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X - value2.X;
            vector3.Y = value1.Y - value2.Y;
            vector3.Z = value1.Z - value2.Z;
            return vector3;
        }

        public static void Subtract(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        public static Vector3 Multiply(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X*value2.X;
            vector3.Y = value1.Y*value2.Y;
            vector3.Z = value1.Z*value2.Z;
            return vector3;
        }

        public static void Multiply(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X*value2.X;
            result.Y = value1.Y*value2.Y;
            result.Z = value1.Z*value2.Z;
        }

        public static Vector3 Multiply(Vector3 value1, float scaleFactor)
        {
            Vector3 vector3;
            vector3.X = value1.X*scaleFactor;
            vector3.Y = value1.Y*scaleFactor;
            vector3.Z = value1.Z*scaleFactor;
            return vector3;
        }

        public static void Multiply(ref Vector3 value1, float scaleFactor, out Vector3 result)
        {
            result.X = value1.X*scaleFactor;
            result.Y = value1.Y*scaleFactor;
            result.Z = value1.Z*scaleFactor;
        }

        public static Vector3 Divide(Vector3 value1, Vector3 value2)
        {
            Vector3 vector3;
            vector3.X = value1.X/value2.X;
            vector3.Y = value1.Y/value2.Y;
            vector3.Z = value1.Z/value2.Z;
            return vector3;
        }

        public static void Divide(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
        {
            result.X = value1.X/value2.X;
            result.Y = value1.Y/value2.Y;
            result.Z = value1.Z/value2.Z;
        }

        public static Vector3 Divide(Vector3 value1, float value2)
        {
            float num = 1f/value2;
            Vector3 vector3;
            vector3.X = value1.X*num;
            vector3.Y = value1.Y*num;
            vector3.Z = value1.Z*num;
            return vector3;
        }

        public static void Divide(ref Vector3 value1, float value2, out Vector3 result)
        {
            float num = 1f/value2;
            result.X = value1.X*num;
            result.Y = value1.Y*num;
            result.Z = value1.Z*num;
        }

        public static Vector3 CalculatePerpendicularVector(Vector3 v)
        {
            Vector3 result;
            v.CalculatePerpendicularVector(out result);
            return result;
        }

        public void CalculatePerpendicularVector(out Vector3 result)
        {
            result = new Vector3((float) -((double) this.Y + (double) this.Z), this.X, this.X);
            Vector3.Normalize(ref result, out result);
        }

        public static void GetAzimuthAndElevation(Vector3 v, out float azimuth, out float elevation)
        {
            float result1;
            Vector3.Dot(ref v, ref Vector3.Up, out result1);
            v.Y = 0.0f;
            double num1 = (double) v.Normalize();
            float result2;
            Vector3.Dot(ref v, ref Vector3.Forward, out result2);
            float num2 = Vector3.Cross(v, Vector3.Forward).Length();
            elevation = (float) Math.Acos((double) result1);
            azimuth = (float) Math.Atan2(-(double) num2, (double) result2);
        }

        public static void CreateFromAzimuthAndElevation(float azimuth, float elevation, out Vector3 direction)
        {
            Matrix rotationY = Matrix.CreateRotationY(azimuth);
            Matrix rotationX = Matrix.CreateRotationX(elevation);
            direction = Vector3.Forward;
            Vector3.TransformNormal(ref direction, ref rotationX, out direction);
            Vector3.TransformNormal(ref direction, ref rotationY, out direction);
        }

        public float Sum()
        {
            return this.X + this.Y + this.Z;
        }

        public float Volume()
        {
            return this.X*this.Y*this.Z;
        }

        public long VolumeInt(float multiplier)
        {
            return (long) ((double) this.X*(double) multiplier)*(long) ((double) this.Y*(double) multiplier)*
                   (long) ((double) this.Z*(double) multiplier);
        }

        public bool IsInsideInclusive(ref Vector3 min, ref Vector3 max)
        {
            if ((double) min.X <= (double) this.X && (double) this.X <= (double) max.X &&
                ((double) min.Y <= (double) this.Y && (double) this.Y <= (double) max.Y) &&
                (double) min.Z <= (double) this.Z)
                return (double) this.Z <= (double) max.Z;
            else
                return false;
        }

        public static Vector3 SwapYZCoordinates(Vector3 v)
        {
            return new Vector3(v.X, v.Z, -v.Y);
        }

        public float GetDim(int i)
        {
            switch (i)
            {
                case 0:
                    return this.X;
                case 1:
                    return this.Y;
                case 2:
                    return this.Z;
                default:
                    return this.GetDim((i%3 + 3)%3);
            }
        }

        public void SetDim(int i, float value)
        {
            switch (i)
            {
                case 0:
                    this.X = value;
                    break;
                case 1:
                    this.Y = value;
                    break;
                case 2:
                    this.Z = value;
                    break;
                default:
                    this.SetDim((i%3 + 3)%3, value);
                    break;
            }
        }
    }
}