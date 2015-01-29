// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_GlobalEventBase
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_GlobalEventBase : MyObjectBuilder_Base
  {
    [ProtoMember(1)]
    public SerializableDefinitionId DefinitionId;
    [ProtoMember(3)]
    public bool Enabled;
    [ProtoMember(4)]
    public long ActivationTimeMs;
    [ProtoMember(5)]
    public MyGlobalEventTypeEnum EventType;

    public bool ShouldSerializeEventType()
    {
      return false;
    }
  }
}
