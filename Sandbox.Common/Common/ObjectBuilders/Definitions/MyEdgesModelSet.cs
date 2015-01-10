// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyEdgesModelSet
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  public class MyEdgesModelSet
  {
    [ProtoMember(1)]
    [ModdableContentFile("mwm")]
    public string Vertical;
    [ModdableContentFile("mwm")]
    [ProtoMember(2)]
    public string VerticalDiagonal;
    [ProtoMember(3)]
    [ModdableContentFile("mwm")]
    public string Horisontal;
    [ProtoMember(4)]
    [ModdableContentFile("mwm")]
    public string HorisontalDiagonal;
  }
}
