// Decompiled with JetBrains decompiler
// Type: DShowNET.FilterInfo
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(false)]
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  public class FilterInfo
  {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string achName;
    [MarshalAs(UnmanagedType.IUnknown)]
    public object pUnk;
  }
}
