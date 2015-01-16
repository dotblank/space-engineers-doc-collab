// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyUtils2
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;
using System.Runtime.InteropServices;
using VRage.Common.Utils;
using VRageMath;
using VRageRender;

namespace Sandbox.Common
{
    public static class MyUtils2
    {
        public static readonly Matrix ZeroMatrix = new Matrix(0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);

        [DllImport("kernel32")]
        public static extern bool SetProcessWorkingSetSize(IntPtr handle, int minSize, int maxSize);

        public static BoundingBox GetNewBoundingBox(Vector3 position, Vector3 sizeInMetres)
        {
            return new BoundingBox(position, position + sizeInMetres);
        }

        public static float GetAngleBetweenVectors(Vector3 vectorA, Vector3 vectorB)
        {
            float num = Vector3.Dot(vectorA, vectorB);
            if ((double) num > 1.0 && (double) num <= 1.00010001659393)
                num = 1f;
            if ((double) num < -1.0 && (double) num >= -1.00010001659393)
                num = -1f;
            return (float) Math.Acos((double) num);
        }

        public static void VectorPlaneRotation(Vector3 xVector, Vector3 yVector, out Vector3 xOut, out Vector3 yOut,
            float angle)
        {
            Vector3 vector3_1 = xVector*(float) Math.Cos((double) angle) + yVector*(float) Math.Sin((double) angle);
            Vector3 vector3_2 = xVector*(float) Math.Cos((double) angle + Math.PI/2.0) +
                                yVector*(float) Math.Sin((double) angle + Math.PI/2.0);
            xOut = vector3_1;
            yOut = vector3_2;
        }

        public static Vector3 LinePlaneIntersection(Vector3 planePoint, Vector3 planeNormal, Vector3 lineStart,
            Vector3 lineDir)
        {
            float num1 = Vector3.Dot(planePoint - lineStart, planeNormal);
            float num2 = Vector3.Dot(lineDir, planeNormal);
            return lineStart + lineDir*(num1/num2);
        }

        public static void ProjectPointOnPlane(ref Vector3 p, ref Vector3 normal, out Vector3 ret)
        {
            float result1;
            Vector3.Dot(ref normal, ref normal, out result1);
            float num1 = 1f/result1;
            float result2;
            Vector3.Dot(ref normal, ref p, out result2);
            float num2 = result2*num1;
            Vector3 vector3;
            vector3.X = normal.X*num1;
            vector3.Y = normal.Y*num1;
            vector3.Z = normal.Z*num1;
            ret.X = p.X - num2*vector3.X;
            ret.Y = p.Y - num2*vector3.Y;
            ret.Z = p.Z - num2*vector3.Z;
        }

        public static void GetPerpendicularVector(ref Vector3 src, out Vector3 ret)
        {
            float num1 = Math.Abs(src.X);
            float num2 = Math.Abs(src.Y);
            float num3 = Math.Abs(src.Z);
            int num4 = 0;
            float num5 = num1;
            if ((double) num2 < (double) num5)
            {
                num4 = 1;
                num5 = num2;
            }
            if ((double) num3 < (double) num5)
                num4 = 2;
            Vector3 p = Vector3.Zero;
            if (num4 == 0)
                p.X = 1f;
            else if (num4 == 1)
                p.Y = 1f;
            else if (num4 == 2)
                p.Z = 1f;
            MyUtils2.ProjectPointOnPlane(ref p, ref src, out ret);
            ret = MyVRageUtils.Normalize(ret);
        }

        public static Vector3 GetTransformNormalNormalized(Vector3 vec, ref Matrix matrix)
        {
            Vector3 result;
            Vector3.TransformNormal(ref vec, ref matrix, out result);
            return MyVRageUtils.Normalize(result);
        }

        public static float GetSmallestDistanceToSphere(ref Vector3 from, ref BoundingSphere sphere)
        {
            return Vector3.Distance(from, sphere.Center) - sphere.Radius;
        }

        public static float GetSmallestDistanceToSphereAlwaysPositive(ref Vector3 from, ref BoundingSphere sphere)
        {
            float num = MyUtils2.GetSmallestDistanceToSphere(ref from, ref sphere);
            if ((double) num < 0.0)
                num = 0.0f;
            return num;
        }

