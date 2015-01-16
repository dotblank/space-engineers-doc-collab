// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyIdentity
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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