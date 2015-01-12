// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyConfigDedicated
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Definitions;
using System.Collections.Generic;

namespace Sandbox.ModAPI
{
    public interface IMyConfigDedicated
    {
        List<string> Administrators { get; set; }

        int AsteroidAmount { get; set; }

        List<ulong> Banned { get; set; }

        ulong GroupID { get; set; }

        bool IgnoreLastSession { get; set; }

        string IP { get; set; }

        string LoadWorld { get; }

        List<ulong> Mods { get; }

        bool PauseGameWhenEmpty { get; set; }

        SerializableDefinitionId Scenario { get; set; }

        string ServerName { get; set; }

        int ServerPort { get; set; }

        MyObjectBuilder_SessionSettings SessionSettings { get; set; }

        int SteamPort { get; set; }

        string WorldName { get; set; }

        string GetFilePath();

        void Load();

        void Save();
    }
}