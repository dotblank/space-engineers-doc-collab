// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyMultiplayer
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using System.Collections.Generic;

namespace Sandbox.ModAPI
{
    public interface IMyMultiplayer
    {
        bool MultiplayerActive { get; }

        bool IsServer { get; }

        ulong ServerId { get; }

        ulong MyId { get; }

        string MyName { get; }

        IMyPlayerCollection Players { get; }

        bool IsServerPlayer(IMyNetworkClient player);

        void SendEntitiesCreated(List<MyObjectBuilder_EntityBase> objectBuilders);
    }
}