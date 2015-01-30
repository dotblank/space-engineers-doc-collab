// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_GlobalEventDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_GlobalEventDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(1)]
    public MyGlobalEventTypeEnum EventType;
    [ProtoMember(2)]
    public long? MinActivationTimeMs;
    [ProtoMember(3)]
    public long? MaxActivationTimeMs;
    [ProtoMember(4)]
    public long? FirstActivationTimeMs;

    public bool ShouldSerializeMinActivationTime()
    {
      return this.MinActivationTimeMs.HasValue;
    }

    public bool ShouldSerializeMaxActivationTime()
    {
      return this.MaxActivationTimeMs.HasValue;
    }

    public bool ShouldSerializeFirstActivationTime()
    {
      return this.FirstActivationTimeMs.HasValue;
    }
  }
}
