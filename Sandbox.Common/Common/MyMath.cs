// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyMath
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;
using VRage.Common.Utils;
using VRageMath;

namespace Sandbox.Common
{
    public static class MyMath
    {
        private static Vector3[] m_corners = new Vector3[8];
        private static readonly float OneOverRoot3 = (float) Math.Pow(3.0, -0.5);
        private static float[] m_precomputedValues = new float[628];
        public static Vector3 Vector3One = Vector3.One;
        private const int ANGLE_GRANULARITY = 628;

        static MyMath()
        {
            for (int index = 0; index < 628; ++index)
                MyMath.m_precomputedValues[index] = (float) Math.Sin((double) index/100.0);
        }

        public static float FastSin(float angle)
        {
            int index = (int) ((double) angle*100.0)%628;
            if (index < 0)
                index += 628;
            return MyMath.m_precomputedValues[index];
        }

        public static float FastCos(float angle)
        {
            return MyMath.FastSin(angle + 1.570796f);
        }

        public static float NormalizeAngle(float angle, float center = 0.0f)
        {
            return angle -
                   6.283185f*(float) Math.Floor(((double) angle + 3.14159297943115 - (double) center)/6.28318500518799);
        }

        public static float ArcTanAngle(float x, float y)
        {
            if ((double) x == 0.0)
            {
                return (double) y == 1.0 ? 1.570796f : -1.570796f;
            }
            else
            {
                if ((double) x > 0.0)
                    return (float) Math.Atan((double) y/(double) x);
                if ((double) x >= 0.0)
                    return 0.0f;
                if ((double) y > 0.0)
                    return (float) Math.Atan((double) y/(double) x) + 3.141593f;
                else
                    return (float) Math.Atan((double) y/(double) x) - 3.141593f;
            }
        }

        public static Vector3 Abs(ref Vector3 vector)
        {
            return new Vector3(Math.Abs(vector.X), Math.Abs(vector.Y), Math.Abs(vector.Z));
        }

        public static Vector3 MaxComponents(ref Vector3 a, ref Vector3 b)
        {
            return new Vector3(MathHelper.Max(a.X, b.X), MathHelper.Max(a.Y, b.Y), MathHelper.Max(a.Z, b.Z));
        }

        public static Vector3 AngleTo(Vector3 From, Vector3 Location)
        {
            Vector3 vector3_1 = Vector3.Zero;
            Vector3 vector3_2 = Vector3.Normalize(Location - From);
            vector3_1.X = (float) Math.Asin((double) vector3_2.Y);
            vector3_1.Y = MyMath.ArcTanAngle(-vector3_2.Z, -vector3_2.X);
            return vector3_1;
        }

        public static float AngleBetween(Vector3 a, Vector3 b)
        {
            float num = Vector3.Dot(a, b)/(a.Length()*b.Length());
            if (MyVRageUtils.IsZero(1f - num, 1E-05f))
                return 0.0f;
            else
                return (float) Math.Acos((double) num);
        }

        public static int Mod(int x, int m)
        {
            return (x%m + m)%m;
        }

        public static long Mod(long x, int m)
        {
            return (x%(long) m + (long) m)%(long) m;
        }

        public static Vector3 QuaternionToEuler(Quaternion Rotation)
        {
            Vector3 Location = Vector3.Transform(Vector3.Forward, Rotation);
            Vector3 position = Vector3.Transform(Vector3.Up, Rotation);
            Vector3 vector3_1 = MyMath.AngleTo(new Vector3(), Location);
            if ((double) vector3_1.X == 1.57079601287842)
            {
                vector3_1.Y = MyMath.ArcTanAngle(position.Z, position.X);
                vector3_1.Z = 0.0f;
            }
            else if ((double) vector3_1.X == -1.57079601287842)
            {
                vector3_1.Y = MyMath.ArcTanAngle(-position.Y, -position.X);
                vector3_1.Z = 0.0f;
            }
            else
            {
                Vector3 vector3_2 = Vector3.Transform(
                    Vector3.Transform(position, Matrix.CreateRotationY(-vector3_1.Y)),
                    Matrix.CreateRotationX(-vector3_1.X));
                vector3_1.Z = MyMath.ArcTanAngle(vector3_2.Y, -vector3_2.X);
            }
            return vector3_1;
        }

        public static Vector3 ForwardVectorProjection(Vector3 forwardVector, Vector3 projectedVector)
        {
            if ((double) Vector3.Dot(projectedVector, forwardVector) > 0.0)
                return Vector3Extensions.Project(forwardVector, projectedVector + forwardVector);
            else
                return Vector3.Zero;
        }

        public static BoundingBox CreateFromInsideRadius(float radius)
        {
            float num = MyMath.OneOverRoot3*radius;
            return new BoundingBox(-new Vector3(num), new Vector3(num));
        }

        public static Vector3 VectorFromColor(byte red, byte green, byte blue)
        {
            return new Vector3((float) red/(float) byte.MaxValue, (float) green/(float) byte.MaxValue,
                (float) blue/(float) byte.MaxValue);
        }

        public static Vector4 VectorFromColor(byte red, byte green, byte blue, byte alpha)
        {
            return new Vector4((float) red/(float) byte.MaxValue, (float) green/(float) byte.MaxValue,
                (float) blue/(float) byte.MaxValue, (float) alpha/(float) byte.MaxValue);
        }

        public static float DistanceSquaredFromLineSegment(Vector3 v, Vector3 w, Vector3 p)
        {
            Vector3 vector2 = w - v;
            float num1 = vector2.LengthSquared();
            if ((double) num1 == 0.0)
                return Vector3.DistanceSquared(p, v);
            float num2 = Vector3.Dot(p - v, vector2);
            if ((double) num2 <= 0.0)
                return Vector3.DistanceSquared(p, v);
            if ((double) num2 >= (double) num1)
                return Vector3.DistanceSquared(p, w);
            else
                return Vector3.DistanceSquared(p, v + num2/num1*vector2);
        }
    }
}