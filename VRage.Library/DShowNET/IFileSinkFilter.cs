// Decompiled with JetBrains decompiler
// Type: DShowNET.IFileSinkFilter
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [Guid("a2104830-7c70-11cf-8bce-00aa00a3f1a6")]
  [ComImport]
  public interface IFileSinkFilter
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetFileName([MarshalAs(UnmanagedType.LPWStr), In] string pszFileName, [MarshalAs(UnmanagedType.LPStruct), In] AMMediaType pmt);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string pszFileName, [MarshalAs(UnmanagedType.LPStruct), Out] AMMediaType pmt);
  }
}
