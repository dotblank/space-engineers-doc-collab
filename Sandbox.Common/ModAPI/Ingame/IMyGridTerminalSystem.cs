// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMyGridTerminalSystem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;
using System.Collections.Generic;

namespace Sandbox.ModAPI.Ingame
{
    public interface IMyGridTerminalSystem
    {
        List<IMyTerminalBlock> Blocks { get; }

        List<IMyBlockGroup> BlockGroups { get; }

        void GetBlocksOfType<T>(List<IMyTerminalBlock> blocks, Func<IMyTerminalBlock, bool> collect = null);

        void SearchBlocksOfName(string name, List<IMyTerminalBlock> blocks, Func<IMyTerminalBlock, bool> collect = null);

        IMyTerminalBlock GetBlockWithName(string name);
    }
}