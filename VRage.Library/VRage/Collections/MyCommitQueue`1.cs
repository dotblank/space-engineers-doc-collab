// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MyCommitQueue`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using ParallelTasks;
using System.Collections.Generic;

namespace VRage.Collections
{
    public class MyCommitQueue<T>
    {
        private Queue<T> m_commited = new Queue<T>();
        private SpinLock m_commitLock = new SpinLock();
        private List<T> m_dirty = new List<T>();
        private SpinLock m_dirtyLock = new SpinLock();

        public int Count
        {
            get
            {
                this.m_commitLock.Enter();
                try
                {
                    return this.m_commited.Count;
                }
                finally
                {
                    this.m_commitLock.Exit();
                }
            }
        }

        public int UncommitedCount
        {
            get
            {
                this.m_dirtyLock.Enter();
                try
                {
                    return this.m_dirty.Count;
                }
                finally
                {
                    this.m_dirtyLock.Exit();
                }
            }
        }

        public void Enqueue(T obj)
        {
            this.m_dirtyLock.Enter();
            try
            {
                this.m_dirty.Add(obj);
            }
            finally
            {
                this.m_dirtyLock.Exit();
            }
        }

        public void Commit()
        {
            this.m_dirtyLock.Enter();
            try
            {
                this.m_commitLock.Enter();
                try
                {
                    foreach (T obj in this.m_dirty)
                        this.m_commited.Enqueue(obj);
                }
                finally
                {
                    this.m_commitLock.Exit();
                }
                this.m_dirty.Clear();
            }
            finally
            {
                this.m_dirtyLock.Exit();
            }
        }

        public bool TryDequeue(out T obj)
        {
            this.m_commitLock.Enter();
            try
            {
                if (this.m_commited.Count > 0)
                {
                    obj = this.m_commited.Dequeue();
                    return true;
                }
            }
            finally
            {
                this.m_commitLock.Exit();
            }
            obj = default (T);
            return false;
        }
    }
}