// Decompiled with JetBrains decompiler
// Type: DShowNET.VideoInfoHeader
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(false)]
  [StructLayout(LayoutKind.Sequential)]
  public class VideoInfoHeader
  {
    public DsRECT SrcRect;
    public DsRECT TagRect;
    public int BitRate;
    public int BitErrorRate;
    public long AvgTimePerFrame;
    public DsBITMAPINFOHEADER BmiHeader;
  }
}
