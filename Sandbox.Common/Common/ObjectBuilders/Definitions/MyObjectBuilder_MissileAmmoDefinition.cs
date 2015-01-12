// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_MissileAmmoDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_MissileAmmoDefinition : MyObjectBuilder_AmmoDefinition
    {
        [ProtoMember(1)] [DefaultValue(null)] public MyObjectBuilder_MissileAmmoDefinition.AmmoMissileProperties
            MissileProperties;

        [ProtoContract]
        public class AmmoMissileProperties
        {
            [ProtoMember(1)] public float MissileMass;
            [ProtoMember(2)] public float MissileExplosionRadius;
            [ProtoMember(3)] [ModdableContentFile("mwm")] public string MissileModelName;
            [ProtoMember(4)] public float MissileAcceleration;
            [ProtoMember(5)] public float MissileInitialSpeed;
            [ProtoMember(6)] public bool MissileSkipAcceleration;
            [ProtoMember(7)] public float MissileExplosionDamage;
        }
    }
}