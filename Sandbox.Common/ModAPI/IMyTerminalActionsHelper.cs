// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyTerminalActionsHelper
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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

        ITerminalProperty GetProperty(string id, Type blockType);

        void GetProperties(Type blockType, List<ITerminalProperty> resultList,
            Func<ITerminalProperty, bool> collect = null);

        IMyGridTerminalSystem GetTerminalSystemForGrid(IMyCubeGrid grid);
    }
}