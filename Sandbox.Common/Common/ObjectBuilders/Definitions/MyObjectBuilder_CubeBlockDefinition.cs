// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_CubeBlockDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.AI;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_CubeBlockDefinition : MyObjectBuilder_DefinitionBase
    {
        [DefaultValue(MyPhysicsOption.Box)] [ProtoMember(11)] public MyPhysicsOption PhysicsOption = MyPhysicsOption.Box;
        [DefaultValue(1f)] [ProtoMember(21)] public float DeformationRatio = 1f;
        [DefaultValue(10f)] [ProtoMember(23)] public float BuildTimeSeconds = 10f;
        [ProtoMember(24)] [DefaultValue(1f)] public float DisassembleRatio = 1f;
        [ProtoMember(30)] public string BuildType = "Unknown";
        [ProtoMember(35)] [DefaultValue(null)] public MyFractureMaterial? FractureType = new MyFractureMaterial?();
        [ProtoMember(37)] [DefaultValue(true)] public bool GuiVisible = true;

        [ProtoMember(39)] [DefaultValue(MyBlockDirection.Both)] public MyBlockDirection Direction =
            MyBlockDirection.Both;

        [DefaultValue(MyBlockRotation.Both)] [ProtoMember(40)] public MyBlockRotation Rotation = MyBlockRotation.Both;
        [ProtoMember(1)] public MyCubeSize CubeSize;
        [ProtoMember(2)] public MyBlockTopology BlockTopology;
        [ProtoMember(3)] public SerializableVector3I Size;
        [ProtoMember(4)] public SerializableVector3 ModelOffset;
        [ProtoMember(5)] [ModdableContentFile("mwm")] public string Model;
        [ProtoMember(6)] public MyObjectBuilder_CubeBlockDefinition.PatternDefinition CubeDefinition;

        [ProtoMember(7)] [XmlArrayItem("Component")] public MyObjectBuilder_CubeBlockDefinition.CubeBlockComponent[]
            Components;

        [ProtoMember(8)] public MyObjectBuilder_CubeBlockDefinition.CriticalPart CriticalComponent;
        [ProtoMember(9)] public MyObjectBuilder_CubeBlockDefinition.MountPoint[] MountPoints;
        [ProtoMember(10)] public MyObjectBuilder_CubeBlockDefinition.Variant[] Variants;

        [ProtoMember(12)] [XmlArrayItem("Model")] [DefaultValue(null)] public
            List<MyObjectBuilder_CubeBlockDefinition.BuildProgressModel> BuildProgressModels;

        [ProtoMember(15)] public string BlockPairName;
        [ProtoMember(17)] public SerializableVector3I? Center;
        [ProtoMember(18)] [DefaultValue(MySymmetryAxisEnum.None)] public MySymmetryAxisEnum MirroringX;
        [ProtoMember(19)] [DefaultValue(MySymmetryAxisEnum.None)] public MySymmetryAxisEnum MirroringY;
        [DefaultValue(MySymmetryAxisEnum.None)] [ProtoMember(20)] public MySymmetryAxisEnum MirroringZ;
        [ProtoMember(22)] public string EdgeType;
        [ProtoMember(25)] public MyAutorotateMode AutorotateMode;
        [ProtoMember(26)] public string MirroringBlock;
        [ProtoMember(27)] public bool UseModelIntersection;
        [ProtoMember(29)] public string PrimarySound;

        [ProtoMember(31)] [DefaultValue(null)] [XmlArrayItem("Model")] public
            MyObjectBuilder_CubeBlockDefinition.MyAdditionalModelDefinition[] AdditionalModels;

        [DefaultValue(null)] [ProtoMember(32)] [XmlArrayItem("Template")] public string[] CompoundTemplates;

        [XmlArrayItem("Definition")] [ProtoMember(33)] [DefaultValue(null)] public
            MyObjectBuilder_CubeBlockDefinition.MySubBlockDefinition[] SubBlockDefinitions;

        [DefaultValue(null)] [ProtoMember(34)] public string MultiBlock;
        [ProtoMember(36)] [DefaultValue(null)] public MyObjectBuilder_BlockNavigationInfo NavigationInfo;

        [DefaultValue(null)] [XmlArrayItem("BlockStage")] [ProtoMember(38)] public SerializableDefinitionId[]
            BlockStages;

        [ProtoMember(41)] [XmlArrayItem("GeneratedBlock")] [DefaultValue(null)] public SerializableDefinitionId[]
            GeneratedBlocks;

        [ProtoMember(42)] [DefaultValue(null)] public string GeneratedBlockType;
        [DefaultValue(false)] [ProtoMember(43)] public bool Mirrored;

        public bool ShouldSerializeCenter()
        {
            return this.Center.HasValue;
        }

        [ProtoContract]
        public class MountPoint
        {
            [ProtoMember(1)] [XmlAttribute] public BlockSideEnum Side;
            [XmlIgnore] [ProtoMember(2)] public SerializableVector2 Start;
            [XmlIgnore] [ProtoMember(3)] public SerializableVector2 End;
            [XmlAttribute] [DefaultValue(0)] [ProtoMember(4)] public byte ExclusionMask;
            [ProtoMember(5)] [XmlAttribute] [DefaultValue(0)] public byte PropertiesMask;

            [XmlAttribute]
            public float StartX
            {
                get { return this.Start.X; }
                set { this.Start.X = value; }
            }

            [XmlAttribute]
            public float StartY
            {
                get { return this.Start.Y; }
                set { this.Start.Y = value; }
            }

            [XmlAttribute]
            public float EndX
            {
                get { return this.End.X; }
                set { this.End.X = value; }
            }

            [XmlAttribute]
            public float EndY
            {
                get { return this.End.Y; }
                set { this.End.Y = value; }
            }
        }

        [ProtoContract]
        public class CubeBlockComponent
        {
            [XmlIgnore] public MyObjectBuilderType Type = (MyObjectBuilderType) typeof (MyObjectBuilder_Component);
            [ProtoMember(1)] [XmlAttribute] public string Subtype;
            [ProtoMember(2)] [XmlAttribute] public ushort Count;
        }

        [ProtoContract]
        public class CriticalPart
        {
            [XmlIgnore] public MyObjectBuilderType Type = (MyObjectBuilderType) typeof (MyObjectBuilder_Component);
            [ProtoMember(1)] [XmlAttribute] public string Subtype;
            [ProtoMember(2)] [XmlAttribute] public int Index;
        }

        [ProtoContract]
        public class Variant
        {
            [XmlAttribute] [ProtoMember(1)] public string Color;
            [ProtoMember(2)] [XmlAttribute] public string Suffix;
        }

        [ProtoContract]
        public class PatternDefinition
        {
            [ProtoMember(1)] public MyCubeTopology CubeTopology;
            [ProtoMember(2)] public MyObjectBuilder_CubeBlockDefinition.Side[] Sides;
            [ProtoMember(3)] public bool ShowEdges;
        }

        [ProtoContract]
        public class Side
        {
            [ProtoMember(1)] [ModdableContentFile("mwm")] [XmlAttribute] public string Model;
            [XmlIgnore] [ProtoMember(2)] public SerializableVector2I PatternSize;

            [XmlAttribute]
            public int PatternWidth
            {
                get { return this.PatternSize.X; }
                set { this.PatternSize.X = value; }
            }

            [XmlAttribute]
            public int PatternHeight
            {
                get { return this.PatternSize.Y; }
                set { this.PatternSize.Y = value; }
            }
        }

        [ProtoContract]
        public class BuildProgressModel
        {
            [ProtoMember(1)] [XmlAttribute] public float BuildPercentUpperBound;
            [ProtoMember(2)] [ModdableContentFile("mwm")] [XmlAttribute] public string File;
            [XmlAttribute] [DefaultValue(false)] [ProtoMember(3)] public bool RandomOrientation;
        }

        [ProtoContract]
        public class MyAdditionalModelDefinition
        {
            [ProtoMember(1)] [XmlAttribute] public string Type;
            [XmlAttribute] [ProtoMember(2)] [ModdableContentFile("mwm")] public string File;
            [DefaultValue(false)] [ProtoMember(3)] [XmlAttribute] public bool EnablePhysics;
        }

        [ProtoContract]
        public class MyGeneratedBlockDefinition
        {
            [ProtoMember(1)] [XmlAttribute] public string Type;
            [ProtoMember(2)] public SerializableDefinitionId Id;
        }

        [ProtoContract]
        public class MySubBlockDefinition
        {
            [XmlAttribute] [ProtoMember(1)] public string SubBlock;
            [ProtoMember(2)] public SerializableDefinitionId Id;
        }
    }
}