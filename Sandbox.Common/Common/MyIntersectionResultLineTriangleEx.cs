// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyIntersectionResultLineTriangleEx
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI;
using System;
using VRage.Common;
using VRage.Common.Utils;
using VRageMath;

namespace Sandbox.Common
{
    public struct MyIntersectionResultLineTriangleEx
    {
        public MyIntersectionResultLineTriangle Triangle;
        public Vector3 IntersectionPointInObjectSpace;
        public Vector3D IntersectionPointInWorldSpace;
        public IMyEntity Entity;
        public Vector3 NormalInWorldSpace;
        public Vector3 NormalInObjectSpace;
        public LineD InputLineInObjectSpace;

        public MyIntersectionResultLineTriangleEx(MyIntersectionResultLineTriangle triangle, IMyEntity entity,
            ref LineD line)
        {
            this.Triangle = triangle;
            this.Entity = entity;
            this.InputLineInObjectSpace = line;
            this.NormalInObjectSpace = MyUtils.GetNormalVectorFromTriangle(ref this.Triangle.InputTriangle);
            this.IntersectionPointInObjectSpace = (Vector3) (line.From + line.Direction*this.Triangle.Distance);
            if (this.Entity is IMyVoxelMap)
            {
                this.IntersectionPointInWorldSpace = (Vector3D) this.IntersectionPointInObjectSpace;
                this.NormalInWorldSpace = this.NormalInObjectSpace;
                this.IntersectionPointInObjectSpace =
                    (Vector3)
                        (this.IntersectionPointInObjectSpace - ((IMyVoxelMap) this.Entity).PositionLeftBottomCorner);
            }
            else
            {
                MatrixD worldMatrix = this.Entity.WorldMatrix;
                this.NormalInWorldSpace =
                    (Vector3)
                        MyVRageUtils.GetTransformNormalNormalized((Vector3D) this.NormalInObjectSpace, ref worldMatrix);
                this.IntersectionPointInWorldSpace = Vector3D.Transform((Vector3D) this.IntersectionPointInObjectSpace,
                    ref worldMatrix);
            }
        }

        public MyIntersectionResultLineTriangleEx(MyIntersectionResultLineTriangle triangle, IMyEntity entity,
            ref LineD line, Vector3D intersectionPointInWorldSpace, Vector3 normalInWorldSpace)
        {
            this.Triangle = triangle;
            this.Entity = entity;
            this.InputLineInObjectSpace = line;
            this.NormalInObjectSpace = this.NormalInWorldSpace = normalInWorldSpace;
            this.IntersectionPointInWorldSpace = intersectionPointInWorldSpace;
            this.IntersectionPointInObjectSpace = (Vector3) this.IntersectionPointInWorldSpace;
        }

        public static MyIntersectionResultLineTriangleEx? GetCloserIntersection(
            ref MyIntersectionResultLineTriangleEx? a, ref MyIntersectionResultLineTriangleEx? b)
        {
            if (!a.HasValue && b.HasValue ||
                a.HasValue && b.HasValue && b.Value.Triangle.Distance < a.Value.Triangle.Distance)
                return b;
            else
                return a;
        }

        public static bool IsDistanceLessThanTolerance(ref MyIntersectionResultLineTriangleEx? a,
            ref MyIntersectionResultLineTriangleEx? b, float distanceTolerance)
        {
            return !a.HasValue && b.HasValue ||
                   a.HasValue && b.HasValue &&
                   Math.Abs(b.Value.Triangle.Distance - a.Value.Triangle.Distance) <= (double) distanceTolerance;
        }
    }
}