// Decompiled with JetBrains decompiler
// Type: VRage.Stats.MyStatTypeEnum
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace VRage.Stats
{
  public enum MyStatTypeEnum : byte
  {
    Unset = (byte) 0,
    CurrentValue = (byte) 1,
    Min = (byte) 2,
    Max = (byte) 3,
    Avg = (byte) 4,
    MinMax = (byte) 5,
    MinMaxAvg = (byte) 6,
    Sum = (byte) 7,
    Counter = (byte) 8,
    CounterSum = (byte) 9,
    DontDisappearFlag = (byte) 16,
    KeepInactiveLongerFlag = (byte) 32,
    LongFlag = (byte) 64,
    FormatFlag = (byte) 128,
    AllFlags = (byte) 240,
  }
}
