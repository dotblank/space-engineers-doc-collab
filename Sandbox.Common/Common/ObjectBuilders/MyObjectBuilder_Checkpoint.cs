// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Checkpoint
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders.Definitions;
using Sandbox.Common.ObjectBuilders.Serializer;
using Sandbox.Common.ObjectBuilders.VRageData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using VRage.Serialization;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_Checkpoint : MyObjectBuilder_Base
    {
        private static SerializableDefinitionId DEFAULT_SCENARIO =
            new SerializableDefinitionId((MyObjectBuilderType) typeof (MyObjectBuilder_ScenarioDefinition), "EmptyWorld");

        [ProtoMember(5)] public MyPositionAndOrientation SpectatorPosition =
            new MyPositionAndOrientation((MatrixD) Matrix.Identity);

        [ProtoMember(9)] [DefaultValue(-1)] public long ControlledObject = -1L;
        [ProtoMember(32)] [DefaultValue(null)] public ulong? WorkshopId = new ulong?();

        [XmlElement("Settings", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_SessionSettings>))] [ProtoMember(51)] public MyObjectBuilder_SessionSettings Settings =
            MyObjectBuilderSerializer.CreateNewObject<MyObjectBuilder_SessionSettings>();

        [ProtoMember(56)] public SerializableDefinitionId Scenario = MyObjectBuilder_Checkpoint.DEFAULT_SCENARIO;

        [ProtoMember(60)] public MyEnvironmentHostilityEnum? PreviousEnvironmentHostility =
            new MyEnvironmentHostilityEnum?();

        [ProtoMember(1)] public SerializableVector3I CurrentSector;
        [ProtoMember(3)] public long ElapsedGameTime;
        [ProtoMember(4)] public string SessionName;
        [DefaultValue(MyCameraControllerEnum.Spectator)] [ProtoMember(7)] public MyCameraControllerEnum CameraController;
        [ProtoMember(8)] public long CameraEntity;
        [ProtoMember(15)] public string Password;
        [ProtoMember(20)] public string Description;
        [ProtoMember(22)] public DateTime LastSaveTime;
        [ProtoMember(23)] public string WorldID;
        [ProtoMember(26)] public float SpectatorDistance;
        public SerializableDictionary<ulong, MyObjectBuilder_Player> Players;

        [ProtoMember(33)] public SerializableDictionary<MyObjectBuilder_Checkpoint.PlayerId, MyObjectBuilder_Player>
            ConnectedPlayers;

        [ProtoMember(34)] public SerializableDictionary<MyObjectBuilder_Checkpoint.PlayerId, long> DisconnectedPlayers;
        [ProtoMember(36)] public MyObjectBuilder_Toolbar CharacterToolbar;

        [ProtoMember(40)] public SerializableDictionaryCompat<long, MyObjectBuilder_Checkpoint.PlayerId, ulong>
            ControlledEntities;

        [ProtoMember(52)] public int AppVersion;
        [DefaultValue(null)] [ProtoMember(53)] public MyObjectBuilder_FactionCollection Factions;
        public List<MyObjectBuilder_Checkpoint.PlayerItem> AllPlayers;
        [ProtoMember(55)] public List<MyObjectBuilder_Checkpoint.ModItem> Mods;
        [ProtoMember(57)] public List<MyObjectBuilder_Checkpoint.RespawnCooldownItem> RespawnCooldowns;
        [ProtoMember(58)] public List<MyObjectBuilder_Identity> Identities;
        [ProtoMember(59)] public List<MyObjectBuilder_Client> Clients;

        [ProtoMember(62)] public SerializableDictionary<MyObjectBuilder_Checkpoint.PlayerId, MyObjectBuilder_Player>
            AllPlayersData;

        [ProtoMember(64)] public List<MyObjectBuilder_ChatHistory> ChatHistory;
        [ProtoMember(65)] public List<MyObjectBuilder_FactionChatHistory> FactionChatHistory;
        [ProtoMember(66)] public List<long> NonPlayerIdentities;

        public DateTime GameTime
        {
            get { return new DateTime(2081, 1, 1, 0, 0, 0, DateTimeKind.Utc) + new TimeSpan(this.ElapsedGameTime); }
            set { this.ElapsedGameTime = (value - new DateTime(2081, 1, 1)).Ticks; }
        }

        public MyOnlineModeEnum OnlineMode
        {
            get { return this.Settings.OnlineMode; }
            set { this.Settings.OnlineMode = value; }
        }

        public MySessionHarvestMode HarvestMode
        {
            get { return MySessionHarvestMode.REALISTIC; }
            set { this.GameType = (MySessionGameType) value; }
        }

        public bool AutoHealing
        {
            get { return this.Settings.AutoHealing; }
            set { this.Settings.AutoHealing = value; }
        }

        public bool AutoSave
        {
            get { return this.Settings.AutoSaveInMinutes > 0U; }
            set { this.Settings.AutoSaveInMinutes = value ? 5U : 0U; }
        }

        public bool EnableCopyPaste
        {
            get { return this.Settings.EnableCopyPaste; }
            set { this.Settings.EnableCopyPaste = value; }
        }

        public short MaxPlayers
        {
            get { return this.Settings.MaxPlayers; }
            set { this.Settings.MaxPlayers = value; }
        }

        public bool WeaponsEnabled
        {
            get { return this.Settings.WeaponsEnabled; }
            set { this.Settings.WeaponsEnabled = value; }
        }

        public bool ShowPlayerNamesOnHud
        {
            get { return this.Settings.ShowPlayerNamesOnHud; }
            set { this.Settings.ShowPlayerNamesOnHud = value; }
        }

        public MySessionGameType GameType
        {
            get { return MySessionGameType.SURVIVAL; }
            set
            {
                switch (value)
                {
                    case MySessionGameType.SURVIVAL:
                        this.GameMode = MyGameModeEnum.Survival;
                        this.InventorySizeMultiplier = 1f;
                        this.AssemblerSpeedMultiplier = 1f;
                        this.AssemblerEfficiencyMultiplier = 1f;
                        this.RefinerySpeedMultiplier = 1f;
                        break;
                    case MySessionGameType.THREE_TIMES:
                        this.GameMode = MyGameModeEnum.Survival;
                        this.InventorySizeMultiplier = 3f;
                        this.AssemblerSpeedMultiplier = 3f;
                        this.AssemblerEfficiencyMultiplier = 3f;
                        this.RefinerySpeedMultiplier = 1f;
                        break;
                    case MySessionGameType.TEN_TIMES:
                        this.GameMode = MyGameModeEnum.Survival;
                        this.InventorySizeMultiplier = 10f;
                        this.AssemblerSpeedMultiplier = 10f;
                        this.AssemblerEfficiencyMultiplier = 10f;
                        this.RefinerySpeedMultiplier = 1f;
                        break;
                    case MySessionGameType.CREATIVE:
                        this.GameMode = MyGameModeEnum.Creative;
                        this.InventorySizeMultiplier = 1f;
                        this.AssemblerSpeedMultiplier = 1f;
                        this.AssemblerEfficiencyMultiplier = 1f;
                        this.RefinerySpeedMultiplier = 1f;
                        break;
                }
            }
        }

        public short MaxFloatingObjects
        {
            get { return this.Settings.MaxFloatingObjects; }
            set { this.Settings.MaxFloatingObjects = value; }
        }

        public MyGameModeEnum GameMode
        {
            get { return this.Settings.GameMode; }
            set { this.Settings.GameMode = value; }
        }

        public float InventorySizeMultiplier
        {
            get { return this.Settings.InventorySizeMultiplier; }
            set { this.Settings.InventorySizeMultiplier = value; }
        }

        public float AssemblerSpeedMultiplier
        {
            get { return this.Settings.AssemblerSpeedMultiplier; }
            set { this.Settings.AssemblerSpeedMultiplier = value; }
        }

        public float AssemblerEfficiencyMultiplier
        {
            get { return this.Settings.AssemblerEfficiencyMultiplier; }
            set { this.Settings.AssemblerEfficiencyMultiplier = value; }
        }

        public float RefinerySpeedMultiplier
        {
            get { return this.Settings.RefinerySpeedMultiplier; }
            set { this.Settings.RefinerySpeedMultiplier = value; }
        }

        public bool ThrusterDamage
        {
            get { return this.Settings.ThrusterDamage; }
            set { this.Settings.ThrusterDamage = value; }
        }

        public bool CargoShipsEnabled
        {
            get { return this.Settings.CargoShipsEnabled; }
            set { this.Settings.CargoShipsEnabled = value; }
        }

        public bool ShouldSerializeGameTime()
        {
            return false;
        }

        public bool ShouldSerializeOnlineMode()
        {
            return false;
        }

        public bool ShouldSerializeHarvestMode()
        {
            return false;
        }

        public bool ShouldSerializeAutoHealing()
        {
            return false;
        }

        public bool ShouldSerializeAutoSave()
        {
            return false;
        }

        public bool ShouldSerializeWorkshopId()
        {
            return this.WorkshopId.HasValue;
        }

        public bool ShouldSerializeEnableCopyPaste()
        {
            return false;
        }

        public bool ShouldSerializeMaxPlayers()
        {
            return false;
        }

        public bool ShouldSerializeWeaponsEnabled()
        {
            return false;
        }

        public bool ShouldSerializeShowPlayerNamesOnHud()
        {
            return false;
        }

        public bool ShouldSerializeGameType()
        {
            return false;
        }

        public bool ShouldSerializeMaxFloatingObjects()
        {
            return false;
        }

        public bool ShouldSerializeGameMode()
        {
            return false;
        }

        public bool ShouldSerializeInventorySizeMultiplier()
        {
            return false;
        }

        public bool ShouldSerializeAssemblerSpeedMultiplier()
        {
            return false;
        }

        public bool ShouldSerializeAssemblerEfficiencyMultiplier()
        {
            return false;
        }

        public bool ShouldSerializeRefinerySpeedMultiplier()
        {
            return false;
        }

        public bool ShouldSerializeThrusterDamage()
        {
            return false;
        }

        public bool ShouldSerializeCargoShipsEnabled()
        {
            return false;
        }

        public bool ShouldSerializeClients()
        {
            if (this.Clients != null)
                return this.Clients.Count != 0;
            else
                return false;
        }

        public struct PlayerId : IAssignableFrom<ulong>
        {
            public ulong ClientId;
            public int SerialId;

            public PlayerId(ulong steamId)
            {
                this.ClientId = steamId;
                this.SerialId = 0;
            }

            public void AssignFrom(ulong steamId)
            {
                this.ClientId = steamId;
                this.SerialId = 0;
            }
        }

        [ProtoContract]
        public struct PlayerItem
        {
            [ProtoMember(1)] public long PlayerId;
            [ProtoMember(2)] public bool IsDead;
            [ProtoMember(3)] public string Name;
            [ProtoMember(4)] public ulong SteamId;
            [ProtoMember(5)] public string Model;

            public PlayerItem(long id, string name, bool isDead, ulong steamId, string model)
            {
                this.PlayerId = id;
                this.IsDead = isDead;
                this.Name = name;
                this.SteamId = steamId;
                this.Model = model;
            }
        }

        [ProtoContract]
        public struct ModItem
        {
            [ProtoMember(1)] public string Name;
            [ProtoMember(2)] [DefaultValue(0)] public ulong PublishedFileId;
            [XmlIgnore] public string FriendlyName;

            public ModItem(ulong publishedFileId)
            {
                this.Name = publishedFileId.ToString() + ".sbm";
                this.PublishedFileId = publishedFileId;
                this.FriendlyName = string.Empty;
            }

            public ModItem(string name, ulong publishedFileId)
            {
                this.Name = name ?? publishedFileId.ToString() + ".sbm";
                this.PublishedFileId = publishedFileId;
                this.FriendlyName = string.Empty;
            }

            public ModItem(string name, ulong publishedFileId, string friendlyName)
            {
                this.Name = name ?? publishedFileId.ToString() + ".sbm";
                this.PublishedFileId = publishedFileId;
                this.FriendlyName = friendlyName;
            }

            public bool ShouldSerializeName()
            {
                return this.Name != null;
            }

            public bool ShouldSerializePublishedFileId()
            {
                return (long) this.PublishedFileId != 0L;
            }
        }

        [ProtoContract]
        public struct RespawnCooldownItem
        {
            [ProtoMember(1)] public ulong PlayerSteamId;
            [ProtoMember(2)] public int PlayerSerialId;
            [ProtoMember(3)] public string RespawnShipId;
            [ProtoMember(4)] public int Cooldown;
        }
    }
}