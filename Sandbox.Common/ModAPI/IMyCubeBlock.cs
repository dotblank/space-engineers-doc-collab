// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyCubeBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using System;
using VRageMath;

namespace Sandbox.ModAPI
{
  public interface IMyCubeBlock : Sandbox.ModAPI.Ingame.IMyCubeBlock, IMyEntity
  {
    SerializableDefinitionId BlockDefinition { get; }

    bool CheckConnectionAllowed { get; set; }

    IMyCubeGrid CubeGrid { get; }

    string DefinitionDisplayNameText { get; }

    float DisassembleRatio { get; }

    string DisplayNameText { get; }

    bool IsBeingHacked { get; }

    bool IsFunctional { get; }

    bool IsWorking { get; }

    Vector3I Max { get; }

    Vector3I Min { get; }

    int NumberInGrid { get; set; }

    MyBlockOrientation Orientation { get; }

    long OwnerId { get; }

    Vector3I Position { get; }

    event Action<IMyCubeBlock> IsWorkingChanged;

    void CalcLocalMatrix(out Matrix localMatrix, out string currModel);

    string CalculateCurrentModel(out Matrix orientation);

    bool DebugDraw();

    MyObjectBuilder_CubeBlock GetObjectBuilderCubeBlock(bool copy = false);

    string GetOwnerFactionTag();

    MyRelationsBetweenPlayerAndBlock GetPlayerRelationToOwner();

    MyRelationsBetweenPlayerAndBlock GetUserRelationToOwner(long playerId);

    void Init();

    void Init(MyObjectBuilder_CubeBlock builder, IMyCubeGrid cubeGrid);

    void OnBuildSuccess(long builtBy);

    void OnDestroy();

    void OnModelChange();

    void OnRegisteredToGridSystems();

    void OnRemovedByCubeBuilder();

    void OnUnregisteredFromGridSystems();

    string RaycastDetectors(Vector3 worldFrom, Vector3 worldTo);

    void ReloadDetectors(bool refreshNetworks = true);

    void UpdateIsWorking();

    void UpdateVisual();
  }
}
