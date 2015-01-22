// Decompiled with JetBrains decompiler
// Type: DShowNET.IAMCollection
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Guid("56a868b9-0ad4-11ce-b03a-0020af0ba770")]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComImport]
  public interface IAMCollection
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Count(out int plCount);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Item(int lItem, [MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_NewEnum([MarshalAs(UnmanagedType.IUnknown)] out object ppUnk);
  }
}
