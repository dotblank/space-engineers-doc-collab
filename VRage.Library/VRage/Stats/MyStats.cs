// Decompiled with JetBrains decompiler
// Type: VRage.Stats.MyStats
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using VRage;

namespace VRage.Stats
{
  public class MyStats
  {
    private static Comparer<KeyValuePair<string, MyStat>> m_nameComparer = (Comparer<KeyValuePair<string, MyStat>>) new MyNameComparer();
    private MyGameTimer m_timer = new MyGameTimer();
    private NumberFormatInfo m_format = new NumberFormatInfo()
    {
      NumberDecimalSeparator = ".",
      NumberGroupSeparator = " "
    };
    private FastResourceLock m_lock = new FastResourceLock();
    private Dictionary<string, MyStat> m_stats = new Dictionary<string, MyStat>(1024);
    private List<KeyValuePair<string, MyStat>> m_tmpWriteList = new List<KeyValuePair<string, MyStat>>(1024);
    public volatile MyStats.SortEnum Sort;

    private MyStat GetStat(string name)
    {
      MyStat myStat;
      using (FastResourceLockExtensions.AcquireSharedUsing(this.m_lock))
      {
        if (this.m_stats.TryGetValue(name, out myStat))
          return myStat;
      }
      using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
      {
        if (this.m_stats.TryGetValue(name, out myStat))
          return myStat;
        myStat = new MyStat();
        this.m_stats[name] = myStat;
        return myStat;
      }
    }

    public void Clear()
    {
      using (FastResourceLockExtensions.AcquireSharedUsing(this.m_lock))
      {
        foreach (KeyValuePair<string, MyStat> keyValuePair in this.m_stats)
          keyValuePair.Value.Clear();
      }
    }

    public void RemoveAll()
    {
      using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
        this.m_stats.Clear();
    }

    public void Remove(string name)
    {
      using (FastResourceLockExtensions.AcquireExclusiveUsing(this.m_lock))
        this.m_stats.Remove(name);
    }

    public void Clear(string name)
    {
      this.GetStat(name).Clear();
    }

    public void Increment(string name, int refreshMs = 0, int clearRateMs = -1)
    {
      this.Write(name, 0L, MyStatTypeEnum.Counter, refreshMs, 0, clearRateMs);
    }

    public MyStatToken Measure(string name, MyStatTypeEnum type = MyStatTypeEnum.Avg, int refreshMs = 200, int numDecimals = 1, int clearRateMs = -1)
    {
      MyStat stat = this.GetStat(name);
      if (stat.DrawText == null)
        stat.DrawText = this.GetMeasureText(name, type);
      stat.ChangeSettings((type | MyStatTypeEnum.FormatFlag) & (MyStatTypeEnum) 191, refreshMs, numDecimals, clearRateMs);
      return new MyStatToken(this.m_timer, stat);
    }

    private string GetMeasureText(string name, MyStatTypeEnum type)
    {
      switch (type & (MyStatTypeEnum) 15)
      {
        case MyStatTypeEnum.MinMax:
          return name + ": {0}ms / {1}ms";
        case MyStatTypeEnum.MinMaxAvg:
          return name + ": {0}ms / {1}ms / {2}ms";
        case MyStatTypeEnum.Counter:
          return name + ": {0}x";
        case MyStatTypeEnum.CounterSum:
          return name + ": {0}x / {1}ms";
        default:
          return name + ": {0}ms";
      }
    }

    public void Write(string name, float value, MyStatTypeEnum type, int refreshMs, int numDecimals, int clearRateMs = -1)
    {
      this.GetStat(name).Write(value, type, refreshMs, numDecimals, clearRateMs);
    }

    public void Write(string name, long value, MyStatTypeEnum type, int refreshMs, int numDecimals, int clearRateMs = -1)
    {
      this.GetStat(name).Write(value, type, refreshMs, numDecimals, clearRateMs);
    }

    public void WriteFormat(string name, float value, MyStatTypeEnum type, int refreshMs, int numDecimals, int clearRateMs = -1)
    {
      this.GetStat(name).Write(value, type | MyStatTypeEnum.FormatFlag, refreshMs, numDecimals, clearRateMs);
    }

    public void WriteFormat(string name, long value, MyStatTypeEnum type, int refreshMs, int numDecimals, int clearRateMs = -1)
    {
      this.GetStat(name).Write(value, type | MyStatTypeEnum.FormatFlag, refreshMs, numDecimals, clearRateMs);
    }

    public void WriteTo(StringBuilder writeTo)
    {
      lock (this.m_tmpWriteList)
      {
        try
        {
          using (FastResourceLockExtensions.AcquireSharedUsing(this.m_lock))
          {
            foreach (KeyValuePair<string, MyStat> item_0 in this.m_stats)
              this.m_tmpWriteList.Add(item_0);
          }
          if (this.Sort == MyStats.SortEnum.Name)
            this.m_tmpWriteList.Sort((IComparer<KeyValuePair<string, MyStat>>) MyStats.m_nameComparer);
          foreach (KeyValuePair<string, MyStat> item_1 in this.m_tmpWriteList)
            this.AppendStat(writeTo, item_1.Key, item_1.Value);
        }
        finally
        {
          this.m_tmpWriteList.Clear();
        }
      }
    }

    private void AppendStatLine<A, B, C>(StringBuilder text, string statName, A arg0, B arg1, C arg2, NumberFormatInfo format, string formatString) where A : IConvertible where B : IConvertible where C : IConvertible
    {
      if (formatString == null)
        StringBuilderExtensions.ConcatFormat<A, B, C>(text, statName, arg0, arg1, arg2, format);
      else
        StringBuilderExtensions.ConcatFormat<string, A, B, C>(text, formatString, statName, arg0, arg1, arg2, format);
      text.AppendLine();
    }

