// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyCubeBuilder
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using VRageMath;

namespace Sandbox.ModAPI
{
  public interface IMyCubeBuilder
  {
    bool BlockCreationIsActivated { get; }

    bool CopyPasteIsActivated { get; }

    bool FreezeGizmo { get; set; }

    bool ShipCreationIsActivated { get; }

    bool ShowRemoveGizmo { get; set; }

    bool UseSymmetry { get; set; }

    bool UseTransparency { get; set; }

    bool IsActivated { get; }

    void Activate();

    void ActivateShipCreationClipboard(MyObjectBuilder_CubeGrid grid, Vector3 centerDeltaDirection, float dragVectorLength);

    void ActivateShipCreationClipboard(MyObjectBuilder_CubeGrid[] grids, Vector3 centerDeltaDirection, float dragVectorLength);

    bool AddConstruction(IMyEntity buildingEntity);

    void Deactivate();

    void DeactivateBlockCreation();

    void DeactivateCopyPaste();

    void DeactivateShipCreationClipboard();

    void StartNewGridPlacement(MyCubeSize cubeSize, bool isStatic);

    IMyCubeGrid FindClosestGrid();
  }
}
