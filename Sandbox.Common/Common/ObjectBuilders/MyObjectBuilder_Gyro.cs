// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Gyro
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_Gyro : MyObjectBuilder_FunctionalBlock
    {
        [ProtoMember(1)] [DefaultValue(1)] public float GyroPower = 1f;
        [ProtoMember(3)] public SerializableVector3 TargetAngularVelocity = new SerializableVector3(0.0f, 0.0f, 0.0f);
        [DefaultValue(false)] [ProtoMember(2)] public bool GyroOverride;

        public bool ShouldSerializeTargetAngularVelocity()
        {
            return !this.TargetAngularVelocity.IsZero;
        }
    }
}