// Decompiled with JetBrains decompiler
// Type: DShowNET.IPersist
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Guid("0000010c-0000-0000-C000-000000000046")]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IPersist
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetClassID(out Guid pClassID);
  }
}
