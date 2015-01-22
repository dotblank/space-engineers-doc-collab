// Decompiled with JetBrains decompiler
// Type: DShowNET.Device.IPropertyBag
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET.Device
{
  [ComVisible(true)]
  [Guid("55272A00-42CB-11CE-8135-00AA004BB851")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IPropertyBag
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Read([MarshalAs(UnmanagedType.LPWStr), In] string pszPropName, [MarshalAs(UnmanagedType.Struct), In, Out] ref object pVar, IntPtr pErrorLog);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Write([MarshalAs(UnmanagedType.LPWStr), In] string pszPropName, [MarshalAs(UnmanagedType.Struct), In] ref object pVar);
  }
}
