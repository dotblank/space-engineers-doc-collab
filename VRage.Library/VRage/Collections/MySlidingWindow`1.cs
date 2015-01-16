// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MySlidingWindow`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace VRage.Collections
{
    public class MySlidingWindow<T>
    {
        private MyQueue<T> m_items;
        public int Size;
        public T DefaultValue;
        public Func<MyQueue<T>, T> AverageFunc;

        public T Average
        {
            get
            {
                if (this.m_items.Count == 0)
                    return this.DefaultValue;
                else
                    return this.AverageFunc(this.m_items);
            }
        }

        public T Last
        {
            get
            {
                if (this.m_items.Count <= 0)
                    return this.DefaultValue;
                else
                    return this.m_items[this.m_items.Count - 1];
            }
        }

        public MySlidingWindow(int size, Func<MyQueue<T>, T> avg, T defaultValue = default(T))
        {
            this.AverageFunc = avg;
            this.Size = size;
            this.DefaultValue = defaultValue;
            this.m_items = new MyQueue<T>(size + 1);
        }

        public void Add(T item)
        {
            this.m_items.Enqueue(item);
            this.RemoveExcess();
        }

        public void Clear()
        {
            this.m_items.Clear();
        }

        private void RemoveExcess()
        {
            while (this.m_items.Count > this.Size)
                this.m_items.Dequeue();
        }
    }
}