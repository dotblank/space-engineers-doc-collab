// Decompiled with JetBrains decompiler
// Type: DShowNET.IReferenceClock
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Guid("56a86897-0ad4-11ce-b03a-0020af0ba770")]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IReferenceClock
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTime(out long pTime);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AdviseTime(long baseTime, long streamTime, IntPtr hEvent, out int pdwAdviseCookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AdvisePeriodic(long startTime, long periodTime, IntPtr hSemaphore, out int pdwAdviseCookie);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Unadvise(int dwAdviseCookie);
  }
}
