// Decompiled with JetBrains decompiler
// Type: VRage.Collections.HeapItem`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace VRage.Collections
{
    public abstract class HeapItem<K>
    {
        public int HeapIndex { get; internal set; }

        public K HeapKey { get; internal set; }
    }
}