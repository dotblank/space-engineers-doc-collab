// Decompiled with JetBrains decompiler
// Type: VRage.Stats.MyNameComparer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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