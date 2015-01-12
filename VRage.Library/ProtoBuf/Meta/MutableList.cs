// Decompiled with JetBrains decompiler
// Type: ProtoBuf.Meta.MutableList
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

namespace ProtoBuf.Meta
{
    internal sealed class MutableList : BasicList
    {
        public new object this[int index]
        {
            get { return this.head[index]; }
            set { this.head[index] = value; }
        }

        public void RemoveLast()
        {
            this.head.RemoveLastWithMutate();
        }
    }
}