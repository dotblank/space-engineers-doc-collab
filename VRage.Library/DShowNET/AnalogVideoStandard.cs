// Decompiled with JetBrains decompiler
// Type: DShowNET.AnalogVideoStandard
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(false)]
  [Flags]
  public enum AnalogVideoStandard
  {
    None = 0,
    NTSC_M = 1,
    NTSC_M_J = 2,
    NTSC_433 = 4,
    PAL_B = 16,
    PAL_D = 32,
    PAL_G = 64,
    PAL_H = 128,
    PAL_I = 256,
    PAL_M = 512,
    PAL_N = 1024,
    PAL_60 = 2048,
    SECAM_B = 4096,
    SECAM_D = 8192,
    SECAM_G = 16384,
    SECAM_H = 32768,
    SECAM_K = 65536,
    SECAM_K1 = 131072,
    SECAM_L = 262144,
    SECAM_L1 = 524288,
    PAL_N_COMBO = 1048576,
  }
}