    private MyTimeSpan RequiredInactivity(MyStatTypeEnum type)
    {
      if ((type & MyStatTypeEnum.DontDisappearFlag) == MyStatTypeEnum.DontDisappearFlag)
        return MyTimeSpan.MaxValue;
      if ((type & MyStatTypeEnum.KeepInactiveLongerFlag) == MyStatTypeEnum.KeepInactiveLongerFlag)
        return MyTimeSpan.FromSeconds(30.0);
      else
        return MyTimeSpan.FromSeconds(3.0);
    }

    private void AppendStat(StringBuilder text, string statKey, MyStat stat)
    {
      MyStat.Value sum;
      int count;
      MyStat.Value min;
      MyStat.Value max;
      MyStat.Value last;
      MyStatTypeEnum type;
      int decimals;
      MyTimeSpan inactivityMs;
      stat.ReadAndClear(this.m_timer.Elapsed, out sum, out count, out min, out max, out last, out type, out decimals, out inactivityMs);
      if (inactivityMs > this.RequiredInactivity(type))
      {
        this.Remove(statKey);
      }
      else
      {
        string statName = stat.DrawText ?? statKey;
        bool flag1 = (type & MyStatTypeEnum.LongFlag) == MyStatTypeEnum.LongFlag;
        float num = (flag1 ? (float) sum.AsLong : sum.AsFloat) / (float) count;
        this.m_format.NumberDecimalDigits = decimals;
        this.m_format.NumberGroupSeparator = decimals == 0 ? "," : string.Empty;
        bool flag2 = (type & MyStatTypeEnum.FormatFlag) == MyStatTypeEnum.FormatFlag;
        switch (type & (MyStatTypeEnum) 15)
        {
          case MyStatTypeEnum.CurrentValue:
            if (flag1)
            {
              this.AppendStatLine<long, int, int>(text, statName, last.AsLong, 0, 0, this.m_format, flag2 ? (string) null : "{0}: {1}");
              break;
            }
            else
            {
              this.AppendStatLine<float, int, int>(text, statName, last.AsFloat, 0, 0, this.m_format, flag2 ? (string) null : "{0}: {1}");
              break;
            }
          case MyStatTypeEnum.Min:
            if (flag1)
            {
              this.AppendStatLine<long, int, int>(text, statName, min.AsLong, 0, 0, this.m_format, flag2 ? (string) null : "{0}: {1}");
              break;
            }
            else
            {
              this.AppendStatLine<float, int, int>(text, statName, min.AsFloat, 0, 0, this.m_format, flag2 ? (string) null : "{0}: {1}");
              break;
            }
          case MyStatTypeEnum.Max:
            if (flag1)
            {
              this.AppendStatLine<long, int, int>(text, statName, max.AsLong, 0, 0, this.m_format, flag2 ? (string) null : "{0}: {1}");
              break;
            }
            else
            {
              this.AppendStatLine<float, int, int>(text, statName, max.AsFloat, 0, 0, this.m_format, flag2 ? (string) null : "{0}: {1}");
              break;
            }
          case MyStatTypeEnum.Avg:
            this.AppendStatLine<float, int, int>(text, statName, num, 0, 0, this.m_format, flag2 ? (string) null : "{0}: {1}");
            break;
          case MyStatTypeEnum.MinMax:
            if (flag1)
            {
              this.AppendStatLine<long, long, int>(text, statName, min.AsLong, max.AsLong, 0, this.m_format, flag2 ? (string) null : "{0}: {1} / {2}");
              break;
            }
            else
            {
              this.AppendStatLine<float, float, int>(text, statName, min.AsFloat, max.AsFloat, 0, this.m_format, flag2 ? (string) null : "{0}: {1} / {2}");
              break;
            }
          case MyStatTypeEnum.MinMaxAvg:
            if (flag1)
            {
              this.AppendStatLine<long, long, float>(text, statName, min.AsLong, max.AsLong, num, this.m_format, flag2 ? (string) null : "{0}: {1} / {2} / {3}");
              break;
            }
            else
            {
              this.AppendStatLine<float, float, float>(text, statName, min.AsFloat, max.AsFloat, num, this.m_format, flag2 ? (string) null : "{0}: {1} / {2} / {3}");
              break;
            }
          case MyStatTypeEnum.Sum:
            if (flag1)
            {
              this.AppendStatLine<long, int, int>(text, statName, sum.AsLong, 0, 0, this.m_format, flag2 ? (string) null : "{0}: {1}");
              break;
            }
            else
            {
              this.AppendStatLine<float, int, int>(text, statName, sum.AsFloat, 0, 0, this.m_format, flag2 ? (string) null : "{0}: {1}");
              break;
            }
          case MyStatTypeEnum.Counter:
            this.AppendStatLine<int, int, int>(text, statName, count, 0, 0, this.m_format, flag2 ? (string) null : "{0}: {1}");
            break;
          case MyStatTypeEnum.CounterSum:
            if (flag1)
            {
              this.AppendStatLine<int, long, int>(text, statName, count, sum.AsLong, 0, this.m_format, flag2 ? (string) null : "{0}: {1} / {2}");
              break;
            }
            else
            {
              this.AppendStatLine<int, float, int>(text, statName, count, sum.AsFloat, 0, this.m_format, flag2 ? (string) null : "{0}: {1} / {2}");
              break;
            }
        }
      }
    }

    public enum SortEnum
    {
      None,
      Name,
    }
  }
}
