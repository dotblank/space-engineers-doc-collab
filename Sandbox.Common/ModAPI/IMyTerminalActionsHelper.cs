// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyTerminalActionsHelper
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using System;
using System.Collections.Generic;

namespace Sandbox.ModAPI
{
    public interface IMyTerminalActionsHelper
    {
        void GetActions(Type blockType, List<ITerminalAction> resultList, Func<ITerminalAction, bool> collect = null);

        void SearchActionsOfName(string name, Type blockType, List<ITerminalAction> resultList,
            Func<ITerminalAction, bool> collect = null);

        ITerminalAction GetActionWithName(string nameType, Type blockType);

        IMyGridTerminalSystem GetTerminalSystemForGrid(IMyCubeGrid grid);
    }
}