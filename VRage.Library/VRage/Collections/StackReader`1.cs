// Decompiled with JetBrains decompiler
// Type: VRage.Collections.StackReader`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;
using System.Collections.Generic;

namespace VRage.Collections
{
    public struct StackReader<T> : IEnumerable<T>, IEnumerable
    {
        private readonly Stack<T> m_collection;

        public StackReader(Stack<T> collection)
        {
            this.m_collection = collection;
        }

        public Stack<T>.Enumerator GetEnumerator()
        {
            return this.m_collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>) this.GetEnumerator();
        }
    }
}