// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyPlayerCollection
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI.Interfaces;
using System;
using System.Collections.Generic;

namespace Sandbox.ModAPI
{
    public interface IMyPlayerCollection
    {
        long Count { get; }

        void ExtendControl(IMyControllableEntity entityWithControl, IMyEntity entityGettingControl);

        void GetPlayers(List<IMyPlayer> players, Func<IMyPlayer, bool> collect = null);

        bool HasExtendedControl(IMyControllableEntity firstEntity, IMyEntity secondEntity);

        void ReduceControl(IMyControllableEntity entityWhichKeepsControl, IMyEntity entityWhichLoosesControl);

        void RemoveControlledEntity(IMyEntity entity);

        void TryExtendControl(IMyControllableEntity entityWithControl, IMyEntity entityGettingControl);

        bool TryReduceControl(IMyControllableEntity entityWhichKeepsControl, IMyEntity entityWhichLoosesControl);

        void SetControlledEntity(ulong steamUserId, IMyEntity entity);

        IMyPlayer GetPlayerControllingEntity(IMyEntity entity);

        void GetAllIdentites(List<IMyIdentity> identities, Func<IMyIdentity, bool> collect = null);
    }
}