// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_PistonBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_PistonBase : MyObjectBuilder_FunctionalBlock
    {
        [ProtoMember(1)] public float Velocity = -0.1f;
        [ProtoMember(2)] public float? MaxLimit;
        [ProtoMember(3)] public float? MinLimit;
        [ProtoMember(4)] public bool Reverse;
        [ProtoMember(5)] public long TopBlockId;
        [ProtoMember(6)] public float CurrentPosition;

        public override void Remap(IMyRemapHelper remapHelper)
        {
            base.Remap(remapHelper);
            if (this.TopBlockId == 0L)
                return;
            this.TopBlockId = remapHelper.RemapEntityId(this.TopBlockId);
        }
    }
}