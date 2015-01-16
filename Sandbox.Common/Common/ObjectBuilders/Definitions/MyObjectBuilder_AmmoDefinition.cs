// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_AmmoDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_AmmoDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(1)] public MyObjectBuilder_AmmoDefinition.AmmoBasicProperties BasicProperties;

        [ProtoContract]
        public class AmmoBasicProperties
        {
            [ProtoMember(1)] public float DesiredSpeed;
            [ProtoMember(2)] public float SpeedVariance;
            [ProtoMember(3)] public float MaxTrajectory;
            [ProtoMember(4)] [DefaultValue(false)] public bool IsExplosive;
            [DefaultValue(0.0f)] [ProtoMember(5)] public float BackkickForce;
        }
    }
}