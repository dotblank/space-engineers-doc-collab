// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ProductionBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 52862CFB-4672-4671-9CE3-6D19982FB841
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Definitions;
using System.ComponentModel;
using System.Xml.Serialization;
using VRage;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public abstract class MyObjectBuilder_ProductionBlock : MyObjectBuilder_FunctionalBlock
  {
    [DefaultValue(true)]
    [ProtoMember(4)]
    public bool UseConveyorSystem = true;
    [ProtoMember(1)]
    public MyObjectBuilder_Inventory InputInventory;
    [ProtoMember(2)]
    public MyObjectBuilder_Inventory OutputInventory;
    [XmlArrayItem("Item")]
    [ProtoMember(3)]
    public MyObjectBuilder_ProductionBlock.QueueItem[] Queue;
    [ProtoMember(5)]
    [DefaultValue(0)]
    public uint NextItemId;

    public MyObjectBuilder_Inventory Inventory
    {
      get
      {
        return this.InputInventory;
      }
      set
      {
        this.InputInventory = value;
      }
    }

    public bool ShouldSerializeInventory()
    {
      return false;
    }

    public override void SetupForProjector()
    {
      base.SetupForProjector();
      if (this.Inventory == null)
        return;
      this.Inventory.Clear();
    }

    [ProtoContract]
    public struct QueueItem
    {
      [ProtoMember(1)]
      public SerializableDefinitionId Id;
      [ProtoMember(2)]
      public MyFixedPoint Amount;
      [ProtoMember(3)]
      public uint? ItemId;
    }
  }
}
