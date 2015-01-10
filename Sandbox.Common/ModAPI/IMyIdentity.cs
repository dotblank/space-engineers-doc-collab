// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyIdentity
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using VRageMath;

namespace Sandbox.ModAPI
{
  public interface IMyIdentity
  {
    long PlayerId { get; }

    string DisplayName { get; }

    string Model { get; }

    Vector3? ColorMask { get; }

    bool IsDead { get; }
  }
}
