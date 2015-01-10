// Decompiled with JetBrains decompiler
// Type: VRageMath.MathHelper
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Runtime.InteropServices;
using VRage;

namespace VRageMath
{
  public static class MathHelper
  {
    public const float E = 2.718282f;
    public const float Log2E = 1.442695f;
    public const float Log10E = 0.4342945f;
    public const float Pi = 3.141593f;
    public const float TwoPi = 6.283185f;
    public const float PiOver2 = 1.570796f;
    public const float PiOver4 = 0.7853982f;
    public const float RadiansPerSecondToRPM = 9.549296f;
    public const float RPMToRadiansPerSecond = 0.1047198f;
    public const float RPMToRadiansPerMillisec = 0.0001047198f;

    public static float ToRadians(float degrees)
    {
      return (float) ((double) degrees / 360.0 * 6.28318500518799);
    }

    public static float ToDegrees(float radians)
    {
      return radians * 57.29578f;
    }

    public static double ToDegrees(double radians)
    {
      return radians * 57.29578;
    }

    public static float Distance(float value1, float value2)
    {
      return Math.Abs(value1 - value2);
    }

    public static float Min(float value1, float value2)
    {
      return Math.Min(value1, value2);
    }

    public static float Max(float value1, float value2)
    {
      return Math.Max(value1, value2);
    }

    public static double Min(double value1, double value2)
    {
      return Math.Min(value1, value2);
    }

    public static double Max(double value1, double value2)
    {
      return Math.Max(value1, value2);
    }

    public static float Clamp(float value, float min, float max)
    {
      value = (double) value > (double) max ? max : value;
      value = (double) value < (double) min ? min : value;
      return value;
    }

    public static double Clamp(double value, double min, double max)
    {
      value = value > max ? max : value;
      value = value < min ? min : value;
      return value;
    }
    public static MyFixedPoint Clamp(MyFixedPoint value, MyFixedPoint min, MyFixedPoint max)
    {
      value = value > max ? max : value;
      value = value < min ? min : value;
      return value;
    }

    public static int Clamp(int value, int min, int max)
    {
      value = value > max ? max : value;
      value = value < min ? min : value;
      return value;
    }

    public static float Lerp(float value1, float value2, float amount)
    {
      return value1 + (value2 - value1) * amount;
    }

    public static double Lerp(double value1, double value2, double amount)
    {
      return value1 + (value2 - value1) * amount;
    }

    public static float InterpLog(float value, float amount1, float amount2)
    {
      return (float) (Math.Pow((double) amount1, 1.0 - (double) value) * Math.Pow((double) amount2, (double) value));
    }

    public static float InterpLogInv(float value, float amount1, float amount2)
    {
      return (float) Math.Log((double) value / (double) amount1, (double) amount2 / (double) amount1);
    }

    public static float Barycentric(float value1, float value2, float value3, float amount1, float amount2)
    {
      return (float) ((double) value1 + (double) amount1 * ((double) value2 - (double) value1) + (double) amount2 * ((double) value3 - (double) value1));
    }

    public static float SmoothStep(float value1, float value2, float amount)
    {
      return MathHelper.Lerp(value1, value2, MathHelper.SCurve3(amount));
    }

    public static float CatmullRom(float value1, float value2, float value3, float value4, float amount)
    {
      float num1 = amount * amount;
      float num2 = amount * num1;
      return (float) (0.5 * (2.0 * (double) value2 + (-(double) value1 + (double) value3) * (double) amount + (2.0 * (double) value1 - 5.0 * (double) value2 + 4.0 * (double) value3 - (double) value4) * (double) num1 + (-(double) value1 + 3.0 * (double) value2 - 3.0 * (double) value3 + (double) value4) * (double) num2));
    }

    public static float Hermite(float value1, float tangent1, float value2, float tangent2, float amount)
    {
      float num1 = amount;
      float num2 = num1 * num1;
      float num3 = num1 * num2;
      float num4 = (float) (2.0 * (double) num3 - 3.0 * (double) num2 + 1.0);
      float num5 = (float) (-2.0 * (double) num3 + 3.0 * (double) num2);
      float num6 = num3 - 2f * num2 + num1;
      float num7 = num3 - num2;
      return (float) ((double) value1 * (double) num4 + (double) value2 * (double) num5 + (double) tangent1 * (double) num6 + (double) tangent2 * (double) num7);
    }

