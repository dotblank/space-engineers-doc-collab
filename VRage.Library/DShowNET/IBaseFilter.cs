// Decompiled with JetBrains decompiler
// Type: DShowNET.IBaseFilter
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Guid("56a86895-0ad4-11ce-b03a-0020af0ba770")]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IBaseFilter
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetClassID(out Guid pClassID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Stop();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Pause();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Run(long tStart);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetState(int dwMilliSecsTimeout, out int filtState);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetSyncSource([In] IReferenceClock pClock);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSyncSource(out IReferenceClock pClock);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumPins(out IEnumPins ppEnum);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FindPin([MarshalAs(UnmanagedType.LPWStr), In] string Id, out IPin ppPin);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryFilterInfo([Out] FilterInfo pInfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int JoinFilterGraph([In] IFilterGraph pGraph, [MarshalAs(UnmanagedType.LPWStr), In] string pName);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryVendorInfo([MarshalAs(UnmanagedType.LPWStr)] out string pVendorInfo);
  }
}
