// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyEdgesModelSet
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    public class MyEdgesModelSet
    {
        [ProtoMember(1)] [ModdableContentFile("mwm")] public string Vertical;
        [ModdableContentFile("mwm")] [ProtoMember(2)] public string VerticalDiagonal;
        [ModdableContentFile("mwm")] [ProtoMember(3)] public string Horisontal;
        [ModdableContentFile("mwm")] [ProtoMember(4)] public string HorisontalDiagonal;
    }
}