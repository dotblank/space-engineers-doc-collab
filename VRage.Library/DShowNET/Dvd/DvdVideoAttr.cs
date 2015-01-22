// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.DvdVideoAttr
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;

namespace DShowNET.Dvd
{
  [ComVisible(false)]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct DvdVideoAttr
  {
    public bool panscanPermitted;
    public bool letterboxPermitted;
    public int aspectX;
    public int aspectY;
    public int frameRate;
    public int frameHeight;
    public DvdVideoCompress compression;
    public bool line21Field1InGOP;
    public bool line21Field2InGOP;
    public int sourceResolutionX;
    public int sourceResolutionY;
    public bool isSourceLetterboxed;
    public bool isFilmMode;
  }
}
