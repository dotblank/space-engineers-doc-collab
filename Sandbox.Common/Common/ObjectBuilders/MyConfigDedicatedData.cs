// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyConfigDedicatedData
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders.Definitions;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
    [XmlRoot("MyConfigDedicated")]
    public class MyConfigDedicatedData
    {
        public MyObjectBuilder_SessionSettings SessionSettings = new MyObjectBuilder_SessionSettings();
        public string IP = "0.0.0.0";
        public int SteamPort = 8766;
        public int ServerPort = 27016;
        public int AsteroidAmount = 4;
        [XmlArrayItem("unsignedLong")] public List<string> Administrators = new List<string>();
        public List<ulong> Banned = new List<ulong>();
        public List<ulong> Mods = new List<ulong>();
        public string ServerName = "";
        public string WorldName = "";
        public SerializableDefinitionId Scenario;
        public string LoadWorld;
        public ulong GroupID;
        public bool PauseGameWhenEmpty;
        public bool IgnoreLastSession;
    }
}