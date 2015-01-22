// Decompiled with JetBrains decompiler
// Type: DShowNET.IMediaPosition
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [ComVisible(true)]
  [Guid("56a868b2-0ad4-11ce-b03a-0020af0ba770")]
  [ComImport]
  public interface IMediaPosition
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Duration(out double pLength);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_CurrentPosition(double llTime);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_CurrentPosition(out double pllTime);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_StopTime(out double pllTime);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_StopTime(double llTime);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_PrerollTime(out double pllTime);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_PrerollTime(double llTime);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Rate(double dRate);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Rate(out double pdRate);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CanSeekForward(out int pCanSeekForward);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CanSeekBackward(out int pCanSeekBackward);
  }
}
