// Decompiled with JetBrains decompiler
// Type: ParallelTasks.Parallel
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using VRage.Collections;

namespace ParallelTasks
{
  public static class Parallel
  {
    public static readonly WorkOptions DefaultOptions = new WorkOptions()
    {
      DetachFromParent = false,
      MaximumThreads = 1
    };
    private static Pool<List<Task>> taskPool = new Pool<List<Task>>();
    private static List<MyConcurrentQueue<WorkItem>> m_buffers = new List<MyConcurrentQueue<WorkItem>>(8);
    private static int[] _processorAffinity = new int[4]
    {
      3,
      4,
      5,
      1
    };
    private static IWorkScheduler scheduler;
    [ThreadStatic]
    private static MyConcurrentQueue<WorkItem> m_callbackBuffer;

    private static MyConcurrentQueue<WorkItem> CallbackBuffer
    {
      get
      {
        if (Parallel.m_callbackBuffer == null)
        {
          Parallel.m_callbackBuffer = new MyConcurrentQueue<WorkItem>(16);
          lock (Parallel.m_buffers)
            Parallel.m_buffers.Add(Parallel.m_callbackBuffer);
        }
        return Parallel.m_callbackBuffer;
      }
    }

    public static int[] ProcessorAffinity
    {
      get
      {
        return Parallel._processorAffinity;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        if (value.Length < 1)
          throw new ArgumentException("The Parallel.ProcessorAffinity must contain at least one value.", "value");
        if (Enumerable.Any<int>((IEnumerable<int>) value, (Func<int, bool>) (id => id < 0)))
          throw new ArgumentException("The processor affinity must not be negative.", "value");
        Parallel._processorAffinity = value;
      }
    }

    public static IWorkScheduler Scheduler
    {
      get
      {
        if (Parallel.scheduler == null)
        {
          IWorkScheduler workScheduler = (IWorkScheduler) new WorkStealingScheduler();
          Interlocked.CompareExchange<IWorkScheduler>(ref Parallel.scheduler, workScheduler, (IWorkScheduler) null);
        }
        return Parallel.scheduler;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException("value");
        Interlocked.Exchange<IWorkScheduler>(ref Parallel.scheduler, value);
      }
    }

    public static void RunCallbacks()
    {
      WorkItem instance;
      while (Parallel.CallbackBuffer.TryDequeue(out instance))
      {
        instance.Callback();
        instance.Callback = (Action) null;
        instance.Requeue();
      }
    }

    public static Task StartBackground(IWork work)
    {
      return Parallel.StartBackground(work, (Action) null);
    }

    public static Task StartBackground(IWork work, Action completionCallback)
    {
      if (work == null)
        throw new ArgumentNullException("work");
      if (work.Options.MaximumThreads < 1)
        throw new ArgumentException("work.Options.MaximumThreads cannot be less than one.");
      WorkItem workItem = WorkItem.Get(Thread.CurrentThread);
      workItem.Callback = completionCallback;
      Task work1 = workItem.PrepareStart(work);
      BackgroundWorker.StartWork(work1);
      return work1;
    }

    public static Task StartBackground(Action action)
    {
      return Parallel.StartBackground(action, (Action) null);
    }

    public static Task StartBackground(Action action, Action completionCallback)
    {
      if (action == null)
        throw new ArgumentNullException("action");
      DelegateWork instance = DelegateWork.GetInstance();
      instance.Action = action;
      instance.Options = Parallel.DefaultOptions;
      return Parallel.StartBackground((IWork) instance, completionCallback);
    }

    public static Task Start(IWork work)
    {
      return Parallel.Start(work, (Action) null);
    }

    public static Task Start(IWork work, Action completionCallback)
    {
      if (work == null)
        throw new ArgumentNullException("work");
      if (work.Options.MaximumThreads < 1)
        throw new ArgumentException("work.Options.MaximumThreads cannot be less than one.");
      WorkItem workItem = WorkItem.Get(Thread.CurrentThread);
      workItem.CompletionCallbacks = Parallel.CallbackBuffer;
      workItem.Callback = completionCallback;
      Task task = workItem.PrepareStart(work);
      Parallel.Scheduler.Schedule(task);
      return task;
    }

    public static Task Start(Action action)
    {
      return Parallel.Start(action, (Action) null);
    }

    public static Task Start(Action action, Action completionCallback)
    {
      return Parallel.Start(action, new WorkOptions()
      {
        MaximumThreads = 1,
        DetachFromParent = false,
        QueueFIFO = false
      }, completionCallback);
    }

    public static Task Start(Action action, WorkOptions options)
    {
      return Parallel.Start(action, options, (Action) null);
    }

    public static Task Start(Action action, WorkOptions options, Action completionCallback)
    {
      if (options.MaximumThreads < 1)
        throw new ArgumentOutOfRangeException("options", "options.MaximumThreads cannot be less than 1.");
      DelegateWork instance = DelegateWork.GetInstance();
      instance.Action = action;
      instance.Options = options;
      return Parallel.Start((IWork) instance, completionCallback);
    }

