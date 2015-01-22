// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ProjectileAmmoDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_ProjectileAmmoDefinition : MyObjectBuilder_AmmoDefinition
    {
        [ProtoMember(1)] [DefaultValue(null)] public MyObjectBuilder_ProjectileAmmoDefinition.AmmoProjectileProperties
            ProjectileProperties;

        [ProtoContract]
        public class AmmoProjectileProperties
        {
            [DefaultValue(0.1f)] [ProtoMember(2)] public float ProjectileTrailScale = 0.1f;
            [ProtoMember(3)] public SerializableVector3 ProjectileTrailColor = new SerializableVector3(1f, 1f, 1f);
            [ProtoMember(4)] [DefaultValue(0.5f)] public float ProjectileTrailProbability = 0.5f;

            [ProtoMember(6)] [DefaultValue(MyCustomHitParticlesMethodType.BasicSmall)] public
                MyCustomHitParticlesMethodType ProjectileOnHitParticlesType = MyCustomHitParticlesMethodType.BasicSmall;

            [ProtoMember(1)] public float ProjectileHitImpulse;

            [ProtoMember(5)] [DefaultValue(MyCustomHitMaterialMethodType.Small)] public MyCustomHitMaterialMethodType
                ProjectileOnHitMaterialParticlesType;

            [ProtoMember(7)] public float ProjectileMassDamage;
            [ProtoMember(8)] public float ProjectileHealthDamage;
        }
    }
}