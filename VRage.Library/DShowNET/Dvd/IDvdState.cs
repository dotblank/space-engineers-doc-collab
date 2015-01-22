// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.IDvdState
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET.Dvd
{
  [Guid("86303d6d-1c4a-4087-ab42-f711167048ef")]
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface IDvdState
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDiscID(out long pullUniqueID);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetParentalLevel(out int pulParentalLevel);
  }
}