    public static float WrapAngle(float angle)
    {
      angle = (float) Math.IEEERemainder((double) angle, 6.28318548202515);
      if ((double) angle <= -3.14159274101257)
        angle += 6.283185f;
      else if ((double) angle > 3.14159274101257)
        angle -= 6.283185f;
      return angle;
    }

    public static int GetNearestBiggerPowerOfTwo(int v)
    {
      --v;
      v |= v >> 1;
      v |= v >> 2;
      v |= v >> 4;
      v |= v >> 8;
      v |= v >> 16;
      ++v;
      return v;
    }

    public static int GetNearestBiggerPowerOfTwo(float f)
    {
      int num = 1;
      while ((double) num < (double) f)
        num <<= 1;
      return num;
    }

    public static int GetNearestBiggerPowerOfTwo(double f)
    {
      int num = 1;
      while ((double) num < f)
        num <<= 1;
      return num;
    }

    public static float Max(float a, float b, float c)
    {
      float num = (double) a > (double) b ? a : b;
      if ((double) num <= (double) c)
        return c;
      else
        return num;
    }

    public static int Max(int a, int b, int c)
    {
      int num = a > b ? a : b;
      if (num <= c)
        return c;
      else
        return num;
    }

    public static float Min(float a, float b, float c)
    {
      float num = (double) a < (double) b ? a : b;
      if ((double) num >= (double) c)
        return c;
      else
        return num;
    }

    public static double Max(double a, double b, double c)
    {
      double num = a > b ? a : b;
      if (num <= c)
        return c;
      else
        return num;
    }

    public static double Min(double a, double b, double c)
    {
      double num = a < b ? a : b;
      if (num >= c)
        return c;
      else
        return num;
    }

    public static unsafe int ComputeHashFromBytes(byte[] bytes)
    {
      int length = bytes.Length;
      int num1 = length - length % 4;
      GCHandle gcHandle = GCHandle.Alloc((object) bytes, GCHandleType.Pinned);
      int num2 = 0;
      try
      {
        int* numPtr = (int*) gcHandle.AddrOfPinnedObject().ToPointer();
        int num3 = 0;
        while (num3 < num1)
        {
          num2 ^= *numPtr;
          num3 += 4;
          ++numPtr;
        }
        return num2;
      }
      finally
      {
        gcHandle.Free();
      }
    }

    public static float RoundOn2(float x)
    {
      return (float) (int) ((double) x * 100.0) / 100f;
    }

    public static bool IsPowerOfTwo(int x)
    {
      if (x > 0)
        return (x & x - 1) == 0;
      else
        return false;
    }

    public static float SCurve3(float t)
    {
      return (float) ((double) t * (double) t * (3.0 - 2.0 * (double) t));
    }

    public static double SCurve3(double t)
    {
      return t * t * (3.0 - 2.0 * t);
    }

    public static float SCurve5(float t)
    {
      return (float) ((double) t * (double) t * (double) t * ((double) t * ((double) t * 6.0 - 15.0) + 10.0));
    }

    public static double SCurve5(double t)
    {
      return t * t * t * (t * (t * 6.0 - 15.0) + 10.0);
    }

    public static float Saturate(float n)
    {
      if ((double) n < 0.0)
        return 0.0f;
      if ((double) n <= 1.0)
        return n;
      else
        return 1f;
    }

    public static double Saturate(double n)
    {
      if (n < 0.0)
        return 0.0;
      if (n <= 1.0)
        return n;
      else
        return 1.0;
    }

    public static int Floor(float n)
    {
      if ((double) n >= 0.0)
        return (int) n;
      else
        return (int) n - 1;
    }

    public static int Floor(double n)
    {
      if (n >= 0.0)
        return (int) n;
      else
        return (int) n - 1;
    }

    public static int Log2(int n)
    {
      int num = 0;
      while ((n >>= 1) > 0)
        ++num;
      return num;
    }

    public static double CubicInterp(double p0, double p1, double p2, double p3, double t)
    {
      double num1 = p3 - p2 - (p0 - p1);
      double num2 = p0 - p1 - num1;
      double num3 = t * t;
      return num1 * num3 * t + num2 * num3 + (p2 - p0) * t + p1;
    }
  }
}
