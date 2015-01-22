// Decompiled with JetBrains decompiler
// Type: DShowNET.DsBugWO
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Services;

namespace DShowNET
{
  public class DsBugWO
  {
    public static object CreateDsInstance(ref Guid clsid, ref Guid riid)
    {
      IntPtr ptrIf;
      int instance = DsBugWO.CoCreateInstance(ref clsid, IntPtr.Zero, CLSCTX.Inproc, ref riid, out ptrIf);
      if (instance != 0 || ptrIf == IntPtr.Zero)
        Marshal.ThrowExceptionForHR(instance);
      Guid iid = new Guid("00000000-0000-0000-C000-000000000046");
      IntPtr ppv;
      Marshal.QueryInterface(ptrIf, ref iid, out ppv);
      object obj = EnterpriseServicesHelper.WrapIUnknownWithComObject(ptrIf);
      Marshal.Release(ptrIf);
      return obj;
    }

    [DllImport("ole32.dll")]
    private static extern int CoCreateInstance(ref Guid clsid, IntPtr pUnkOuter, CLSCTX dwClsContext, ref Guid iid, out IntPtr ptrIf);
  }
}
