// Decompiled with JetBrains decompiler
// Type: DShowNET.IAMStreamConfig
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(true)]
  [Guid("C6E13340-30AC-11d0-A18C-00A0C9118956")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IAMStreamConfig
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetFormat([MarshalAs(UnmanagedType.LPStruct), In] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetFormat([MarshalAs(UnmanagedType.LPStruct), Out] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetNumberOfCapabilities(out int piCount, out int piSize);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetStreamCaps(int iIndex, [MarshalAs(UnmanagedType.LPStruct)] out AMMediaType ppmt, IntPtr pSCC);
  }
}
