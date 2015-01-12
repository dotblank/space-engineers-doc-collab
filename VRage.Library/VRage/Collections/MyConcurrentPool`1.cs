// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MyConcurrentPool`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;
using System.Threading;

namespace VRage.Collections
{
    public class MyConcurrentPool<T> where T : new()
    {
        private Stack<T> m_instances;
        private ParallelTasks.SpinLock m_lock;
        private int m_instancesCreated;

        public int Count
        {
            get
            {
                this.m_lock.Enter();
                try
                {
                    return this.m_instances.Count;
                }
                finally
                {
                    this.m_lock.Exit();
                }
            }
        }

        public int InstancesCreated
        {
            get { return this.m_instancesCreated; }
        }

        public MyConcurrentPool(int defaultCapacity = 0, bool preallocate = false)
        {
            this.m_lock = new ParallelTasks.SpinLock();
            this.m_instances = new Stack<T>(defaultCapacity);
            if (!preallocate)
                return;
            this.m_instancesCreated = defaultCapacity;
            for (int index = 0; index < defaultCapacity; ++index)
                this.m_instances.Push(new T());
        }

        public T Get()
        {
            this.m_lock.Enter();
            try
            {
                if (this.m_instances.Count > 0)
                    return this.m_instances.Pop();
            }
            finally
            {
                this.m_lock.Exit();
            }
            Interlocked.Increment(ref this.m_instancesCreated);
            return new T();
        }

        public void Return(T instance)
        {
            this.m_lock.Enter();
            try
            {
                this.m_instances.Push(instance);
            }
            finally
            {
                this.m_lock.Exit();
            }
        }
    }
}