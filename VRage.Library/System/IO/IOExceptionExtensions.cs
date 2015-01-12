// Decompiled with JetBrains decompiler
// Type: System.IO.IOExceptionExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
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