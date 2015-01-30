// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_PistonBaseDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_PistonBaseDefinition : MyObjectBuilder_CubeBlockDefinition
  {
    [ProtoMember(2)]
    public float Maximum = 10f;
    [ProtoMember(4)]
    public float MaxVelocity = 5f;
    [ProtoMember(1)]
    public float Minimum;
    [ProtoMember(3)]
    public string TopPart;
    [ProtoMember(5)]
    public float RequiredPowerInput;
  }
}
