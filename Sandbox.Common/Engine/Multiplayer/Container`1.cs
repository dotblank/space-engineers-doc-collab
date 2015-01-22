// Decompiled with JetBrains decompiler
// Type: Sandbox.Engine.Multiplayer.Container`1
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Engine.Multiplayer
{
    [ProtoContract]
    public class Container<T> where T : struct
    {
        [ProtoMember(1)] public T Message;
    }
}