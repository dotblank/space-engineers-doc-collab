// Decompiled with JetBrains decompiler
// Type: System.Linq.LinqExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;

namespace System.Linq
{
  public static class LinqExtensions
  {
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
      foreach (T obj in source)
        action(obj);
    }
  }
}
