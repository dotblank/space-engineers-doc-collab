// Decompiled with JetBrains decompiler
// Type: VRageMath.CapsuleD
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;

namespace VRageMath
{
    public struct CapsuleD
    {
        public Vector3D P0;
        public Vector3D P1;
        public float Radius;

        public CapsuleD(Vector3D p0, Vector3D p1, float radius)
        {
            this.P0 = p0;
            this.P1 = p1;
            this.Radius = radius;
        }

        public bool Intersect(RayD ray, ref Vector3D p1, ref Vector3D p2, ref Vector3 n1, ref Vector3 n2)
        {
            Vector3D v1 = this.P1 - this.P0;
            Vector3D v2 = ray.Position - this.P0;
            double num1 = v1.Dot(ray.Direction);
            double num2 = v1.Dot(v2);
            double num3 = v1.Dot(v1);
            double num4 = num3 > 0.0 ? num1/num3 : 0.0;
            double num5 = num3 > 0.0 ? num2/num3 : 0.0;
            Vector3D v3 = ray.Direction - v1*num4;
            Vector3D v4 = v2 - v1*num5;
            double num6 = v3.Dot(v3);
            double num7 = 2.0*v3.Dot(v4);
            double num8 = v4.Dot(v4) - (double) this.Radius*(double) this.Radius;
            if (num6 == 0.0)
            {
                BoundingSphereD boundingSphereD1;
                boundingSphereD1.Center = this.P0;
                boundingSphereD1.Radius = (double) this.Radius;
                BoundingSphereD boundingSphereD2;
                boundingSphereD2.Center = this.P1;
                boundingSphereD2.Radius = (double) this.Radius;
                double tmin1;
                double tmax1;
                double tmin2;
                double tmax2;
                if (!boundingSphereD1.IntersectRaySphere(ray, out tmin1, out tmax1) ||
                    !boundingSphereD2.IntersectRaySphere(ray, out tmin2, out tmax2))
                    return false;
                if (tmin1 < tmin2)
                {
                    p1 = ray.Position + ray.Direction*tmin1;
                    n1 = (Vector3) (p1 - this.P0);
                    double num9 = (double) n1.Normalize();
                }
                else
                {
                    p1 = ray.Position + ray.Direction*tmin2;
                    n1 = (Vector3) (p1 - this.P1);
                    double num9 = (double) n1.Normalize();
                }
                if (tmax1 > tmax2)
                {
                    p2 = ray.Position + ray.Direction*tmax1;
                    n2 = (Vector3) (p2 - this.P0);
                    double num9 = (double) n2.Normalize();
                }
                else
                {
                    p2 = ray.Position + ray.Direction*tmax2;
                    n2 = (Vector3) (p2 - this.P1);
                    double num9 = (double) n2.Normalize();
                }
                return true;
            }
            else
            {
                double d = num7*num7 - 4.0*num6*num8;
                if (d < 0.0)
                    return false;
                double num9 = (-num7 - Math.Sqrt(d))/(2.0*num6);
                double num10 = (-num7 + Math.Sqrt(d))/(2.0*num6);
                if (num9 > num10)
                {
                    double num11 = num9;
                    num9 = num10;
                    num10 = num11;
                }
                double num12 = num9*num4 + num5;
                if (num12 < 0.0)
                {
                    BoundingSphereD boundingSphereD;
                    boundingSphereD.Center = this.P0;
                    boundingSphereD.Radius = (double) this.Radius;
                    double tmin;
                    double tmax;
                    if (!boundingSphereD.IntersectRaySphere(ray, out tmin, out tmax))
                        return false;
                    p1 = ray.Position + ray.Direction*tmin;
                    n1 = (Vector3) (p1 - this.P0);
                    double num11 = (double) n1.Normalize();
                }
                else if (num12 > 1.0)
                {
                    BoundingSphereD boundingSphereD;
                    boundingSphereD.Center = this.P1;
                    boundingSphereD.Radius = (double) this.Radius;
                    double tmin;
                    double tmax;
                    if (!boundingSphereD.IntersectRaySphere(ray, out tmin, out tmax))
                        return false;
                    p1 = ray.Position + ray.Direction*tmin;
                    n1 = (Vector3) (p1 - this.P1);
                    double num11 = (double) n1.Normalize();
                }
                else
                {
                    p1 = ray.Position + ray.Direction*num9;
                    Vector3 vector3 = (Vector3) (this.P0 + v1*num12);
                    n1 = (Vector3) (p1 - vector3);
                    double num11 = (double) n1.Normalize();
                }
                double num13 = num10*num4 + num5;
                if (num13 < 0.0)
                {
                    BoundingSphereD boundingSphereD;
                    boundingSphereD.Center = this.P0;
                    boundingSphereD.Radius = (double) this.Radius;
                    double tmin;
                    double tmax;
                    if (!boundingSphereD.IntersectRaySphere(ray, out tmin, out tmax))
                        return false;
                    p2 = ray.Position + ray.Direction*tmax;
                    n2 = (Vector3) (p2 - this.P0);
                    double num11 = (double) n2.Normalize();
                }
                else if (num13 > 1.0)
                {
                    BoundingSphereD boundingSphereD;
                    boundingSphereD.Center = this.P1;
                    boundingSphereD.Radius = (double) this.Radius;
                    double tmin;
                    double tmax;
                    if (!boundingSphereD.IntersectRaySphere(ray, out tmin, out tmax))
                        return false;
                    p2 = ray.Position + ray.Direction*tmax;
                    n2 = (Vector3) (p2 - this.P1);
                    double num11 = (double) n2.Normalize();
                }
                else
                {
                    p2 = ray.Position + ray.Direction*num10;
                    Vector3D vector3D = this.P0 + v1*num13;
                    n2 = (Vector3) (p2 - vector3D);
                    double num11 = (double) n2.Normalize();
                }
                return true;
            }
        }

        public bool Intersect(LineD line, ref Vector3D p1, ref Vector3D p2, ref Vector3 n1, ref Vector3 n2)
        {
            if (!this.Intersect(new RayD(line.From, line.Direction), ref p1, ref p2, ref n1, ref n2))
                return false;
            Vector3D vector2_1 = p1 - line.From;
            Vector3D vector2_2 = p2 - line.From;
            double num = vector2_1.Normalize();
            vector2_2.Normalize();
            return Vector3D.Dot(line.Direction, vector2_1) >= 0.9 && Vector3D.Dot(line.Direction, vector2_2) >= 0.9 &&
                   line.Length >= num;
        }
    }
}