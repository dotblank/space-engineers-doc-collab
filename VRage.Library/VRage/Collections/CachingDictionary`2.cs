// Decompiled with JetBrains decompiler
// Type: VRage.Collections.CachingDictionary`2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VRage.Collections
{
    public class CachingDictionary<K, V> : IEnumerable<KeyValuePair<K, V>>, IEnumerable
    {
        private static Predicate<K> m_keyEquals = new Predicate<K>(CachingDictionary<K, V>.KeyEquals);

        private static Predicate<KeyValuePair<K, V>> m_keyValueEquals =
            new Predicate<KeyValuePair<K, V>>(CachingDictionary<K, V>.KeyValueEquals);

        private Dictionary<K, V> m_dictionary = new Dictionary<K, V>();
        private List<KeyValuePair<K, V>> m_additionsAndModifications = new List<KeyValuePair<K, V>>();
        private List<K> m_removals = new List<K>();
        private static K m_keyToCompare;

        private static K KeyToCompare
        {
            set { CachingDictionary<K, V>.m_keyToCompare = value; }
        }

        public ICollection<K> Keys
        {
            get { return (ICollection<K>) this.m_dictionary.Keys; }
        }

        public V this[K key]
        {
            get { return this.m_dictionary[key]; }
            set { this.Add(key, value, false); }
        }

        public void Add(K key, V value, bool immediate = false)
        {
            if (immediate)
            {
                this.m_dictionary[key] = value;
            }
            else
            {
                this.m_additionsAndModifications.Add(new KeyValuePair<K, V>(key, value));
                CachingDictionary<K, V>.m_keyToCompare = key;
                this.m_removals.RemoveAll(CachingDictionary<K, V>.m_keyEquals);
            }
        }

        public void Remove(K key, bool immediate = false)
        {
            if (immediate)
            {
                this.m_dictionary.Remove(key);
            }
            else
            {
                this.m_removals.Add(key);
                CachingDictionary<K, V>.m_keyToCompare = key;
                this.m_additionsAndModifications.RemoveAll(CachingDictionary<K, V>.m_keyValueEquals);
            }
        }

        public bool TryGetValue(K key, out V value)
        {
            return this.m_dictionary.TryGetValue(key, out value);
        }

        public bool ContainsKey(K key)
        {
            return this.m_dictionary.ContainsKey(key);
        }

        public void Clear()
        {
            this.m_dictionary.Clear();
            this.m_additionsAndModifications.Clear();
            this.m_removals.Clear();
        }

        public void ApplyChanges()
        {
            this.ApplyAdditionsAndModifications();
            this.ApplyRemovals();
        }

        public void ApplyAdditionsAndModifications()
        {
            foreach (KeyValuePair<K, V> keyValuePair in this.m_additionsAndModifications)
                this.m_dictionary[keyValuePair.Key] = keyValuePair.Value;
            this.m_additionsAndModifications.Clear();
        }

        public void ApplyRemovals()
        {
            foreach (K key in this.m_removals)
                this.m_dictionary.Remove(key);
            this.m_removals.Clear();
        }

        private static bool KeyEquals(K key)
        {
            return EqualityComparer<K>.Default.Equals(key, CachingDictionary<K, V>.m_keyToCompare);
        }

        private static bool KeyValueEquals(KeyValuePair<K, V> keyValue)
        {
            return EqualityComparer<K>.Default.Equals(keyValue.Key, CachingDictionary<K, V>.m_keyToCompare);
        }

        public Dictionary<K, V>.Enumerator GetEnumerator()
        {
            return this.m_dictionary.GetEnumerator();
        }

        IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<K, V>>) this.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }
    }
}