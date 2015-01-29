// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Cockpit
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
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
