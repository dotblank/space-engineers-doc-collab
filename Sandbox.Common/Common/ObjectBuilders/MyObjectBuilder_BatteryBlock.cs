// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_BatteryBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_BatteryBlock : MyObjectBuilder_FunctionalBlock
    {
        [ProtoMember(2)] public bool ProducerEnabled = true;
        [ProtoMember(1)] public float CurrentStoredPower;
        [ProtoMember(3)] public float MaxStoredPower;
        [ProtoMember(4)] public bool SemiautoEnabled;

        public override void SetupForProjector()
        {
            base.SetupForProjector();
            this.CurrentStoredPower = 0.0f;
        }
    }
}