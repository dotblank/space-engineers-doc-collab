// Decompiled with JetBrains decompiler
// Type: VRage.InstanceComparer`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VRage
{
  public class InstanceComparer<T> : IEqualityComparer<T> where T : class
  {
    public static readonly InstanceComparer<T> Default = new InstanceComparer<T>();

    public bool Equals(T x, T y)
    {
      return object.ReferenceEquals((object) x, (object) y);
    }

    public int GetHashCode(T obj)
    {
      return RuntimeHelpers.GetHashCode((object) obj);
    }
  }
}
