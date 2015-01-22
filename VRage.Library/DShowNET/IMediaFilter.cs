// Decompiled with JetBrains decompiler
// Type: DShowNET.IMediaFilter
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(true)]
  [Guid("56a86899-0ad4-11ce-b03a-0020af0ba770")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IMediaFilter
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetClassID(out Guid pClassID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Stop();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Pause();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Run(long tStart);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetState(int dwMilliSecsTimeout, out int filtState);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetSyncSource([In] IReferenceClock pClock);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSyncSource(out IReferenceClock pClock);
  }
}
