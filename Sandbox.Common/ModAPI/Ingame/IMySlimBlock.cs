// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMySlimBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Collections.Generic;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
    public interface IMySlimBlock
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

        Vector3I Position { get; }

        IMyCubeGrid CubeGrid { get; }

        void GetMissingComponents(Dictionary<string, int> addToDictionary);

        void UpdateVisual();
    }
}