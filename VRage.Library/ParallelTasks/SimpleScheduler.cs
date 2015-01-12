// Decompiled with JetBrains decompiler
// Type: ParallelTasks.SimpleScheduler
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Threading;

namespace ParallelTasks
{
    public class SimpleScheduler : IWorkScheduler
    {
        private Stack<Task> scheduledItems;
        private AutoResetEvent fastLock;
        private int waitingForWork;
        private ManualResetEvent[] hasNoWork;

        public int ThreadCount
        {
            get { return this.hasNoWork.Length; }
        }

        public SimpleScheduler()
            : this(Environment.ProcessorCount)
        {
        }

        public SimpleScheduler(int threadCount)
        {
            this.scheduledItems = new Stack<Task>();
            this.fastLock = new AutoResetEvent(false);
            for (int index = 0; index < threadCount; ++index)
            {
                this.hasNoWork[index] = new ManualResetEvent(false);
                new Thread(new ParameterizedThreadStart(this.WorkerLoop))
                {
                    IsBackground = true,
                    Name = "ParallelTasks Worker"
                }.Start((object) this.hasNoWork[index]);
            }
        }

        private void WorkerLoop(object o)
        {
            ManualResetEvent manualResetEvent = (ManualResetEvent) o;
            Task task = new Task();
            while (true)
            {
                bool flag;
                do
                {
                    manualResetEvent.Set();
                    this.fastLock.WaitOne();
                    manualResetEvent.Reset();
                    if (this.scheduledItems.Count > 0)
                    {
                        flag = false;
                        lock (this.scheduledItems)
                        {
                            if (this.scheduledItems.Count > 0)
                            {
                                task = this.scheduledItems.Pop();
                                flag = true;
                            }
                        }
                    }
                    else
                        goto label_10;
                } while (!flag);
                task.DoWork();
                continue;
                label_10:
                Task? replicable = WorkItem.Replicable;
                if (replicable.HasValue)
                    replicable.Value.DoWork();
                WorkItem.SetReplicableNull(replicable);
            }
        }

        public void Schedule(Task work)
        {
            if (work.Item.Work == null)
                return;
            int maximumThreads = work.Item.Work.Options.MaximumThreads;
            lock (this.scheduledItems)
                this.scheduledItems.Push(work);
            if (maximumThreads > 0)
                WorkItem.Replicable = new Task?(work);
            this.fastLock.Set();
        }

        public bool WaitForTasksToFinish(TimeSpan waitTimeout)
        {
            return Parallel.WaitForAll((WaitHandle[]) this.hasNoWork, waitTimeout);
        }
    }
}