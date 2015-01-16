// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyFactionCollection
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using System;

namespace Sandbox.ModAPI
{
    public interface IMyFactionCollection
    {
        event Action<long, bool, bool> FactionAutoAcceptChanged;

        event Action<long> FactionEdited;

        event Action<long> FactionCreated;

        bool FactionTagExists(string tag, IMyFaction doNotCheck = null);

        bool FactionNameExists(string name, IMyFaction doNotCheck = null);

        IMyFaction TryGetFactionById(long factionId);

        IMyFaction TryGetPlayerFaction(long playerId);

        void AddPlayerToFaction(long playerId, long factionId);

        void KickPlayerFromFaction(long playerId);

        MyRelationsBetweenFactions GetRelationBetweenFactions(long factionId1, long factionId2);

        bool AreFactionsEnemies(long factionId1, long factionId2);

        bool IsPeaceRequestStateSent(long myFactionId, long foreignFactionId);

        bool IsPeaceRequestStatePending(long myFactionId, long foreignFactionId);

        void RemoveFaction(long factionId);

        void SendPeaceRequest(long fromFactionId, long toFactionId);

        void CancelPeaceRequest(long fromFactionId, long toFactionId);

        void AcceptPeace(long fromFactionId, long toFactionId);

        void DeclareWar(long fromFactionId, long toFactionId);

        void SendJoinRequest(long factionId, long playerId);

        void CancelJoinRequest(long factionId, long playerId);

        void AcceptJoin(long factionId, long playerId);

        void KickMember(long factionId, long playerId);

        void PromoteMember(long factionId, long playerId);

        void DemoteMember(long factionId, long playerId);

        void MemberLeaves(long factionId, long playerId);

        void ChangeAutoAccept(long factionId, long playerId, bool autoAcceptMember, bool autoAcceptPeace);

        void EditFaction(long factionId, string tag, string name, string desc, string privateInfo);

        void CreateFaction(long founderId, string tag, string name, string desc, string privateInfo);

        MyObjectBuilder_FactionCollection GetObjectBuilder();
    }
}