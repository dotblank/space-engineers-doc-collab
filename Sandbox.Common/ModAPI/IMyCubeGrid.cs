// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyCubeGrid
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using System;
using System.Collections.Generic;
using VRageMath;

namespace Sandbox.ModAPI
{
  public interface IMyCubeGrid : IMyEntity, Sandbox.ModAPI.Ingame.IMyCubeGrid
  {
    List<long> BigOwners { get; }

    List<long> SmallOwners { get; }

    float GridSize { get; }

    MyCubeSize GridSizeEnum { get; set; }

    bool IsStatic { get; }

    Vector3I Max { get; }

    Vector3I Min { get; }

    event Action<IMySlimBlock> OnBlockAdded;

    event Action<IMySlimBlock> OnBlockRemoved;

    event Action<IMyCubeGrid> OnBlockOwnershipChanged;

    void ApplyDestructionDeformation(IMySlimBlock block);

    void ChangeGridOwnership(long playerId, MyOwnershipShareModeEnum shareMode);

    void ClearSymmetries();

    void ColorBlocks(Vector3I min, Vector3I max, Vector3 newHSV);

    void ConvertToDynamic();

    bool CubeExists(Vector3I pos);

    void FixTargetCube(out Vector3I cube, Vector3 fractionalGridPosition);

    Vector3 GetClosestCorner(Vector3I gridPos, Vector3 position);

    IMySlimBlock GetCubeBlock(Vector3I pos);

    Vector3D? GetLineIntersectionExactAll(ref LineD line, out double distance, out IMySlimBlock intersectedBlock);

    bool GetLineIntersectionExactGrid(ref LineD line, ref Vector3I position, ref double distanceSquared);

    Vector3D GridIntegerToWorld(Vector3I gridCoords);

    bool IsTouchingAnyNeighbor(Vector3I min, Vector3I max);

    bool IsTrash();

    bool CanMergeCubes(IMyCubeGrid gridToMerge, Vector3I gridOffset);

    MatrixI CalculateMergeTransform(IMyCubeGrid gridToMerge, Vector3I gridOffset);

    IMyCubeGrid MergeGrid_CopyPaste(IMyCubeGrid gridToMerge, MatrixI mergeTransform);

    IMyCubeGrid MergeGrid_MergeBlock(IMyCubeGrid gridToMerge, Vector3I gridOffset);

    Vector3I? RayCastBlocks(Vector3D worldStart, Vector3D worldEnd);

    void RayCastCells(Vector3D worldStart, Vector3D worldEnd, List<Vector3I> outHitPositions, Vector3I? gridSizeInflate = null, bool havokWorld = false);

    void RazeBlock(Vector3I position);

    void RazeBlocks(ref Vector3I pos, ref Vector3UByte size);

    void RazeBlocks(List<Vector3I> locations);

    void RemoveBlock(IMySlimBlock block, bool updatePhysics = false);

    void RemoveDestroyedBlock(IMySlimBlock block);

    void UpdateBlockNeighbours(IMySlimBlock block);

    Vector3I WorldToGridInteger(Vector3 coords);

    void GetBlocks(List<IMySlimBlock> blocks, Func<IMySlimBlock, bool> collect = null);

    List<IMySlimBlock> GetBlocksInsideSphere(ref BoundingSphereD sphere);
  }
}
