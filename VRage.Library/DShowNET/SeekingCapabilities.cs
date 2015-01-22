// Decompiled with JetBrains decompiler
// Type: DShowNET.SeekingCapabilities
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Flags]
  [ComVisible(false)]
  public enum SeekingCapabilities
  {
    CanSeekAbsolute = 1,
    CanSeekForwards = 2,
    CanSeekBackwards = 4,
    CanGetCurrentPos = 8,
    CanGetStopPos = 16,
    CanGetDuration = 32,
    CanPlayBackwards = 64,
    CanDoSegments = 128,
    Source = 256,
  }
}
