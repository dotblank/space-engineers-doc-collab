// Decompiled with JetBrains decompiler
// Type: ParallelTasks.Future`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ParallelTasks
{
  public struct Future<T>
  {
    private Task task;
    private FutureWork<T> work;
    private int id;

    public bool IsComplete
    {
      get
      {
        return this.task.IsComplete;
      }
    }

    public Exception[] Exceptions
    {
      get
      {
        return this.task.Exceptions;
      }
    }

    internal Future(Task task, FutureWork<T> work)
    {
      this.task = task;
      this.work = work;
      this.id = work.ID;
    }

    public T GetResult()
    {
      if (this.work == null || this.work.ID != this.id)
        throw new InvalidOperationException("The result of a future can only be retrieved once.");
      this.task.Wait();
      T result = this.work.Result;
      this.work.ReturnToPool();
      this.work = (FutureWork<T>) null;
      return result;
    }
  }
}
