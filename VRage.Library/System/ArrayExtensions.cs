// Decompiled with JetBrains decompiler
// Type: System.ArrayExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using VRage.Extensions;

namespace System
{
  public static class ArrayExtensions
  {
    public static bool IsValidIndex<T>(this T[] self, int index)
    {
      return (uint) index < (uint) self.Length;
    }

    public static bool IsNullOrEmpty<T>(this T[] self)
    {
      if (self != null)
        return self.Length == 0;
      else
        return true;
    }

    public static bool TryGetValue<T>(this T[] self, int index, out T value)
    {
      if ((uint) index < (uint) self.Length)
      {
        value = self[index];
        return true;
      }
      else
      {
        value = default (T);
        return false;
      }
    }

    public static ArrayOfTypeEnumerator<TBase, ArrayEnumerator<TBase>, T> OfTypeFast<TBase, T>(this TBase[] array) where T : TBase
    {
      return new ArrayOfTypeEnumerator<TBase, ArrayEnumerator<TBase>, T>(new ArrayEnumerator<TBase>(array));
    }
  }
}
