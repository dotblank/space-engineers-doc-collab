// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_GravityGeneratorSphere
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_GravityGeneratorSphere : MyObjectBuilder_FunctionalBlock
  {
    [ProtoMember(2)]
    public float Radius = 150f;
    [ProtoMember(3)]
    [DefaultValue(9.81f)]
    public float GravityAcceleration = 9.81f;
  }
}
