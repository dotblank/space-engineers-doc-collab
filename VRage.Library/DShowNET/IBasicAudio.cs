// Decompiled with JetBrains decompiler
// Type: DShowNET.IBasicAudio
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("56a868b3-0ad4-11ce-b03a-0020af0ba770")]
  [ComImport]
  public interface IBasicAudio
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Volume(int lVolume);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Volume(out int plVolume);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Balance(int lBalance);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Balance(out int plBalance);
  }
}
