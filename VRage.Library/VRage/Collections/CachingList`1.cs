// Decompiled with JetBrains decompiler
// Type: VRage.Collections.CachingList`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace VRage.Collections
{
    public class CachingList<T> : IEnumerable<T>, IEnumerable
    {
        private List<T> m_list = new List<T>();
        private List<T> m_toAdd = new List<T>();
        private List<T> m_toRemove = new List<T>();

        public int Count
        {
            get { return this.m_list.Count; }
        }

        public T this[int index]
        {
            get { return this.m_list[index]; }
        }

        public void Add(T entity)
        {
            if (this.m_toRemove.Contains(entity))
                this.m_toRemove.Remove(entity);
            else
                this.m_toAdd.Add(entity);
        }

        public void Remove(T entity, bool immediate = false)
        {
            if (this.m_toAdd.Contains(entity))
                this.m_toAdd.Remove(entity);
            else
                this.m_toRemove.Add(entity);
            if (!immediate)
                return;
            this.m_list.Remove(entity);
            this.m_toRemove.Remove(entity);
        }

        public void ApplyChanges()
        {
            this.ApplyAdditions();
            this.ApplyRemovals();
        }

        public void ApplyAdditions()
        {
            ListExtensions.AddList<T>(this.m_list, this.m_toAdd);
            this.m_toAdd.Clear();
        }

        public void ApplyRemovals()
        {
            foreach (T obj in this.m_toRemove)
                this.m_list.Remove(obj);
            this.m_toRemove.Clear();
        }

        public List<T>.Enumerator GetEnumerator()
        {
            return this.m_list.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>) this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        [Conditional("DEBUG")]
        public void DebugCheckEmpty()
        {
        }

        public override string ToString()
        {
            return string.Format("Count = {0}; ToAdd = {1}; ToRemove = {2}", (object) this.m_list.Count,
                (object) this.m_toAdd.Count, (object) this.m_toRemove.Count);
        }
    }
}