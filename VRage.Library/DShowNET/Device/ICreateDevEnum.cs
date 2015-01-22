// Decompiled with JetBrains decompiler
// Type: DShowNET.Device.ICreateDevEnum
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET.Device
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("29840822-5B84-11D0-BD3B-00A0C911CE86")]
  [ComVisible(true)]
  [ComImport]
  public interface ICreateDevEnum
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CreateClassEnumerator([In] ref Guid pType, out UCOMIEnumMoniker ppEnumMoniker, [In] int dwFlags);
  }
}
