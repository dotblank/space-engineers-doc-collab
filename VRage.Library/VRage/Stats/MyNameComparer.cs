// Decompiled with JetBrains decompiler
// Type: VRage.Stats.MyNameComparer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;

namespace VRage.Stats
{
  internal class MyNameComparer : Comparer<KeyValuePair<string, MyStat>>
  {
    public override int Compare(KeyValuePair<string, MyStat> x, KeyValuePair<string, MyStat> y)
    {
      return x.Key.CompareTo(y.Key);
    }
  }
}
