// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMySlimBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using System.Collections.Generic;
using VRageMath;

namespace Sandbox.ModAPI
{
  public interface IMySlimBlock : Sandbox.ModAPI.Ingame.IMySlimBlock
  {
    float AccumulatedDamage { get; }

    float BuildIntegrity { get; }

    float BuildLevelRatio { get; }

    float CurrentDamage { get; }

    float DamageRatio { get; }

    IMyCubeBlock FatBlock { get; }

    bool HasDeformation { get; }

    bool IsDestroyed { get; }

    bool IsFullIntegrity { get; }

    bool IsFullyDismounted { get; }

    float MaxDeformation { get; }

    float MaxIntegrity { get; }

    bool ShowParts { get; }

    bool StockpileAllocated { get; }

    bool StockpileEmpty { get; }

    Vector3I Position { get; set; }

    IMyCubeGrid CubeGrid { get; }

    void AddNeighbours();

    void ApplyAccumulatedDamage(bool addDirtyParts = true);

    string CalculateCurrentModel(out Matrix orientation);

    void ComputeScaledCenter(out Vector3D scaledCenter);

    void ComputeScaledHalfExtents(out Vector3 scaledHalfExtents);

    void ComputeWorldCenter(out Vector3D worldCenter);

    void FixBones(float oldDamage, float maxAllowedBoneMovement);

    MyObjectBuilder_CubeBlock GetCopyObjectBuilder();

    void GetMissingComponents(Dictionary<string, int> addToDictionary);

    MyObjectBuilder_CubeBlock GetObjectBuilder();

    void InitOrientation(ref Vector3I forward, ref Vector3I up);

    void InitOrientation(Base6Directions.Direction Forward, Base6Directions.Direction Up);

    void InitOrientation(MyBlockOrientation orientation);

    void RemoveNeighbours();

    void SetToConstructionSite();

    void SpawnConstructionStockpile();

    void SpawnFirstItemInConstructionStockpile();

    void UpdateVisual();

    Vector3 GetColorMask();
  }
}
