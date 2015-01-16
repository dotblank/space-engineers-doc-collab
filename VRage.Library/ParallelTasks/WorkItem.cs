// Decompiled with JetBrains decompiler
// Type: ParallelTasks.WorkItem
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Threading;
using VRage;
using VRage.Collections;

namespace ParallelTasks
{
    internal class WorkItem
    {
        private static readonly Stack<Task> replicables = new Stack<Task>();
        private static readonly object replicablesLock = new object();
        private static Pool<WorkItem> idleWorkItems = new Pool<WorkItem>();

        private static Hashtable<Thread, Stack<Task>> runningTasks =
            new Hashtable<Thread, Stack<Task>>(Environment.ProcessorCount);

        private FastResourceLock executionLock = new FastResourceLock();
        private static Task? topReplicable;
        private List<Exception> exceptionBuffer;
        private Hashtable<int, Exception[]> exceptions;
        private ManualResetEvent resetEvent;
        private IWork work;
        private volatile int runCount;
        private volatile int executing;
        private List<Task> children;
        private volatile int waitCount;
        private Thread ExecutingThread;

        internal static Task? Replicable
        {
            get
            {
                bool flag = false;
                try
                {
                    flag = Monitor.TryEnter(WorkItem.replicablesLock);
                    if (flag)
                        return WorkItem.topReplicable;
                    else
                        return new Task?();
                }
                finally
                {
                    if (flag)
                        Monitor.Exit(WorkItem.replicablesLock);
                }
            }
            set
            {
                lock (WorkItem.replicablesLock)
                {
                    WorkItem.replicables.Push(value.Value);
                    WorkItem.topReplicable = new Task?(value.Value);
                }
            }
        }

        public int RunCount
        {
            get { return this.runCount; }
        }

        public Hashtable<int, Exception[]> Exceptions
        {
            get { return this.exceptions; }
        }

        public IWork Work
        {
            get { return this.work; }
        }

        public Action Callback { get; set; }

        public MyConcurrentQueue<WorkItem> CompletionCallbacks { get; set; }

        public static Task? CurrentTask
        {
            get
            {
                Stack<Task> data;
                if (WorkItem.runningTasks.TryGet(Thread.CurrentThread, out data) && data.Count > 0)
                    return new Task?(data.Peek());
                else
                    return new Task?();
            }
        }

        public WorkItem()
        {
            this.resetEvent = new ManualResetEvent(true);
            this.exceptions = new Hashtable<int, Exception[]>(1);
            this.children = new List<Task>();
        }

        internal static void SetReplicableNull(Task? task)
        {
            if (!WorkItem.topReplicable.HasValue)
                return;
            if (!task.HasValue)
            {
                lock (WorkItem.replicablesLock)
                {
                    WorkItem.replicables.Clear();
                    WorkItem.topReplicable = new Task?();
                }
            }
            else
            {
                bool flag = false;
                try
                {
                    flag = Monitor.TryEnter(WorkItem.replicablesLock);
                    if (!flag || WorkItem.replicables.Count <= 0)
                        return;
                    Task task1 = WorkItem.replicables.Peek();
                    if (task1.ID == task.Value.ID && task1.Item == task.Value.Item)
                        WorkItem.replicables.Pop();
                    if (WorkItem.replicables.Count > 0)
                        WorkItem.topReplicable = new Task?(WorkItem.replicables.Peek());
                    else
                        WorkItem.topReplicable = new Task?();
                }
                finally
                {
                    if (flag)
                        Monitor.Exit(WorkItem.replicablesLock);
                }
            }
        }

        public Task PrepareStart(IWork work)
        {
            this.work = work;
            this.resetEvent.Reset();
            this.children.Clear();
            this.exceptionBuffer = (List<Exception>) null;
            this.ExecutingThread = Thread.CurrentThread;
            Task task = new Task(this);
            Task? currentTask = WorkItem.CurrentTask;
            if (currentTask.HasValue && currentTask.Value.Item == this)
                throw new Exception("whadafak?");
            if (!work.Options.DetachFromParent && currentTask.HasValue)
                currentTask.Value.Item.AddChild(task);
            return task;
        }

        public bool DoWork(int expectedID)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.executionLock))
            {
                if (expectedID < this.runCount)
                    return true;
                if (this.work == null || this.executing == this.work.Options.MaximumThreads)
                    return false;
                ++this.executing;
            }
            Stack<Task> data = (Stack<Task>) null;
            if (!WorkItem.runningTasks.TryGet(Thread.CurrentThread, out data))
            {
                data = new Stack<Task>();
                WorkItem.runningTasks.Add(Thread.CurrentThread, data);
            }
            data.Push(new Task(this));
            try
            {
                this.work.DoWork();
            }
            catch (Exception ex)
            {
                if (this.exceptionBuffer == null)
                    Interlocked.CompareExchange<List<Exception>>(ref this.exceptionBuffer, new List<Exception>(),
                        (List<Exception>) null);
                lock (this.exceptionBuffer)
                    this.exceptionBuffer.Add(ex);
            }
            if (data != null)
                data.Pop();
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.executionLock))
            {
                --this.executing;
                if (this.executing != 0)
                    return false;
                if (this.exceptionBuffer != null)
                    this.exceptions.Add(this.runCount, this.exceptionBuffer.ToArray());
                foreach (Task task in this.children)
                    task.Wait();
                ++this.runCount;
                this.resetEvent.Set();
                do
                    ; while (this.waitCount > 0);
                if (this.Callback == null)
                    this.Requeue();
                else
                    this.CompletionCallbacks.Enqueue(this);
                return true;
            }
        }

        public void Requeue()
        {
            if (this.runCount >= int.MaxValue || this.exceptionBuffer != null)
                return;
            this.work = (IWork) null;
            WorkItem.idleWorkItems.Return(this.ExecutingThread, this);
        }

        public void Wait(int id)
        {
            this.WaitOrExecute(id);
            Exception[] data;
            if (this.exceptions.TryGet(id, out data))
                throw new TaskException(data);
        }

        private void WaitOrExecute(int id)
        {
            if (this.runCount != id)
                return;
            if (this.DoWork(id))
                return;
            try
            {
                Interlocked.Increment(ref this.waitCount);
                int num = 0;
                while (this.runCount == id)
                {
                    if (num > 1000)
                        this.resetEvent.WaitOne();
                    else
                        Thread.Sleep(0);
                    ++num;
                }
            }
            finally
            {
                Interlocked.Decrement(ref this.waitCount);
            }
        }

        public void AddChild(Task item)
        {
            using (FastResourceLockExtensions.AcquireExclusiveUsing(this.executionLock))
                this.children.Add(item);
        }

        public static WorkItem Get(Thread thread)
        {
            return WorkItem.idleWorkItems.Get(thread);
        }

        public static void Clean()
        {
            WorkItem.replicables.Clear();
            WorkItem.idleWorkItems.Clean();
        }
    }
}