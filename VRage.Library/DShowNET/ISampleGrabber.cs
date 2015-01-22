// Decompiled with JetBrains decompiler
// Type: DShowNET.ISampleGrabber
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("6B652FFF-11FE-4fce-92AD-0266B5D7C78F")]
  [ComVisible(true)]
  [ComImport]
  public interface ISampleGrabber
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetOneShot([MarshalAs(UnmanagedType.Bool), In] bool OneShot);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetMediaType([MarshalAs(UnmanagedType.LPStruct), In] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetConnectedMediaType([MarshalAs(UnmanagedType.LPStruct), Out] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetBufferSamples([MarshalAs(UnmanagedType.Bool), In] bool BufferThem);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentBuffer(ref int pBufferSize, IntPtr pBuffer);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentSample(IntPtr ppSample);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetCallback(ISampleGrabberCB pCallback, int WhichMethodToCallback);
  }
}
