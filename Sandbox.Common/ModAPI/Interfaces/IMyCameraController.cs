// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.IMyCameraController
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using VRageMath;

namespace Sandbox.ModAPI.Interfaces
{
  public interface IMyCameraController
  {
    bool IsInFirstPersonView { get; set; }

    bool ForceFirstPersonCamera { get; set; }

    MatrixD GetViewMatrix();

    void Rotate(Vector2 rotationIndicator, float rollIndicator);

    void RotateStopped();

    void OnAssumeControl(IMyCameraController previousCameraController);

    void OnReleaseControl(IMyCameraController newCameraController);

    bool HandleUse();
  }
}
