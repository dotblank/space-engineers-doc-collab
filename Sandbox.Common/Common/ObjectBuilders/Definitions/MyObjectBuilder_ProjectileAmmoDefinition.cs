// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ProjectileAmmoDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_ProjectileAmmoDefinition : MyObjectBuilder_AmmoDefinition
    {
        [ProtoMember(1)] [DefaultValue(null)] public MyObjectBuilder_ProjectileAmmoDefinition.AmmoProjectileProperties
            ProjectileProperties;

        [ProtoContract]
        public class AmmoProjectileProperties
        {
            [ProtoMember(2)] [DefaultValue(0.1f)] public float ProjectileTrailScale = 0.1f;
            [ProtoMember(3)] public SerializableVector3 ProjectileTrailColor = new SerializableVector3(1f, 1f, 1f);
            [DefaultValue(0.5f)] [ProtoMember(4)] public float ProjectileTrailProbability = 0.5f;

            [ProtoMember(6)] [DefaultValue(MyCustomHitParticlesMethodType.BasicSmall)] public
                MyCustomHitParticlesMethodType ProjectileOnHitParticlesType = MyCustomHitParticlesMethodType.BasicSmall;

            [ProtoMember(1)] public float ProjectileHitImpulse;

            [DefaultValue(MyCustomHitMaterialMethodType.Small)] [ProtoMember(5)] public MyCustomHitMaterialMethodType
                ProjectileOnHitMaterialParticlesType;

            [ProtoMember(7)] public float ProjectileMassDamage;
            [ProtoMember(8)] public float ProjectileHealthDamage;
        }
    }
}