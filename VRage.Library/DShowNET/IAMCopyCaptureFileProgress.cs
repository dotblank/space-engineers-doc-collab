// Decompiled with JetBrains decompiler
// Type: DShowNET.IAMCopyCaptureFileProgress
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("670d1d20-a068-11d0-b3f0-00aa003761c5")]
  [ComVisible(true)]
  [ComImport]
  public interface IAMCopyCaptureFileProgress
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Progress(int iProgress);
  }
}
