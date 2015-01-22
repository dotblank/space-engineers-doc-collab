// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_InventoryItem
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders.Serializer;
using System;
using System.Xml.Serialization;
using VRage;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_InventoryItem : MyObjectBuilder_Base
    {
        [XmlElement("Amount")] [ProtoMember(1)] public MyFixedPoint Amount;
        [XmlElement("PhysicalContent", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_PhysicalObject>))] [ProtoMember(2)] public MyObjectBuilder_PhysicalObject PhysicalContent;
        [ProtoMember(3)] public uint ItemId;

        [XmlElement("AmountDecimal")]
        public Decimal Obsolete_AmountDecimal
        {
            get { return (Decimal) this.Amount; }
            set { this.Amount = (MyFixedPoint) value; }
        }

        public MyObjectBuilder_Base Content
        {
            get { return (MyObjectBuilder_Base) this.PhysicalContent; }
            set
            {
                if (value is MyObjectBuilder_PhysicalObject)
                    this.PhysicalContent = (MyObjectBuilder_PhysicalObject) value;
                else if (value is MyObjectBuilder_HandDrill)
                {
                    MyObjectBuilder_PhysicalGunObject newObject =
                        MyObjectBuilderSerializer.CreateNewObject<MyObjectBuilder_PhysicalGunObject>("HandDrillItem");
                    newObject.GunEntity = (MyObjectBuilder_EntityBase) value;
                    newObject.GunEntity.EntityId = 0L;
                    this.PhysicalContent = (MyObjectBuilder_PhysicalObject) newObject;
                }
                else if (value is MyObjectBuilder_AutomaticRifle)
                {
                    MyObjectBuilder_PhysicalGunObject newObject =
                        MyObjectBuilderSerializer.CreateNewObject<MyObjectBuilder_PhysicalGunObject>(
                            "AutomaticRifleItem");
                    newObject.GunEntity = (MyObjectBuilder_EntityBase) value;
                    newObject.GunEntity.EntityId = 0L;
                    this.PhysicalContent = (MyObjectBuilder_PhysicalObject) newObject;
                }
                else if (value is MyObjectBuilder_Welder)
                {
                    MyObjectBuilder_PhysicalGunObject newObject =
                        MyObjectBuilderSerializer.CreateNewObject<MyObjectBuilder_PhysicalGunObject>("WelderItem");
                    newObject.GunEntity = (MyObjectBuilder_EntityBase) value;
                    newObject.GunEntity.EntityId = 0L;
                    this.PhysicalContent = (MyObjectBuilder_PhysicalObject) newObject;
                }
                else
                {
                    if (!(value is MyObjectBuilder_AngleGrinder))
                        return;
                    MyObjectBuilder_PhysicalGunObject newObject =
                        MyObjectBuilderSerializer.CreateNewObject<MyObjectBuilder_PhysicalGunObject>("AngleGrinderItem");
                    newObject.GunEntity = (MyObjectBuilder_EntityBase) value;
                    newObject.GunEntity.EntityId = 0L;
                    this.PhysicalContent = (MyObjectBuilder_PhysicalObject) newObject;
                }
            }
        }

        public bool ShouldSerializeAmountDecimal()
        {
            return false;
        }

        public bool ShouldSerializeContent()
        {
            return false;
        }
    }
}