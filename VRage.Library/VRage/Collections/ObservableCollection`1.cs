// Decompiled with JetBrains decompiler
// Type: VRage.Collections.ObservableCollection`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace VRage.Collections
{
    public class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>
  {
    protected override void ClearItems()
    {
      this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (IList) this));
      base.ClearItems();
    }

    public ObservableCollection<T>.Enumerator GetEnumerator()
    {
      return new ObservableCollection<T>.Enumerator(this);
    }

    public int FindIndex(Predicate<T> match)
    {
      int num = -1;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        if (match(this.Items[index]))
        {
          num = index;
          break;
        }
      }
      return num;
    }

    public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private ObservableCollection<T> m_collection;
      private int m_index;

      public T Current
      {
        get
        {
          return this.m_collection[this.m_index];
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      public Enumerator(ObservableCollection<T> collection)
      {
        this.m_index = -1;
        this.m_collection = collection;
      }

      public void Dispose()
      {
      }

      public bool MoveNext()
      {
        ++this.m_index;
        return this.m_index < this.m_collection.Count;
      }

      public void Reset()
      {
        this.m_index = -1;
      }
    }
  }
}
