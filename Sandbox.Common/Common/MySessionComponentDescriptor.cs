// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MySessionComponentDescriptor
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class MySessionComponentDescriptor : Attribute
    {
        public MyUpdateOrder UpdateOrder;
        public int Priority;

        public MySessionComponentDescriptor(MyUpdateOrder updateOrder)
            : this(updateOrder, 1000)
        {
        }

        public MySessionComponentDescriptor(MyUpdateOrder updateOrder, int priority)
        {
            this.UpdateOrder = updateOrder;
            this.Priority = priority;
        }
    }
}