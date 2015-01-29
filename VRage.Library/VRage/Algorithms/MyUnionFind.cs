// Decompiled with JetBrains decompiler
// Type: VRage.Algorithms.MyUnionFind
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F987C912-6032-4943-850E-69DEE0217B30
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;

namespace VRage.Algorithms
{
  public class MyUnionFind
  {
    private List<int> m_indices;

    public int MakeSet()
    {
      int count = this.m_indices.Count;
      this.m_indices.Add(count);
      return count;
    }

    public void Union(int a, int b)
    {
      this.m_indices[this.Find(a)] = this.Find(b);
    }

    public int Find(int a)
    {
      if (this.m_indices[a] != a)
        this.m_indices[a] = this.Find(this.m_indices[a]);
      return this.m_indices[a];
    }
  }
}
