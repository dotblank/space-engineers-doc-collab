// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_GravityGenerator
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_GravityGenerator : MyObjectBuilder_FunctionalBlock
  {
    [ProtoMember(2)]
    public SerializableVector3 FieldSize = new SerializableVector3(150f, 150f, 150f);
    [ProtoMember(3)]
    [DefaultValue(9.81f)]
    public float GravityAcceleration = 9.81f;
  }
}
