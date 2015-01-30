// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_WeaponDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_WeaponDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(1)]
    public MyObjectBuilder_WeaponDefinition.WeaponAmmoData ProjectileAmmoData;
    [ProtoMember(2)]
    public MyObjectBuilder_WeaponDefinition.WeaponAmmoData MissileAmmoData;
    [ProtoMember(3)]
    public string NoAmmoSoundName;
    [ProtoMember(4)]
    public float DeviateShotAngle;
    [ProtoMember(5)]
    public float ReleaseTimeAfterFire;
    [ProtoMember(6)]
    public int MuzzleFlashLifeSpan;
    [XmlArrayItem("AmmoMagazine")]
    [ProtoMember(8)]
    public MyObjectBuilder_WeaponDefinition.WeaponAmmoMagazine[] AmmoMagazines;

    [ProtoContract]
    public class WeaponAmmoData
    {
      [XmlAttribute]
      public int RateOfFire;
      [XmlAttribute]
      public string ShootSoundName;
    }

    [ProtoContract]
    public class WeaponAmmoMagazine
    {
      [XmlIgnore]
      public MyObjectBuilderType Type = (MyObjectBuilderType) typeof (MyObjectBuilder_AmmoMagazine);
      [XmlAttribute]
      [ProtoMember(1)]
      public string Subtype;
    }
  }
}
