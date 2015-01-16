// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyHudObjectiveLine
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Collections.Generic;

namespace Sandbox.ModAPI
{
    public interface IMyHudObjectiveLine
    {
        bool Visible { get; }

        string Title { get; set; }

        string CurrentObjective { get; }

        List<string> Objectives { get; set; }

        void Show();

        void Hide();

        void AdvanceObjective();
    }
}