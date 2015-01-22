// Decompiled with JetBrains decompiler
// Type: DShowNET.IMediaEvent
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Guid("56a868b6-0ad4-11ce-b03a-0020af0ba770")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  [ComImport]
  public interface IMediaEvent
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetEventHandle(out IntPtr hEvent);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetEvent(out DsEvCode lEventCode, out int lParam1, out int lParam2, int msTimeout);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int WaitForCompletion(int msTimeout, out int pEvCode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CancelDefaultHandling(int lEvCode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RestoreDefaultHandling(int lEvCode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FreeEventParams(DsEvCode lEvCode, int lParam1, int lParam2);
  }
}
