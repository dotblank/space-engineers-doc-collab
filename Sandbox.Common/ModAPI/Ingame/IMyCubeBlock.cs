// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMyCubeBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.ModAPI;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
    public interface IMyCubeBlock : IMyEntity
    {
        SerializableDefinitionId BlockDefinition { get; }

        bool CheckConnectionAllowed { get; }

        IMyCubeGrid CubeGrid { get; }

        string DefinitionDisplayNameText { get; }

        float DisassembleRatio { get; }

        string DisplayNameText { get; }

        bool IsBeingHacked { get; }

        bool IsFunctional { get; }

        bool IsWorking { get; }

        Vector3I Max { get; }

        Vector3I Min { get; }

        int NumberInGrid { get; }

        MyBlockOrientation Orientation { get; }

        long OwnerId { get; }

        Vector3I Position { get; }

        string GetOwnerFactionTag();

        MyRelationsBetweenPlayerAndBlock GetPlayerRelationToOwner();

        MyRelationsBetweenPlayerAndBlock GetUserRelationToOwner(long playerId);

        void UpdateIsWorking();

        void UpdateVisual();
    }
}