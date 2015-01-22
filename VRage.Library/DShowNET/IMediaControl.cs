﻿// Decompiled with JetBrains decompiler
// Type: DShowNET.IMediaControl
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  [Guid("56a868b1-0ad4-11ce-b03a-0020af0ba770")]
  [ComImport]
  public interface IMediaControl
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Run();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Pause();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Stop();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetState(int msTimeout, out int pfs);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RenderFile(string strFilename);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AddSourceFilter([In] string strFilename, [MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_FilterCollection([MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_RegFilterCollection([MarshalAs(UnmanagedType.IDispatch)] out object ppUnk);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int StopWhenReady();
  }
}
