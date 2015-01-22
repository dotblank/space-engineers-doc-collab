// Decompiled with JetBrains decompiler
// Type: DShowNET.ISpecifyPropertyPages
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(true)]
  [Guid("B196B28B-BAB4-101A-B69C-00AA00341D07")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface ISpecifyPropertyPages
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetPages(out DsCAUUID pPages);
  }
}
