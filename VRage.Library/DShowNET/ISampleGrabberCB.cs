// Decompiled with JetBrains decompiler
// Type: DShowNET.ISampleGrabberCB
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Guid("0579154A-2B53-4994-B0D0-E773148EFF85")]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface ISampleGrabberCB
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SampleCB(double SampleTime, IMediaSample pSample);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen);
  }
}
