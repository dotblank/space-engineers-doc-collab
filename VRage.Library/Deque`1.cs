// Decompiled with JetBrains decompiler
// Type: Deque`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Threading;

internal class Deque<T>
{
    private T[] m_array = new T[32];
    private int m_mask = 31;
    private object m_foreignLock = new object();
    private const int INITIAL_SIZE = 32;
    private volatile int m_headIndex;
    private volatile int m_tailIndex;

    public bool IsEmpty
    {
        get { return this.m_headIndex >= this.m_tailIndex; }
    }

    public int Count
    {
        get { return this.m_tailIndex - this.m_headIndex; }
    }

    public void LocalPush(T obj)
    {
        lock (this.m_foreignLock)
        {
            int local_0 = this.m_tailIndex;
            if (local_0 < this.m_headIndex + this.m_mask)
            {
                this.m_array[local_0 & this.m_mask] = obj;
                this.m_tailIndex = local_0 + 1;
            }
            else
            {
                int local_1 = this.m_headIndex;
                int local_2 = this.m_tailIndex - this.m_headIndex;
                if (local_2 >= this.m_mask)
                {
                    T[] local_3 = new T[this.m_array.Length << 1];
                    for (int local_4 = 0; local_4 < local_2; ++local_4)
                        local_3[local_4] = this.m_array[local_4 + local_1 & this.m_mask];
                    this.m_array = local_3;
                    this.m_headIndex = 0;
                    this.m_tailIndex = local_0 = local_2;
                    this.m_mask = this.m_mask << 1 | 1;
                }
                this.m_array[local_0 & this.m_mask] = obj;
                this.m_tailIndex = local_0 + 1;
            }
        }
    }

    public bool LocalPop(ref T obj)
    {
        lock (this.m_foreignLock)
        {
            int local_0 = this.m_tailIndex;
            if (this.m_headIndex >= local_0)
                return false;
            int local_0_1 = local_0 - 1;
            Interlocked.Exchange(ref this.m_tailIndex, local_0_1);
            if (this.m_headIndex <= local_0_1)
            {
                obj = this.m_array[local_0_1 & this.m_mask];
                return true;
            }
            else if (this.m_headIndex <= local_0_1)
            {
                obj = this.m_array[local_0_1 & this.m_mask];
                return true;
            }
            else
            {
                this.m_tailIndex = local_0_1 + 1;
                return false;
            }
        }
    }

    public bool TrySteal(ref T obj)
    {
        bool flag = false;
        try
        {
            flag = Monitor.TryEnter(this.m_foreignLock);
            if (flag)
            {
                int num = this.m_headIndex;
                Interlocked.Exchange(ref this.m_headIndex, num + 1);
                if (num < this.m_tailIndex)
                {
                    obj = this.m_array[num & this.m_mask];
                    return true;
                }
                else
                {
                    this.m_headIndex = num;
                    return false;
                }
            }
        }
        finally
        {
            if (flag)
                Monitor.Exit(this.m_foreignLock);
        }
        return false;
    }

    public void Clear()
    {
        for (int index = 0; index < this.m_array.Length; ++index)
            this.m_array[index] = default (T);
        this.m_headIndex = 0;
        this.m_tailIndex = 0;
    }
}