// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_LightingBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public abstract class MyObjectBuilder_LightingBlock : MyObjectBuilder_FunctionalBlock
    {
        [DefaultValue(-1f)] [ProtoMember(2)] public float Radius = -1f;
        [DefaultValue(1f)] [ProtoMember(3)] public float ColorRed = 1f;
        [DefaultValue(1f)] [ProtoMember(4)] public float ColorGreen = 1f;
        [ProtoMember(5)] [DefaultValue(1f)] public float ColorBlue = 1f;
        [DefaultValue(1f)] [ProtoMember(6)] public float ColorAlpha = 1f;
        [DefaultValue(-1f)] [ProtoMember(7)] public float Falloff = -1f;
        [ProtoMember(8)] [DefaultValue(-1f)] public float Intensity = -1f;
        [ProtoMember(9)] [DefaultValue(-1f)] public float BlinkIntervalSeconds = -1f;
        [DefaultValue(-1f)] [ProtoMember(10)] public float BlinkLenght = -1f;
        [DefaultValue(-1f)] [ProtoMember(11)] public float BlinkOffset = -1f;
    }
}