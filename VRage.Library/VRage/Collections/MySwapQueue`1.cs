// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MySwapQueue`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Threading;

namespace VRage.Collections
{
    public class MySwapQueue<T> where T : class
    {
        private T m_read;
        private T m_write;
        private T m_waitingData;
        private T m_unusedData;

        public T Read
        {
            get { return this.m_read; }
        }

        public T Write
        {
            get { return this.m_write; }
        }

        public MySwapQueue(Func<T> factoryMethod)
            : this(factoryMethod(), factoryMethod(), factoryMethod())
        {
        }

        public MySwapQueue(T first, T second, T third)
        {
            this.m_read = first;
            this.m_write = second;
            this.m_unusedData = third;
            this.m_waitingData = default (T);
        }

        public bool RefreshRead()
        {
            if ((object) Interlocked.CompareExchange<T>(ref this.m_unusedData, this.m_read, default (T)) != null)
                return false;
            this.m_read = Interlocked.Exchange<T>(ref this.m_waitingData, default (T));
            return true;
        }

        public void CommitWrite()
        {
            this.m_write = Interlocked.Exchange<T>(ref this.m_waitingData, this.m_write);
            if ((object) this.m_write != null)
                return;
            this.m_write = Interlocked.Exchange<T>(ref this.m_unusedData, default (T));
        }
    }
}