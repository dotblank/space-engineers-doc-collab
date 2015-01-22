// Decompiled with JetBrains decompiler
// Type: VRage.MyTuple
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;

namespace VRage
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct MyTuple
  {
    public static MyTuple<T1> Create<T1>(T1 arg1)
    {
      return new MyTuple<T1>(arg1);
    }

    public static MyTuple<T1, T2> Create<T1, T2>(T1 arg1, T2 arg2)
    {
      return new MyTuple<T1, T2>(arg1, arg2);
    }

    public static MyTuple<T1, T2, T3> Create<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3)
    {
      return new MyTuple<T1, T2, T3>(arg1, arg2, arg3);
    }

    public static MyTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
      return new MyTuple<T1, T2, T3, T4>(arg1, arg2, arg3, arg4);
    }

    public static MyTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
      return new MyTuple<T1, T2, T3, T4, T5>(arg1, arg2, arg3, arg4, arg5);
    }

    public static MyTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
      return new MyTuple<T1, T2, T3, T4, T5, T6>(arg1, arg2, arg3, arg4, arg5, arg6);
    }
  }
}
