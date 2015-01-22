// Decompiled with JetBrains decompiler
// Type: VRageMath.QuaternionD
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using ProtoBuf;
using System;
using System.Globalization;

namespace VRageMath
{
  [ProtoContract]
  [Serializable]
  public struct QuaternionD
  {
    public static QuaternionD Identity = new QuaternionD(0.0, 0.0, 0.0, 1.0);
    [ProtoMember(1)]
    public double X;
    [ProtoMember(2)]
    public double Y;
    [ProtoMember(3)]
    public double Z;
    [ProtoMember(4)]
    public double W;

    public QuaternionD(double x, double y, double z, double w)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
      this.W = w;
    }

    public QuaternionD(Vector3D vectorPart, double scalarPart)
    {
      this.X = vectorPart.X;
      this.Y = vectorPart.Y;
      this.Z = vectorPart.Z;
      this.W = scalarPart;
    }

    public static QuaternionD operator -(QuaternionD quaternion)
    {
      QuaternionD quaternionD;
      quaternionD.X = -quaternion.X;
      quaternionD.Y = -quaternion.Y;
      quaternionD.Z = -quaternion.Z;
      quaternionD.W = -quaternion.W;
      return quaternionD;
    }

    public static bool operator ==(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      if (quaternion1.X == quaternion2.X && quaternion1.Y == quaternion2.Y && quaternion1.Z == quaternion2.Z)
        return quaternion1.W == quaternion2.W;
      else
        return false;
    }

    public static bool operator !=(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      if (quaternion1.X == quaternion2.X && quaternion1.Y == quaternion2.Y && quaternion1.Z == quaternion2.Z)
        return quaternion1.W != quaternion2.W;
      else
        return true;
    }

    public static QuaternionD operator +(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      QuaternionD quaternionD;
      quaternionD.X = quaternion1.X + quaternion2.X;
      quaternionD.Y = quaternion1.Y + quaternion2.Y;
      quaternionD.Z = quaternion1.Z + quaternion2.Z;
      quaternionD.W = quaternion1.W + quaternion2.W;
      return quaternionD;
    }

    public static QuaternionD operator -(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      QuaternionD quaternionD;
      quaternionD.X = quaternion1.X - quaternion2.X;
      quaternionD.Y = quaternion1.Y - quaternion2.Y;
      quaternionD.Z = quaternion1.Z - quaternion2.Z;
      quaternionD.W = quaternion1.W - quaternion2.W;
      return quaternionD;
    }

    public static QuaternionD operator *(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      double num1 = quaternion1.X;
      double num2 = quaternion1.Y;
      double num3 = quaternion1.Z;
      double num4 = quaternion1.W;
      double num5 = quaternion2.X;
      double num6 = quaternion2.Y;
      double num7 = quaternion2.Z;
      double num8 = quaternion2.W;
      double num9 = num2 * num7 - num3 * num6;
      double num10 = num3 * num5 - num1 * num7;
      double num11 = num1 * num6 - num2 * num5;
      double num12 = num1 * num5 + num2 * num6 + num3 * num7;
      QuaternionD quaternionD;
      quaternionD.X = num1 * num8 + num5 * num4 + num9;
      quaternionD.Y = num2 * num8 + num6 * num4 + num10;
      quaternionD.Z = num3 * num8 + num7 * num4 + num11;
      quaternionD.W = num4 * num8 - num12;
      return quaternionD;
    }

    public static QuaternionD operator *(QuaternionD quaternion1, double scaleFactor)
    {
      QuaternionD quaternionD;
      quaternionD.X = quaternion1.X * scaleFactor;
      quaternionD.Y = quaternion1.Y * scaleFactor;
      quaternionD.Z = quaternion1.Z * scaleFactor;
      quaternionD.W = quaternion1.W * scaleFactor;
      return quaternionD;
    }

