// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Gyro
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
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
    [DefaultValue(1)]
    [ProtoMember(1)]
    public float GyroPower = 1f;
    [ProtoMember(3)]
    public SerializableVector3 TargetAngularVelocity = new SerializableVector3(0.0f, 0.0f, 0.0f);
    [ProtoMember(2)]
    [DefaultValue(false)]
    public bool GyroOverride;

    public bool ShouldSerializeTargetAngularVelocity()
    {
      return !this.TargetAngularVelocity.IsZero;
    }
  }
}
