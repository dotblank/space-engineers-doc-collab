// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_EntityBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public abstract class MyObjectBuilder_EntityBase : MyObjectBuilder_Base
    {
        [ProtoMember(1)] public long EntityId;
        [ProtoMember(2)] public MyPersistentEntityFlags2 PersistentFlags;
        [ProtoMember(3)] public string Name;
        [ProtoMember(4)] public MyPositionAndOrientation? PositionAndOrientation;

        public bool ShouldSerializePositionAndOrientation()
        {
            return this.PositionAndOrientation.HasValue;
        }

        public virtual void Remap(IMyRemapHelper remapHelper)
        {
            this.EntityId = remapHelper.RemapEntityId(this.EntityId);
        }
    }
}