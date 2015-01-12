// Decompiled with JetBrains decompiler
// Type: VRage.Extensions.ArrayOfTypeEnumerator`3
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace VRage.Extensions
{
    public struct ArrayOfTypeEnumerator<T, TInner, TOfType> : IEnumerator<TOfType>, IDisposable, IEnumerator
        where TInner : struct, IEnumerator<T> where TOfType : T
    {
        private TInner m_inner;

        public TOfType Current
        {
            get { return (TOfType) (object) this.m_inner.Current; }
        }

        object IEnumerator.Current
        {
            get { return (object) this.m_inner.Current; }
        }

        public ArrayOfTypeEnumerator(TInner enumerator)
        {
            this.m_inner = enumerator;
        }

        public ArrayOfTypeEnumerator<T, TInner, TOfType> GetEnumerator()
        {
            return this;
        }

        public void Dispose()
        {
            this.m_inner.Dispose();
        }

        public bool MoveNext()
        {
            while (this.m_inner.MoveNext())
            {
                if ((object) this.m_inner.Current is TOfType)
                    return true;
            }
            return false;
        }

        public void Reset()
        {
            this.m_inner.Reset();
        }
    }
}