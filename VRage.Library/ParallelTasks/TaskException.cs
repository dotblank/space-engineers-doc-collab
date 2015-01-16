// Decompiled with JetBrains decompiler
// Type: ParallelTasks.TaskException
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace ParallelTasks
{
    public class TaskException : Exception
    {
        public Exception[] InnerExceptions { get; private set; }

        public TaskException(Exception[] inner)
            : base("An exception(s) was thrown while executing a task.", (Exception) null)
        {
            this.InnerExceptions = inner;
        }

        public override string ToString()
        {
            string str = base.ToString() + Environment.NewLine;
            for (int index = 0; index < this.InnerExceptions.Length; ++index)
                str = str + string.Format("Task exception, inner exception {0}:", (object) index) + Environment.NewLine +
                      ((object) this.InnerExceptions[index]).ToString();
            return str;
        }
    }
}