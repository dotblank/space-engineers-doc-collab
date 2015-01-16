// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyVoxelMap
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI.Interfaces;
using VRageMath;

namespace Sandbox.ModAPI
{
    public interface IMyVoxelMap
    {
        IMyStorage Storage { get; }

        Vector3D PositionLeftBottomCorner { get; }

        string StorageName { get; }

        void ClampVoxelCoord(ref Vector3I voxelCoord);

        void Init(MyObjectBuilder_EntityBase builder);

        MyObjectBuilder_EntityBase GetObjectBuilder(bool copy = false);

        void Close();

        bool DoOverlapSphereTest(float sphereRadius, Vector3D spherePos);

        bool GetIntersectionWithSphere(ref BoundingSphereD sphere);

        bool IsBoxIntersectingBoundingBoxOfThisVoxelMap(ref BoundingBoxD boundingBox);

        float GetVoxelContentInBoundingBox(BoundingBoxD worldAabb, out float cellCount);

        Vector3I GetVoxelCoordinateFromMeters(Vector3D pos);
    }
}