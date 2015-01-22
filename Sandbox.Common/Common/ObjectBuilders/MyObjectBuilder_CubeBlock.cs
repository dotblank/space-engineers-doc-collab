// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_CubeBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders.Serializer;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.ComponentModel;
using System.Xml.Serialization;
using VRage.Common.Utils;
using VRageMath;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_CubeBlock : MyObjectBuilder_Base
    {
        [DefaultValue(1f)] [ProtoMember(5)] public float IntegrityPercent = 1f;
        [ProtoMember(6)] [DefaultValue(1f)] public float BuildPercent = 1f;
        [ProtoMember(9)] public SerializableVector3 ColorMaskHSV = new SerializableVector3(0.0f, -1f, 0.0f);
        [ProtoMember(1)] [DefaultValue(0)] public long EntityId;
        [ProtoMember(2)] public SerializableVector3I Min;
        private SerializableQuaternion m_orientation;
        [ProtoMember(7)] public SerializableBlockOrientation BlockOrientation;
        [ProtoMember(8)] [DefaultValue(null)] public MyObjectBuilder_Inventory ConstructionInventory;
        [DefaultValue(null)] [ProtoMember(10)] public MyObjectBuilder_ConstructionStockpile ConstructionStockpile;
        [ProtoMember(11)] [DefaultValue(0)] public long Owner;
        [ProtoMember(14)] public MyOwnershipShareModeEnum ShareMode;
        [ProtoMember(15)] public float DeformationRatio;
        [XmlArrayItem("SubBlock")] [ProtoMember(16)] public MyObjectBuilder_CubeBlock.MySubBlockId[] SubBlocks;

        public SerializableQuaternion Orientation
        {
            get { return this.m_orientation; }
            set
            {
                this.m_orientation = MyVRageUtils.IsZero((Quaternion) value, 1E-05f)
                    ? (SerializableQuaternion) Quaternion.Identity
                    : value;
                this.BlockOrientation =
                    new SerializableBlockOrientation(Base6Directions.GetForward((Quaternion) this.m_orientation),
                        Base6Directions.GetUp((Quaternion) this.m_orientation));
            }
        }

        public bool ShouldSerializeEntityId()
        {
            return this.EntityId != 0L;
        }

        public bool ShouldSerializeOrientation()
        {
            return false;
        }

        public bool ShouldSerializeConstructionInventory()
        {
            return false;
        }

        public static MyObjectBuilder_CubeBlock Upgrade(MyObjectBuilder_CubeBlock cubeBlock, MyObjectBuilderType newType,
            string newSubType)
        {
            MyObjectBuilder_CubeBlock builderCubeBlock =
                MyObjectBuilderSerializer.CreateNewObject(newType, newSubType) as MyObjectBuilder_CubeBlock;
            if (builderCubeBlock == null)
                return (MyObjectBuilder_CubeBlock) null;
            builderCubeBlock.EntityId = cubeBlock.EntityId;
            builderCubeBlock.Min = cubeBlock.Min;
            builderCubeBlock.m_orientation = cubeBlock.m_orientation;
            builderCubeBlock.IntegrityPercent = cubeBlock.IntegrityPercent;
            builderCubeBlock.BuildPercent = cubeBlock.BuildPercent;
            builderCubeBlock.BlockOrientation = cubeBlock.BlockOrientation;
            builderCubeBlock.ConstructionInventory = cubeBlock.ConstructionInventory;
            builderCubeBlock.ColorMaskHSV = cubeBlock.ColorMaskHSV;
            return builderCubeBlock;
        }

        public bool ShouldSerializeConstructionStockpile()
        {
            return this.ConstructionStockpile != null;
        }

        public virtual void Remap(IMyRemapHelper remapHelper)
        {
            if (this.EntityId == 0L)
                return;
            this.EntityId = remapHelper.RemapEntityId(this.EntityId);
        }

        public virtual void SetupForProjector()
        {
        }

        [ProtoContract]
        public struct MySubBlockId
        {
            [ProtoMember(1)] public long SubGridId;
            [ProtoMember(2)] public string SubGridName;
            [ProtoMember(3)] public SerializableVector3I SubBlockPosition;
        }
    }
}