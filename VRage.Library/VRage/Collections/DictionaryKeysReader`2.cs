// Decompiled with JetBrains decompiler
// Type: VRage.Collections.DictionaryKeysReader`2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;

namespace VRage.Collections
{
    public struct DictionaryKeysReader<K, V> : IEnumerable<K>, IEnumerable
    {
        private readonly Dictionary<K, V> m_collection;

        public DictionaryKeysReader(Dictionary<K, V> collection)
        {
            this.m_collection = collection;
        }

        public Dictionary<K, V>.KeyCollection.Enumerator GetEnumerator()
        {
            return this.m_collection.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        IEnumerator<K> IEnumerable<K>.GetEnumerator()
        {
            return (IEnumerator<K>) this.GetEnumerator();
        }
    }
}