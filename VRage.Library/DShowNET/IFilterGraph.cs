// Decompiled with JetBrains decompiler
// Type: DShowNET.IFilterGraph
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [Guid("56a8689f-0ad4-11ce-b03a-0020af0ba770")]
  [ComImport]
  public interface IFilterGraph
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AddFilter([In] IBaseFilter pFilter, [MarshalAs(UnmanagedType.LPWStr), In] string pName);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RemoveFilter([In] IBaseFilter pFilter);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int EnumFilters(out IEnumFilters ppEnum);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int FindFilterByName([MarshalAs(UnmanagedType.LPWStr), In] string pName, out IBaseFilter ppFilter);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ConnectDirect([In] IPin ppinOut, [In] IPin ppinIn, [MarshalAs(UnmanagedType.LPStruct), In] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Reconnect([In] IPin ppin);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Disconnect([In] IPin ppin);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetDefaultSyncSource();
  }
}
