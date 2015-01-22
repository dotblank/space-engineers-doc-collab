// Decompiled with JetBrains decompiler
// Type: VRage.Utils.MyInterpolationQueue`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using VRage;
using VRage.Collections;

namespace VRage.Utils
{
  public class MyInterpolationQueue<T>
  {
    private MyTimeSpan m_lastTimeStamp = MyTimeSpan.Zero;
    private MyQueue<MyInterpolationQueue<T>.Item> m_queue;
    private InterpolationHandler<T> m_interpolator;

    public MyTimeSpan LastSample
    {
      get
      {
        return this.m_lastTimeStamp;
      }
    }

    public int Count
    {
      get
      {
        return this.m_queue.Count;
      }
    }

    public MyInterpolationQueue(int defaultCapacity, InterpolationHandler<T> interpolator)
    {
      this.m_queue = new MyQueue<MyInterpolationQueue<T>.Item>(defaultCapacity);
      this.m_interpolator = interpolator;
    }

    public void DiscardOld(MyTimeSpan currentTimestamp)
    {
      int num = -1;
      for (int index = 0; index < this.m_queue.Count && this.m_queue[index].Timestamp < currentTimestamp; ++index)
        ++num;
      for (int index = 0; index < num && this.m_queue.Count > 2; ++index)
        this.m_queue.Dequeue();
    }

    public void Clear()
    {
      this.m_queue.Clear();
      this.m_lastTimeStamp = MyTimeSpan.Zero;
    }

    public void AddSample(ref T item, MyTimeSpan sampleTimestamp)
    {
      if (sampleTimestamp < this.m_lastTimeStamp)
        return;
      if (sampleTimestamp == this.m_lastTimeStamp && this.m_queue.Count > 0)
      {
        this.m_queue[this.m_queue.Count - 1] = new MyInterpolationQueue<T>.Item(item, sampleTimestamp);
      }
      else
      {
        this.m_queue.Enqueue(new MyInterpolationQueue<T>.Item(item, sampleTimestamp));
        this.m_lastTimeStamp = sampleTimestamp;
      }
    }

    public float Interpolate(MyTimeSpan currentTimestamp, out T result)
    {
      this.DiscardOld(currentTimestamp);
      if (this.m_queue.Count > 1)
      {
        MyInterpolationQueue<T>.Item obj1 = this.m_queue[0];
        MyInterpolationQueue<T>.Item obj2 = this.m_queue[1];
        float interpolator = (float) ((currentTimestamp - obj1.Timestamp).Seconds / (obj2.Timestamp - obj1.Timestamp).Seconds);
        this.m_interpolator(obj1.Userdata, obj2.Userdata, interpolator, out result);
        return interpolator;
      }
      else
      {
        result = this.m_queue[0].Userdata;
        return 0.0f;
      }
    }

    private struct Item
    {
      public T Userdata;
      public MyTimeSpan Timestamp;

      public Item(T userdata, MyTimeSpan timespan)
      {
        this.Userdata = userdata;
        this.Timestamp = timespan;
      }
    }
  }
}
