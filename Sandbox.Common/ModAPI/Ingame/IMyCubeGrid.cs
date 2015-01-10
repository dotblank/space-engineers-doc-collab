// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMyCubeGrid
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using System;
using System.Collections.Generic;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
  public interface IMyCubeGrid
  {
    List<long> BigOwners { get; }

    List<long> SmallOwners { get; }

    float GridSize { get; }

    MyCubeSize GridSizeEnum { get; }

    bool IsStatic { get; }

    Vector3I Max { get; }

    Vector3I Min { get; }

    void ConvertToDynamic();

    bool CubeExists(Vector3I pos);

    void FixTargetCube(out Vector3I cube, Vector3 fractionalGridPosition);

    Vector3 GetClosestCorner(Vector3I gridPos, Vector3 position);

    IMySlimBlock GetCubeBlock(Vector3I pos);

    Vector3D GridIntegerToWorld(Vector3I gridCoords);

    Vector3I? RayCastBlocks(Vector3D worldStart, Vector3D worldEnd);

    void RayCastCells(Vector3D worldStart, Vector3D worldEnd, List<Vector3I> outHitPositions, Vector3I? gridSizeInflate = null, bool havokWorld = false);

    void UpdateOwnership(long ownerId, bool isFunctional);

    Vector3I WorldToGridInteger(Vector3D coords);

    void GetBlocks(List<IMySlimBlock> blocks, Func<IMySlimBlock, bool> collect = null);

    List<IMySlimBlock> GetBlocksInsideSphere(ref BoundingSphereD sphere);
  }
}
