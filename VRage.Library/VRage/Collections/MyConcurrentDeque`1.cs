// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MyConcurrentDeque`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Diagnostics;
using VRage;

namespace VRage.Collections
{
    [DebuggerDisplay("Count = {Count}")]
    public class MyConcurrentDeque<T>
    {
        private readonly MyDeque<T> m_deque = new MyDeque<T>(8);
        private readonly FastResourceLock m_lock = new FastResourceLock();

        public bool Empty
        {
            get
            {
                using (FastResourceLockExtensions.AcquireSharedUsing(this.m_lock))
                    return this.m_deque.Empty;
            }
        }

        public int Count
        {
            get
            {
                using (FastResourceLockExtensions.AcquireSharedUsing(this.m_lock))
                    return this.m_deque.Count;
            }
        }

        public void Clear()
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
                this.m_deque.Clear();
        }

        public void EnqueueFront(T value)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
                this.m_deque.EnqueueFront(value);
        }

        public void EnqueueBack(T value)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
                this.m_deque.EnqueueBack(value);
        }

        public bool TryDequeueFront(out T value)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
            {
                if (this.m_deque.Empty)
                {
                    value = default (T);
                    return false;
                }
                else
                {
                    value = this.m_deque.DequeueFront();
                    return true;
                }
            }
        }

        public bool TryDequeueBack(out T value)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
            {
                if (this.m_deque.Empty)
                {
                    value = default (T);
                    return false;
                }
                else
                {
                    value = this.m_deque.DequeueBack();
                    return true;
                }
            }
        }
    }
}