// Decompiled with JetBrains decompiler
// Type: VRage.Native.NativeMethod
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace VRage.Native
{
  public static class NativeMethod
  {
    public static unsafe IntPtr CalculateAddress(IntPtr instance, int methodOffset)
    {
      return *(IntPtr*) instance.ToPointer() + methodOffset * sizeof (void*);
    }
  }
}
