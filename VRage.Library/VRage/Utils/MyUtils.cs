// Decompiled with JetBrains decompiler
// Type: VRage.Utils.MyUtils
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VRage.Utils
{
  public class MyUtils
  {
    [DebuggerStepThrough]
    [Conditional("DEBUG")]
    public static void AssertBlittable<T>()
    {
      try
      {
        if ((object) default (T) == null)
          return;
        GCHandle.Alloc((object) default (T), GCHandleType.Pinned).Free();
      }
      catch
      {
      }
    }

    public static void ThrowNonBlittable<T>()
    {
      try
      {
        if ((object) default (T) == null)
          throw new InvalidOperationException("Class is never blittable");
        GCHandle.Alloc((object) default (T), GCHandleType.Pinned).Free();
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException("Type '" + (object) typeof (T) + "' is not blittable", ex);
      }
    }
  }
}
