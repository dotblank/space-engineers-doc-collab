// Decompiled with JetBrains decompiler
// Type: VRage.Utils.MyTimer
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace VRage.Utils
{
  public sealed class MyTimer : IDisposable
  {
    private const int TIME_ONESHOT = 0;
    private const int TIME_PERIODIC = 1;
    private int m_interval;
    private Action m_callback;
    private int mTimerId;
    private MyTimer.TimerEventHandler mHandler;

    public MyTimer(int intervalMS, Action callback)
    {
      this.m_interval = intervalMS;
      this.m_callback = callback;
      this.mHandler = new MyTimer.TimerEventHandler(this.OnTimer);
    }

    ~MyTimer()
    {
      this.Stop();
    }

    private void OnTimer(int id, int msg, IntPtr user, int dw1, int dw2)
    {
      this.m_callback();
    }

    public static void StartOneShot(int intervalMS, MyTimer.TimerEventHandler handler)
    {
      MyTimer.timeSetEvent(intervalMS, 1, handler, IntPtr.Zero, 0);
    }

    public void Start()
    {
      MyTimer.timeBeginPeriod(1);
      this.mTimerId = MyTimer.timeSetEvent(this.m_interval, 1, this.mHandler, IntPtr.Zero, 1);
    }

    public void Stop()
    {
      if (this.mTimerId == 0)
        return;
      MyTimer.timeKillEvent(this.mTimerId);
      MyTimer.timeEndPeriod(1);
      this.mTimerId = 0;
    }

    public void Dispose()
    {
      this.Stop();
      GC.SuppressFinalize((object) this);
    }

    [DllImport("winmm.dll")]
    private static extern int timeSetEvent(int delay, int resolution, MyTimer.TimerEventHandler handler, IntPtr user, int eventType);

    [DllImport("winmm.dll")]
    private static extern int timeKillEvent(int id);

    [DllImport("winmm.dll")]
    private static extern int timeBeginPeriod(int msec);

    [DllImport("winmm.dll")]
    private static extern int timeEndPeriod(int msec);

    public delegate void TimerEventHandler(int id, int msg, IntPtr user, int dw1, int dw2);
  }
}
