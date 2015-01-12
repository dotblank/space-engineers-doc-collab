// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MyConcurrentQueue`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ParallelTasks;
using System.Collections.Generic;

namespace VRage.Collections
{
    public class MyConcurrentQueue<T>
    {
        private SpinLockRef m_lock = new SpinLockRef();
        private Queue<T> m_queue;

        public int Count
        {
            get
            {
                using (this.m_lock.Acquire())
                    return this.m_queue.Count;
            }
        }

        public MyConcurrentQueue(int capacity = 0)
        {
            this.m_queue = new Queue<T>(capacity);
        }

        public void Clear()
        {
            using (this.m_lock.Acquire())
                this.m_queue.Clear();
        }

        public void Enqueue(T instance)
        {
            using (this.m_lock.Acquire())
                this.m_queue.Enqueue(instance);
        }

        public T Dequeue()
        {
            using (this.m_lock.Acquire())
                return this.m_queue.Dequeue();
        }

        public bool TryDequeue(out T instance)
        {
            using (this.m_lock.Acquire())
            {
                if (this.m_queue.Count > 0)
                {
                    instance = this.m_queue.Dequeue();
                    return true;
                }
            }
            instance = default (T);
            return false;
        }

        public bool TryPeek(out T instance)
        {
            using (this.m_lock.Acquire())
            {
                if (this.m_queue.Count > 0)
                {
                    instance = this.m_queue.Peek();
                    return true;
                }
            }
            instance = default (T);
            return false;
        }
    }
}