// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MySessionComponentBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;
using System.Reflection;

namespace Sandbox.Common
{
    public abstract class MySessionComponentBase : IMyUserInputComponent
    {
        public readonly string DebugName;
        public readonly int Priority;
        public readonly MyUpdateOrder UpdateOrder;
        public bool Loaded;

        public virtual Type[] Dependencies
        {
            get { return Type.EmptyTypes; }
        }

        public virtual bool IsRequiredByGame
        {
            get { return true; }
        }

        public MySessionComponentBase()
        {
            Type type = this.GetType();
            MySessionComponentDescriptor componentDescriptor =
                (MySessionComponentDescriptor)
                    Attribute.GetCustomAttribute((MemberInfo) type, typeof (MySessionComponentDescriptor), false);
            this.DebugName = type.Name;
            this.Priority = componentDescriptor.Priority;
            this.UpdateOrder = componentDescriptor.UpdateOrder;
        }

        public virtual void LoadData()
        {
        }

        protected virtual void UnloadData()
        {
        }

        public void AfterLoadData()
        {
            this.Loaded = true;
        }

        public void UnloadDataConditional()
        {
            if (!this.Loaded)
                return;
            this.UnloadData();
            this.Loaded = false;
        }

        public virtual void BeforeStart()
        {
        }

        public virtual void UpdateBeforeSimulation()
        {
        }

        public virtual void Simulate()
        {
        }

        public virtual void UpdateAfterSimulation()
        {
        }

        public virtual void UpdatingStopped()
        {
        }

        public virtual void Draw()
        {
        }

        public virtual void HandleInput()
        {
        }

        public override string ToString()
        {
            return this.GetType().Name;
        }
    }
}