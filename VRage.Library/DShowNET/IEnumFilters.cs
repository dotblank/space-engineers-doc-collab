// Decompiled with JetBrains decompiler
// Type: DShowNET.IEnumFilters
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Guid("56a86893-0ad4-11ce-b03a-0020af0ba770")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [ComImport]
  public interface IEnumFilters
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Next([In] int cFilters, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] out IBaseFilter[] ppFilter, out int pcFetched);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Skip([In] int cFilters);

    void Reset();

    void Clone(out IEnumFilters ppEnum);
  }
}