    public static QuaternionD operator /(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      double num1 = quaternion1.X;
      double num2 = quaternion1.Y;
      double num3 = quaternion1.Z;
      double num4 = quaternion1.W;
      double num5 = 1.0 / (quaternion2.X * quaternion2.X + quaternion2.Y * quaternion2.Y + quaternion2.Z * quaternion2.Z + quaternion2.W * quaternion2.W);
      double num6 = -quaternion2.X * num5;
      double num7 = -quaternion2.Y * num5;
      double num8 = -quaternion2.Z * num5;
      double num9 = quaternion2.W * num5;
      double num10 = num2 * num8 - num3 * num7;
      double num11 = num3 * num6 - num1 * num8;
      double num12 = num1 * num7 - num2 * num6;
      double num13 = num1 * num6 + num2 * num7 + num3 * num8;
      QuaternionD quaternionD;
      quaternionD.X = num1 * num9 + num6 * num4 + num10;
      quaternionD.Y = num2 * num9 + num7 * num4 + num11;
      quaternionD.Z = num3 * num9 + num8 * num4 + num12;
      quaternionD.W = num4 * num9 - num13;
      return quaternionD;
    }

    public override string ToString()
    {
      CultureInfo currentCulture = CultureInfo.CurrentCulture;
      return string.Format((IFormatProvider) currentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", (object) this.X.ToString((IFormatProvider) currentCulture), (object) this.Y.ToString((IFormatProvider) currentCulture), (object) this.Z.ToString((IFormatProvider) currentCulture), (object) this.W.ToString((IFormatProvider) currentCulture));
    }

    public bool Equals(QuaternionD other)
    {
      if (this.X == other.X && this.Y == other.Y && this.Z == other.Z)
        return this.W == other.W;
      else
        return false;
    }

    public override bool Equals(object obj)
    {
      bool flag = false;
      if (obj is QuaternionD)
        flag = this.Equals((QuaternionD) obj);
      return flag;
    }

    public override int GetHashCode()
    {
      return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
    }

    public double LengthSquared()
    {
      return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
    }

    public double Length()
    {
      return Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W);
    }

