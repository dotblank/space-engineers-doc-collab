// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_SoundBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_SoundBlock : MyObjectBuilder_FunctionalBlock
    {
        [ProtoMember(1)] public float Range = 50f;
        [ProtoMember(2)] public float Volume = 1f;
        [ProtoMember(4)] public float LoopPeriod = 1f;
        [ProtoMember(3)] public int CueId;
    }
}