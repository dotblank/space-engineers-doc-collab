// Decompiled with JetBrains decompiler
// Type: VRageMath.Line
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E0D0468A-72EF-4E34-8D6D-888CDEEC91D0
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;

namespace VRageMath
{
    public struct Line
    {
        public Vector3 From;
        public Vector3 To;
        public Vector3 Direction;
        public float Length;
        public BoundingBox BoundingBox;

        public Line(Vector3 from, Vector3 to, bool calculateBoundingBox = true)
        {
            this.From = from;
            this.To = to;
            this.Direction = to - from;
            this.Length = this.Direction.Normalize();
            this.BoundingBox = BoundingBox.CreateInvalid();
            if (!calculateBoundingBox)
                return;
            this.BoundingBox = this.BoundingBox.Include(ref from);
            this.BoundingBox = this.BoundingBox.Include(ref to);
        }

        public static float GetShortestDistanceSquared(Line line1, Line line2)
        {
            Vector3 res1;
            Vector3 res2;
            Vector3 shortestVector = Line.GetShortestVector(ref line1, ref line2, out res1, out res2);
            return Vector3.Dot(shortestVector, shortestVector);
        }

        public static Vector3 GetShortestVector(ref Line line1, ref Line line2, out Vector3 res1, out Vector3 res2)
        {
            float num1 = 1E-06f;
            Vector3 vector3_1 = new Vector3();
            vector3_1.X = line1.To.X - line1.From.X;
            vector3_1.Y = line1.To.Y - line1.From.Y;
            vector3_1.Z = line1.To.Z - line1.From.Z;
            Vector3 vector3_2 = new Vector3();
            vector3_2.X = line2.To.X - line2.From.X;
            vector3_2.Y = line2.To.Y - line2.From.Y;
            vector3_2.Z = line2.To.Z - line2.From.Z;
            Vector3 vector2 = new Vector3();
            vector2.X = line1.From.X - line2.From.X;
            vector2.Y = line1.From.Y - line2.From.Y;
            vector2.Z = line1.From.Z - line2.From.Z;
            float num2 = Vector3.Dot(vector3_1, vector3_1);
            float num3 = Vector3.Dot(vector3_1, vector3_2);
            float num4 = Vector3.Dot(vector3_2, vector3_2);
            float num5 = Vector3.Dot(vector3_1, vector2);
            float num6 = Vector3.Dot(vector3_2, vector2);
            float num7 = (float) ((double) num2*(double) num4 - (double) num3*(double) num3);
            float num8 = num7;
            float num9 = num7;
            float num10;
            float num11;
            if ((double) num7 < (double) num1)
            {
                num10 = 0.0f;
                num8 = 1f;
                num11 = num6;
                num9 = num4;
            }
            else
            {
                num10 = (float) ((double) num3*(double) num6 - (double) num4*(double) num5);
                num11 = (float) ((double) num2*(double) num6 - (double) num3*(double) num5);
                if ((double) num10 < 0.0)
                {
                    num10 = 0.0f;
                    num11 = num6;
                    num9 = num4;
                }
                else if ((double) num10 > (double) num8)
                {
                    num10 = num8;
                    num11 = num6 + num3;
                    num9 = num4;
                }
            }
            if ((double) num11 < 0.0)
            {
                num11 = 0.0f;
                if (-(double) num5 < 0.0)
                    num10 = 0.0f;
                else if (-(double) num5 > (double) num2)
                {
                    num10 = num8;
                }
                else
                {
                    num10 = -num5;
                    num8 = num2;
                }
            }
            else if ((double) num11 > (double) num9)
            {
                num11 = num9;
                if (-(double) num5 + (double) num3 < 0.0)
                    num10 = 0.0f;
                else if (-(double) num5 + (double) num3 > (double) num2)
                {
                    num10 = num8;
                }
                else
                {
                    num10 = -num5 + num3;
                    num8 = num2;
                }
            }
            float num12 = (double) Math.Abs(num10) >= (double) num1 ? num10/num8 : 0.0f;
            float num13 = (double) Math.Abs(num11) >= (double) num1 ? num11/num9 : 0.0f;
            res1.X = num12*vector3_1.X;
            res1.Y = num12*vector3_1.Y;
            res1.Z = num12*vector3_1.Z;
            Vector3 vector3_3 = new Vector3();
            vector3_3.X = vector2.X - num13*vector3_2.X + res1.X;
            vector3_3.Y = vector2.Y - num13*vector3_2.Y + res1.Y;
            vector3_3.Z = vector2.Z - num13*vector3_2.Z + res1.Z;
            res2 = res1 - vector3_3;
            return vector3_3;
        }
    }
}