// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.IDvdGraphBuilder
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using DShowNET;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET.Dvd
{
  [ComVisible(true)]
  [Guid("FCC152B6-F372-11d0-8E00-00C04FD7C08B")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IDvdGraphBuilder
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetFiltergraph(out IGraphBuilder ppGB);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDvdInterface([In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvIF);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RenderDvdVideoVolume([MarshalAs(UnmanagedType.LPWStr), In] string lpcwszPathName, DvdGraphFlags dwFlags, out DvdRenderStatus pStatus);
  }
}
