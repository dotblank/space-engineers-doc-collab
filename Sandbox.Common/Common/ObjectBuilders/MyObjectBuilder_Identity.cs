// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Identity
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
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
    public long IdentityId;
    [ProtoMember(2)]
    public string DisplayName;
    [ProtoMember(3)]
    public long CharacterEntityId;
    [ProtoMember(4)]
    public string Model;
    [ProtoMember(5)]
    public SerializableVector3? ColorMask;

    public long PlayerId
    {
      get
      {
        return this.IdentityId;
      }
      set
      {
        this.IdentityId = value;
      }
    }

    public bool ShouldSerializePlayerId()
    {
      return false;
    }

    public bool ShouldSerializeColorMask()
    {
      return this.ColorMask.HasValue;
    }
  }
}
