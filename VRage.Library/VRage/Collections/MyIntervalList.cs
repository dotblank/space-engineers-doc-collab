﻿// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MyIntervalList
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;

namespace VRage.Collections
{
    public class MyIntervalList
    {
        private List<int> m_list;

        public MyIntervalList()
        {
            this.m_list = new List<int>(8);
        }

        public override string ToString()
        {
            string str = "";
            int index = 0;
            while (index < this.m_list.Count)
            {
                if (index != 0)
                    str = str + "; ";
                str = str + (object) "<" + (string) (object) this.m_list[index] + "," +
                      (string) (object) this.m_list[index + 1] + ">";
                index += 2;
            }
            return str;
        }

        public void Add(int value)
        {
            if (value == int.MinValue)
            {
                if (this.m_list.Count == 0)
                    this.InsertInterval(0, value, value);
                else if (this.m_list[0] == -2147483647)
                {
                    this.ExtendIntervalDown(0);
                }
                else
                {
                    if (this.m_list[0] == int.MinValue)
                        return;
                    this.InsertInterval(0, value, value);
                }
            }
            else if (value == int.MaxValue)
            {
                int i = this.m_list.Count - 2;
                if (i < 0)
                    this.InsertInterval(0, value, value);
                else if (this.m_list[i + 1] == 2147483646)
                {
                    this.ExtendIntervalUp(i);
                }
                else
                {
                    if (this.m_list[i + 1] == int.MaxValue)
                        return;
                    this.InsertInterval(this.m_list.Count, value, value);
                }
            }
            else
            {
                int index = 0;
                while (index < this.m_list.Count)
                {
                    if (value + 1 < this.m_list[index])
                    {
                        this.InsertInterval(index, value, value);
                        return;
                    }
                    else
                    {
                        if (value - 1 <= this.m_list[index + 1])
                        {
                            if (value + 1 == this.m_list[index])
                            {
                                this.ExtendIntervalDown(index);
                                return;
                            }
                            else if (value - 1 == this.m_list[index + 1])
                            {
                                this.ExtendIntervalUp(index);
                                return;
                            }
                        }
                        index += 2;
                    }
                }
                this.InsertInterval(this.m_list.Count, value, value);
            }
        }

        public bool Contains(int value)
        {
            int index = 0;
            while (index < this.m_list.Count && value >= this.m_list[index])
            {
                if (value <= this.m_list[index + 1])
                    return true;
                index += 2;
            }
            return false;
        }

        private void InsertInterval(int listPosition, int min, int max)
        {
            if (listPosition == this.m_list.Count)
            {
                this.m_list.Add(min);
                this.m_list.Add(max);
            }
            else
            {
                int index = this.m_list.Count - 2;
                this.m_list.Add(this.m_list[index]);
                this.m_list.Add(this.m_list[index + 1]);
                while (index > listPosition)
                {
                    this.m_list[index] = this.m_list[index - 2];
                    this.m_list[index + 1] = this.m_list[index - 1];
                    index -= 2;
                }
                this.m_list[index] = min;
                this.m_list[index + 1] = max;
            }
        }

        private void ExtendIntervalDown(int i)
        {
            List<int> list;
            int index;
            (list = this.m_list)[index = i] = list[index] - 1;
            if (i == 0)
                return;
            this.TryMergeIntervals(i - 1, i);
        }

        private void ExtendIntervalUp(int i)
        {
            List<int> list;
            int index;
            (list = this.m_list)[index = i + 1] = list[index] + 1;
            if (i >= this.m_list.Count - 2)
                return;
            this.TryMergeIntervals(i + 1, i + 2);
        }

        private void TryMergeIntervals(int i1, int i2)
        {
            if (this.m_list[i1] + 1 != this.m_list[i2])
                return;
            for (int index = i1; index < this.m_list.Count - 2; ++index)
                this.m_list[index] = this.m_list[index + 2];
            this.m_list.RemoveAt(this.m_list.Count - 1);
            this.m_list.RemoveAt(this.m_list.Count - 1);
        }
    }
}