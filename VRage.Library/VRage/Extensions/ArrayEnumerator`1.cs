// Decompiled with JetBrains decompiler
// Type: VRage.Extensions.ArrayEnumerator`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VRage.Extensions
{
    public struct ArrayEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator
    {
        private T[] m_array;
        private int m_currentIndex;

        public T Current
        {
            get { return this.m_array[this.m_currentIndex]; }
        }

        object IEnumerator.Current
        {
            get { return (object) this.Current; }
        }

        public ArrayEnumerator(T[] array)
        {
            this.m_array = array;
            this.m_currentIndex = -1;
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            ++this.m_currentIndex;
            return this.m_currentIndex < this.m_array.Length;
        }

        public void Reset()
        {
            this.m_currentIndex = -1;
        }
    }
}