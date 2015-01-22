// Decompiled with JetBrains decompiler
// Type: VRageMath.LineD
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;

namespace VRageMath
{
  public struct LineD
  {
    public Vector3D From;
    public Vector3D To;
    public Vector3D Direction;
    public double Length;

    public LineD(Vector3D from, Vector3D to)
    {
      this.From = from;
      this.To = to;
      this.Direction = to - from;
      this.Length = this.Direction.Normalize();
    }

    public static explicit operator Line(LineD b)
    {
      return new Line((Vector3) b.From, (Vector3) b.To, true);
    }

    public static explicit operator LineD(Line b)
    {
      return new LineD((Vector3D) b.From, (Vector3D) b.To);
    }

    public static double GetShortestDistanceSquared(LineD line1, LineD line2)
    {
      Vector3D res1;
      Vector3D res2;
      Vector3D shortestVector = LineD.GetShortestVector(ref line1, ref line2, out res1, out res2);
      return Vector3D.Dot(shortestVector, shortestVector);
    }

    public static Vector3D GetShortestVector(ref LineD line1, ref LineD line2, out Vector3D res1, out Vector3D res2)
    {
      double num1 = 9.99999997475243E-07;
      Vector3D vector3D1 = new Vector3D();
      vector3D1.X = line1.To.X - line1.From.X;
      vector3D1.Y = line1.To.Y - line1.From.Y;
      vector3D1.Z = line1.To.Z - line1.From.Z;
      Vector3D vector3D2 = new Vector3D();
      vector3D2.X = line2.To.X - line2.From.X;
      vector3D2.Y = line2.To.Y - line2.From.Y;
      vector3D2.Z = line2.To.Z - line2.From.Z;
      Vector3D vector2 = new Vector3D();
      vector2.X = line1.From.X - line2.From.X;
      vector2.Y = line1.From.Y - line2.From.Y;
      vector2.Z = line1.From.Z - line2.From.Z;
      double num2 = Vector3D.Dot(vector3D1, vector3D1);
      double num3 = Vector3D.Dot(vector3D1, vector3D2);
      double num4 = Vector3D.Dot(vector3D2, vector3D2);
      double num5 = Vector3D.Dot(vector3D1, vector2);
      double num6 = Vector3D.Dot(vector3D2, vector2);
      double num7 = num2 * num4 - num3 * num3;
      double num8 = num7;
      double num9 = num7;
      double num10;
      double num11;
      if (num7 < num1)
      {
        num10 = 0.0;
        num8 = 1.0;
        num11 = num6;
        num9 = num4;
      }
      else
      {
        num10 = num3 * num6 - num4 * num5;
        num11 = num2 * num6 - num3 * num5;
        if (num10 < 0.0)
        {
          num10 = 0.0;
          num11 = num6;
          num9 = num4;
        }
        else if (num10 > num8)
        {
          num10 = num8;
          num11 = num6 + num3;
          num9 = num4;
        }
      }
      if (num11 < 0.0)
      {
        num11 = 0.0;
        if (-num5 < 0.0)
          num10 = 0.0;
        else if (-num5 > num2)
        {
          num10 = num8;
        }
        else
        {
          num10 = -num5;
          num8 = num2;
        }
      }
      else if (num11 > num9)
      {
        num11 = num9;
        if (-num5 + num3 < 0.0)
          num10 = 0.0;
        else if (-num5 + num3 > num2)
        {
          num10 = num8;
        }
        else
        {
          num10 = -num5 + num3;
          num8 = num2;
        }
      }
      double num12 = Math.Abs(num10) >= num1 ? num10 / num8 : 0.0;
      double num13 = Math.Abs(num11) >= num1 ? num11 / num9 : 0.0;
      res1.X = num12 * vector3D1.X;
      res1.Y = num12 * vector3D1.Y;
      res1.Z = num12 * vector3D1.Z;
      Vector3D vector3D3 = new Vector3D();
      vector3D3.X = vector2.X - num13 * vector3D2.X + res1.X;
      vector3D3.Y = vector2.Y - num13 * vector3D2.Y + res1.Y;
      vector3D3.Z = vector2.Z - num13 * vector3D2.Z + res1.Z;
      res2 = res1 - vector3D3;
      return vector3D3;
    }
  }
}
