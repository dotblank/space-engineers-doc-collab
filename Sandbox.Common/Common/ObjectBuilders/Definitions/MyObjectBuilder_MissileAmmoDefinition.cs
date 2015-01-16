// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_MissileAmmoDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_MissileAmmoDefinition : MyObjectBuilder_AmmoDefinition
    {
        [DefaultValue(null)] [ProtoMember(1)] public MyObjectBuilder_MissileAmmoDefinition.AmmoMissileProperties
            MissileProperties;

        [ProtoContract]
        public class AmmoMissileProperties
        {
            [ProtoMember(1)] public float MissileMass;
            [ProtoMember(2)] public float MissileExplosionRadius;
            [ModdableContentFile("mwm")] [ProtoMember(3)] public string MissileModelName;
            [ProtoMember(4)] public float MissileAcceleration;
            [ProtoMember(5)] public float MissileInitialSpeed;
            [ProtoMember(6)] public bool MissileSkipAcceleration;
            [ProtoMember(7)] public float MissileExplosionDamage;
        }
    }
}