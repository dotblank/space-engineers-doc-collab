// Decompiled with JetBrains decompiler
// Type: VRage.Native.NativeMethod
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace VRage.Native
{
    public static class NativeMethod
    {
        public static unsafe IntPtr CalculateAddress(IntPtr instance, int methodOffset)
        {
            return *(IntPtr*) instance.ToPointer() + methodOffset*sizeof (void*);
        }
    }
}