        public static float GetLargestDistanceToSphere(ref Vector3 from, ref BoundingSphere sphere)
        {
            return Vector3.Distance(from, sphere.Center) + sphere.Radius;
        }

        public static bool GetInsidePolygonForSphereCollision(ref Vector3 point, ref MyTriangle_Vertexes triangle)
        {
            return
                (double)
                    (0.0f +
                     MyUtils2.GetAngleBetweenVectorsForSphereCollision(triangle.Vertex0 - point,
                         triangle.Vertex1 - point) +
                     MyUtils2.GetAngleBetweenVectorsForSphereCollision(triangle.Vertex1 - point,
                         triangle.Vertex2 - point) +
                     MyUtils2.GetAngleBetweenVectorsForSphereCollision(triangle.Vertex2 - point,
                         triangle.Vertex0 - point)) >= 6.22035415919481;
        }

        public static float GetAngleBetweenVectorsForSphereCollision(Vector3 vector1, Vector3 vector2)
        {
            float f =
                (float) Math.Acos((double) Vector3.Dot(vector1, vector2)/(double) (vector1.Length()*vector2.Length()));
            if (float.IsNaN(f))
                return 0.0f;
            else
                return f;
        }

        public static Vector3? GetEdgeSphereCollision(ref Vector3 sphereCenter, float sphereRadius,
            ref MyTriangle_Vertexes triangle)
        {
            Vector3 closestPointOnLine1 = MyUtils2.GetClosestPointOnLine(ref triangle.Vertex0, ref triangle.Vertex1,
                ref sphereCenter);
            if ((double) Vector3.Distance(closestPointOnLine1, sphereCenter) < (double) sphereRadius)
                return new Vector3?(closestPointOnLine1);
            Vector3 closestPointOnLine2 = MyUtils2.GetClosestPointOnLine(ref triangle.Vertex1, ref triangle.Vertex2,
                ref sphereCenter);
            if ((double) Vector3.Distance(closestPointOnLine2, sphereCenter) < (double) sphereRadius)
                return new Vector3?(closestPointOnLine2);
            Vector3 closestPointOnLine3 = MyUtils2.GetClosestPointOnLine(ref triangle.Vertex2, ref triangle.Vertex0,
                ref sphereCenter);
            if ((double) Vector3.Distance(closestPointOnLine3, sphereCenter) < (double) sphereRadius)
                return new Vector3?(closestPointOnLine3);
            else
                return new Vector3?();
        }

        public static Vector3 GetClosestPointOnLine(ref Vector3 linePointA, ref Vector3 linePointB, ref Vector3 point)
        {
            float dist = 0.0f;
            return MyUtils2.GetClosestPointOnLine(ref linePointA, ref linePointB, ref point, out dist);
        }

        public static Vector3 GetClosestPointOnLine(ref Vector3 linePointA, ref Vector3 linePointB, ref Vector3 point,
            out float dist)
        {
            Vector3 vector2 = point - linePointA;
            Vector3 vector1 = MyVRageUtils.Normalize(linePointB - linePointA);
            float num1 = Vector3.Distance(linePointA, linePointB);
            float num2 = Vector3.Dot(vector1, vector2);
            dist = num2;
            if ((double) num2 <= 0.0)
                return linePointA;
            if ((double) num2 >= (double) num1)
                return linePointB;
            Vector3 vector3 = vector1*num2;
            return linePointA + vector3;
        }

        public static Vector3 GetNormalVectorFromTriangle(ref MyTriangle_Vertexes inputTriangle)
        {
            return
                Vector3.Normalize(Vector3.Cross(inputTriangle.Vertex2 - inputTriangle.Vertex0,
                    inputTriangle.Vertex1 - inputTriangle.Vertex0));
        }

        public static BoundingSphere GetBoundingSphereFromBoundingBox(ref BoundingBox box)
        {
            BoundingSphere boundingSphere;
            boundingSphere.Center = (box.Max + box.Min)/2f;
            boundingSphere.Radius = Vector3.Distance(boundingSphere.Center, box.Max);
            return boundingSphere;
        }

