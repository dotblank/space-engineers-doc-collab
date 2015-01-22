// Decompiled with JetBrains decompiler
// Type: DShowNET.SeekingFlags
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [Flags]
  [ComVisible(false)]
  public enum SeekingFlags
  {
    NoPositioning = 0,
    AbsolutePositioning = 1,
    RelativePositioning = 2,
    IncrementalPositioning = RelativePositioning | AbsolutePositioning,
    SeekToKeyFrame = 4,
    ReturnTime = 8,
    Segment = 16,
    NoFlush = 32,
  }
}
