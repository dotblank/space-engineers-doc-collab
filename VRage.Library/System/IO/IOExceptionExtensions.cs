// Decompiled with JetBrains decompiler
// Type: System.IO.IOExceptionExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace System.IO
{
  public static class IOExceptionExtensions
  {
    public static bool IsFileLocked(this IOException e)
    {
      int num = Marshal.GetHRForException((Exception) e) & (int) ushort.MaxValue;
      if (num != 32)
        return num == 33;
      else
        return true;
    }
  }
}
