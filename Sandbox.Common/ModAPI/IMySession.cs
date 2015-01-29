// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMySession
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.ModAPI.Interfaces;
using System;
using System.Collections.Generic;
using VRage;
using VRage.Common.Utils;

namespace Sandbox.ModAPI
{
  public interface IMySession
  {
    float AssemblerEfficiencyMultiplier { get; }

    float AssemblerSpeedMultiplier { get; }

    bool AutoHealing { get; }

    uint AutoSaveInMinutes { get; }

    IMyCameraController CameraController { get; }

    bool CargoShipsEnabled { get; }

    bool ClientCanSave { get; }

    bool CreativeMode { get; }

    string CurrentPath { get; }

    string Description { get; set; }

    TimeSpan ElapsedPlayTime { get; }

    bool EnableCopyPaste { get; }

    MyEnvironmentHostilityEnum EnvironmentHostility { get; }

    DateTime GameDateTime { get; set; }

    float GrinderSpeedMultiplier { get; }

    float HackSpeedMultiplier { get; }

    float InventoryMultiplier { get; }

    bool IsCameraAwaitingEntity { get; set; }

    short MaxFloatingObjects { get; }

    short MaxPlayers { get; }

    bool MultiplayerAlive { get; set; }

    bool MultiplayerDirect { get; set; }

    double MultiplayerLastMsg { get; set; }

    string Name { get; set; }

    float NegativeIntegrityTotal { get; set; }

    MyOnlineModeEnum OnlineMode { get; }

    string Password { get; set; }

    float PositiveIntegrityTotal { get; set; }

    float RefinerySpeedMultiplier { get; }

    bool ShowPlayerNamesOnHud { get; }

    bool SurvivalMode { get; }

    bool ThrusterDamage { get; }

    string ThumbPath { get; }

    TimeSpan TimeOnBigShip { get; }

    TimeSpan TimeOnFoot { get; }

    TimeSpan TimeOnJetpack { get; }

    TimeSpan TimeOnSmallShip { get; }

    bool WeaponsEnabled { get; }

    float WelderSpeedMultiplier { get; }

    ulong? WorkshopId { get; }

    string WorldID { get; set; }

    IMyVoxelMaps VoxelMaps { get; }

    IMyPlayer Player { get; }

    IMyControllableEntity ControlledObject { get; }

    MyObjectBuilder_SessionSettings SessionSettings { get; }

    IMyFactionCollection Factions { get; }

    void BeforeStartComponents();

    void Draw();

    void GameOver();

    void GameOver(MyStringId? customMessage);

    MyObjectBuilder_Checkpoint GetCheckpoint(string saveName);

    MyObjectBuilder_Sector GetSector();

    Dictionary<string, byte[]> GetVoxelMapsArray();

    MyObjectBuilder_World GetWorld();

    bool IsPausable();

    void RegisterComponent(MySessionComponentBase component, MyUpdateOrder updateOrder, int priority);

    bool Save(string customSaveName = null);

    void SetAsNotReady();

    void Unload();

    void UnloadDataComponents();

    void UnloadMultiplayer();

    void UnregisterComponent(MySessionComponentBase component);

    void Update(MyTimeSpan time);

    void UpdateComponents();
  }
}