    public void Normalize()
    {
      double num = 1.0 / Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W);
      this.X *= num;
      this.Y *= num;
      this.Z *= num;
      this.W *= num;
    }

    public void GetAxisAngle(out Vector3D axis, out double angle)
    {
      axis.X = this.X;
      axis.Y = this.Y;
      axis.Z = this.Z;
      double y = axis.Length();
      double x = this.W;
      if (y != 0.0)
      {
        axis.X /= y;
        axis.Y /= y;
        axis.Z /= y;
      }
      angle = Math.Atan2(y, x) * 2.0;
    }

    public static QuaternionD Normalize(QuaternionD quaternion)
    {
      double num = 1.0 / Math.Sqrt(quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z + quaternion.W * quaternion.W);
      QuaternionD quaternionD;
      quaternionD.X = quaternion.X * num;
      quaternionD.Y = quaternion.Y * num;
      quaternionD.Z = quaternion.Z * num;
      quaternionD.W = quaternion.W * num;
      return quaternionD;
    }

    public static void Normalize(ref QuaternionD quaternion, out QuaternionD result)
    {
      double num = 1.0 / Math.Sqrt(quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z + quaternion.W * quaternion.W);
      result.X = quaternion.X * num;
      result.Y = quaternion.Y * num;
      result.Z = quaternion.Z * num;
      result.W = quaternion.W * num;
    }

    public void Conjugate()
    {
      this.X = -this.X;
      this.Y = -this.Y;
      this.Z = -this.Z;
    }

    public static QuaternionD Conjugate(QuaternionD value)
    {
      QuaternionD quaternionD;
      quaternionD.X = -value.X;
      quaternionD.Y = -value.Y;
      quaternionD.Z = -value.Z;
      quaternionD.W = value.W;
      return quaternionD;
    }

    public static void Conjugate(ref QuaternionD value, out QuaternionD result)
    {
      result.X = -value.X;
      result.Y = -value.Y;
      result.Z = -value.Z;
      result.W = value.W;
    }

    public static QuaternionD Inverse(QuaternionD quaternion)
    {
      double num = 1.0 / (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z + quaternion.W * quaternion.W);
      QuaternionD quaternionD;
      quaternionD.X = -quaternion.X * num;
      quaternionD.Y = -quaternion.Y * num;
      quaternionD.Z = -quaternion.Z * num;
      quaternionD.W = quaternion.W * num;
      return quaternionD;
    }

    public static void Inverse(ref QuaternionD quaternion, out QuaternionD result)
    {
      double num = 1.0 / (quaternion.X * quaternion.X + quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z + quaternion.W * quaternion.W);
      result.X = -quaternion.X * num;
      result.Y = -quaternion.Y * num;
      result.Z = -quaternion.Z * num;
      result.W = quaternion.W * num;
    }

    public static QuaternionD CreateFromAxisAngle(Vector3D axis, double angle)
    {
      double num1 = angle * 0.5;
      double num2 = Math.Sin(num1);
      double num3 = Math.Cos(num1);
      QuaternionD quaternionD;
      quaternionD.X = axis.X * num2;
      quaternionD.Y = axis.Y * num2;
      quaternionD.Z = axis.Z * num2;
      quaternionD.W = num3;
      return quaternionD;
    }

    public static void CreateFromAxisAngle(ref Vector3D axis, double angle, out QuaternionD result)
    {
      double num1 = angle * 0.5;
      double num2 = Math.Sin(num1);
      double num3 = Math.Cos(num1);
      result.X = axis.X * num2;
      result.Y = axis.Y * num2;
      result.Z = axis.Z * num2;
      result.W = num3;
    }

    public static QuaternionD CreateFromYawPitchRoll(double yaw, double pitch, double roll)
    {
      double num1 = roll * 0.5;
      double num2 = Math.Sin(num1);
      double num3 = Math.Cos(num1);
      double num4 = pitch * 0.5;
      double num5 = Math.Sin(num4);
      double num6 = Math.Cos(num4);
      double num7 = yaw * 0.5;
      double num8 = Math.Sin(num7);
      double num9 = Math.Cos(num7);
      QuaternionD quaternionD;
      quaternionD.X = num9 * num5 * num3 + num8 * num6 * num2;
      quaternionD.Y = num8 * num6 * num3 - num9 * num5 * num2;
      quaternionD.Z = num9 * num6 * num2 - num8 * num5 * num3;
      quaternionD.W = num9 * num6 * num3 + num8 * num5 * num2;
      return quaternionD;
    }

    public static void CreateFromYawPitchRoll(double yaw, double pitch, double roll, out QuaternionD result)
    {
      double num1 = roll * 0.5;
      double num2 = Math.Sin(num1);
      double num3 = Math.Cos(num1);
      double num4 = pitch * 0.5;
      double num5 = Math.Sin(num4);
      double num6 = Math.Cos(num4);
      double num7 = yaw * 0.5;
      double num8 = Math.Sin(num7);
      double num9 = Math.Cos(num7);
      result.X = num9 * num5 * num3 + num8 * num6 * num2;
      result.Y = num8 * num6 * num3 - num9 * num5 * num2;
      result.Z = num9 * num6 * num2 - num8 * num5 * num3;
      result.W = num9 * num6 * num3 + num8 * num5 * num2;
    }

    public static QuaternionD CreateFromForwardUp(Vector3D forward, Vector3D up)
    {
      Vector3D vector3D1 = -forward;
      Vector3D vector2 = Vector3D.Cross(up, vector3D1);
      Vector3D vector3D2 = Vector3D.Cross(vector3D1, vector2);
      double num1 = vector2.X;
      double num2 = vector2.Y;
      double num3 = vector2.Z;
      double num4 = vector3D2.X;
      double num5 = vector3D2.Y;
      double num6 = vector3D2.Z;
      double num7 = vector3D1.X;
      double num8 = vector3D1.Y;
      double num9 = vector3D1.Z;
      double num10 = num1 + num5 + num9;
      QuaternionD quaternionD = new QuaternionD();
      if (num10 > 0.0)
      {
        double num11 = Math.Sqrt(num10 + 1.0);
        quaternionD.W = num11 * 0.5;
        double num12 = 0.5 / num11;
        quaternionD.X = (num6 - num8) * num12;
        quaternionD.Y = (num7 - num3) * num12;
        quaternionD.Z = (num2 - num4) * num12;
        return quaternionD;
      }
      else if (num1 >= num5 && num1 >= num9)
      {
        double num11 = Math.Sqrt(1.0 + num1 - num5 - num9);
        double num12 = 0.5 / num11;
        quaternionD.X = 0.5 * num11;
        quaternionD.Y = (num2 + num4) * num12;
        quaternionD.Z = (num3 + num7) * num12;
        quaternionD.W = (num6 - num8) * num12;
        return quaternionD;
      }
      else if (num5 > num9)
      {
        double num11 = Math.Sqrt(1.0 + num5 - num1 - num9);
        double num12 = 0.5 / num11;
        quaternionD.X = (num4 + num2) * num12;
        quaternionD.Y = 0.5 * num11;
        quaternionD.Z = (num8 + num6) * num12;
        quaternionD.W = (num7 - num3) * num12;
        return quaternionD;
      }
      else
      {
        double num11 = Math.Sqrt(1.0 + num9 - num1 - num5);
        double num12 = 0.5 / num11;
        quaternionD.X = (num7 + num3) * num12;
        quaternionD.Y = (num8 + num6) * num12;
        quaternionD.Z = 0.5 * num11;
        quaternionD.W = (num2 - num4) * num12;
        return quaternionD;
      }
    }

    public static QuaternionD CreateFromRotationMatrix(MatrixD matrix)
    {
      double num1 = matrix.M11 + matrix.M22 + matrix.M33;
      QuaternionD quaternionD = new QuaternionD();
      if (num1 > 0.0)
      {
        double num2 = Math.Sqrt(num1 + 1.0);
        quaternionD.W = num2 * 0.5;
        double num3 = 0.5 / num2;
        quaternionD.X = (matrix.M23 - matrix.M32) * num3;
        quaternionD.Y = (matrix.M31 - matrix.M13) * num3;
        quaternionD.Z = (matrix.M12 - matrix.M21) * num3;
      }
      else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
      {
        double num2 = Math.Sqrt(1.0 + matrix.M11 - matrix.M22 - matrix.M33);
        double num3 = 0.5 / num2;
        quaternionD.X = 0.5 * num2;
        quaternionD.Y = (matrix.M12 + matrix.M21) * num3;
        quaternionD.Z = (matrix.M13 + matrix.M31) * num3;
        quaternionD.W = (matrix.M23 - matrix.M32) * num3;
      }
      else if (matrix.M22 > matrix.M33)
      {
        double num2 = Math.Sqrt(1.0 + matrix.M22 - matrix.M11 - matrix.M33);
        double num3 = 0.5 / num2;
        quaternionD.X = (matrix.M21 + matrix.M12) * num3;
        quaternionD.Y = 0.5 * num2;
        quaternionD.Z = (matrix.M32 + matrix.M23) * num3;
        quaternionD.W = (matrix.M31 - matrix.M13) * num3;
      }
      else
      {
        double num2 = Math.Sqrt(1.0 + matrix.M33 - matrix.M11 - matrix.M22);
        double num3 = 0.5 / num2;
        quaternionD.X = (matrix.M31 + matrix.M13) * num3;
        quaternionD.Y = (matrix.M32 + matrix.M23) * num3;
        quaternionD.Z = 0.5 * num2;
        quaternionD.W = (matrix.M12 - matrix.M21) * num3;
      }
      return quaternionD;
    }

    public static void CreateFromRotationMatrix(ref MatrixD matrix, out QuaternionD result)
    {
      double num1 = matrix.M11 + matrix.M22 + matrix.M33;
      if (num1 > 0.0)
      {
        double num2 = Math.Sqrt(num1 + 1.0);
        result.W = num2 * 0.5;
        double num3 = 0.5 / num2;
        result.X = (matrix.M23 - matrix.M32) * num3;
        result.Y = (matrix.M31 - matrix.M13) * num3;
        result.Z = (matrix.M12 - matrix.M21) * num3;
      }
      else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
      {
        double num2 = Math.Sqrt(1.0 + matrix.M11 - matrix.M22 - matrix.M33);
        double num3 = 0.5 / num2;
        result.X = 0.5 * num2;
        result.Y = (matrix.M12 + matrix.M21) * num3;
        result.Z = (matrix.M13 + matrix.M31) * num3;
        result.W = (matrix.M23 - matrix.M32) * num3;
      }
      else if (matrix.M22 > matrix.M33)
      {
        double num2 = Math.Sqrt(1.0 + matrix.M22 - matrix.M11 - matrix.M33);
        double num3 = 0.5 / num2;
        result.X = (matrix.M21 + matrix.M12) * num3;
        result.Y = 0.5 * num2;
        result.Z = (matrix.M32 + matrix.M23) * num3;
        result.W = (matrix.M31 - matrix.M13) * num3;
      }
      else
      {
        double num2 = Math.Sqrt(1.0 + matrix.M33 - matrix.M11 - matrix.M22);
        double num3 = 0.5 / num2;
        result.X = (matrix.M31 + matrix.M13) * num3;
        result.Y = (matrix.M32 + matrix.M23) * num3;
        result.Z = 0.5 * num2;
        result.W = (matrix.M12 - matrix.M21) * num3;
      }
    }

    public static double Dot(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      return quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
    }

    public static void Dot(ref QuaternionD quaternion1, ref QuaternionD quaternion2, out double result)
    {
      result = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
    }

    public static QuaternionD Slerp(QuaternionD quaternion1, QuaternionD quaternion2, double amount)
    {
      double num1 = amount;
      double d = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
      bool flag = false;
      if (d < 0.0)
      {
        flag = true;
        d = -d;
      }
      double num2;
      double num3;
      if (d > 0.999998986721039)
      {
        num2 = 1.0 - num1;
        num3 = flag ? -num1 : num1;
      }
      else
      {
        double a = Math.Acos(d);
        double num4 = 1.0 / Math.Sin(a);
        num2 = Math.Sin((1.0 - num1) * a) * num4;
        num3 = flag ? -Math.Sin(num1 * a) * num4 : Math.Sin(num1 * a) * num4;
      }
      QuaternionD quaternionD;
      quaternionD.X = num2 * quaternion1.X + num3 * quaternion2.X;
      quaternionD.Y = num2 * quaternion1.Y + num3 * quaternion2.Y;
      quaternionD.Z = num2 * quaternion1.Z + num3 * quaternion2.Z;
      quaternionD.W = num2 * quaternion1.W + num3 * quaternion2.W;
      return quaternionD;
    }

    public static void Slerp(ref QuaternionD quaternion1, ref QuaternionD quaternion2, double amount, out QuaternionD result)
    {
      double num1 = amount;
      double d = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
      bool flag = false;
      if (d < 0.0)
      {
        flag = true;
        d = -d;
      }
      double num2;
      double num3;
      if (d > 0.999998986721039)
      {
        num2 = 1.0 - num1;
        num3 = flag ? -num1 : num1;
      }
      else
      {
        double a = Math.Acos(d);
        double num4 = 1.0 / Math.Sin(a);
        num2 = Math.Sin((1.0 - num1) * a) * num4;
        num3 = flag ? -Math.Sin(num1 * a) * num4 : Math.Sin(num1 * a) * num4;
      }
      result.X = num2 * quaternion1.X + num3 * quaternion2.X;
      result.Y = num2 * quaternion1.Y + num3 * quaternion2.Y;
      result.Z = num2 * quaternion1.Z + num3 * quaternion2.Z;
      result.W = num2 * quaternion1.W + num3 * quaternion2.W;
    }

    public static QuaternionD Lerp(QuaternionD quaternion1, QuaternionD quaternion2, double amount)
    {
      double num1 = amount;
      double num2 = 1.0 - num1;
      QuaternionD quaternionD = new QuaternionD();
      if (quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W >= 0.0)
      {
        quaternionD.X = num2 * quaternion1.X + num1 * quaternion2.X;
        quaternionD.Y = num2 * quaternion1.Y + num1 * quaternion2.Y;
        quaternionD.Z = num2 * quaternion1.Z + num1 * quaternion2.Z;
        quaternionD.W = num2 * quaternion1.W + num1 * quaternion2.W;
      }
      else
      {
        quaternionD.X = num2 * quaternion1.X - num1 * quaternion2.X;
        quaternionD.Y = num2 * quaternion1.Y - num1 * quaternion2.Y;
        quaternionD.Z = num2 * quaternion1.Z - num1 * quaternion2.Z;
        quaternionD.W = num2 * quaternion1.W - num1 * quaternion2.W;
      }
      double num3 = 1.0 / Math.Sqrt(quaternionD.X * quaternionD.X + quaternionD.Y * quaternionD.Y + quaternionD.Z * quaternionD.Z + quaternionD.W * quaternionD.W);
      quaternionD.X *= num3;
      quaternionD.Y *= num3;
      quaternionD.Z *= num3;
      quaternionD.W *= num3;
      return quaternionD;
    }

    public static void Lerp(ref QuaternionD quaternion1, ref QuaternionD quaternion2, double amount, out QuaternionD result)
    {
      double num1 = amount;
      double num2 = 1.0 - num1;
      if (quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W >= 0.0)
      {
        result.X = num2 * quaternion1.X + num1 * quaternion2.X;
        result.Y = num2 * quaternion1.Y + num1 * quaternion2.Y;
        result.Z = num2 * quaternion1.Z + num1 * quaternion2.Z;
        result.W = num2 * quaternion1.W + num1 * quaternion2.W;
      }
      else
      {
        result.X = num2 * quaternion1.X - num1 * quaternion2.X;
        result.Y = num2 * quaternion1.Y - num1 * quaternion2.Y;
        result.Z = num2 * quaternion1.Z - num1 * quaternion2.Z;
        result.W = num2 * quaternion1.W - num1 * quaternion2.W;
      }
      double num3 = 1.0 / Math.Sqrt(result.X * result.X + result.Y * result.Y + result.Z * result.Z + result.W * result.W);
      result.X *= num3;
      result.Y *= num3;
      result.Z *= num3;
      result.W *= num3;
    }

    public static QuaternionD Concatenate(QuaternionD value1, QuaternionD value2)
    {
      double num1 = value2.X;
      double num2 = value2.Y;
      double num3 = value2.Z;
      double num4 = value2.W;
      double num5 = value1.X;
      double num6 = value1.Y;
      double num7 = value1.Z;
      double num8 = value1.W;
      double num9 = num2 * num7 - num3 * num6;
      double num10 = num3 * num5 - num1 * num7;
      double num11 = num1 * num6 - num2 * num5;
      double num12 = num1 * num5 + num2 * num6 + num3 * num7;
      QuaternionD quaternionD;
      quaternionD.X = num1 * num8 + num5 * num4 + num9;
      quaternionD.Y = num2 * num8 + num6 * num4 + num10;
      quaternionD.Z = num3 * num8 + num7 * num4 + num11;
      quaternionD.W = num4 * num8 - num12;
      return quaternionD;
    }

    public static void Concatenate(ref QuaternionD value1, ref QuaternionD value2, out QuaternionD result)
    {
      double num1 = value2.X;
      double num2 = value2.Y;
      double num3 = value2.Z;
      double num4 = value2.W;
      double num5 = value1.X;
      double num6 = value1.Y;
      double num7 = value1.Z;
      double num8 = value1.W;
      double num9 = num2 * num7 - num3 * num6;
      double num10 = num3 * num5 - num1 * num7;
      double num11 = num1 * num6 - num2 * num5;
      double num12 = num1 * num5 + num2 * num6 + num3 * num7;
      result.X = num1 * num8 + num5 * num4 + num9;
      result.Y = num2 * num8 + num6 * num4 + num10;
      result.Z = num3 * num8 + num7 * num4 + num11;
      result.W = num4 * num8 - num12;
    }

    public static QuaternionD Negate(QuaternionD quaternion)
    {
      QuaternionD quaternionD;
      quaternionD.X = -quaternion.X;
      quaternionD.Y = -quaternion.Y;
      quaternionD.Z = -quaternion.Z;
      quaternionD.W = -quaternion.W;
      return quaternionD;
    }

    public static void Negate(ref QuaternionD quaternion, out QuaternionD result)
    {
      result.X = -quaternion.X;
      result.Y = -quaternion.Y;
      result.Z = -quaternion.Z;
      result.W = -quaternion.W;
    }

    public static QuaternionD Add(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      QuaternionD quaternionD;
      quaternionD.X = quaternion1.X + quaternion2.X;
      quaternionD.Y = quaternion1.Y + quaternion2.Y;
      quaternionD.Z = quaternion1.Z + quaternion2.Z;
      quaternionD.W = quaternion1.W + quaternion2.W;
      return quaternionD;
    }

    public static void Add(ref QuaternionD quaternion1, ref QuaternionD quaternion2, out QuaternionD result)
    {
      result.X = quaternion1.X + quaternion2.X;
      result.Y = quaternion1.Y + quaternion2.Y;
      result.Z = quaternion1.Z + quaternion2.Z;
      result.W = quaternion1.W + quaternion2.W;
    }

    public static QuaternionD Subtract(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      QuaternionD quaternionD;
      quaternionD.X = quaternion1.X - quaternion2.X;
      quaternionD.Y = quaternion1.Y - quaternion2.Y;
      quaternionD.Z = quaternion1.Z - quaternion2.Z;
      quaternionD.W = quaternion1.W - quaternion2.W;
      return quaternionD;
    }

    public static void Subtract(ref QuaternionD quaternion1, ref QuaternionD quaternion2, out QuaternionD result)
    {
      result.X = quaternion1.X - quaternion2.X;
      result.Y = quaternion1.Y - quaternion2.Y;
      result.Z = quaternion1.Z - quaternion2.Z;
      result.W = quaternion1.W - quaternion2.W;
    }

    public static QuaternionD Multiply(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      double num1 = quaternion1.X;
      double num2 = quaternion1.Y;
      double num3 = quaternion1.Z;
      double num4 = quaternion1.W;
      double num5 = quaternion2.X;
      double num6 = quaternion2.Y;
      double num7 = quaternion2.Z;
      double num8 = quaternion2.W;
      double num9 = num2 * num7 - num3 * num6;
      double num10 = num3 * num5 - num1 * num7;
      double num11 = num1 * num6 - num2 * num5;
      double num12 = num1 * num5 + num2 * num6 + num3 * num7;
      QuaternionD quaternionD;
      quaternionD.X = num1 * num8 + num5 * num4 + num9;
      quaternionD.Y = num2 * num8 + num6 * num4 + num10;
      quaternionD.Z = num3 * num8 + num7 * num4 + num11;
      quaternionD.W = num4 * num8 - num12;
      return quaternionD;
    }

    public static void Multiply(ref QuaternionD quaternion1, ref QuaternionD quaternion2, out QuaternionD result)
    {
      double num1 = quaternion1.X;
      double num2 = quaternion1.Y;
      double num3 = quaternion1.Z;
      double num4 = quaternion1.W;
      double num5 = quaternion2.X;
      double num6 = quaternion2.Y;
      double num7 = quaternion2.Z;
      double num8 = quaternion2.W;
      double num9 = num2 * num7 - num3 * num6;
      double num10 = num3 * num5 - num1 * num7;
      double num11 = num1 * num6 - num2 * num5;
      double num12 = num1 * num5 + num2 * num6 + num3 * num7;
      result.X = num1 * num8 + num5 * num4 + num9;
      result.Y = num2 * num8 + num6 * num4 + num10;
      result.Z = num3 * num8 + num7 * num4 + num11;
      result.W = num4 * num8 - num12;
    }

    public static QuaternionD Multiply(QuaternionD quaternion1, double scaleFactor)
    {
      QuaternionD quaternionD;
      quaternionD.X = quaternion1.X * scaleFactor;
      quaternionD.Y = quaternion1.Y * scaleFactor;
      quaternionD.Z = quaternion1.Z * scaleFactor;
      quaternionD.W = quaternion1.W * scaleFactor;
      return quaternionD;
    }

    public static void Multiply(ref QuaternionD quaternion1, double scaleFactor, out QuaternionD result)
    {
      result.X = quaternion1.X * scaleFactor;
      result.Y = quaternion1.Y * scaleFactor;
      result.Z = quaternion1.Z * scaleFactor;
      result.W = quaternion1.W * scaleFactor;
    }

    public static QuaternionD Divide(QuaternionD quaternion1, QuaternionD quaternion2)
    {
      double num1 = quaternion1.X;
      double num2 = quaternion1.Y;
      double num3 = quaternion1.Z;
      double num4 = quaternion1.W;
      double num5 = 1.0 / (quaternion2.X * quaternion2.X + quaternion2.Y * quaternion2.Y + quaternion2.Z * quaternion2.Z + quaternion2.W * quaternion2.W);
      double num6 = -quaternion2.X * num5;
      double num7 = -quaternion2.Y * num5;
      double num8 = -quaternion2.Z * num5;
      double num9 = quaternion2.W * num5;
      double num10 = num2 * num8 - num3 * num7;
      double num11 = num3 * num6 - num1 * num8;
      double num12 = num1 * num7 - num2 * num6;
      double num13 = num1 * num6 + num2 * num7 + num3 * num8;
      QuaternionD quaternionD;
      quaternionD.X = num1 * num9 + num6 * num4 + num10;
      quaternionD.Y = num2 * num9 + num7 * num4 + num11;
      quaternionD.Z = num3 * num9 + num8 * num4 + num12;
      quaternionD.W = num4 * num9 - num13;
      return quaternionD;
    }

    public static void Divide(ref QuaternionD quaternion1, ref QuaternionD quaternion2, out QuaternionD result)
    {
      double num1 = quaternion1.X;
      double num2 = quaternion1.Y;
      double num3 = quaternion1.Z;
      double num4 = quaternion1.W;
      double num5 = 1.0 / (quaternion2.X * quaternion2.X + quaternion2.Y * quaternion2.Y + quaternion2.Z * quaternion2.Z + quaternion2.W * quaternion2.W);
      double num6 = -quaternion2.X * num5;
      double num7 = -quaternion2.Y * num5;
      double num8 = -quaternion2.Z * num5;
      double num9 = quaternion2.W * num5;
      double num10 = num2 * num8 - num3 * num7;
      double num11 = num3 * num6 - num1 * num8;
      double num12 = num1 * num7 - num2 * num6;
      double num13 = num1 * num6 + num2 * num7 + num3 * num8;
      result.X = num1 * num9 + num6 * num4 + num10;
      result.Y = num2 * num9 + num7 * num4 + num11;
      result.Z = num3 * num9 + num8 * num4 + num12;
      result.W = num4 * num9 - num13;
    }

    public static QuaternionD FromVector4(Vector4D v)
    {
      return new QuaternionD(v.X, v.Y, v.Z, v.W);
    }

    public Vector4D ToVector4()
    {
      return new Vector4D(this.X, this.Y, this.Z, this.W);
    }

    public static bool IsZero(QuaternionD value)
    {
      return QuaternionD.IsZero(value, 0.0001);
    }

    public static bool IsZero(QuaternionD value, double epsilon)
    {
      if (Math.Abs(value.X) < epsilon && Math.Abs(value.Y) < epsilon && Math.Abs(value.Z) < epsilon)
        return Math.Abs(value.W) < epsilon;
      else
        return false;
    }
  }
}
