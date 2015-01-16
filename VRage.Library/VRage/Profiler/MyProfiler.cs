// Decompiled with JetBrains decompiler
// Type: VRage.Profiler.MyProfiler
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using VRage;

namespace VRage.Profiler
{
    public class MyProfiler
    {
        public static bool EnableAsserts = true;
        public static readonly int MAX_FRAMES = 1024;
        public static readonly int UPDATE_WINDOW = 16;
        private int m_nextId = 1;

        private Dictionary<MyProfiler.MyProfilerBlockKey, MyProfiler.MyProfilerBlock> m_profilingBlocks =
            new Dictionary<MyProfiler.MyProfilerBlockKey, MyProfiler.MyProfilerBlock>(8192,
                (IEqualityComparer<MyProfiler.MyProfilerBlockKey>) new MyProfiler.MyProfilerBlockKeyComparer());

        private List<MyProfiler.MyProfilerBlock> m_rootBlocks = new List<MyProfiler.MyProfilerBlock>(32);
        private Stack<MyProfiler.MyProfilerBlock> m_currentProfilingStack = new Stack<MyProfiler.MyProfilerBlock>(1024);
        private int m_levelLimit = -1;
        private volatile int m_newLevelLimit = -1;
        private int m_remainingWindow = MyProfiler.UPDATE_WINDOW;
        public FastResourceLock m_historyLock = new FastResourceLock();

        private Dictionary<MyProfiler.MyProfilerBlockKey, MyProfiler.MyProfilerBlock> m_blocksToAdd =
            new Dictionary<MyProfiler.MyProfilerBlockKey, MyProfiler.MyProfilerBlock>(8192,
                (IEqualityComparer<MyProfiler.MyProfilerBlockKey>) new MyProfiler.MyProfilerBlockKeyComparer());

        public int[] TotalCalls = new int[MyProfiler.MAX_FRAMES];
        public bool AutoCommit = true;
        public readonly Stopwatch Stopwatch = new Stopwatch();
        private const int ROOT_ID = 0;
        private MyProfiler.MyProfilerBlock m_selectedRoot;
        private int m_levelSkipCount;
        public string m_customName;
        private volatile int m_lastFrameIndex;
        public readonly bool MemoryProfiling;
        public readonly int GlobalProfilerIndex;
        public readonly Thread OwnerThread;

        public MyProfiler.MyProfilerBlock SelectedRoot
        {
            get { return this.m_selectedRoot; }
            set { this.m_selectedRoot = value; }
        }

        public List<MyProfiler.MyProfilerBlock> SelectedRootChildren
        {
            get
            {
                if (this.m_selectedRoot == null)
                    return this.m_rootBlocks;
                else
                    return this.m_selectedRoot.Children;
            }
        }

        public int LastFrameIndexDebug
        {
            get { return this.m_lastFrameIndex; }
        }

        public string DisplayedName
        {
            get
            {
                if (this.m_customName != null)
                    return this.m_customName;
                else
                    return this.OwnerThread.Name;
            }
        }

        public MyProfiler(int globalProfilerIndex, bool memoryProfiling)
        {
            this.GlobalProfilerIndex = globalProfilerIndex;
            this.OwnerThread = Thread.CurrentThread;
            this.MemoryProfiling = memoryProfiling;
            this.m_lastFrameIndex = MyProfiler.MAX_FRAMES - 1;
        }

        private string GetParentName()
        {
            if (this.m_currentProfilingStack.Count > 0)
                return this.m_currentProfilingStack.Peek().Key.Name;
            else
                return "<root>";
        }

        private int GetParentId()
        {
            if (this.m_currentProfilingStack.Count > 0)
                return this.m_currentProfilingStack.Peek().Id;
            else
                return 0;
        }

        private void OnHistorySafe()
        {
            Interlocked.Exchange(ref this.m_remainingWindow, MyProfiler.UPDATE_WINDOW);
        }

        public static MyProfiler.MyProfilerBlock CreateExternalBlock(string name, int blockId)
        {
            MyProfiler.MyProfilerBlockKey key = new MyProfiler.MyProfilerBlockKey(string.Empty, string.Empty, name, 0, 0);
            return new MyProfiler.MyProfilerBlock(ref key, string.Empty, blockId);
        }

