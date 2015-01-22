// Decompiled with JetBrains decompiler
// Type: VRage.Stats.MyStatToken
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using VRage;

namespace VRage.Stats
{
  public struct MyStatToken : IDisposable
  {
    private readonly MyGameTimer m_timer;
    private readonly MyTimeSpan m_startTime;
    private readonly MyStat m_stat;

    internal MyStatToken(MyGameTimer timer, MyStat stat)
    {
      this.m_timer = timer;
      this.m_startTime = timer.Elapsed;
      this.m_stat = stat;
    }

    public void Dispose()
    {
      this.m_stat.Write((float) (this.m_timer.Elapsed - this.m_startTime).Miliseconds);
    }
  }
}
