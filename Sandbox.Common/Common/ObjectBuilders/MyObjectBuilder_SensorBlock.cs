// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_SensorBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.VRageData;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_SensorBlock : MyObjectBuilder_FunctionalBlock
    {
        [ProtoMember(1)] public SerializableVector3 FieldMin = new SerializableVector3(-5f, -5f, -5f);
        [ProtoMember(2)] public SerializableVector3 FieldMax = new SerializableVector3(5f, 5f, 5f);
        [ProtoMember(4)] public bool DetectPlayers = true;
        [ProtoMember(11)] public bool DetectOwner = true;
        [ProtoMember(12)] public bool DetectFriendly = true;
        [ProtoMember(13)] public bool DetectNeutral = true;
        [ProtoMember(14)] public bool DetectEnemy = true;
        [ProtoMember(3)] public MyObjectBuilder_Toolbar Toolbar;
        [ProtoMember(5)] public bool DetectFloatingObjects;
        [ProtoMember(6)] public bool DetectSmallShips;
        [ProtoMember(7)] public bool DetectLargeShips;
        [ProtoMember(8)] public bool DetectStations;
        [ProtoMember(9)] public bool IsActive;
        [ProtoMember(10)] public bool DetectAsteroids;

        public override void Remap(IMyRemapHelper remapHelper)
        {
            base.Remap(remapHelper);
            this.Toolbar.Remap(remapHelper);
        }
    }
}