// Decompiled with JetBrains decompiler
// Type: ParallelTasks.Hashtable`2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace ParallelTasks
{
  public class Hashtable<Key, Data> : IEnumerable<KeyValuePair<Key, Data>>, IEnumerable
  {
    private static readonly EqualityComparer<Key> KeyComparer = EqualityComparer<Key>.Default;
    private static readonly Hashtable<Key, Data>.Node DeletedNode = new Hashtable<Key, Data>.Node()
    {
      Key = default (Key),
      Data = default (Data),
      Token = Hashtable<Key, Data>.Token.Deleted
    };
    private volatile Hashtable<Key, Data>.Node[] array;
    private SpinLock writeLock;

    public Hashtable(int initialCapacity)
    {
      if (initialCapacity < 1)
        throw new ArgumentOutOfRangeException("initialCapacity", "cannot be < 1");
      this.array = new Hashtable<Key, Data>.Node[initialCapacity];
      this.writeLock = new SpinLock();
    }

    public void Add(Key key, Data data)
    {
      try
      {
        this.writeLock.Enter();
        if (this.Insert(this.array, key, data))
          return;
        this.Resize();
        this.Insert(this.array, key, data);
      }
      finally
      {
        this.writeLock.Exit();
      }
    }

    private void Resize()
    {
      Hashtable<Key, Data>.Node[] table = new Hashtable<Key, Data>.Node[this.array.Length * 2];
      for (int index = 0; index < this.array.Length; ++index)
      {
        Hashtable<Key, Data>.Node node = this.array[index];
        if (node.Token == Hashtable<Key, Data>.Token.Used)
          this.Insert(table, node.Key, node.Data);
      }
      this.array = table;
    }

    private bool Insert(Hashtable<Key, Data>.Node[] table, Key key, Data data)
    {
      int num = Math.Abs(key.GetHashCode()) % table.Length;
      int index = num;
      bool flag = false;
      do
      {
        Hashtable<Key, Data>.Node node = table[index];
        if (node.Token == Hashtable<Key, Data>.Token.Empty || node.Token == Hashtable<Key, Data>.Token.Deleted || Hashtable<Key, Data>.KeyComparer.Equals(key, node.Key))
        {
          table[index] = new Hashtable<Key, Data>.Node()
          {
            Key = key,
            Data = data,
            Token = Hashtable<Key, Data>.Token.Used
          };
          flag = true;
          break;
        }
        else
          index = (index + 1) % table.Length;
      }
      while (index != num);
      return flag;
    }

    public void UnsafeSet(Key key, Data value)
    {
      bool flag = false;
      Hashtable<Key, Data>.Node[] nodeArray;
      do
      {
        nodeArray = this.array;
        int num = Math.Abs(key.GetHashCode()) % nodeArray.Length;
        int index = num;
        do
        {
          Hashtable<Key, Data>.Node node = nodeArray[index];
          if (Hashtable<Key, Data>.KeyComparer.Equals(key, node.Key))
          {
            nodeArray[index] = new Hashtable<Key, Data>.Node()
            {
              Key = key,
              Data = value,
              Token = Hashtable<Key, Data>.Token.Used
            };
            flag = true;
            break;
          }
          else
            index = (index + 1) % nodeArray.Length;
        }
        while (index != num);
      }
      while (nodeArray != this.array);
      if (flag)
        return;
      this.Add(key, value);
    }

    private bool Find(Key key, out Hashtable<Key, Data>.Node node)
    {
      node = new Hashtable<Key, Data>.Node();
      Hashtable<Key, Data>.Node[] nodeArray = this.array;
      int num = Math.Abs(key.GetHashCode()) % nodeArray.Length;
      int index = num;
      Hashtable<Key, Data>.Node node1;
      do
      {
        node1 = nodeArray[index];
        if (node1.Token == Hashtable<Key, Data>.Token.Empty)
          return false;
        if (node1.Token == Hashtable<Key, Data>.Token.Deleted || !Hashtable<Key, Data>.KeyComparer.Equals(key, node1.Key))
          index = (index + 1) % nodeArray.Length;
        else
          goto label_5;
      }
      while (index != num);
      goto label_6;
label_5:
      node = node1;
      return true;
label_6:
      return false;
    }

    public bool TryGet(Key key, out Data data)
    {
      Hashtable<Key, Data>.Node node;
      if (this.Find(key, out node))
      {
        data = node.Data;
        return true;
      }
      else
      {
        data = default (Data);
        return false;
      }
    }

    public void Remove(Key key)
    {
      try
      {
        this.writeLock.Enter();
        Hashtable<Key, Data>.Node[] nodeArray = this.array;
        int num = Math.Abs(key.GetHashCode()) % nodeArray.Length;
        int index = num;
        do
        {
          Hashtable<Key, Data>.Node node = nodeArray[index];
          if (node.Token != Hashtable<Key, Data>.Token.Empty)
          {
            if (node.Token == Hashtable<Key, Data>.Token.Deleted || !Hashtable<Key, Data>.KeyComparer.Equals(key, node.Key))
              index = (index + 1) % nodeArray.Length;
            else
              nodeArray[index] = Hashtable<Key, Data>.DeletedNode;
          }
          else
            goto label_7;
        }
        while (index != num);
        goto label_2;
label_7:
        return;
label_2:;
      }
      finally
      {
        this.writeLock.Exit();
      }
    }

    public IEnumerator<KeyValuePair<Key, Data>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<Key, Data>>) new Hashtable<Key, Data>.Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    private struct Node
    {
      public Key Key;
      public Data Data;
      public Hashtable<Key, Data>.Token Token;
    }

    private enum Token
    {
      Empty,
      Used,
      Deleted,
    }

    private class Enumerator : IEnumerator<KeyValuePair<Key, Data>>, IDisposable, IEnumerator
    {
      private int currentIndex = -1;
      private Hashtable<Key, Data> table;

      public KeyValuePair<Key, Data> Current { get; private set; }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.Current;
        }
      }

      public Enumerator(Hashtable<Key, Data> table)
      {
        this.table = table;
      }

      public void Dispose()
      {
      }

      public bool MoveNext()
      {
        Hashtable<Key, Data>.Node node;
        do
        {
          ++this.currentIndex;
          if (this.table.array.Length <= this.currentIndex)
            return false;
          node = this.table.array[this.currentIndex];
        }
        while (node.Token != Hashtable<Key, Data>.Token.Used);
        this.Current = new KeyValuePair<Key, Data>(node.Key, node.Data);
        return true;
      }

      public void Reset()
      {
        this.currentIndex = -1;
      }
    }
  }
}
