// Decompiled with JetBrains decompiler
// Type: DShowNET.IMediaSample
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Guid("56a8689a-0ad4-11ce-b03a-0020af0ba770")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [ComImport]
  public interface IMediaSample
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetPointer(out IntPtr ppBuffer);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSize();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTime(out long pTimeStart, out long pTimeEnd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetTime([MarshalAs(UnmanagedType.LPStruct), In] DsOptInt64 pTimeStart, [MarshalAs(UnmanagedType.LPStruct), In] DsOptInt64 pTimeEnd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsSyncPoint();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetSyncPoint([MarshalAs(UnmanagedType.Bool), In] bool bIsSyncPoint);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsPreroll();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetPreroll([MarshalAs(UnmanagedType.Bool), In] bool bIsPreroll);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetActualDataLength();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetActualDataLength(int len);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMediaType([MarshalAs(UnmanagedType.LPStruct)] out AMMediaType ppMediaType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetMediaType([MarshalAs(UnmanagedType.LPStruct), In] AMMediaType pMediaType);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDiscontinuity();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetDiscontinuity([MarshalAs(UnmanagedType.Bool), In] bool bDiscontinuity);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMediaTime(out long pTimeStart, out long pTimeEnd);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetMediaTime([MarshalAs(UnmanagedType.LPStruct), In] DsOptInt64 pTimeStart, [MarshalAs(UnmanagedType.LPStruct), In] DsOptInt64 pTimeEnd);
  }
}
