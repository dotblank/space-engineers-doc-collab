﻿// Decompiled with JetBrains decompiler
// Type: VRage.Collections.ListReader`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;

namespace VRage.Collections
{
    public struct ListReader<T> : IEnumerable<T>, IEnumerable
    {
        public static ListReader<T> Empty = new ListReader<T>(new List<T>(0));
        private readonly List<T> m_list;

        public int Count
        {
            get { return this.m_list.Count; }
        }

        public ListReader(List<T> list)
        {
            this.m_list = list ?? ListReader<T>.Empty.m_list;
        }

        public static implicit operator ListReader<T>(List<T> list)
        {
            return new ListReader<T>(list);
        }

        public T ItemAt(int index)
        {
            return this.m_list[index];
        }

        public int IndexOf(T item)
        {
            return this.m_list.IndexOf(item);
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