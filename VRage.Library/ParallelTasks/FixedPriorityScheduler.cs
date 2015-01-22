// Decompiled with JetBrains decompiler
// Type: ParallelTasks.FixedPriorityScheduler
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Globalization;
using System.Threading;
using VRage.Collections;

namespace ParallelTasks
{
  public class FixedPriorityScheduler : IWorkScheduler
  {
    private readonly MyConcurrentQueue<Task>[] m_taskQueuesByPriority;
    private readonly FixedPriorityScheduler.Worker[] m_workers;
    private readonly ManualResetEvent[] m_hasNoWork;
    private long m_scheduledTaskCount;

    public int ThreadCount
    {
      get
      {
        return this.m_workers.Length;
      }
    }

    public FixedPriorityScheduler(int threadCount, ThreadPriority priority)
    {
      this.m_taskQueuesByPriority = new MyConcurrentQueue<Task>[typeof (WorkPriority).GetEnumValues().Length];
      for (int index = 0; index < this.m_taskQueuesByPriority.Length; ++index)
        this.m_taskQueuesByPriority[index] = new MyConcurrentQueue<Task>(0);
      this.m_hasNoWork = new ManualResetEvent[threadCount];
      this.m_workers = new FixedPriorityScheduler.Worker[threadCount];
      for (int index = 0; index < threadCount; ++index)
      {
        this.m_workers[index] = new FixedPriorityScheduler.Worker(this, "Parallel " + (object) index, priority);
        this.m_hasNoWork[index] = this.m_workers[index].HasNoWork;
      }
    }

    private bool TryGetTask(out Task task)
    {
      while (this.m_scheduledTaskCount > 0L)
      {
        for (int index = 0; index < this.m_taskQueuesByPriority.Length; ++index)
        {
          if (this.m_taskQueuesByPriority[index].TryDequeue(out task))
          {
            Interlocked.Decrement(ref this.m_scheduledTaskCount);
            return true;
          }
        }
      }
      task = new Task();
      return false;
    }

    public void Schedule(Task task)
    {
      if (task.Item.Work == null)
        return;
      WorkPriority workPriority = WorkPriority.Normal;
      IPrioritizedWork prioritizedWork = task.Item.Work as IPrioritizedWork;
      if (prioritizedWork != null)
        workPriority = prioritizedWork.Priority;
      this.m_taskQueuesByPriority[(int) workPriority].Enqueue(task);
      Interlocked.Increment(ref this.m_scheduledTaskCount);
      foreach (FixedPriorityScheduler.Worker worker in this.m_workers)
        worker.Gate.Set();
    }

    public bool WaitForTasksToFinish(TimeSpan waitTimeout)
    {
      return Parallel.WaitForAll((WaitHandle[]) this.m_hasNoWork, waitTimeout);
    }

    private class Worker
    {
      private readonly FixedPriorityScheduler m_scheduler;
      private readonly Thread m_thread;
      public readonly ManualResetEvent HasNoWork;
      public readonly AutoResetEvent Gate;

      public Worker(FixedPriorityScheduler scheduler, string name, ThreadPriority priority)
      {
        this.m_scheduler = scheduler;
        this.m_thread = new Thread(new ParameterizedThreadStart(this.WorkerLoop));
        this.HasNoWork = new ManualResetEvent(false);
        this.Gate = new AutoResetEvent(false);
        this.m_thread.Name = name;
        this.m_thread.IsBackground = true;
        this.m_thread.Priority = priority;
        this.m_thread.CurrentCulture = CultureInfo.InvariantCulture;
        this.m_thread.CurrentUICulture = CultureInfo.InvariantCulture;
        this.m_thread.Start((object) null);
      }

      private void WorkerLoop(object o)
      {
        Task task = new Task();
        while (true)
        {
          while (!this.m_scheduler.TryGetTask(out task))
          {
            this.HasNoWork.Set();
            this.Gate.WaitOne();
            this.HasNoWork.Reset();
          }
          task.DoWork();
        }
      }
    }
  }
}
