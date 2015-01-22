// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.HashsetExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace System.Collections.Generic
{
  public static class HashsetExtensions
  {
    public static T GetFirstElement<T>(this HashSet<T> hashset)
    {
      HashSet<T>.Enumerator enumerator = hashset.GetEnumerator();
      enumerator.MoveNext();
      return enumerator.Current;
    }
  }
}
