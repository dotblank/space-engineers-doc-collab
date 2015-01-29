// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyMultiplayer
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using System;
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

    bool SendMessageToServer(ushort id, byte[] message, bool reliable = true);

    bool SendMessageToOthers(ushort id, byte[] message, bool reliable = true);

    bool SendMessageTo(ushort id, byte[] message, ulong recipient, bool reliable = true);

    void RegisterMessageHandler(ushort id, Action<byte[]> messageHandler);

    void UnregisterMessageHandler(ushort id, Action<byte[]> messageHandler);
  }
}
