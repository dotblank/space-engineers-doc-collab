// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.HashsetExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace System.Collections.Generic
{
    public static class HashsetExtensions
    {
        public static T GetFirstElement<T>(this HashSet<T> hashset)
        {
            HashSet<T>.Enumerator enumerator = hashset.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current;
        }
    }
}