// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MyGameLogicComponent
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI;

namespace Sandbox.Common.Components
{
    public abstract class MyGameLogicComponent : MyComponentBase
    {
        public MyEntityUpdateEnum NeedsUpdate
        {
            get
            {
                MyEntityUpdateEnum entityUpdateEnum = MyEntityUpdateEnum.NONE;
                if ((this.Entity.Flags & EntityFlags.NeedsUpdate) != (EntityFlags) 0)
                    entityUpdateEnum |= MyEntityUpdateEnum.EACH_FRAME;
                if ((this.Entity.Flags & EntityFlags.NeedsUpdate10) != (EntityFlags) 0)
                    entityUpdateEnum |= MyEntityUpdateEnum.EACH_10TH_FRAME;
                if ((this.Entity.Flags & EntityFlags.NeedsUpdate100) != (EntityFlags) 0)
                    entityUpdateEnum |= MyEntityUpdateEnum.EACH_100TH_FRAME;
                if ((this.Entity.Flags & EntityFlags.NeedsUpdateBeforeNextFrame) != (EntityFlags) 0)
                    entityUpdateEnum |= MyEntityUpdateEnum.BEFORE_NEXT_FRAME;
                return entityUpdateEnum;
            }
            set
            {
                if (value == this.NeedsUpdate)
                    return;
                if (this.Entity.InScene)
                    MyAPIGateway.Entities.UnregisterForUpdate(this.Entity, false);
                this.Entity.Flags &= ~EntityFlags.NeedsUpdateBeforeNextFrame;
                this.Entity.Flags &= ~EntityFlags.NeedsUpdate;
                this.Entity.Flags &= ~EntityFlags.NeedsUpdate10;
                this.Entity.Flags &= ~EntityFlags.NeedsUpdate100;
                if ((value & MyEntityUpdateEnum.BEFORE_NEXT_FRAME) != MyEntityUpdateEnum.NONE)
                    this.Entity.Flags |= EntityFlags.NeedsUpdateBeforeNextFrame;
                if ((value & MyEntityUpdateEnum.EACH_FRAME) != MyEntityUpdateEnum.NONE)
                    this.Entity.Flags |= EntityFlags.NeedsUpdate;
                if ((value & MyEntityUpdateEnum.EACH_10TH_FRAME) != MyEntityUpdateEnum.NONE)
                    this.Entity.Flags |= EntityFlags.NeedsUpdate10;
                if ((value & MyEntityUpdateEnum.EACH_100TH_FRAME) != MyEntityUpdateEnum.NONE)
                    this.Entity.Flags |= EntityFlags.NeedsUpdate100;
                if (!this.Entity.InScene)
                    return;
                MyAPIGateway.Entities.RegisterForUpdate(this.Entity);
            }
        }

        public bool Closed { get; protected set; }

        public bool MarkedForClose { get; protected set; }

        public virtual void UpdateOnceBeforeFrame()
        {
        }

        public virtual void UpdateBeforeSimulation()
        {
        }

        public virtual void UpdateBeforeSimulation10()
        {
        }

        public virtual void UpdateBeforeSimulation100()
        {
        }

        public virtual void UpdateAfterSimulation()
        {
        }

        public virtual void UpdateAfterSimulation10()
        {
        }

        public virtual void UpdateAfterSimulation100()
        {
        }

        public virtual void UpdatingStopped()
        {
        }

        public virtual void Init(MyObjectBuilder_EntityBase objectBuilder)
        {
        }

        public abstract MyObjectBuilder_EntityBase GetObjectBuilder(bool copy = false);

        public virtual void MarkForClose()
        {
        }

        public virtual void Close()
        {
        }
    }
}