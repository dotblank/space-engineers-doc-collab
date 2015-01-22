// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Gyro
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
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