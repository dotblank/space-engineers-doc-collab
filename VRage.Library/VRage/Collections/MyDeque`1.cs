// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MyDeque`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Diagnostics;

namespace VRage.Collections
{
    [DebuggerDisplay("Count = {Count}")]
    public class MyDeque<T>
    {
        private T[] m_buffer;
        private int m_front;
        private int m_back;

        public bool Empty
        {
            get { return this.m_front == this.m_back; }
        }

        private bool Full
        {
            get { return (this.m_back + 1)%this.m_buffer.Length == this.m_front; }
        }

        public int Count
        {
            get { return this.m_back - this.m_front + (this.m_back < this.m_front ? this.m_buffer.Length : 0); }
        }

        public MyDeque(int baseCapacity = 8)
        {
            this.m_buffer = new T[baseCapacity + 1];
        }

        public void Clear()
        {
            Array.Clear((Array) this.m_buffer, 0, this.m_buffer.Length);
            this.m_front = 0;
            this.m_back = 0;
        }

        public void EnqueueFront(T value)
        {
            this.EnsureCapacityForOne();
            this.Decrement(ref this.m_front);
            this.m_buffer[this.m_front] = value;
        }

        public void EnqueueBack(T value)
        {
            this.EnsureCapacityForOne();
            this.m_buffer[this.m_back] = value;
            this.Increment(ref this.m_back);
        }

        public T DequeueFront()
        {
            T obj = this.m_buffer[this.m_front];
            this.m_buffer[this.m_front] = default (T);
            this.Increment(ref this.m_front);
            return obj;
        }

        public T DequeueBack()
        {
            this.Decrement(ref this.m_back);
            T obj = this.m_buffer[this.m_back];
            this.m_buffer[this.m_back] = default (T);
            return obj;
        }

        private void Increment(ref int index)
        {
            index = (index + 1)%this.m_buffer.Length;
        }

        private void Decrement(ref int index)
        {
            --index;
            if (index >= 0)
                return;
            index += this.m_buffer.Length;
        }

        private void EnsureCapacityForOne()
        {
            if (!this.Full)
                return;
            T[] objArray = new T[(this.m_buffer.Length - 1)*2 + 1];
            int num = 0;
            int index = this.m_front;
            while (index != this.m_back)
            {
                objArray[num++] = this.m_buffer[index];
                this.Increment(ref index);
            }
            this.m_buffer = objArray;
            this.m_front = 0;
            this.m_back = num;
        }
    }
}