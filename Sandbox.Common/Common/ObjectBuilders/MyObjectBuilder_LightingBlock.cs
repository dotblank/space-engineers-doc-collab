// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_LightingBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public abstract class MyObjectBuilder_LightingBlock : MyObjectBuilder_FunctionalBlock
  {
    [ProtoMember(2)]
    [DefaultValue(-1f)]
    public float Radius = -1f;
    [DefaultValue(1f)]
    [ProtoMember(3)]
    public float ColorRed = 1f;
    [ProtoMember(4)]
    [DefaultValue(1f)]
    public float ColorGreen = 1f;
    [DefaultValue(1f)]
    [ProtoMember(5)]
    public float ColorBlue = 1f;
    [ProtoMember(6)]
    [DefaultValue(1f)]
    public float ColorAlpha = 1f;
    [DefaultValue(-1f)]
    [ProtoMember(7)]
    public float Falloff = -1f;
    [DefaultValue(-1f)]
    [ProtoMember(8)]
    public float Intensity = -1f;
    [ProtoMember(9)]
    [DefaultValue(-1f)]
    public float BlinkIntervalSeconds = -1f;
    [DefaultValue(-1f)]
    [ProtoMember(10)]
    public float BlinkLenght = -1f;
    [ProtoMember(11)]
    [DefaultValue(-1f)]
    public float BlinkOffset = -1f;
  }
}
