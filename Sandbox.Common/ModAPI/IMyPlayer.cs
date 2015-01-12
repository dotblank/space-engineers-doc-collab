// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyPlayer
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using System.Collections.Generic;
using VRageMath;

namespace Sandbox.ModAPI
{
    public interface IMyPlayer
    {
        IMyNetworkClient Client { get; }

        HashSet<long> Grids { get; }

        IMyEntityController Controller { get; }

        ulong SteamUserId { get; }

        string DisplayName { get; }

        long PlayerID { get; }

        MyRelationsBetweenPlayerAndBlock GetRelationTo(long playerId);

        void AddGrid(long gridEntityId);

        void RemoveGrid(long gridEntityId);

        Vector3 GetPosition();
    }
}