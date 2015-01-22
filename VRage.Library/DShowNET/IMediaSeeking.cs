// Decompiled with JetBrains decompiler
// Type: DShowNET.IMediaSeeking
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("36b73880-c2c8-11cf-8b46-00805f6cef60")]
  [ComImport]
  public interface IMediaSeeking
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCapabilities(out SeekingCapabilities pCapabilities);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CheckCapabilities([In, Out] ref SeekingCapabilities pCapabilities);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsFormatSupported([In] ref Guid pFormat);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryPreferredFormat(out Guid pFormat);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTimeFormat(out Guid pFormat);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsUsingTimeFormat([In] ref Guid pFormat);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetTimeFormat([In] ref Guid pFormat);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDuration(out long pDuration);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetStopPosition(out long pStop);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentPosition(out long pCurrent);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ConvertTimeFormat(out long pTarget, [In] ref Guid pTargetFormat, long Source, [In] ref Guid pSourceFormat);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetPositions([MarshalAs(UnmanagedType.LPStruct), In, Out] DsOptInt64 pCurrent, SeekingFlags dwCurrentFlags, [MarshalAs(UnmanagedType.LPStruct), In, Out] DsOptInt64 pStop, SeekingFlags dwStopFlags);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetPositions(out long pCurrent, out long pStop);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetAvailable(out long pEarliest, out long pLatest);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetRate(double dRate);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetRate(out double pdRate);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetPreroll(out long pllPreroll);
  }
}
