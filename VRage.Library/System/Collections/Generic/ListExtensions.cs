// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.ListExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Collections.Generic
{
  public static class ListExtensions
  {
    private const bool USE_FAST_ADD_METHODS = true;

    public static ListExtensions.ClearToken<T> GetClearToken<T>(this List<T> list)
    {
      return new ListExtensions.ClearToken<T>()
      {
        List = list
      };
    }

    public static void RemoveAtFast<T>(this IList<T> list, int index)
    {
      int index1 = list.Count - 1;
      list[index] = list[index1];
      list.RemoveAt(index1);
    }

    public static T[] GetInternalArray<T>(this List<T> list)
    {
      return ListExtensions.ListInternalAccessor<T>.GetArray(list);
    }

    public static void AddArray<T>(this List<T> list, T[] itemsToAdd)
    {
      ListExtensions.AddArray<T>(list, itemsToAdd, itemsToAdd.Length);
    }

    public static void AddArray<T>(this List<T> list, T[] itemsToAdd, int itemCount)
    {
      if (list.Capacity < list.Count + itemCount)
        list.Capacity = list.Count + itemCount;
      Array.Copy((Array) itemsToAdd, 0, (Array) ListExtensions.GetInternalArray<T>(list), list.Count, itemCount);
      ListExtensions.ListInternalAccessor<T>.SetSize(list, list.Count + itemCount);
    }

    public static void SetSize<T>(this List<T> list, int newSize)
    {
      ListExtensions.ListInternalAccessor<T>.SetSize(list, newSize);
    }

    public static void AddList<T>(this List<T> list, List<T> itemsToAdd)
    {
      ListExtensions.AddArray<T>(list, ListExtensions.GetInternalArray<T>(itemsToAdd), itemsToAdd.Count);
    }

    public static void AddHashset<T>(this List<T> list, HashSet<T> hashset)
    {
      foreach (T obj in hashset)
        list.Add(obj);
    }

    public static void Move<T>(this List<T> list, int originalIndex, int targetIndex)
    {
      int num = Math.Sign(targetIndex - originalIndex);
      if (num == 0)
        return;
      T obj = list[originalIndex];
      int index = originalIndex;
      while (index != targetIndex)
      {
        list[index] = list[index + num];
        index += num;
      }
      list[targetIndex] = obj;
    }

    public static bool IsValidIndex<T>(this List<T> list, int index)
    {
      if (0 <= index)
        return index < list.Count;
      else
        return false;
    }

    public struct ClearToken<T> : IDisposable
    {
      public List<T> List;

      public void Dispose()
      {
        this.List.Clear();
      }
    }

    private static class ListInternalAccessor<T>
    {
      public static Func<List<T>, T[]> GetArray;
      public static Action<List<T>, int> SetSize;

      static ListInternalAccessor()
      {
        DynamicMethod dynamicMethod1 = new DynamicMethod("get", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, typeof (T[]), new Type[1]
        {
          typeof (List<T>)
        }, typeof (ListExtensions.ListInternalAccessor<T>), 1 != 0);
        ILGenerator ilGenerator1 = dynamicMethod1.GetILGenerator();
        ilGenerator1.Emit(OpCodes.Ldarg_0);
        ilGenerator1.Emit(OpCodes.Ldfld, typeof (List<T>).GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic));
        ilGenerator1.Emit(OpCodes.Ret);
        ListExtensions.ListInternalAccessor<T>.GetArray = (Func<List<T>, T[]>) dynamicMethod1.CreateDelegate(typeof (Func<List<T>, T[]>));
        DynamicMethod dynamicMethod2 = new DynamicMethod("set", MethodAttributes.Public | MethodAttributes.Static, CallingConventions.Standard, (Type) null, new Type[2]
        {
          typeof (List<T>),
          typeof (int)
        }, typeof (ListExtensions.ListInternalAccessor<T>), 1 != 0);
        ILGenerator ilGenerator2 = dynamicMethod2.GetILGenerator();
        ilGenerator2.Emit(OpCodes.Ldarg_0);
        ilGenerator2.Emit(OpCodes.Ldarg_1);
        ilGenerator2.Emit(OpCodes.Stfld, typeof (List<T>).GetField("_size", BindingFlags.Instance | BindingFlags.NonPublic));
        FieldInfo field = typeof (List<T>).GetField("_version", BindingFlags.Instance | BindingFlags.NonPublic);
        ilGenerator2.Emit(OpCodes.Ldarg_0);
        ilGenerator2.Emit(OpCodes.Dup);
        ilGenerator2.Emit(OpCodes.Ldfld, field);
        ilGenerator2.Emit(OpCodes.Ldc_I4_1);
        ilGenerator2.Emit(OpCodes.Add);
        ilGenerator2.Emit(OpCodes.Stfld, field);
        ilGenerator2.Emit(OpCodes.Ret);
        ListExtensions.ListInternalAccessor<T>.SetSize = (Action<List<T>, int>) dynamicMethod2.CreateDelegate(typeof (Action<List<T>, int>));
      }
    }
  }
}
