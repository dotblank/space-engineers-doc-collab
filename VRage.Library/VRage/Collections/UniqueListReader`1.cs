// Decompiled with JetBrains decompiler
// Type: VRage.Collections.UniqueListReader`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;

namespace VRage.Collections
{
    public struct UniqueListReader<T> : IEnumerable<T>, IEnumerable
    {
        public static UniqueListReader<T> Empty = new UniqueListReader<T>(new MyUniqueList<T>());
        private readonly MyUniqueList<T> m_list;

        public int Count
        {
            get { return this.m_list.Count; }
        }

        public UniqueListReader(MyUniqueList<T> list)
        {
            this.m_list = list;
        }

        public static implicit operator ListReader<T>(UniqueListReader<T> list)
        {
            return list.m_list.ItemList;
        }

        public static implicit operator UniqueListReader<T>(MyUniqueList<T> list)
        {
            return new UniqueListReader<T>(list);
        }

        public T ItemAt(int index)
        {
            return this.m_list[index];
        }

        public List<T>.Enumerator GetEnumerator()
        {
            return this.m_list.GetEnumerator();
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