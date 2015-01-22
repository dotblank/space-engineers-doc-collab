// Decompiled with JetBrains decompiler
// Type: ParallelTasks.BackgroundWorker
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;
using System.Threading;

namespace ParallelTasks
{
  internal class BackgroundWorker
  {
    private static Stack<BackgroundWorker> idleWorkers = new Stack<BackgroundWorker>();
    private Thread thread;
    private AutoResetEvent resetEvent;
    private Task work;

    public BackgroundWorker()
    {
      this.resetEvent = new AutoResetEvent(false);
      this.thread = new Thread(new ThreadStart(this.WorkLoop));
      this.thread.IsBackground = true;
      this.thread.Start();
    }

    private void WorkLoop()
    {
      while (true)
      {
        this.resetEvent.WaitOne();
        this.work.DoWork();
        lock (BackgroundWorker.idleWorkers)
          BackgroundWorker.idleWorkers.Push(this);
      }
    }

    private void Start(Task work)
    {
      this.work = work;
      this.resetEvent.Set();
    }

    public static void StartWork(Task work)
    {
      BackgroundWorker backgroundWorker = (BackgroundWorker) null;
      lock (BackgroundWorker.idleWorkers)
      {
        if (BackgroundWorker.idleWorkers.Count > 0)
          backgroundWorker = BackgroundWorker.idleWorkers.Pop();
      }
      if (backgroundWorker == null)
        backgroundWorker = new BackgroundWorker();
      backgroundWorker.Start(work);
    }
  }
}
