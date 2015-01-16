// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Door
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_Door : MyObjectBuilder_FunctionalBlock
    {
        [DefaultValue(false)] [ProtoMember(1)] public bool State;
        [ProtoMember(3)] [DefaultValue(0.0f)] public float Opening;
        [ProtoMember(4)] public string OpenSound;
        [ProtoMember(5)] public string CloseSound;
    }
}