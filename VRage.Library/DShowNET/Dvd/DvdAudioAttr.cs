// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.DvdAudioAttr
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;

namespace DShowNET.Dvd
{
  [ComVisible(false)]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct DvdAudioAttr
  {
    public DvdAudioAppMode appMode;
    public int appModeData;
    public DvdAudioFormat audioFormat;
    public int language;
    public DvdAudioLangExt languageExtension;
    public bool hasMultichannelInfo;
    public int frequency;
    public byte quantization;
    public byte numberOfChannels;
    public short dummy;
    public int res1;
    public int res2;
  }
}
