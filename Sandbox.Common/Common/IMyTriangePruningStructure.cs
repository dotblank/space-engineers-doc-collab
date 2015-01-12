// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.IMyTriangePruningStructure
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Engine.Physics;
using Sandbox.ModAPI;
using System.Collections.Generic;
using VRage.Common;
using VRageMath;

namespace Sandbox.Common
{
    public interface IMyTriangePruningStructure
    {
        int Size { get; }

        MyIntersectionResultLineTriangleEx? GetIntersectionWithLine(IMyEntity entity, ref LineD line,
            IntersectionFlags flags = IntersectionFlags.DIRECT_TRIANGLES);

        MyIntersectionResultLineTriangleEx? GetIntersectionWithLine(IMyEntity entity, ref LineD line,
            ref MatrixD customInvMatrix, IntersectionFlags flags = IntersectionFlags.DIRECT_TRIANGLES);

        void GetTrianglesIntersectingLine(IMyEntity entity, ref LineD line, ref MatrixD customInvMatrix,
            IntersectionFlags flags, List<MyIntersectionResultLineTriangleEx> result);

        void GetTrianglesIntersectingLine(IMyEntity entity, ref LineD line, IntersectionFlags flags,
            List<MyIntersectionResultLineTriangleEx> result);

        void GetTrianglesIntersectingSphere(ref BoundingSphereD sphere, Vector3? referenceNormalVector, float? maxAngle,
            List<MyTriangle_Vertex_Normals> retTriangles, int maxNeighbourTriangles);

        bool GetIntersectionWithSphere(IMyEntity physObject, ref BoundingSphereD sphere);

        void GetTrianglesIntersectingSphere(ref BoundingSphereD sphere, List<MyTriangle_Vertex_Normal> retTriangles,
            int maxNeighbourTriangles);

        void GetTrianglesIntersectingAABB(ref BoundingBoxD sphere, List<MyTriangle_Vertex_Normal> retTriangles,
            int maxNeighbourTriangles);

        void Close();
    }
}