// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Identity
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.VRageData;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_Identity : MyObjectBuilder_Base
  {
    [ProtoMember(1)]
    public long PlayerId;
    [ProtoMember(2)]
    public string DisplayName;
    [ProtoMember(3)]
    public long CharacterEntityId;
    [ProtoMember(4)]
    public string Model;
    [ProtoMember(5)]
    public SerializableVector3? ColorMask;

    public bool ShouldSerializeColorMask()
    {
      return this.ColorMask.HasValue;
    }
  }
}
