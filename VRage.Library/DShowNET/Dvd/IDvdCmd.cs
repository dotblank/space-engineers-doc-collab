// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.IDvdCmd
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET.Dvd
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  [Guid("5a4a97e4-94ee-4a55-9751-74b5643aa27d")]
  [ComImport]
  public interface IDvdCmd
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int WaitForStart();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int WaitForEnd();
  }
}
