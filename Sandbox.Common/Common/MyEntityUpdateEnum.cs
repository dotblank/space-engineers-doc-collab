// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyEntityUpdateEnum
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Common
{
    [Flags]
    public enum MyEntityUpdateEnum
    {
        NONE = 0,
        EACH_FRAME = 1,
        EACH_10TH_FRAME = 2,
        EACH_100TH_FRAME = 4,
        BEFORE_NEXT_FRAME = 8,
    }
}