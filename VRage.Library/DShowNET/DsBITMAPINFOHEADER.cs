// Decompiled with JetBrains decompiler
// Type: DShowNET.DsBITMAPINFOHEADER
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(false)]
  [StructLayout(LayoutKind.Sequential, Pack = 2)]
  public struct DsBITMAPINFOHEADER
  {
    public int Size;
    public int Width;
    public int Height;
    public short Planes;
    public short BitCount;
    public int Compression;
    public int ImageSize;
    public int XPelsPerMeter;
    public int YPelsPerMeter;
    public int ClrUsed;
    public int ClrImportant;
  }
}