    private static void RunPerWorker(Action action, Barrier barrier)
    {
      barrier.SignalAndWait();
      action();
    }

    public static void StartOnEachWorker(Action action, bool waitForCompletion = true)
    {
      Barrier barrier = new Barrier(Parallel.Scheduler.ThreadCount);
      Action action1 = (Action) (() => Parallel.RunPerWorker(action, barrier));
      if (waitForCompletion)
      {
        barrier.AddParticipant();
        Task[] taskArray = new Task[Parallel.Scheduler.ThreadCount];
        for (int index = 0; index < Parallel.Scheduler.ThreadCount; ++index)
          taskArray[index] = Parallel.Start(action1);
        barrier.SignalAndWait();
        for (int index = 0; index < Parallel.Scheduler.ThreadCount; ++index)
          taskArray[index].Wait();
      }
      else
      {
        for (int index = 0; index < Parallel.Scheduler.ThreadCount; ++index)
          Parallel.Start(action1);
      }
    }

    public static Future<T> Start<T>(Func<T> function)
    {
      return Parallel.Start<T>(function, (Action) null);
    }

    public static Future<T> Start<T>(Func<T> function, Action completionCallback)
    {
      return Parallel.Start<T>(function, Parallel.DefaultOptions, completionCallback);
    }

    public static Future<T> Start<T>(Func<T> function, WorkOptions options)
    {
      return Parallel.Start<T>(function, options, (Action) null);
    }

    public static Future<T> Start<T>(Func<T> function, WorkOptions options, Action completionCallback)
    {
      if (options.MaximumThreads < 1)
        throw new ArgumentOutOfRangeException("options", "options.MaximumThreads cannot be less than 1.");
      FutureWork<T> instance = FutureWork<T>.GetInstance();
      instance.Function = function;
      instance.Options = options;
      return new Future<T>(Parallel.Start((IWork) instance, completionCallback), instance);
    }

    public static void Do(IWork a, IWork b)
    {
      Task task = Parallel.Start(b);
      a.DoWork();
      task.Wait();
    }

    public static void Do(params IWork[] work)
    {
      List<Task> instance = Parallel.taskPool.Get(Thread.CurrentThread);
      for (int index = 0; index < work.Length; ++index)
        instance.Add(Parallel.Start(work[index]));
      for (int index = 0; index < instance.Count; ++index)
        instance[index].Wait();
      instance.Clear();
      Parallel.taskPool.Return(Thread.CurrentThread, instance);
    }

    public static void Do(Action action1, Action action2)
    {
      DelegateWork instance = DelegateWork.GetInstance();
      instance.Action = action2;
      instance.Options = Parallel.DefaultOptions;
      Task task = Parallel.Start((IWork) instance);
      action1();
      task.Wait();
    }

    public static void Do(params Action[] actions)
    {
      List<Task> instance1 = Parallel.taskPool.Get(Thread.CurrentThread);
      for (int index = 0; index < actions.Length; ++index)
      {
        DelegateWork instance2 = DelegateWork.GetInstance();
        instance2.Action = actions[index];
        instance2.Options = Parallel.DefaultOptions;
        instance1.Add(Parallel.Start((IWork) instance2));
      }
      for (int index = 0; index < actions.Length; ++index)
        instance1[index].Wait();
      instance1.Clear();
      Parallel.taskPool.Return(Thread.CurrentThread, instance1);
    }

    public static void For(int startInclusive, int endExclusive, Action<int> body)
    {
      Parallel.For(startInclusive, endExclusive, body, 8);
    }

    public static void For(int startInclusive, int endExclusive, Action<int> body, int stride)
    {
      ForLoopWork forLoopWork = ForLoopWork.Get();
      forLoopWork.Prepare(body, startInclusive, endExclusive, stride);
      Parallel.Start((IWork) forLoopWork).Wait();
      forLoopWork.Return();
    }

    public static void ForEach<T>(IEnumerable<T> collection, Action<T> action)
    {
      ForEachLoopWork<T> forEachLoopWork = ForEachLoopWork<T>.Get();
      forEachLoopWork.Prepare(action, collection.GetEnumerator());
      Parallel.Start((IWork) forEachLoopWork).Wait();
      forEachLoopWork.Return();
    }

    public static void Clean()
    {
      Parallel.taskPool.Clean();
      lock (Parallel.m_buffers)
      {
        foreach (MyConcurrentQueue<WorkItem> item_0 in Parallel.m_buffers)
          item_0.Clear();
        Parallel.m_buffers.Clear();
      }
      WorkItem.Clean();
    }

    public static bool WaitForAll(WaitHandle[] waitHandles, TimeSpan timeout)
    {
      if (Thread.CurrentThread.GetApartmentState() == ApartmentState.MTA)
        return WaitHandle.WaitAll(waitHandles, timeout);
      bool result = false;
      Thread thread = new Thread((ThreadStart) (() => result = WaitHandle.WaitAll(waitHandles, timeout)));
      thread.SetApartmentState(ApartmentState.MTA);
      thread.Start();
      thread.Join();
      return result;
    }
  }
}
