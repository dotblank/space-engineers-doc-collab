// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyUpdateOrder
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Common
{
    [Flags]
    public enum MyUpdateOrder
    {
        BeforeSimulation = 1,
        Simulation = 2,
        AfterSimulation = 4,
        NoUpdate = 8,
    }
}