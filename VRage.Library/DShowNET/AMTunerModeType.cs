// Decompiled with JetBrains decompiler
// Type: DShowNET.AMTunerModeType
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(false)]
  [Flags]
  public enum AMTunerModeType
  {
    Default = 0,
    TV = 1,
    FMRadio = 2,
    AMRadio = 4,
    Dss = 8,
  }
}
