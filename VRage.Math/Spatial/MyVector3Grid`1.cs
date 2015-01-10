// Decompiled with JetBrains decompiler
// Type: VRageMath.Spatial.MyVector3Grid`1
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 76578FE0-3A72-4D7F-8EAF-153F1DCC9FAC
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System.Collections.Generic;
using VRageMath;

namespace VRageMath.Spatial
{
  public class MyVector3Grid<T>
  {
    private float m_cellSize;
    private float m_divisor;
    private int m_nextFreeEntry;
    private List<MyVector3Grid<T>.Entry> m_storage;
    private Dictionary<Vector3I, int> m_bins;

    public MyVector3Grid(float cellSize)
    {
      this.m_cellSize = cellSize;
      this.m_divisor = 1f / this.m_cellSize;
      this.m_nextFreeEntry = 0;
      this.m_storage = new List<MyVector3Grid<T>.Entry>();
      this.m_bins = new Dictionary<Vector3I, int>();
    }

    public void AddPoint(ref Vector3 point, T data)
    {
      Vector3I key = Vector3I.Floor(point * this.m_divisor);
      int index1;
      if (!this.m_bins.TryGetValue(key, out index1))
      {
        int num = this.AddNewEntry(ref point, data);
        this.m_bins.Add(key, num);
      }
      else
      {
        MyVector3Grid<T>.Entry entry = this.m_storage[index1];
        for (int index2 = entry.NextEntry; index2 != -1; index2 = entry.NextEntry)
        {
          index1 = index2;
          entry = this.m_storage[index1];
        }
        int num = this.AddNewEntry(ref point, data);
        entry.NextEntry = num;
        this.m_storage[index1] = entry;
      }
    }

    public MyVector3Grid<T>.Enumerator GetPointsCloserThan(ref Vector3 point, float dist)
    {
      return new MyVector3Grid<T>.Enumerator(this, ref point, dist);
    }

    private int AddNewEntry(ref Vector3 point, T data)
    {
      if (this.m_nextFreeEntry == this.m_storage.Count)
      {
        this.m_storage.Add(new MyVector3Grid<T>.Entry()
        {
          Point = point,
          Data = data,
          NextEntry = -1
        });
        return this.m_nextFreeEntry++;
      }
      else
      {
        if ((long) (uint) this.m_nextFreeEntry > (long) this.m_storage.Count)
          return -1;
        int index = this.m_nextFreeEntry;
        this.m_nextFreeEntry = this.m_storage[this.m_nextFreeEntry].NextEntry;
        this.m_storage[index] = new MyVector3Grid<T>.Entry()
        {
          Point = point,
          Data = data,
          NextEntry = -1
        };
        return index;
      }
    }

    private struct Entry
    {
      public Vector3 Point;
      public T Data;
      public int NextEntry;
    }

    public struct Enumerator
    {
      private MyVector3Grid<T> m_parent;
      private Vector3 m_point;
      private float m_distSq;
      private int m_storageIndex;
      private Vector3I.RangeIterator m_rangeIterator;

      public T Current
      {
        get
        {
          return this.m_parent.m_storage[this.m_storageIndex].Data;
        }
      }

      public Enumerator(MyVector3Grid<T> parent, ref Vector3 point, float dist)
      {
        this.m_parent = parent;
        this.m_point = point;
        this.m_distSq = dist * dist;
        Vector3 vector3 = new Vector3(dist);
        Vector3I start = Vector3I.Floor((point - vector3) * parent.m_divisor);
        Vector3I end = Vector3I.Floor((point + vector3) * parent.m_divisor);
        this.m_rangeIterator = new Vector3I.RangeIterator(ref start, ref end);
        this.m_storageIndex = -1;
      }

      public bool MoveNext()
      {
        return false;
      }

      private bool FindNextNonemptyBin()
      {
        if (!this.m_rangeIterator.IsValid())
          return false;
        Vector3I next = this.m_rangeIterator.Current;
        while (!this.m_parent.m_bins.TryGetValue(next, out this.m_storageIndex))
        {
          this.m_rangeIterator.GetNext(out next);
          if (!this.m_rangeIterator.IsValid())
            return false;
        }
        return true;
      }
    }
  }
}
