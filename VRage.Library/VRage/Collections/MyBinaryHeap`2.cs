// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MyBinaryHeap`2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;

namespace VRage.Collections
{
    public class MyBinaryHeap<K, V> where V : HeapItem<K>
    {
        private HeapItem<K>[] m_array;
        private int m_count;
        private int m_capacity;
        private IComparer<K> m_comparer;

        public int Count
        {
            get { return this.m_count; }
        }

        public MyBinaryHeap(int initialCapacity = 128, IComparer<K> comparer = null)
        {
            this.m_array = new HeapItem<K>[initialCapacity];
            this.m_count = 0;
            this.m_capacity = initialCapacity;
            this.m_comparer = comparer ?? (IComparer<K>) Comparer<K>.Default;
        }

        public void Insert(V value, K key)
        {
            if (this.m_count == this.m_capacity)
                this.Reallocate();
            value.HeapKey = key;
            this.MoveItem((HeapItem<K>) value, this.m_count);
            this.Up(this.m_count);
            ++this.m_count;
        }

        public V Min()
        {
            return (V) this.m_array[0];
        }

        public V RemoveMin()
        {
            V v = (V) this.m_array[0];
            if (this.m_count != 1)
            {
                this.MoveItem(this.m_count - 1, 0);
                this.m_array[this.m_count - 1] = (HeapItem<K>) null;
                --this.m_count;
                this.Down(0);
            }
            else
                --this.m_count;
            return v;
        }

        public void ModifyUp(V item, K newKey)
        {
            item.HeapKey = newKey;
            this.Up(item.HeapIndex);
        }

        public void ModifyDown(V item, K newKey)
        {
            item.HeapKey = newKey;
            this.Down(item.HeapIndex);
        }

        public void Clear()
        {
            for (int index = 0; index < this.m_count; ++index)
                this.m_array[index] = (HeapItem<K>) null;
            this.m_count = 0;
        }

        private void Up(int index)
        {
            if (index == 0)
                return;
            int fromIndex = (index - 1)/2;
            if (this.m_comparer.Compare(this.m_array[fromIndex].HeapKey, this.m_array[index].HeapKey) <= 0)
                return;
            HeapItem<K> fromItem = this.m_array[index];
            do
            {
                this.MoveItem(fromIndex, index);
                index = fromIndex;
                if (index != 0)
                    fromIndex = (index - 1)/2;
                else
                    break;
            } while (this.m_comparer.Compare(this.m_array[fromIndex].HeapKey, fromItem.HeapKey) > 0);
            this.MoveItem(fromItem, index);
        }

        private void Down(int index)
        {
            if (this.m_count == index + 1)
                return;
            int fromIndex1 = index*2 + 1;
            int fromIndex2 = fromIndex1 + 1;
            HeapItem<K> fromItem = this.m_array[index];
            while (fromIndex2 <= this.m_count)
            {
                if (fromIndex2 == this.m_count ||
                    this.m_comparer.Compare(this.m_array[fromIndex1].HeapKey, this.m_array[fromIndex2].HeapKey) < 0)
                {
                    if (this.m_comparer.Compare(fromItem.HeapKey, this.m_array[fromIndex1].HeapKey) > 0)
                    {
                        this.MoveItem(fromIndex1, index);
                        index = fromIndex1;
                        fromIndex1 = index*2 + 1;
                        fromIndex2 = fromIndex1 + 1;
                    }
                    else
                        break;
                }
                else if (this.m_comparer.Compare(fromItem.HeapKey, this.m_array[fromIndex2].HeapKey) > 0)
                {
                    this.MoveItem(fromIndex2, index);
                    index = fromIndex2;
                    fromIndex1 = index*2 + 1;
                    fromIndex2 = fromIndex1 + 1;
                }
                else
                    break;
            }
            this.MoveItem(fromItem, index);
        }

        private void MoveItem(int fromIndex, int toIndex)
        {
            this.m_array[toIndex] = this.m_array[fromIndex];
            this.m_array[toIndex].HeapIndex = toIndex;
        }

        private void MoveItem(HeapItem<K> fromItem, int toIndex)
        {
            this.m_array[toIndex] = fromItem;
            this.m_array[toIndex].HeapIndex = toIndex;
        }

        private void Reallocate()
        {
            HeapItem<K>[] heapItemArray = new HeapItem<K>[this.m_capacity*2];
            Array.Copy((Array) this.m_array, (Array) heapItemArray, this.m_capacity);
            this.m_array = heapItemArray;
            this.m_capacity *= 2;
        }
    }
}