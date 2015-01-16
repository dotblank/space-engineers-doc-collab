// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyIntersectionResultLineTriangle
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using VRageMath;
using VRageRender;

namespace Sandbox.Common
{
    public struct MyIntersectionResultLineTriangle
    {
        public double Distance;
        public MyTriangle_Vertexes InputTriangle;
        public Vector3 InputTriangleNormal;

        public MyIntersectionResultLineTriangle(ref MyTriangle_Vertexes triangle, ref Vector3 triangleNormal,
            double distance)
        {
            this.InputTriangle = triangle;
            this.InputTriangleNormal = triangleNormal;
            this.Distance = distance;
        }

        public static MyIntersectionResultLineTriangle? GetCloserIntersection(ref MyIntersectionResultLineTriangle? a,
            ref MyIntersectionResultLineTriangle? b)
        {
            if (!a.HasValue && b.HasValue || a.HasValue && b.HasValue && b.Value.Distance < a.Value.Distance)
                return b;
            else
                return a;
        }
    }
}