// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_SessionSettings
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;
using System.Xml.Serialization;
using SysUtils.Utils;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_SessionSettings : MyObjectBuilder_Base
  {
    [ProtoMember(1)]
    public MyGameModeEnum GameMode = MyGameModeEnum.Survival;
    [ProtoMember(2)]
    public float InventorySizeMultiplier = 1f;
    [ProtoMember(3)]
    public float AssemblerSpeedMultiplier = 1f;
    [ProtoMember(4)]
    public float AssemblerEfficiencyMultiplier = 1f;
    [ProtoMember(5)]
    public float RefinerySpeedMultiplier = 1f;
    [ProtoMember(7)]
    public short MaxPlayers = (short) 4;
    [ProtoMember(8)]
    public short MaxFloatingObjects = (short) 256;
    [ProtoMember(10)]
    public bool AutoHealing = true;
    [ProtoMember(11)]
    public bool EnableCopyPaste = true;
    [ProtoMember(13)]
    public bool WeaponsEnabled = true;
    [ProtoMember(14)]
    public bool ShowPlayerNamesOnHud = true;
    [ProtoMember(15)]
    public bool ThrusterDamage = true;
    [ProtoMember(16)]
    public bool CargoShipsEnabled = true;
    [ProtoMember(20)]
    public bool RespawnShipDelete = true;
    [ProtoMember(22)]
    public float WelderSpeedMultiplier = 1f;
    [ProtoMember(23)]
    public float GrinderSpeedMultiplier = 1f;
    [ProtoMember(26)]
    public float HackSpeedMultiplier = 0.33f;
    [ProtoMember(27)]
    public bool? PermanentDeath = new bool?(true);
    [ProtoMember(28)]
    public uint AutoSaveInMinutes = 5U;
    [ProtoMember(29)]
    public float SpawnShipTimeMultiplier = 1f;
    [ProtoMember(32)]
    public bool DestructibleBlocks = true;
    [ProtoMember(33)]
    public bool EnableIngameScripts = true;
    [ProtoMember(34)]
    public int ViewDistance = 20000;
    [XmlIgnore]
    public const uint DEFAULT_AUTOSAVE_IN_MINUTES = 5U;
    [ProtoMember(6)]
    public MyOnlineModeEnum OnlineMode;
    [ProtoMember(9)]
    public MyEnvironmentHostilityEnum EnvironmentHostility;
    [ProtoMember(17)]
    public bool EnableSpectator;
    [ProtoMember(18)]
    public bool RemoveTrash;
    [ProtoMember(19)]
    public int WorldSizeKm;
    [ProtoMember(21)]
    public bool ResetOwnership;
    [ProtoMember(24)]
    public bool RealisticSound;
    [ProtoMember(25)]
    public bool ClientCanSave;
    [DefaultValue(0.0f)]
    [ProtoMember(30)]
    public float ProceduralDensity;
    [ProtoMember(31)]
    public int ProceduralSeed;
    [ProtoMember(35)]
    public bool? EnableToolShake;

    public bool AutoSave
    {
      get
      {
        return this.AutoSaveInMinutes > 0U;
      }
      set
      {
        this.AutoSaveInMinutes = value ? 5U : 0U;
      }
    }

    public bool ShouldSerializeAutoSave()
    {
      return false;
    }

    public bool ShouldSerializeProceduralDensity()
    {
      return (double) this.ProceduralDensity > 0.0;
    }

    public bool ShouldSerializeProceduralSeed()
    {
      return (double) this.ProceduralDensity > 0.0;
    }

    public void LogMembers(MyLog log, LoggingOptions options)
    {
      log.WriteLine("Settings:");
      using (log.IndentUsing(options))
      {
        log.WriteLine("GameMode = " + (object) this.GameMode);
        log.WriteLine("MaxPlayers = " + (object) this.MaxPlayers);
        log.WriteLine("OnlineMode = " + (object) this.OnlineMode);
        log.WriteLine("AutoHealing = " + (object) (this.AutoHealing ? 1 : 0));
        log.WriteLine("WeaponsEnabled = " + (object) (this.WeaponsEnabled ? 1 : 0));
        log.WriteLine("ThrusterDamage = " + (object) (this.ThrusterDamage ? 1 : 0));
        log.WriteLine("EnableSpectator = " + (object) (this.EnableSpectator ? 1 : 0));
        log.WriteLine("EnableCopyPaste = " + (object) (this.EnableCopyPaste ? 1 : 0));
        log.WriteLine("MaxFloatingObjects = " + (object) this.MaxFloatingObjects);
        log.WriteLine("CargoShipsEnabled = " + (object) (this.CargoShipsEnabled ? 1 : 0));
        log.WriteLine("EnvironmentHostility = " + (object) this.EnvironmentHostility);
        log.WriteLine("ShowPlayerNamesOnHud = " + (object) (this.ShowPlayerNamesOnHud ? 1 : 0));
        log.WriteLine("InventorySizeMultiplier = " + (object) this.InventorySizeMultiplier);
        log.WriteLine("RefinerySpeedMultiplier = " + (object) this.RefinerySpeedMultiplier);
        log.WriteLine("AssemblerSpeedMultiplier = " + (object) this.AssemblerSpeedMultiplier);
        log.WriteLine("AssemblerEfficiencyMultiplier = " + (object) this.AssemblerEfficiencyMultiplier);
        log.WriteLine("WelderSpeedMultiplier = " + (object) this.WelderSpeedMultiplier);
        log.WriteLine("GrinderSpeedMultiplier = " + (object) this.GrinderSpeedMultiplier);
        log.WriteLine("ClientCanSave = " + (object) (this.ClientCanSave ? 1 : 0));
        log.WriteLine("HackSpeedMultiplier = " + (object) this.HackSpeedMultiplier);
        log.WriteLine("PermanentDeath = " + (object) this.PermanentDeath);
        log.WriteLine("DestructibleBlocks =  " + (object) (this.DestructibleBlocks ? 1 : 0));
        log.WriteLine("EnableScripts =  " + (object) (this.EnableIngameScripts ? 1 : 0));
        log.WriteLine("AutoSaveInMinutes = " + (object) this.AutoSaveInMinutes);
        log.WriteLine("SpawnShipTimeMultiplier = " + (object) this.SpawnShipTimeMultiplier);
        log.WriteLine("ProceduralDensity = " + (object) this.ProceduralDensity);
        log.WriteLine("ProceduralSeed = " + (object) this.ProceduralSeed);
        log.WriteLine("DestructibleBlocks = " + (object) (this.DestructibleBlocks ? 1 : 0));
        log.WriteLine("EnableIngameScripts = " + (object) (this.EnableIngameScripts ? 1 : 0));
        log.WriteLine("ViewDistance = " + (object) this.ViewDistance);
      }
    }
  }
}
