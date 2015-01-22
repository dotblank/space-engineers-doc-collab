// Decompiled with JetBrains decompiler
// Type: DShowNET.IGraphBuilder
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(true)]
  [Guid("56a868a9-0ad4-11ce-b03a-0020af0ba770")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IGraphBuilder
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AddFilter([In] IBaseFilter pFilter, [MarshalAs(UnmanagedType.LPWStr), In] string pName);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RemoveFilter([In] IBaseFilter pFilter);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumFilters(out IEnumFilters ppEnum);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FindFilterByName([MarshalAs(UnmanagedType.LPWStr), In] string pName, out IBaseFilter ppFilter);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ConnectDirect([In] IPin ppinOut, [In] IPin ppinIn, [MarshalAs(UnmanagedType.LPStruct), In] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Reconnect([In] IPin ppin);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Disconnect([In] IPin ppin);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetDefaultSyncSource();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Connect([In] IPin ppinOut, [In] IPin ppinIn);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Render([In] IPin ppinOut);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RenderFile([MarshalAs(UnmanagedType.LPWStr), In] string lpcwstrFile, [MarshalAs(UnmanagedType.LPWStr), In] string lpcwstrPlayList);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AddSourceFilter([MarshalAs(UnmanagedType.LPWStr), In] string lpcwstrFileName, [MarshalAs(UnmanagedType.LPWStr), In] string lpcwstrFilterName, out IBaseFilter ppFilter);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetLogFile(IntPtr hFile);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Abort();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ShouldOperationContinue();
  }
}
