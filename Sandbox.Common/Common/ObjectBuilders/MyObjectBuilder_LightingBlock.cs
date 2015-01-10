// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_LightingBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public abstract class MyObjectBuilder_LightingBlock : MyObjectBuilder_FunctionalBlock
  {
    [DefaultValue(-1f)]
    [ProtoMember(2)]
    public float Radius = -1f;
    [ProtoMember(3)]
    [DefaultValue(1f)]
    public float ColorRed = 1f;
    [DefaultValue(1f)]
    [ProtoMember(4)]
    public float ColorGreen = 1f;
    [ProtoMember(5)]
    [DefaultValue(1f)]
    public float ColorBlue = 1f;
    [ProtoMember(6)]
    [DefaultValue(1f)]
    public float ColorAlpha = 1f;
    [DefaultValue(-1f)]
    [ProtoMember(7)]
    public float Falloff = -1f;
    [ProtoMember(8)]
    [DefaultValue(-1f)]
    public float Intensity = -1f;
    [DefaultValue(-1f)]
    [ProtoMember(9)]
    public float BlinkIntervalSeconds = -1f;
    [DefaultValue(-1f)]
    [ProtoMember(10)]
    public float BlinkLenght = -1f;
    [DefaultValue(-1f)]
    [ProtoMember(11)]
    public float BlinkOffset = -1f;
  }
}
