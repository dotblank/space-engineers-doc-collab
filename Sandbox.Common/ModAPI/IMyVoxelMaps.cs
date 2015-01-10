// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyVoxelMaps
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI.Interfaces;
using System;
using System.Collections.Generic;
using VRageMath;

namespace Sandbox.ModAPI
{
  public interface IMyVoxelMaps
  {
    void Clear();

    bool Exist(IMyVoxelMap voxelMap);

    IMyVoxelMap GetOverlappingWithSphere(ref BoundingSphereD sphere);

    IMyVoxelMap GetVoxelMapWhoseBoundingBoxIntersectsBox(ref BoundingBoxD boundingBox, IMyVoxelMap ignoreVoxelMap);

    void GetInstances(List<IMyVoxelMap> outInstances, Func<IMyVoxelMap, bool> collect = null);

    IMyStorage CreateStorage(Vector3I size);
  }
}
