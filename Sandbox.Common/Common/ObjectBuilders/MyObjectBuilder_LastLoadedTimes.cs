// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_LastLoadedTimes
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System;
using VRage.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_LastLoadedTimes : MyObjectBuilder_Base
    {
        [ProtoMember(1)] public SerializableDictionary<string, DateTime> LastLoaded;
    }
}