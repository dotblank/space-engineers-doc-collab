// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Components.MyComponentBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI;

namespace Sandbox.Common.Components
{
    public abstract class MyComponentBase
    {
        public IMyEntity Entity
        {
            get
            {
                if (this.CurrentContainer == null)
                    return (IMyEntity) null;
                else
                    return this.CurrentContainer.Entity;
            }
        }

        public MyComponentContainer CurrentContainer { get; private set; }

        public virtual void OnAddedToContainer(MyComponentContainer container)
        {
            this.CurrentContainer = container;
        }

        public virtual void OnRemovedFromContainer(MyComponentContainer container)
        {
            this.CurrentContainer = (MyComponentContainer) null;
        }
    }
}