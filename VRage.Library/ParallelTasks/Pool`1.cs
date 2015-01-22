// Decompiled with JetBrains decompiler
// Type: ParallelTasks.Pool`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Threading;

namespace ParallelTasks
{
  public class Pool<T> : Singleton<Pool<T>> where T : class, new()
  {
    private Hashtable<Thread, Pool<T>.DequeEnumerator> instances;

    public Pool()
    {
      this.instances = new Hashtable<Thread, Pool<T>.DequeEnumerator>(Environment.ProcessorCount);
    }

    public T Get(Thread thread)
    {
      Pool<T>.DequeEnumerator data;
      if (this.instances.TryGet(thread, out data))
      {
        T obj = default (T);
        if (data.Deque.LocalPop(ref obj))
          return obj;
        data.Enumerator.Reset();
        while (data.Enumerator.MoveNext())
        {
          if (data.Enumerator.Current.Value.Deque.TrySteal(ref obj))
            return obj;
        }
      }
      return Activator.CreateInstance<T>();
    }

    public void Return(Thread thread, T instance)
    {
      Pool<T>.DequeEnumerator data1;
      if (this.instances.TryGet(thread, out data1))
      {
        data1.Deque.LocalPush(instance);
      }
      else
      {
        Pool<T>.DequeEnumerator data2 = new Pool<T>.DequeEnumerator()
        {
          Deque = new Deque<T>(),
          Enumerator = this.instances.GetEnumerator()
        };
        data2.Deque.LocalPush(instance);
        this.instances.Add(thread, data2);
      }
    }

    public void Clean()
    {
      foreach (KeyValuePair<Thread, Pool<T>.DequeEnumerator> keyValuePair in this.instances)
        keyValuePair.Value.Deque.Clear();
    }

    private struct DequeEnumerator
    {
      public Deque<T> Deque;
      public IEnumerator<KeyValuePair<Thread, Pool<T>.DequeEnumerator>> Enumerator;
    }
  }
}
