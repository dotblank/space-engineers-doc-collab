// Decompiled with JetBrains decompiler
// Type: VRage.Collections.HashSetReader`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;

namespace VRage.Collections
{
    public struct HashSetReader<T> : IEnumerable<T>, IEnumerable
    {
        private readonly HashSet<T> m_hashset;

        public bool IsValid
        {
            get { return this.m_hashset != null; }
        }

        public int Count
        {
            get { return this.m_hashset.Count; }
        }

        public HashSetReader(HashSet<T> set)
        {
            this.m_hashset = set;
        }

        public static implicit operator HashSetReader<T>(HashSet<T> v)
        {
            return new HashSetReader<T>(v);
        }

        public bool Contains(T item)
        {
            return this.m_hashset.Contains(item);
        }

        public HashSet<T>.Enumerator GetEnumerator()
        {
            return this.m_hashset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>) this.GetEnumerator();
        }
    }
}