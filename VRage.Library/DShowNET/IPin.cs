// Decompiled with JetBrains decompiler
// Type: DShowNET.IPin
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Guid("56a86891-0ad4-11ce-b03a-0020af0ba770")]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IPin
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Connect([In] IPin pReceivePin, [MarshalAs(UnmanagedType.LPStruct), In] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ReceiveConnection([In] IPin pReceivePin, [MarshalAs(UnmanagedType.LPStruct), In] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Disconnect();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ConnectedTo(out IPin ppPin);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ConnectionMediaType([MarshalAs(UnmanagedType.LPStruct), Out] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryPinInfo(IntPtr pInfo);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryDirection(out PinDirection pPinDir);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryId([MarshalAs(UnmanagedType.LPWStr)] out string Id);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryAccept([MarshalAs(UnmanagedType.LPStruct), In] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumMediaTypes(IntPtr ppEnum);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int QueryInternalConnections(IntPtr apPin, [In, Out] ref int nPin);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EndOfStream();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int BeginFlush();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EndFlush();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int NewSegment(long tStart, long tStop, double dRate);
  }
}
