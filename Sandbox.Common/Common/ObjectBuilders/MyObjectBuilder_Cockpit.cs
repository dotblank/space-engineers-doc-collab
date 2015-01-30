// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Cockpit
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_Cockpit : MyObjectBuilder_ShipController
  {
    [ProtoMember(1)]
    public MyObjectBuilder_Character Pilot;
    [ProtoMember(2)]
    public MyPositionAndOrientation? PilotRelativeWorld;
    [ProtoMember(3)]
    public MyObjectBuilder_AutopilotBase Autopilot;
    [ProtoMember(4)]
    public SerializableDefinitionId? PilotGunDefinition;
    [ProtoMember(5)]
    public bool IsInFirstPersonView;

    public void ClearPilotAndAutopilot()
    {
      this.Pilot = (MyObjectBuilder_Character) null;
      this.Autopilot = (MyObjectBuilder_AutopilotBase) null;
    }
  }
}
