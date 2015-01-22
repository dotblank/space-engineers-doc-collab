// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MyUniqueList`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;

namespace VRage.Collections
{
  public class MyUniqueList<T>
  {
    private List<T> m_list = new List<T>();
    private HashSet<T> m_hashSet = new HashSet<T>();

    public int Count
    {
      get
      {
        return this.m_list.Count;
      }
    }

    public T this[int index]
    {
      get
      {
        return this.m_list[index];
      }
    }

    public UniqueListReader<T> Items
    {
      get
      {
        return new UniqueListReader<T>(this);
      }
    }

    public ListReader<T> ItemList
    {
      get
      {
        return new ListReader<T>(this.m_list);
      }
    }

    public bool Add(T item)
    {
      if (!this.m_hashSet.Add(item))
        return false;
      this.m_list.Add(item);
      return true;
    }

    public bool Insert(int index, T item)
    {
      if (this.m_hashSet.Add(item))
      {
        this.m_list.Insert(index, item);
        return true;
      }
      else
      {
        this.m_list.Remove(item);
        this.m_list.Insert(index, item);
        return false;
      }
    }

    public bool Remove(T item)
    {
      if (!this.m_hashSet.Remove(item))
        return false;
      this.m_list.Remove(item);
      return true;
    }

    public void Clear()
    {
      this.m_list.Clear();
      this.m_hashSet.Clear();
    }

    public bool Contains(T item)
    {
      return this.m_hashSet.Contains(item);
    }

    public List<T>.Enumerator GetEnumerator()
    {
      return this.m_list.GetEnumerator();
    }
  }
}
