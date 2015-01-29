// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ProjectileAmmoDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
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
    [DefaultValue(null)]
    [ProtoMember(1)]
    public MyObjectBuilder_ProjectileAmmoDefinition.AmmoProjectileProperties ProjectileProperties;

    [ProtoContract]
    public class AmmoProjectileProperties
    {
      [DefaultValue(0.1f)]
      [ProtoMember(2)]
      public float ProjectileTrailScale = 0.1f;
      [ProtoMember(3)]
      public SerializableVector3 ProjectileTrailColor = new SerializableVector3(1f, 1f, 1f);
      [DefaultValue(0.5f)]
      [ProtoMember(4)]
      public float ProjectileTrailProbability = 0.5f;
      [ProtoMember(6)]
      [DefaultValue(MyCustomHitParticlesMethodType.BasicSmall)]
      public MyCustomHitParticlesMethodType ProjectileOnHitParticlesType = MyCustomHitParticlesMethodType.BasicSmall;
      [ProtoMember(1)]
      public float ProjectileHitImpulse;
      [ProtoMember(5)]
      [DefaultValue(MyCustomHitMaterialMethodType.Small)]
      public MyCustomHitMaterialMethodType ProjectileOnHitMaterialParticlesType;
      [ProtoMember(7)]
      public float ProjectileMassDamage;
      [ProtoMember(8)]
      public float ProjectileHealthDamage;
    }
  }
}
