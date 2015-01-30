// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_ReactorDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_ReactorDefinition : MyObjectBuilder_PowerProducerDefinition
  {
    [ProtoMember(1)]
    public Vector3 InventorySize = new Vector3(10f, 10f, 10f);
    [ProtoMember(2)]
    public SerializableDefinitionId FuelId = new SerializableDefinitionId((MyObjectBuilderType) typeof (MyObjectBuilder_Ingot), "Uranium");
  }
}
