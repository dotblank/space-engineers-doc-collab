// Decompiled with JetBrains decompiler
// Type: ParallelTasks.Worker
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Globalization;
using System.Threading;

namespace ParallelTasks
{
    internal class Worker
    {
        private static Hashtable<Thread, Worker> workers = new Hashtable<Thread, Worker>(Environment.ProcessorCount);
        private Thread thread;
        private Deque<Task> tasks;
        private WorkStealingScheduler scheduler;

        public AutoResetEvent Gate { get; private set; }

        public ManualResetEvent HasNoWork { get; private set; }

        public static Worker CurrentWorker
        {
            get
            {
                Thread currentThread = Thread.CurrentThread;
                Worker data;
                if (Worker.workers.TryGet(currentThread, out data))
                    return data;
                else
                    return (Worker) null;
            }
        }

        public Worker(WorkStealingScheduler scheduler, int index, ThreadPriority priority)
        {
            this.thread = new Thread(new ThreadStart(this.Work));
            this.thread.Name = "Parallel " + (object) index;
            this.thread.IsBackground = true;
            this.thread.Priority = priority;
            this.thread.CurrentCulture = CultureInfo.InvariantCulture;
            this.thread.CurrentUICulture = CultureInfo.InvariantCulture;
            this.tasks = new Deque<Task>();
            this.scheduler = scheduler;
            this.Gate = new AutoResetEvent(false);
            this.HasNoWork = new ManualResetEvent(false);
            Worker.workers.Add(this.thread, this);
        }

        public void Start()
        {
            this.thread.Start();
        }

        public void AddWork(Task task)
        {
            this.tasks.LocalPush(task);
        }

        private void Work()
        {
            while (true)
            {
                Task task;
                this.FindWork(out task);
                task.DoWork();
            }
        }

        private void FindWork(out Task task)
        {
            bool flag = false;
            task = new Task();
            while (!this.tasks.LocalPop(ref task) && !this.scheduler.TryGetTask(out task))
            {
                for (int index = 0; index < this.scheduler.Workers.Count; ++index)
                {
                    Worker worker = this.scheduler.Workers[index];
                    if (worker != this && worker.tasks.TrySteal(ref task))
                    {
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    this.HasNoWork.Set();
                    this.Gate.WaitOne();
                    this.HasNoWork.Reset();
                }
                if (flag)
                    break;
            }
        }
    }
}