// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_SpaceBall
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_SpaceBall : MyObjectBuilder_FunctionalBlock
  {
    [ProtoMember(1)]
    public float VirtualMass = 2000f;
    [ProtoMember(2)]
    public float Friction = 0.5f;
    [ProtoMember(3)]
    public float Restitution = 0.5f;
  }
}
