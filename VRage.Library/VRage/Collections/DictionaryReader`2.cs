// Decompiled with JetBrains decompiler
// Type: VRage.Collections.DictionaryReader`2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;

namespace VRage.Collections
{
  public struct DictionaryReader<K, V> : IEnumerable<KeyValuePair<K, V>>, IEnumerable
  {
    private readonly Dictionary<K, V> m_collection;

    public bool HasValue
    {
      get
      {
        return this.m_collection != null;
      }
    }

    public V this[K key]
    {
      get
      {
        return this.m_collection[key];
      }
    }

    public DictionaryReader(Dictionary<K, V> collection)
    {
      this.m_collection = collection;
    }

    public static implicit operator DictionaryReader<K, V>(Dictionary<K, V> v)
    {
      return new DictionaryReader<K, V>(v);
    }

    public bool ContainsKey(K key)
    {
      return this.m_collection.ContainsKey(key);
    }

    public int Count()
    {
      return this.m_collection.Count;
    }

    public Dictionary<K, V>.Enumerator GetEnumerator()
    {
      return this.m_collection.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<K, V>>) this.GetEnumerator();
    }
  }
}