        public void SetNewLevelLimit(int newLevelLimit)
        {
            this.m_newLevelLimit = newLevelLimit;
        }

        public MyProfiler.HistoryLock LockHistory(out int lastValidFrame)
        {
            MyProfiler.HistoryLock historyLock = new MyProfiler.HistoryLock(this, this.m_historyLock);
            lastValidFrame = this.m_lastFrameIndex;
            return historyLock;
        }

        public void CommitFrame()
        {
            this.CommitInternal();
        }

        private void CommitInternal()
        {
            if (this.m_blocksToAdd.Count > 0)
            {
                using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_historyLock))
                {
                    foreach (
                        KeyValuePair<MyProfiler.MyProfilerBlockKey, MyProfiler.MyProfilerBlock> keyValuePair in
                            this.m_blocksToAdd)
                    {
                        if (keyValuePair.Value.Parent != null)
                            keyValuePair.Value.Parent.Children.Add(keyValuePair.Value);
                        else
                            this.m_rootBlocks.Add(keyValuePair.Value);
                        this.m_profilingBlocks.Add(keyValuePair.Key, keyValuePair.Value);
                    }
                    this.m_blocksToAdd.Clear();
                    Interlocked.Exchange(ref this.m_remainingWindow, MyProfiler.UPDATE_WINDOW - 1);
                }
            }
            else if (this.m_historyLock.TryAcquireExclusive())
            {
                Interlocked.Exchange(ref this.m_remainingWindow, MyProfiler.UPDATE_WINDOW - 1);
                this.m_historyLock.ReleaseExclusive();
            }
            else if (Interlocked.Decrement(ref this.m_remainingWindow) < 0)
            {
                using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_historyLock))
                    Interlocked.Exchange(ref this.m_remainingWindow, MyProfiler.UPDATE_WINDOW - 1);
            }
            int num = 0;
            this.m_levelLimit = this.m_newLevelLimit;
            int index = (this.m_lastFrameIndex + 1)%MyProfiler.MAX_FRAMES;
            foreach (MyProfiler.MyProfilerBlock myProfilerBlock in this.m_profilingBlocks.Values)
            {
                num += myProfilerBlock.NumCalls;
                myProfilerBlock.ManagedMemory[index] = myProfilerBlock.ManagedDeltaMB;
                if (this.MemoryProfiling)
                    myProfilerBlock.ProcessMemory[index] = myProfilerBlock.ProcessDeltaMB;
                myProfilerBlock.NumCallsArray[index] = myProfilerBlock.NumCalls;
                myProfilerBlock.CustomValues[index] = myProfilerBlock.CustomValue;
                myProfilerBlock.Miliseconds[index] = (float) myProfilerBlock.Elapsed.Miliseconds;
                myProfilerBlock.averageMiliseconds =
                    (float)
                        (0.899999976158142*(double) myProfilerBlock.averageMiliseconds +
                         0.100000001490116*myProfilerBlock.Elapsed.Miliseconds);
                myProfilerBlock.Clear();
            }
            this.TotalCalls[index] = num;
            this.m_lastFrameIndex = index;
        }

        public void ClearFrame()
        {
            this.m_levelLimit = this.m_newLevelLimit;
            foreach (MyProfiler.MyProfilerBlock myProfilerBlock in this.m_profilingBlocks.Values)
                myProfilerBlock.Clear();
        }

        public void InitMemoryHack(string name)
        {
            this.StartBlock(name, "InitMemoryHack", 0, string.Empty);
            MyProfiler.MyProfilerBlock myProfilerBlock = this.m_currentProfilingStack.Peek();
            this.EndBlock("InitMemoryHack", 0, string.Empty, new MyTimeSpan?(), 0.0f, (string) null, (string) null);
            myProfilerBlock.StartManagedMB = 0L;
            myProfilerBlock.EndManagedMB = GC.GetTotalMemory(true);
            if (!this.MemoryProfiling)
                return;
            myProfilerBlock.StartProcessMB = 0L;
            myProfilerBlock.EndProcessMB = Environment.WorkingSet;
        }

        public void StartBlock(string name, string memberName, int line, string file)
        {
            if (this.m_levelLimit != -1 && this.m_currentProfilingStack.Count >= this.m_levelLimit)
            {
                ++this.m_levelSkipCount;
            }
            else
            {
                MyProfiler.MyProfilerBlock myProfilerBlock = (MyProfiler.MyProfilerBlock) null;
                MyProfiler.MyProfilerBlockKey key = new MyProfiler.MyProfilerBlockKey(file, memberName, name, line,
                    this.GetParentId());
                if (!this.m_profilingBlocks.TryGetValue(key, out myProfilerBlock) &&
                    !this.m_blocksToAdd.TryGetValue(key, out myProfilerBlock))
                {
                    myProfilerBlock = new MyProfiler.MyProfilerBlock(ref key, memberName, this.m_nextId++);
                    if (this.m_currentProfilingStack.Count > 0)
                        myProfilerBlock.Parent = this.m_currentProfilingStack.Peek();
                    this.m_blocksToAdd.Add(key, myProfilerBlock);
                }
                myProfilerBlock.Start(this.MemoryProfiling);
                this.m_currentProfilingStack.Push(myProfilerBlock);
            }
        }

        [Conditional("DEBUG")]
        private void CheckEndBlock(MyProfiler.MyProfilerBlock profilingBlock, string member, string file, int parentId)
        {
            if ((!MyProfiler.EnableAsserts || profilingBlock.Key.Member.Equals(member)) &&
                (profilingBlock.Key.ParentId == parentId && !(profilingBlock.Key.File != file)))
                return;
            StackTrace stackTrace = new StackTrace(2, true);
            for (int index = 0; index < stackTrace.FrameCount; ++index)
            {
                StackFrame frame = stackTrace.GetFrame(index);
                if (frame.GetFileName() == profilingBlock.Key.File && frame.GetMethod().Name == member)
                    break;
            }
        }

        public void EndBlock(string member, int line, string file, MyTimeSpan? customTime = null,
            float customValue = 0.0f, string timeFormat = null, string valueFormat = null)
        {
            if (this.m_levelSkipCount > 0)
            {
                --this.m_levelSkipCount;
            }
            else
            {
                MyProfiler.MyProfilerBlock myProfilerBlock = this.m_currentProfilingStack.Pop();
                myProfilerBlock.CustomValue = customValue;
                myProfilerBlock.TimeFormat = timeFormat;
                myProfilerBlock.ValueFormat = valueFormat;
                myProfilerBlock.End(this.MemoryProfiling, customTime);
                if (!this.AutoCommit || this.m_currentProfilingStack.Count != 0)
                    return;
                this.CommitInternal();
            }
        }

        public void ProfileCustomValue(string name, string member, int line, string file, float value,
            MyTimeSpan? customTime, string timeFormat, string valueFormat)
        {
            this.StartBlock(name, member, line, file);
            this.EndBlock(member, line, file, customTime, value, timeFormat, valueFormat);
        }

        public struct HistoryLock : IDisposable
        {
            private MyProfiler m_profiler;
            private FastResourceLock m_lock;

            public HistoryLock(MyProfiler profiler, FastResourceLock historyLock)
            {
                this.m_profiler = profiler;
                this.m_lock = historyLock;
                this.m_lock.AcquireExclusive();
                this.m_profiler.OnHistorySafe();
            }

            public void Dispose()
            {
                this.m_profiler.OnHistorySafe();
                this.m_lock.ReleaseExclusive();
                this.m_lock = (FastResourceLock) null;
            }
        }

        public class MyProfilerBlock
        {
            public float[] ProcessMemory = new float[MyProfiler.MAX_FRAMES];
            public float[] ManagedMemory = new float[MyProfiler.MAX_FRAMES];
            public float[] Miliseconds = new float[MyProfiler.MAX_FRAMES];
            public float[] CustomValues = new float[MyProfiler.MAX_FRAMES];
            public int[] NumCallsArray = new int[MyProfiler.MAX_FRAMES];
            public List<MyProfiler.MyProfilerBlock> Children = new List<MyProfiler.MyProfilerBlock>();
            public readonly int Id;
            public readonly MyProfiler.MyProfilerBlockKey Key;
            public MyTimeSpan MeasureStart;
            public MyTimeSpan Elapsed;
            public long StartManagedMB;
            public long EndManagedMB;
            public float DeltaManagedB;
            public float TotalManagedMB;
            public long StartProcessMB;
            public long EndProcessMB;
            public float DeltaProcessB;
            public float TotalProcessMB;
            public bool Invalid;
            public int NumCalls;
            public float CustomValue;
            public string TimeFormat;
            public string ValueFormat;
            public float averageMiliseconds;
            public MyProfiler.MyProfilerBlock Parent;

            public string Name
            {
                get { return this.Key.Name; }
            }

            public float ManagedDeltaMB
            {
                get { return this.DeltaManagedB*9.53674E-07f; }
            }

            public float ProcessDeltaMB
            {
                get { return this.DeltaProcessB*9.53674E-07f; }
            }

            public MyProfilerBlock(ref MyProfiler.MyProfilerBlockKey key, string memberName, int blockId)
            {
                this.Id = blockId;
                this.Key = key;
            }

            public void Reset()
            {
                this.MeasureStart = new MyTimeSpan(Stopwatch.GetTimestamp());
                this.Elapsed = MyTimeSpan.Zero;
            }

            public void Clear()
            {
                this.Reset();
                this.NumCalls = 0;
                this.StartManagedMB = 0L;
                this.EndManagedMB = 0L;
                this.DeltaManagedB = 0.0f;
                this.StartProcessMB = 0L;
                this.EndProcessMB = 0L;
                this.DeltaProcessB = 0.0f;
                this.CustomValue = 0.0f;
            }

            public void Start(bool memoryProfiling)
            {
                ++this.NumCalls;
                if (memoryProfiling)
                {
                    this.StartManagedMB = GC.GetTotalMemory(false);
                    this.StartProcessMB = Environment.WorkingSet;
                }
                this.MeasureStart = new MyTimeSpan(Stopwatch.GetTimestamp());
            }

            public void End(bool memoryProfiling, MyTimeSpan? customTime = null)
            {
                this.Elapsed += customTime ?? new MyTimeSpan(Stopwatch.GetTimestamp()) - this.MeasureStart;
                if (memoryProfiling)
                {
                    this.EndManagedMB = GC.GetTotalMemory(false);
                    this.EndProcessMB = Environment.WorkingSet;
                }
                this.DeltaManagedB += (float) (this.EndManagedMB - this.StartManagedMB);
                if (!memoryProfiling)
                    return;
                this.DeltaProcessB += (float) (this.EndProcessMB - this.StartProcessMB);
            }

            public override string ToString()
            {
                return this.Key.Name + " (" + this.NumCalls.ToString() + " calls)";
            }
        }

        public struct MyProfilerBlockKey
        {
            public readonly string File;
            public readonly string Member;
            public readonly string Name;
            public readonly int Line;
            public readonly int ParentId;
            public readonly int HashCode;

            public MyProfilerBlockKey(string file, string member, string name, int line, int parentId)
            {
                this.File = file;
                this.Member = member;
                this.Name = name;
                this.Line = line;
                this.ParentId = parentId;
                this.HashCode = file.GetHashCode();
                this.HashCode = 397*this.HashCode ^ member.GetHashCode();
                this.HashCode = 397*this.HashCode ^ (name ?? string.Empty).GetHashCode();
                this.HashCode = 397*this.HashCode ^ parentId.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                throw new InvalidBranchException("Equals is not supposed to be called, use comparer!");
            }

            public override int GetHashCode()
            {
                throw new InvalidBranchException("Get hash code is not supposed to be called, use comparer!");
            }
        }

        public class MyProfilerBlockKeyComparer : IEqualityComparer<MyProfiler.MyProfilerBlockKey>
        {
            public bool Equals(MyProfiler.MyProfilerBlockKey x, MyProfiler.MyProfilerBlockKey y)
            {
                if (x.ParentId == y.ParentId && x.Name == y.Name && (x.Member == y.Member && x.File == y.File))
                    return x.Line == y.Line;
                else
                    return false;
            }

            public int GetHashCode(MyProfiler.MyProfilerBlockKey obj)
            {
                return obj.HashCode;
            }
        }
    }
}