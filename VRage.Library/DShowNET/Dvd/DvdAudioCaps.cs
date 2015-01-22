// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.DvdAudioCaps
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace DShowNET.Dvd
{
  [Flags]
  public enum DvdAudioCaps
  {
    Ac3 = 1,
    Mpeg2 = 2,
    Lpcm = 4,
    Dts = 8,
    Sdds = 16,
  }
}
