// Decompiled with JetBrains decompiler
// Type: VRage.Extensions.ArrayEnumerable`2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;

namespace VRage.Extensions
{
  public struct ArrayEnumerable<T, TEnumerator> : IEnumerable<T>, IEnumerable where TEnumerator : struct, IEnumerator<T>
  {
    private TEnumerator m_enumerator;

    public ArrayEnumerable(TEnumerator enumerator)
    {
      this.m_enumerator = enumerator;
    }

    public TEnumerator GetEnumerator()
    {
      return this.m_enumerator;
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return (IEnumerator<T>) this.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }
  }
}
