// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyFaction
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using VRage.Collections;

namespace Sandbox.ModAPI
{
    public interface IMyFaction
    {
        long FactionId { get; }

        string Tag { get; }

        string Name { get; }

        string Description { get; }

        string PrivateInfo { get; }

        bool AutoAcceptMember { get; }

        bool AutoAcceptPeace { get; }

        DictionaryReader<long, MyFactionMember> Members { get; }

        DictionaryReader<long, MyFactionMember> JoinRequests { get; }

        bool IsFounder(long playerId);

        bool IsLeader(long playerId);

        bool IsMember(long playerId);

        bool IsNeutral(long playerId);
    }
}