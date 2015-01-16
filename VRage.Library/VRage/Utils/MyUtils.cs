// Decompiled with JetBrains decompiler
// Type: VRage.Utils.MyUtils
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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