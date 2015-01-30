// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_RadioAntenna
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_RadioAntenna : MyObjectBuilder_FunctionalBlock
  {
    [ProtoMember(3)]
    public bool EnableBroadcasting = true;
    [ProtoMember(1)]
    public float BroadcastRadius;
    [ProtoMember(2)]
    public bool ShowShipName;
  }
}
