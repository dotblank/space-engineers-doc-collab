// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.DvdCmdFlags
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace DShowNET.Dvd
{
  [Flags]
  public enum DvdCmdFlags
  {
    None = 0,
    Flush = 1,
    SendEvt = 2,
    Block = 4,
    StartWRendered = 8,
    EndARendered = 16,
  }
}
