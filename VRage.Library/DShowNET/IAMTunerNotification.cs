// Decompiled with JetBrains decompiler
// Type: DShowNET.IAMTunerNotification
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("211A8760-03AC-11d1-8D13-00AA00BD8339")]
  [ComImport]
  public interface IAMTunerNotification
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int OnEvent(AMTunerEventType Event);
  }
}
