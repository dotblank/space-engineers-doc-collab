// Decompiled with JetBrains decompiler
// Type: DShowNET.IVideoFrameStep
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("e46a9787-2b71-444d-a4b5-1fab7b708d6a")]
  [ComImport]
  public interface IVideoFrameStep
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Step(int dwFrames, [MarshalAs(UnmanagedType.IUnknown), In] object pStepObject);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CanStep(int bMultiple, [MarshalAs(UnmanagedType.IUnknown), In] object pStepObject);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CancelStep();
  }
}
