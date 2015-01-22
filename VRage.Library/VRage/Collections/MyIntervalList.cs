// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MyIntervalList
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;

namespace VRage.Collections
{
  public class MyIntervalList
  {
    private List<int> m_list;

    public int IntervalCount
    {
      get
      {
        return this.m_list.Count / 2;
      }
    }

    public MyIntervalList()
    {
      this.m_list = new List<int>(8);
    }

    public override string ToString()
    {
      string str = "";
      int index = 0;
      while (index < this.m_list.Count)
      {
        if (index != 0)
          str = str + "; ";
        str = str + (object) "<" + (string) (object) this.m_list[index] + "," + (string) (object) this.m_list[index + 1] + ">";
        index += 2;
      }
      return str;
    }

    public void Add(int value)
    {
      if (value == int.MinValue)
      {
        if (this.m_list.Count == 0)
          this.InsertInterval(0, value, value);
        else if (this.m_list[0] == -2147483647)
        {
          this.ExtendIntervalDown(0);
        }
        else
        {
          if (this.m_list[0] == int.MinValue)
            return;
          this.InsertInterval(0, value, value);
        }
      }
      else if (value == int.MaxValue)
      {
        int i = this.m_list.Count - 2;
        if (i < 0)
          this.InsertInterval(0, value, value);
        else if (this.m_list[i + 1] == 2147483646)
        {
          this.ExtendIntervalUp(i);
        }
        else
        {
          if (this.m_list[i + 1] == int.MaxValue)
            return;
          this.InsertInterval(this.m_list.Count, value, value);
        }
      }
      else
      {
        int index = 0;
        while (index < this.m_list.Count)
        {
          if (value + 1 < this.m_list[index])
          {
            this.InsertInterval(index, value, value);
            return;
          }
          else
          {
            if (value - 1 <= this.m_list[index + 1])
            {
              if (value + 1 == this.m_list[index])
              {
                this.ExtendIntervalDown(index);
                return;
              }
              else if (value - 1 == this.m_list[index + 1])
              {
                this.ExtendIntervalUp(index);
                return;
              }
            }
            index += 2;
          }
        }
        this.InsertInterval(this.m_list.Count, value, value);
      }
    }

    public void Clear()
    {
      this.m_list.Clear();
    }

    public bool Contains(int value)
    {
      int index = 0;
      while (index < this.m_list.Count && value >= this.m_list[index])
      {
        if (value <= this.m_list[index + 1])
          return true;
        index += 2;
      }
      return false;
    }

    public MyIntervalList.Enumerator GetEnumerator()
    {
      return new MyIntervalList.Enumerator(this);
    }

    private void InsertInterval(int listPosition, int min, int max)
    {
      if (listPosition == this.m_list.Count)
      {
        this.m_list.Add(min);
        this.m_list.Add(max);
      }
      else
      {
        int index = this.m_list.Count - 2;
        this.m_list.Add(this.m_list[index]);
        this.m_list.Add(this.m_list[index + 1]);
        while (index > listPosition)
        {
          this.m_list[index] = this.m_list[index - 2];
          this.m_list[index + 1] = this.m_list[index - 1];
          index -= 2;
        }
        this.m_list[index] = min;
        this.m_list[index + 1] = max;
      }
    }

    private void ExtendIntervalDown(int i)
    {
      List<int> list;
      int index;
      (list = this.m_list)[index = i] = list[index] - 1;
      if (i == 0)
        return;
      this.TryMergeIntervals(i - 1, i);
    }

    private void ExtendIntervalUp(int i)
    {
      List<int> list;
      int index;
      (list = this.m_list)[index = i + 1] = list[index] + 1;
      if (i >= this.m_list.Count - 2)
        return;
      this.TryMergeIntervals(i + 1, i + 2);
    }

    private void TryMergeIntervals(int i1, int i2)
    {
      if (this.m_list[i1] + 1 != this.m_list[i2])
        return;
      for (int index = i1; index < this.m_list.Count - 2; ++index)
        this.m_list[index] = this.m_list[index + 2];
      this.m_list.RemoveAt(this.m_list.Count - 1);
      this.m_list.RemoveAt(this.m_list.Count - 1);
    }

    public struct Enumerator
    {
      private int m_interval;
      private int m_dist;
      private int m_lowerBound;
      private int m_upperBound;
      private MyIntervalList m_parent;

      public int Current
      {
        get
        {
          return this.m_lowerBound + this.m_dist;
        }
      }

      public Enumerator(MyIntervalList parent)
      {
        this.m_interval = -1;
        this.m_dist = 0;
        this.m_lowerBound = 0;
        this.m_upperBound = 0;
        this.m_parent = parent;
      }

      public bool MoveNext()
      {
        if (this.m_interval == -1 || this.m_lowerBound + this.m_dist >= this.m_upperBound)
          return this.MoveNextInterval();
        ++this.m_dist;
        return true;
      }

      private bool MoveNextInterval()
      {
        ++this.m_interval;
        if (this.m_interval >= this.m_parent.IntervalCount)
          return false;
        this.m_dist = 0;
        this.m_lowerBound = this.m_parent.m_list[this.m_interval * 2];
        this.m_upperBound = this.m_parent.m_list[this.m_interval * 2 + 1];
        return true;
      }
    }
  }
}
