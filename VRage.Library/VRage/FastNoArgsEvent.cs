// Decompiled with JetBrains decompiler
// Type: VRage.FastNoArgsEvent
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;

namespace VRage
{
  public class FastNoArgsEvent
  {
    private FastResourceLock m_lock = new FastResourceLock();
    private List<MyNoArgsDelegate> m_delegates = new List<MyNoArgsDelegate>(2);
    private List<MyNoArgsDelegate> m_delegatesIterator = new List<MyNoArgsDelegate>(2);

    public event MyNoArgsDelegate Event
    {
      add
      {
        using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
          this.m_delegates.Add(value);
      }
      remove
      {
        using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
          this.m_delegates.Remove(value);
      }
    }

    public void Raise()
    {
      using (FastResourceLockExtensions.AcquireSharedUsing(this.m_lock))
      {
        this.m_delegatesIterator.Clear();
        foreach (MyNoArgsDelegate myNoArgsDelegate in this.m_delegates)
          this.m_delegatesIterator.Add(myNoArgsDelegate);
      }
      foreach (MyNoArgsDelegate myNoArgsDelegate in this.m_delegatesIterator)
        myNoArgsDelegate();
      this.m_delegatesIterator.Clear();
    }
  }
}