        public static bool IsLineIntersectingBoundingBox(ref Line line, ref BoundingBox boundingBox)
        {
            Ray ray = new Ray(line.From, line.Direction);
            float? nullable = boundingBox.Intersects(ray);
            return nullable.HasValue && (double) nullable.Value <= (double) line.Length;
        }

        public static float? GetLineBoundingBoxIntersection(ref Line line, ref BoundingBox boundingBox)
        {
            Ray ray = new Ray(line.From, line.Direction);
            float? nullable = boundingBox.Intersects(ray);
            if (!nullable.HasValue)
                return new float?();
            if ((double) nullable.Value <= (double) line.Length)
                return new float?(nullable.Value);
            else
                return new float?();
        }

        public static bool IsLineIntersectingBoundingSphere(ref Line line, ref BoundingSphere boundingSphere)
        {
            Ray ray = new Ray(line.From, line.Direction);
            float? nullable = boundingSphere.Intersects(ray);
            return nullable.HasValue && (double) nullable.Value <= (double) line.Length;
        }

        public static float? GetLineTriangleIntersection(ref Line line, ref MyTriangle_Vertexes triangle)
        {
            Vector3 result1;
            Vector3.Subtract(ref triangle.Vertex1, ref triangle.Vertex0, out result1);
            Vector3 result2;
            Vector3.Subtract(ref triangle.Vertex2, ref triangle.Vertex0, out result2);
            Vector3 result3;
            Vector3.Cross(ref line.Direction, ref result2, out result3);
            float result4;
            Vector3.Dot(ref result1, ref result3, out result4);
            if ((double) result4 > -1.40129846432482E-45 && (double) result4 < 1.40129846432482E-45)
                return new float?();
            float num1 = 1f/result4;
            Vector3 result5;
            Vector3.Subtract(ref line.From, ref triangle.Vertex0, out result5);
            float result6;
            Vector3.Dot(ref result5, ref result3, out result6);
            float num2 = result6*num1;
            if ((double) num2 < 0.0 || (double) num2 > 1.0)
                return new float?();
            Vector3 result7;
            Vector3.Cross(ref result5, ref result1, out result7);
            float result8;
            Vector3.Dot(ref line.Direction, ref result7, out result8);
            float num3 = result8*num1;
            if ((double) num3 < 0.0 || (double) num2 + (double) num3 > 1.0)
                return new float?();
            float result9;
            Vector3.Dot(ref result2, ref result7, out result9);
            float num4 = result9*num1;
            if ((double) num4 < 0.0)
                return new float?();
            if ((double) num4 > (double) line.Length)
                return new float?();
            else
                return new float?(num4);
        }

        public static bool IsValid(float f)
        {
            if (!float.IsNaN(f))
                return !float.IsInfinity(f);
            else
                return false;
        }

        public static bool IsValid(Vector3 vec)
        {
            if (MyUtils2.IsValid(vec.X) && MyUtils2.IsValid(vec.Y))
                return MyUtils2.IsValid(vec.Z);
            else
                return false;
        }

        public static bool IsValid(Vector3? vec)
        {
            if (!vec.HasValue)
                return true;
            if (MyUtils2.IsValid(vec.Value.X) && MyUtils2.IsValid(vec.Value.Y))
                return MyUtils2.IsValid(vec.Value.Z);
            else
                return false;
        }

        public static bool IsValid(Matrix matrix)
        {
            if (MyUtils2.IsValid(matrix.Up) && MyUtils2.IsValid(matrix.Left) &&
                (MyUtils2.IsValid(matrix.Forward) && MyUtils2.IsValid(matrix.Translation)))
                return matrix != MyUtils2.ZeroMatrix;
            else
                return false;
        }

        public static void AssertIsValid(Vector3 vec)
        {
        }

        public static void AssertIsValid(Vector3? vec)
        {
        }

        public static void AssertIsValid(float f)
        {
        }

        public static void AssertIsValid(Matrix matrix)
        {
        }
    }
}