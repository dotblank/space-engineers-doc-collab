// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MySinglyLinkedList`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VRage.Collections
{
  public class MySinglyLinkedList<V> : IList<V>, ICollection<V>, IEnumerable<V>, IEnumerable
  {
    private MySinglyLinkedList<V>.Node m_rootNode;
    private MySinglyLinkedList<V>.Node m_lastNode;
    private int m_count;

    public V this[int index]
    {
      get
      {
        if (index < 0 || index >= this.m_count)
          throw new IndexOutOfRangeException();
        MySinglyLinkedList<V>.Enumerator enumerator = this.GetEnumerator();
        for (int index1 = -1; index1 < index; ++index1)
          enumerator.MoveNext();
        return enumerator.Current;
      }
      set
      {
        if (index < 0 || index >= this.m_count)
          throw new IndexOutOfRangeException();
        MySinglyLinkedList<V>.Enumerator enumerator = this.GetEnumerator();
        for (int index1 = -1; index1 < index; ++index1)
          enumerator.MoveNext();
        enumerator.m_currentNode.Data = value;
      }
    }

    public int Count
    {
      get
      {
        return this.m_count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public MySinglyLinkedList()
    {
      this.m_rootNode = (MySinglyLinkedList<V>.Node) null;
      this.m_lastNode = (MySinglyLinkedList<V>.Node) null;
      this.m_count = 0;
    }

    public int IndexOf(V item)
    {
      int num = 0;
      foreach (V v in this)
      {
        if (v.Equals((object) item))
          return num;
        ++num;
      }
      return -1;
    }

    public void Insert(int index, V item)
    {
      if (index < 0 || index > this.m_count)
        throw new IndexOutOfRangeException();
      if (index == 0)
        this.Prepend(item);
      else if (index == this.m_count)
      {
        this.Add(item);
      }
      else
      {
        MySinglyLinkedList<V>.Enumerator enumerator = this.GetEnumerator();
        for (int index1 = 0; index1 < index; ++index1)
          enumerator.MoveNext();
        MySinglyLinkedList<V>.Node node = new MySinglyLinkedList<V>.Node(enumerator.m_currentNode.Next, item);
        enumerator.m_currentNode.Next = node;
        ++this.m_count;
      }
    }

    public void RemoveAt(int index)
    {
      if (index < 0 || index >= this.m_count)
        throw new IndexOutOfRangeException();
      if (index == 0)
      {
        this.m_rootNode = this.m_rootNode.Next;
        --this.m_count;
        if (this.m_count != 0)
          return;
        this.m_lastNode = (MySinglyLinkedList<V>.Node) null;
      }
      else
      {
        MySinglyLinkedList<V>.Enumerator enumerator = this.GetEnumerator();
        for (int index1 = 0; index1 < index; ++index1)
          enumerator.MoveNext();
        enumerator.m_currentNode.Next = enumerator.m_currentNode.Next.Next;
        --this.m_count;
        if (this.m_count != index)
          return;
        this.m_lastNode = enumerator.m_currentNode;
      }
    }

    public MySinglyLinkedList<V> Split(MySinglyLinkedList<V>.Enumerator newLastPosition, int newCount = -1)
    {
      if (newCount == -1)
      {
        newCount = 1;
        for (MySinglyLinkedList<V>.Node node = this.m_rootNode; node != newLastPosition.m_currentNode; node = node.Next)
          ++newCount;
      }
      MySinglyLinkedList<V> singlyLinkedList = new MySinglyLinkedList<V>();
      singlyLinkedList.m_rootNode = newLastPosition.m_currentNode.Next;
      singlyLinkedList.m_lastNode = singlyLinkedList.m_rootNode == null ? (MySinglyLinkedList<V>.Node) null : this.m_lastNode;
      singlyLinkedList.m_count = this.m_count - newCount;
      this.m_lastNode = newLastPosition.m_currentNode;
      this.m_lastNode.Next = (MySinglyLinkedList<V>.Node) null;
      this.m_count = newCount;
      return singlyLinkedList;
    }

    public void Add(V item)
    {
      if (this.m_lastNode == null)
      {
        this.Prepend(item);
      }
      else
      {
        this.m_lastNode.Next = new MySinglyLinkedList<V>.Node((MySinglyLinkedList<V>.Node) null, item);
        ++this.m_count;
        this.m_lastNode = this.m_lastNode.Next;
      }
    }

    public void Append(V item)
    {
      this.Add(item);
    }

    public void Prepend(V item)
    {
      this.m_rootNode = new MySinglyLinkedList<V>.Node(this.m_rootNode, item);
      ++this.m_count;
      if (this.m_count != 1)
        return;
      this.m_lastNode = this.m_rootNode;
    }

    public void Merge(MySinglyLinkedList<V> otherList)
    {
      if (this.m_lastNode == null)
      {
        this.m_rootNode = otherList.m_rootNode;
        this.m_lastNode = otherList.m_lastNode;
      }
      else if (otherList.m_lastNode != null)
      {
        this.m_lastNode.Next = otherList.m_rootNode;
        this.m_lastNode = otherList.m_lastNode;
      }
      this.m_count += otherList.m_count;
      otherList.m_count = 0;
      otherList.m_lastNode = (MySinglyLinkedList<V>.Node) null;
      otherList.m_rootNode = (MySinglyLinkedList<V>.Node) null;
    }

    public V PopFirst()
    {
      if (this.m_count == 0)
        throw new InvalidOperationException();
      MySinglyLinkedList<V>.Node node = this.m_rootNode;
      if (node == this.m_lastNode)
        this.m_lastNode = (MySinglyLinkedList<V>.Node) null;
      this.m_rootNode = node.Next;
      --this.m_count;
      return node.Data;
    }

    public V First()
    {
      if (this.m_count == 0)
        throw new InvalidOperationException();
      else
        return this.m_rootNode.Data;
    }

    public V Last()
    {
      if (this.m_count == 0)
        throw new InvalidOperationException();
      else
        return this.m_lastNode.Data;
    }

    public void Clear()
    {
      this.m_rootNode = (MySinglyLinkedList<V>.Node) null;
      this.m_lastNode = (MySinglyLinkedList<V>.Node) null;
      this.m_count = 0;
    }

    public bool Contains(V item)
    {
      foreach (V v in this)
      {
        if (v.Equals((object) item))
          return true;
      }
      return false;
    }

    public void CopyTo(V[] array, int arrayIndex)
    {
      foreach (V v in this)
      {
        array[arrayIndex] = v;
        ++arrayIndex;
      }
    }

    public void Reverse()
    {
      
    }

    public void VerifyConsistency()
    {
      MySinglyLinkedList<V>.Node node1 = this.m_lastNode;
      MySinglyLinkedList<V>.Node node2 = this.m_rootNode;
      MySinglyLinkedList<V>.Node node3 = this.m_rootNode;
      MySinglyLinkedList<V>.Node node4 = this.m_lastNode;
      int num = 0;
      MySinglyLinkedList<V>.Node node5 = this.m_rootNode;
      while (node5 != null)
      {
        node5 = node5.Next;
        ++num;
      }
    }

    public bool Remove(V item)
    {
      MySinglyLinkedList<V>.Node node1 = this.m_rootNode;
      if (node1 == null)
        return false;
      if (this.m_rootNode.Data.Equals((object) item))
      {
        this.m_rootNode = this.m_rootNode.Next;
        --this.m_count;
        if (this.m_count == 0)
          this.m_lastNode = (MySinglyLinkedList<V>.Node) null;
        return true;
      }
      else
      {
        for (MySinglyLinkedList<V>.Node node2 = node1.Next; node2 != null; node2 = node2.Next)
        {
          if (node2.Data.Equals((object) item))
          {
            node1.Next = node2.Next;
            --this.m_count;
            if (node2 == this.m_lastNode)
              this.m_lastNode = node1;
            return true;
          }
          else
            node1 = node2;
        }
        return false;
      }
    }

    public MySinglyLinkedList<V>.Enumerator GetEnumerator()
    {
      return new MySinglyLinkedList<V>.Enumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new MySinglyLinkedList<V>.Enumerator(this);
    }

    IEnumerator<V> IEnumerable<V>.GetEnumerator()
    {
      return (IEnumerator<V>) new MySinglyLinkedList<V>.Enumerator(this);
    }

    internal class Node
    {
      public MySinglyLinkedList<V>.Node Next;
      public V Data;

      public Node(MySinglyLinkedList<V>.Node next, V data)
      {
        this.Next = next;
        this.Data = data;
      }
    }

    public struct Enumerator : IEnumerator<V>, IDisposable, IEnumerator
    {
      internal MySinglyLinkedList<V>.Node m_previousNode;
      internal MySinglyLinkedList<V>.Node m_currentNode;
      internal MySinglyLinkedList<V> m_list;

      public V Current
      {
        get
        {
          return this.m_currentNode.Data;
        }
      }

      public bool HasCurrent
      {
        get
        {
          return this.m_currentNode != null;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          return (object) this.m_currentNode.Data;
        }
      }

      public Enumerator(MySinglyLinkedList<V> parentList)
      {
        this.m_list = parentList;
        this.m_currentNode = (MySinglyLinkedList<V>.Node) null;
        this.m_previousNode = (MySinglyLinkedList<V>.Node) null;
      }

      public void Dispose()
      {
      }

      public bool MoveNext()
      {
        if (this.m_currentNode == null)
        {
          if (this.m_previousNode != null)
            return false;
          this.m_currentNode = this.m_list.m_rootNode;
          this.m_previousNode = (MySinglyLinkedList<V>.Node) null;
        }
        else
        {
          this.m_previousNode = this.m_currentNode;
          this.m_currentNode = this.m_currentNode.Next;
        }
        return this.m_currentNode != null;
      }

      public V RemoveCurrent()
      {
        if (this.m_currentNode == null)
          throw new InvalidOperationException();
        if (this.m_previousNode == null)
        {
          this.m_currentNode = this.m_currentNode.Next;
          return this.m_list.PopFirst();
        }
        else
        {
          this.m_previousNode.Next = this.m_currentNode.Next;
          if (this.m_list.m_lastNode == this.m_currentNode)
            this.m_list.m_lastNode = this.m_previousNode;
          MySinglyLinkedList<V>.Node node = this.m_currentNode;
          this.m_currentNode = this.m_currentNode.Next;
          --this.m_list.m_count;
          return node.Data;
        }
      }

      public void Reset()
      {
        this.m_currentNode = (MySinglyLinkedList<V>.Node) null;
        this.m_previousNode = (MySinglyLinkedList<V>.Node) null;
      }
    }
  }
}
