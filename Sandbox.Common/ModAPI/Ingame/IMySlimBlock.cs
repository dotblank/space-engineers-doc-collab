// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMySlimBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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