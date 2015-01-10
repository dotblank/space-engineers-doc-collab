// Decompiled with JetBrains decompiler
// Type: VRageMath.Capsule
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;

namespace VRageMath
{
  public struct Capsule
  {
    public Vector3 P0;
    public Vector3 P1;
    public float Radius;

    public Capsule(Vector3 p0, Vector3 p1, float radius)
    {
      this.P0 = p0;
      this.P1 = p1;
      this.Radius = radius;
    }

    public bool Intersect(Ray ray, ref Vector3 p1, ref Vector3 p2, ref Vector3 n1, ref Vector3 n2)
    {
      Vector3 v1 = this.P1 - this.P0;
      Vector3 v2 = ray.Position - this.P0;
      float num1 = v1.Dot(ray.Direction);
      float num2 = v1.Dot(v2);
      float num3 = v1.Dot(v1);
      float num4 = (double) num3 > 0.0 ? num1 / num3 : 0.0f;
      float num5 = (double) num3 > 0.0 ? num2 / num3 : 0.0f;
      Vector3 v3 = ray.Direction - v1 * num4;
      Vector3 v4 = v2 - v1 * num5;
      float num6 = v3.Dot(v3);
      float num7 = 2f * v3.Dot(v4);
      float num8 = v4.Dot(v4) - this.Radius * this.Radius;
      if ((double) num6 == 0.0)
      {
        BoundingSphere boundingSphere1;
        boundingSphere1.Center = this.P0;
        boundingSphere1.Radius = this.Radius;
        BoundingSphere boundingSphere2;
        boundingSphere2.Center = this.P1;
        boundingSphere2.Radius = this.Radius;
        float tmin1;
        float tmax1;
        float tmin2;
        float tmax2;
        if (!boundingSphere1.IntersectRaySphere(ray, out tmin1, out tmax1) || !boundingSphere2.IntersectRaySphere(ray, out tmin2, out tmax2))
          return false;
        if ((double) tmin1 < (double) tmin2)
        {
          p1 = ray.Position + ray.Direction * tmin1;
          n1 = p1 - this.P0;
          double num9 = (double) n1.Normalize();
        }
        else
        {
          p1 = ray.Position + ray.Direction * tmin2;
          n1 = p1 - this.P1;
          double num9 = (double) n1.Normalize();
        }
        if ((double) tmax1 > (double) tmax2)
        {
          p2 = ray.Position + ray.Direction * tmax1;
          n2 = p2 - this.P0;
          double num9 = (double) n2.Normalize();
        }
        else
        {
          p2 = ray.Position + ray.Direction * tmax2;
          n2 = p2 - this.P1;
          double num9 = (double) n2.Normalize();
        }
        return true;
      }
      else
      {
        float num9 = (float) ((double) num7 * (double) num7 - 4.0 * (double) num6 * (double) num8);
        if ((double) num9 < 0.0)
          return false;
        float num10 = (float) ((-(double) num7 - Math.Sqrt((double) num9)) / (2.0 * (double) num6));
        float num11 = (float) ((-(double) num7 + Math.Sqrt((double) num9)) / (2.0 * (double) num6));
        if ((double) num10 > (double) num11)
        {
          float num12 = num10;
          num10 = num11;
          num11 = num12;
        }
        float num13 = num10 * num4 + num5;
        if ((double) num13 < 0.0)
        {
          BoundingSphere boundingSphere;
          boundingSphere.Center = this.P0;
          boundingSphere.Radius = this.Radius;
          float tmin;
          float tmax;
          if (!boundingSphere.IntersectRaySphere(ray, out tmin, out tmax))
            return false;
          p1 = ray.Position + ray.Direction * tmin;
          n1 = p1 - this.P0;
          double num12 = (double) n1.Normalize();
        }
        else if ((double) num13 > 1.0)
        {
          BoundingSphere boundingSphere;
          boundingSphere.Center = this.P1;
          boundingSphere.Radius = this.Radius;
          float tmin;
          float tmax;
          if (!boundingSphere.IntersectRaySphere(ray, out tmin, out tmax))
            return false;
          p1 = ray.Position + ray.Direction * tmin;
          n1 = p1 - this.P1;
          double num12 = (double) n1.Normalize();
        }
        else
        {
          p1 = ray.Position + ray.Direction * num10;
          Vector3 vector3 = this.P0 + v1 * num13;
          n1 = p1 - vector3;
          double num12 = (double) n1.Normalize();
        }
        float num14 = num11 * num4 + num5;
        if ((double) num14 < 0.0)
        {
          BoundingSphere boundingSphere;
          boundingSphere.Center = this.P0;
          boundingSphere.Radius = this.Radius;
          float tmin;
          float tmax;
          if (!boundingSphere.IntersectRaySphere(ray, out tmin, out tmax))
            return false;
          p2 = ray.Position + ray.Direction * tmax;
          n2 = p2 - this.P0;
          double num12 = (double) n2.Normalize();
        }
        else if ((double) num14 > 1.0)
        {
          BoundingSphere boundingSphere;
          boundingSphere.Center = this.P1;
          boundingSphere.Radius = this.Radius;
          float tmin;
          float tmax;
          if (!boundingSphere.IntersectRaySphere(ray, out tmin, out tmax))
            return false;
          p2 = ray.Position + ray.Direction * tmax;
          n2 = p2 - this.P1;
          double num12 = (double) n2.Normalize();
        }
        else
        {
          p2 = ray.Position + ray.Direction * num11;
          Vector3 vector3 = this.P0 + v1 * num14;
          n2 = p2 - vector3;
          double num12 = (double) n2.Normalize();
        }
        return true;
      }
    }

    public bool Intersect(Line line, ref Vector3 p1, ref Vector3 p2, ref Vector3 n1, ref Vector3 n2)
    {
      if (!this.Intersect(new Ray(line.From, line.Direction), ref p1, ref p2, ref n1, ref n2))
        return false;
      Vector3 vector2_1 = p1 - line.From;
      Vector3 vector2_2 = p2 - line.From;
      float num1 = vector2_1.Normalize();
      double num2 = (double) vector2_2.Normalize();
      return (double) Vector3.Dot(line.Direction, vector2_1) >= 0.899999976158142 && (double) Vector3.Dot(line.Direction, vector2_2) >= 0.899999976158142 && (double) line.Length >= (double) num1;
    }
  }
}
