﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_WeaponDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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
        [ProtoMember(1)] public MyObjectBuilder_WeaponDefinition.WeaponAmmoData ProjectileAmmoData;
        [ProtoMember(2)] public MyObjectBuilder_WeaponDefinition.WeaponAmmoData MissileAmmoData;
        [ProtoMember(3)] public string NoAmmoSoundName;
        [ProtoMember(4)] public float DeviateShotAngle;
        [ProtoMember(5)] public float ReleaseTimeAfterFire;
        [ProtoMember(6)] public int MuzzleFlashLifeSpan;

        [ProtoMember(8)] [XmlArrayItem("AmmoMagazine")] public MyObjectBuilder_WeaponDefinition.WeaponAmmoMagazine[]
            AmmoMagazines;

        [ProtoContract]
        public class WeaponAmmoData
        {
            [XmlAttribute] public int RateOfFire;
            [XmlAttribute] public string ShootSoundName;
        }

        [ProtoContract]
        public class WeaponAmmoMagazine
        {
            [XmlIgnore] public MyObjectBuilderType Type = (MyObjectBuilderType) typeof (MyObjectBuilder_AmmoMagazine);
            [XmlAttribute] [ProtoMember(1)] public string Subtype;
        }
    }
}