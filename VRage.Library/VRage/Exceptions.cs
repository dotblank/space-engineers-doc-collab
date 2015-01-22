// Decompiled with JetBrains decompiler
// Type: VRage.Exceptions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Diagnostics;

namespace VRage
{
  public static class Exceptions
  {
    [DebuggerStepThrough]
    public static void ThrowIf<TException>(bool condition) where TException : Exception
    {
      if (condition)
        throw (Exception) Activator.CreateInstance(typeof (TException));
    }

    [DebuggerStepThrough]
    public static void ThrowIf<TException>(bool condition, string arg1) where TException : Exception
    {
      if (!condition)
        return;
      throw (Exception) Activator.CreateInstance(typeof (TException), new object[1]
      {
        (object) arg1
      });
    }

    [DebuggerStepThrough]
    public static void ThrowIf<TException>(bool condition, string arg1, string arg2) where TException : Exception
    {
      if (!condition)
        return;
      throw (Exception) Activator.CreateInstance(typeof (TException), new object[2]
      {
        (object) arg1,
        (object) arg2
      });
    }

    [DebuggerStepThrough]
    public static void ThrowIf<TException>(bool condition, params object[] args) where TException : Exception
    {
      if (condition)
        throw (Exception) Activator.CreateInstance(typeof (TException), args);
    }

    [DebuggerStepThrough]
    public static void ThrowAny<TException>(bool[] conditions, params object[] args) where TException : Exception
    {

    }

    [DebuggerStepThrough]
    public static void ThrowAll<TException>(bool[] conditions, params object[] args) where TException : Exception
    {

    }
  }
}
