// Decompiled with JetBrains decompiler
// Type: ParallelTasks.WorkStealingScheduler
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using VRage;

namespace ParallelTasks
{
  public class WorkStealingScheduler : IWorkScheduler
  {
    private Queue<Task> tasks;
    private FastResourceLock tasksLock;

    internal List<Worker> Workers { get; private set; }

    public int ThreadCount
    {
      get
      {
        return this.Workers.Count;
      }
    }

    public WorkStealingScheduler()
      : this(Environment.ProcessorCount, ThreadPriority.BelowNormal)
    {
    }

    public WorkStealingScheduler(int numThreads, ThreadPriority priority)
    {
      this.tasks = new Queue<Task>();
      this.tasksLock = new FastResourceLock();
      this.Workers = new List<Worker>(numThreads);
      for (int index = 0; index < numThreads; ++index)
        this.Workers.Add(new Worker(this, index, priority));
      for (int index = 0; index < numThreads; ++index)
        this.Workers[index].Start();
    }

    internal bool TryGetTask(out Task task)
    {
      if (this.tasks.Count == 0)
      {
        task = new Task();
        return false;
      }
      else
      {
        using (FastResourceLockExtensions.AcquireExclusiveUsing(this.tasksLock))
        {
          if (this.tasks.Count > 0)
          {
            task = this.tasks.Dequeue();
            return true;
          }
          else
          {
            task = new Task();
            return false;
          }
        }
      }
    }

    public void Schedule(Task task)
    {
      if (task.Item.Work == null)
        return;
      int maximumThreads = task.Item.Work.Options.MaximumThreads;
      Worker currentWorker = Worker.CurrentWorker;
      if (!task.Item.Work.Options.QueueFIFO && currentWorker != null)
      {
        currentWorker.AddWork(task);
      }
      else
      {
        using (FastResourceLockExtensions.AcquireExclusiveUsing(this.tasksLock))
          this.tasks.Enqueue(task);
      }
      for (int index = 0; index < this.Workers.Count; ++index)
        this.Workers[index].Gate.Set();
    }

    public bool WaitForTasksToFinish(TimeSpan waitTimeout)
    {
      return Parallel.WaitForAll((WaitHandle[]) Enumerable.ToArray<ManualResetEvent>(Enumerable.Select<Worker, ManualResetEvent>((IEnumerable<Worker>) this.Workers, (Func<Worker, ManualResetEvent>) (s => s.HasNoWork))), waitTimeout);
    }
  }
}
