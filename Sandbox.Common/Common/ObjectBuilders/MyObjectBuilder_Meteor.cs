// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Meteor
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_Meteor : MyObjectBuilder_EntityBase
  {
    [ProtoMember(4)]
    public float Integrity = 100f;
    [ProtoMember(1)]
    public MyObjectBuilder_InventoryItem Item;
    [ProtoMember(2)]
    public Vector3 LinearVelocity;
    [ProtoMember(3)]
    public Vector3 AngularVelocity;
  }
}
