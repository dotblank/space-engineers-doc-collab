// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.IMyCameraController
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using VRageMath;

namespace Sandbox.ModAPI.Interfaces
{
    /// <summary>
    /// Inaccessible as of version 01.066 (22.1.2015)
    /// </summary>
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