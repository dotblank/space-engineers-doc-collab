// Decompiled with JetBrains decompiler
// Type: VRage.FastResourceLockExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Diagnostics;

namespace VRage
{
    public static class FastResourceLockExtensions
    {
        [DebuggerStepThrough]
        public static FastResourceLockExtensions.MySharedLock AcquireSharedUsing(this FastResourceLock lockObject)
        {
            return new FastResourceLockExtensions.MySharedLock(lockObject);
        }

        [DebuggerStepThrough]
        public static FastResourceLockExtensions.MyExclusiveLock AcquireExclusiveUsing(this FastResourceLock lockObject)
        {
            return new FastResourceLockExtensions.MyExclusiveLock(lockObject);
        }

        public struct MySharedLock : IDisposable
        {
            private FastResourceLock m_lockObject;

            [DebuggerStepThrough]
            public MySharedLock(FastResourceLock lockObject)
            {
                this.m_lockObject = lockObject;
                this.m_lockObject.AcquireShared();
            }

            public void Dispose()
            {
                this.m_lockObject.ReleaseShared();
            }
        }

        public struct MyExclusiveLock : IDisposable
        {
            private FastResourceLock m_lockObject;

            [DebuggerStepThrough]
            public MyExclusiveLock(FastResourceLock lockObject)
            {
                this.m_lockObject = lockObject;
                this.m_lockObject.AcquireExclusive();
            }

            public void Dispose()
            {
                this.m_lockObject.ReleaseExclusive();
            }
        }
    }
}