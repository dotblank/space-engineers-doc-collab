// Decompiled with JetBrains decompiler
// Type: VRageMath.CurveKeyCollection
// Assembly: VRage.Math, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35FD5618-DF34-49B8-BA9B-FE095A7EFE3B
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Math.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace VRageMath
{
  [TypeConverter(typeof (ExpandableObjectConverter))]
  [Serializable]
  public class CurveKeyCollection : ICollection<CurveKey>, IEnumerable<CurveKey>, IEnumerable
  {
    private List<CurveKey> Keys = new List<CurveKey>();
    internal bool IsCacheAvailable = true;
    internal float TimeRange;
    internal float InvTimeRange;

    public CurveKey this[int index]
    {
      get
      {
        return this.Keys[index];
      }
      set
      {
        if (value == (CurveKey) null)
          throw new ArgumentNullException();
        if ((double) this.Keys[index].Position == (double) value.Position)
        {
          this.Keys[index] = value;
        }
        else
        {
          this.Keys.RemoveAt(index);
          this.Add(value);
        }
      }
    }

    public int Count
    {
      get
      {
        return this.Keys.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    public int IndexOf(CurveKey item)
    {
      return this.Keys.IndexOf(item);
    }

    public void RemoveAt(int index)
    {
      this.Keys.RemoveAt(index);
      this.IsCacheAvailable = false;
    }

    public void Add(CurveKey item)
    {
      if (item == (CurveKey) null)
        throw new ArgumentNullException();
      int index = this.Keys.BinarySearch(item);
      if (index >= 0)
      {
        while (index < this.Keys.Count && (double) item.Position == (double) this.Keys[index].Position)
          ++index;
      }
      else
        index = ~index;
      this.Keys.Insert(index, item);
      this.IsCacheAvailable = false;
    }

    public void Clear()
    {
      this.Keys.Clear();
      this.TimeRange = this.InvTimeRange = 0.0f;
      this.IsCacheAvailable = false;
    }

    public bool Contains(CurveKey item)
    {
      return this.Keys.Contains(item);
    }

    public void CopyTo(CurveKey[] array, int arrayIndex)
    {
      this.Keys.CopyTo(array, arrayIndex);
      this.IsCacheAvailable = false;
    }

    public bool Remove(CurveKey item)
    {
      this.IsCacheAvailable = false;
      return this.Keys.Remove(item);
    }

    public IEnumerator<CurveKey> GetEnumerator()
    {
      return (IEnumerator<CurveKey>) this.Keys.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.Keys.GetEnumerator();
    }

    public CurveKeyCollection Clone()
    {
      return new CurveKeyCollection()
      {
        Keys = new List<CurveKey>((IEnumerable<CurveKey>) this.Keys),
        InvTimeRange = this.InvTimeRange,
        TimeRange = this.TimeRange,
        IsCacheAvailable = true
      };
    }

    internal void ComputeCacheValues()
    {
      this.TimeRange = this.InvTimeRange = 0.0f;
      if (this.Keys.Count > 1)
      {
        this.TimeRange = this.Keys[this.Keys.Count - 1].Position - this.Keys[0].Position;
        if ((double) this.TimeRange > 1.40129846432482E-45)
          this.InvTimeRange = 1f / this.TimeRange;
      }
      this.IsCacheAvailable = true;
    }
  }
}
