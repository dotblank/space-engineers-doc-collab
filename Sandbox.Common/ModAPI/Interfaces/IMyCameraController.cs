// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.IMyCameraController
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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