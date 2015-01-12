// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Door
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_Door : MyObjectBuilder_FunctionalBlock
    {
        [DefaultValue(false)] [ProtoMember(1)] public bool State;
        [DefaultValue(0.0f)] [ProtoMember(3)] public float Opening;
        [ProtoMember(4)] public string OpenSound;
        [ProtoMember(5)] public string CloseSound;
    }
}