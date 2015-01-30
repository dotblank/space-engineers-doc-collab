// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyEdgesModelSet
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
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
    [ProtoMember(2)]
    [ModdableContentFile("mwm")]
    public string VerticalDiagonal;
    [ModdableContentFile("mwm")]
    [ProtoMember(3)]
    public string Horisontal;
    [ModdableContentFile("mwm")]
    [ProtoMember(4)]
    public string HorisontalDiagonal;
  }
}
