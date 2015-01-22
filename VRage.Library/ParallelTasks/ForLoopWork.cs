// Decompiled with JetBrains decompiler
// Type: ParallelTasks.ForLoopWork
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Threading;

namespace ParallelTasks
{
  internal class ForLoopWork : IWork
  {
    private static Pool<ForLoopWork> pool = new Pool<ForLoopWork>();
    private Action<int> action;
    private int length;
    private int stride;
    private volatile int index;

    public WorkOptions Options { get; private set; }

    public ForLoopWork()
    {
      this.Options = new WorkOptions()
      {
        MaximumThreads = int.MaxValue
      };
    }

    public void Prepare(Action<int> action, int startInclusive, int endExclusive, int stride)
    {
      this.action = action;
      this.index = startInclusive;
      this.length = endExclusive;
      this.stride = stride;
    }

    public void DoWork()
    {
      int num1;
      while ((num1 = this.IncrementIndex()) < this.length)
      {
        int num2 = Math.Min(num1 + this.stride, this.length);
        for (int index = num1; index < num2; ++index)
          this.action(index);
      }
    }

    private int IncrementIndex()
    {
      return Interlocked.Add(ref this.index, this.stride) - this.stride;
    }

    public static ForLoopWork Get()
    {
      return ForLoopWork.pool.Get(Thread.CurrentThread);
    }

    public void Return()
    {
      this.action = (Action<int>) null;
      ForLoopWork.pool.Return(Thread.CurrentThread, this);
    }
  }
}
