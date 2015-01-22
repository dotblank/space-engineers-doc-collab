// Decompiled with JetBrains decompiler
// Type: DShowNET.ICaptureGraphBuilder2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("93E5A4E0-2D50-11d2-ABFA-00A0C9C6E38D")]
  [ComVisible(true)]
  [ComImport]
  public interface ICaptureGraphBuilder2
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetFiltergraph([In] IGraphBuilder pfg);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetFiltergraph(out IGraphBuilder ppfg);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetOutputFileName([In] ref Guid pType, [MarshalAs(UnmanagedType.LPWStr), In] string lpstrFile, out IBaseFilter ppbf, out IFileSinkFilter ppSink);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FindInterface([In] ref Guid pCategory, [In] ref Guid pType, [In] IBaseFilter pbf, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppint);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RenderStream([In] ref Guid pCategory, [In] ref Guid pType, [MarshalAs(UnmanagedType.IUnknown), In] object pSource, [In] IBaseFilter pfCompressor, [In] IBaseFilter pfRenderer);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ControlStream([In] ref Guid pCategory, [In] ref Guid pType, [In] IBaseFilter pFilter, [In] IntPtr pstart, [In] IntPtr pstop, [In] short wStartCookie, [In] short wStopCookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AllocCapFile([MarshalAs(UnmanagedType.LPWStr), In] string lpstrFile, [In] long dwlSize);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CopyCaptureFile([MarshalAs(UnmanagedType.LPWStr), In] string lpwstrOld, [MarshalAs(UnmanagedType.LPWStr), In] string lpwstrNew, [In] int fAllowEscAbort, [In] IAMCopyCaptureFileProgress pFilter);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FindPin([In] object pSource, [In] int pindir, [In] ref Guid pCategory, [In] ref Guid pType, [MarshalAs(UnmanagedType.Bool), In] bool fUnconnected, [In] int num, out IPin ppPin);
  }
}
