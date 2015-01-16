// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.BoundingFrustumExtensions
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;
using VRageMath;

namespace Sandbox.Common
{
    public static class BoundingFrustumExtensions
    {
        public static BoundingSphere ToBoundingSphere(this BoundingFrustum frustum, Vector3[] corners)
        {
            if (corners.Length < 8)
                throw new ArgumentException("Corners length must be at least 8");
            frustum.GetCorners(corners);
            Vector3 vector3_1;
            Vector3 vector3_2 = vector3_1 = corners[0];
            Vector3 vector3_3 = vector3_1;
            Vector3 vector3_4 = vector3_1;
            Vector3 vector3_5 = vector3_1;
            Vector3 vector3_6 = vector3_1;
            Vector3 vector3_7 = vector3_1;
            for (int index = 0; index < corners.Length; ++index)
            {
                Vector3 vector3_8 = corners[index];
                if ((double) vector3_8.X < (double) vector3_7.X)
                    vector3_7 = vector3_8;
                if ((double) vector3_8.X > (double) vector3_6.X)
                    vector3_6 = vector3_8;
                if ((double) vector3_8.Y < (double) vector3_5.Y)
                    vector3_5 = vector3_8;
                if ((double) vector3_8.Y > (double) vector3_4.Y)
                    vector3_4 = vector3_8;
                if ((double) vector3_8.Z < (double) vector3_3.Z)
                    vector3_3 = vector3_8;
                if ((double) vector3_8.Z > (double) vector3_2.Z)
                    vector3_2 = vector3_8;
            }
            float result1;
            Vector3.Distance(ref vector3_6, ref vector3_7, out result1);
            float result2;
            Vector3.Distance(ref vector3_4, ref vector3_5, out result2);
            float result3;
            Vector3.Distance(ref vector3_2, ref vector3_3, out result3);
            Vector3 result4;
            float num1;
            if ((double) result1 > (double) result2)
            {
                if ((double) result1 > (double) result3)
                {
                    Vector3.Lerp(ref vector3_6, ref vector3_7, 0.5f, out result4);
                    num1 = result1*0.5f;
                }
                else
                {
                    Vector3.Lerp(ref vector3_2, ref vector3_3, 0.5f, out result4);
                    num1 = result3*0.5f;
                }
            }
            else if ((double) result2 > (double) result3)
            {
                Vector3.Lerp(ref vector3_4, ref vector3_5, 0.5f, out result4);
                num1 = result2*0.5f;
            }
            else
            {
                Vector3.Lerp(ref vector3_2, ref vector3_3, 0.5f, out result4);
                num1 = result3*0.5f;
            }
            for (int index = 0; index < corners.Length; ++index)
            {
                Vector3 vector3_8 = corners[index];
                Vector3 vector3_9;
                vector3_9.X = vector3_8.X - result4.X;
                vector3_9.Y = vector3_8.Y - result4.Y;
                vector3_9.Z = vector3_8.Z - result4.Z;
                float num2 = vector3_9.Length();
                if ((double) num2 > (double) num1)
                {
                    num1 = (float) (((double) num1 + (double) num2)*0.5);
                    result4 += (float) (1.0 - (double) num1/(double) num2)*vector3_9;
                }
            }
            BoundingSphere boundingSphere;
            boundingSphere.Center = result4;
            boundingSphere.Radius = num1;
            return boundingSphere;
        }
    }
}