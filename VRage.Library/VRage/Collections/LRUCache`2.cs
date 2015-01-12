// Decompiled with JetBrains decompiler
// Type: VRage.Collections.LRUCache`2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;
using System.Diagnostics;
using VRage;

namespace VRage.Collections
{
    public class LRUCache<TKey, TValue>
    {
        private static HashSet<int> m_debugEntrySet = new HashSet<int>();
        private readonly FastResourceLock m_lock = new FastResourceLock();
        private const int INVALID_ENTRY = -1;
        private int m_first;
        private int m_last;
        private readonly IEqualityComparer<TKey> m_comparer;
        private readonly Dictionary<TKey, int> m_entryLookup;
        private readonly LRUCache<TKey, TValue>.CacheEntry[] m_cacheEntries;

        public LRUCache(int cacheSize, IEqualityComparer<TKey> comparer = null)
        {
            this.m_comparer = comparer ?? (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
            this.m_cacheEntries = new LRUCache<TKey, TValue>.CacheEntry[cacheSize];
            this.m_entryLookup = new Dictionary<TKey, int>(cacheSize, this.m_comparer);
            this.Reset();
        }

        public void Reset()
        {
            LRUCache<TKey, TValue>.CacheEntry cacheEntry;
            cacheEntry.Data = default (TValue);
            cacheEntry.Key = default (TKey);
            for (int index = 0; index < this.m_cacheEntries.Length; ++index)
            {
                cacheEntry.Prev = index - 1;
                cacheEntry.Next = index + 1;
                this.m_cacheEntries[index] = cacheEntry;
            }
            this.m_first = 0;
            this.m_last = this.m_cacheEntries.Length - 1;
            this.m_cacheEntries[this.m_last].Next = -1;
            this.m_entryLookup.Clear();
        }

        public TValue Read(TKey key)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
            {
                int entryIndex;
                if (!this.m_entryLookup.TryGetValue(key, out entryIndex))
                    return default (TValue);
                if (entryIndex != this.m_first)
                {
                    this.Remove(entryIndex);
                    this.AddFirst(entryIndex);
                }
                return this.m_cacheEntries[entryIndex].Data;
            }
        }

        public void Write(TKey key, TValue value)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
            {
                int index;
                if (this.m_entryLookup.TryGetValue(key, out index))
                {
                    this.m_cacheEntries[index].Data = value;
                }
                else
                {
                    int entryIndex = this.m_last;
                    this.RemoveLast();
                    this.m_entryLookup.Remove(this.m_cacheEntries[entryIndex].Key);
                    this.m_cacheEntries[entryIndex].Key = key;
                    this.m_cacheEntries[entryIndex].Data = value;
                    this.AddFirst(entryIndex);
                    this.m_entryLookup.Add(key, entryIndex);
                }
            }
        }

        private void RemoveLast()
        {
            int index = this.m_cacheEntries[this.m_last].Prev;
            this.m_cacheEntries[index].Next = -1;
            this.m_cacheEntries[this.m_last].Prev = -1;
            this.m_last = index;
        }

        private void Remove(int entryIndex)
        {
            int index1 = this.m_cacheEntries[entryIndex].Prev;
            int index2 = this.m_cacheEntries[entryIndex].Next;
            if (index1 != -1)
                this.m_cacheEntries[index1].Next = this.m_cacheEntries[entryIndex].Next;
            else
                this.m_first = this.m_cacheEntries[entryIndex].Next;
            if (index2 != -1)
                this.m_cacheEntries[index2].Prev = this.m_cacheEntries[entryIndex].Prev;
            else
                this.m_last = this.m_cacheEntries[entryIndex].Prev;
            this.m_cacheEntries[entryIndex].Prev = -1;
            this.m_cacheEntries[entryIndex].Next = -1;
        }

        private void AddFirst(int entryIndex)
        {
            this.m_cacheEntries[this.m_first].Prev = entryIndex;
            this.m_cacheEntries[entryIndex].Next = this.m_first;
            this.m_first = entryIndex;
        }

        [Conditional("FULLDEBUG")]
        private void AssertConsistent()
        {
            for (int index1 = 0; index1 < 3; ++index1)
            {
                for (int index2 = 0; index2 < this.m_cacheEntries.Length; ++index2)
                    LRUCache<TKey, TValue>.m_debugEntrySet.Add(index2);
                switch (index1)
                {
                    case 0:
                        for (int index2 = this.m_first; index2 != -1; index2 = this.m_cacheEntries[index2].Next)
                            LRUCache<TKey, TValue>.m_debugEntrySet.Remove(index2);
                        break;
                    case 1:
                        for (int index2 = this.m_last; index2 != -1; index2 = this.m_cacheEntries[index2].Prev)
                            LRUCache<TKey, TValue>.m_debugEntrySet.Remove(index2);
                        break;
                    case 2:
                        foreach (KeyValuePair<TKey, int> keyValuePair in this.m_entryLookup)
                            LRUCache<TKey, TValue>.m_debugEntrySet.Remove(keyValuePair.Value);
                        LRUCache<TKey, TValue>.m_debugEntrySet.Clear();
                        break;
                }
            }
        }

        [DebuggerDisplay("Prev={Prev}, Next={Next}, Key={Key}, Data={Data}")]
        private struct CacheEntry
        {
            public int Prev;
            public int Next;
            public TValue Data;
            public TKey Key;
        }
    }
}