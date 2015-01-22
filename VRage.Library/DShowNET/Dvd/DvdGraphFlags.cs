// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.DvdGraphFlags
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace DShowNET.Dvd
{
  [Flags]
  public enum DvdGraphFlags
  {
    Default = 0,
    HwDecPrefer = 1,
    HwDecOnly = 2,
    SwDecPrefer = 4,
    SwDecOnly = 8,
    NoVpe = 256,
  }
}
