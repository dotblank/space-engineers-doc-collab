// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_CubeBlockDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
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
        [ProtoMember(11)] [DefaultValue(MyPhysicsOption.Box)] public MyPhysicsOption PhysicsOption = MyPhysicsOption.Box;
        [DefaultValue(1f)] [ProtoMember(21)] public float DeformationRatio = 1f;
        [ProtoMember(23)] [DefaultValue(10f)] public float BuildTimeSeconds = 10f;
        [ProtoMember(24)] [DefaultValue(1f)] public float DisassembleRatio = 1f;
        [ProtoMember(30)] public string BuildType = "Unknown";
        [ProtoMember(35)] [DefaultValue(null)] public MyFractureMaterial? FractureType = new MyFractureMaterial?();
        [DefaultValue(true)] [ProtoMember(36)] public bool GuiVisible = true;

        [DefaultValue(MyBlockDirection.Both)] [ProtoMember(38)] public MyBlockDirection Direction =
            MyBlockDirection.Both;

        [ProtoMember(39)] [DefaultValue(MyBlockRotation.Both)] public MyBlockRotation Rotation = MyBlockRotation.Both;
        [ProtoMember(1)] public MyCubeSize CubeSize;
        [ProtoMember(2)] public MyBlockTopology BlockTopology;
        [ProtoMember(3)] public SerializableVector3I Size;
        [ProtoMember(4)] public SerializableVector3 ModelOffset;
        [ModdableContentFile("mwm")] [ProtoMember(5)] public string Model;
        [ProtoMember(6)] public MyObjectBuilder_CubeBlockDefinition.PatternDefinition CubeDefinition;

        [ProtoMember(7)] [XmlArrayItem("Component")] public MyObjectBuilder_CubeBlockDefinition.CubeBlockComponent[]
            Components;

        [ProtoMember(8)] public MyObjectBuilder_CubeBlockDefinition.CriticalPart CriticalComponent;
        [ProtoMember(9)] public MyObjectBuilder_CubeBlockDefinition.MountPoint[] MountPoints;
        [ProtoMember(10)] public MyObjectBuilder_CubeBlockDefinition.Variant[] Variants;

        [XmlArrayItem("Model")] [ProtoMember(12)] [DefaultValue(null)] public
            List<MyObjectBuilder_CubeBlockDefinition.BuildProgressModel> BuildProgressModels;

        [ProtoMember(15)] public string BlockPairName;
        [ProtoMember(17)] public SerializableVector3I? Center;
        [DefaultValue(MySymmetryAxisEnum.None)] [ProtoMember(18)] public MySymmetryAxisEnum MirroringX;
        [ProtoMember(19)] [DefaultValue(MySymmetryAxisEnum.None)] public MySymmetryAxisEnum MirroringY;
        [ProtoMember(20)] [DefaultValue(MySymmetryAxisEnum.None)] public MySymmetryAxisEnum MirroringZ;
        [ProtoMember(22)] public string EdgeType;
        [ProtoMember(25)] public MyAutorotateMode AutorotateMode;
        [ProtoMember(26)] public string MirroringBlock;
        [ProtoMember(27)] public bool UseModelIntersection;
        [ProtoMember(29)] public string PrimarySound;

        [ProtoMember(31)] [DefaultValue(null)] [XmlArrayItem("Model")] public
            MyObjectBuilder_CubeBlockDefinition.MyAdditionalModelDefinition[] AdditionalModels;

        [ProtoMember(32)] [DefaultValue(null)] [XmlArrayItem("Template")] public string[] CompoundTemplates;

        [DefaultValue(null)] [XmlArrayItem("Definition")] [ProtoMember(33)] public
            MyObjectBuilder_CubeBlockDefinition.MySubBlockDefinition[] SubBlockDefinitions;

        [ProtoMember(34)] [DefaultValue(null)] public string MultiBlock;

        [XmlArrayItem("BlockStage")] [DefaultValue(null)] [ProtoMember(37)] public SerializableDefinitionId[]
            BlockStages;

        public bool ShouldSerializeCenter()
        {
            return this.Center.HasValue;
        }

        [ProtoContract]
        public class MountPoint
        {
            [XmlAttribute] [ProtoMember(1)] public BlockSideEnum Side;
            [XmlIgnore] [ProtoMember(2)] public SerializableVector2 Start;
            [XmlIgnore] [ProtoMember(3)] public SerializableVector2 End;
            [DefaultValue(0)] [XmlAttribute] [ProtoMember(4)] public byte ExclusionMask;
            [XmlAttribute] [DefaultValue(0)] [ProtoMember(5)] public byte PropertiesMask;

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
            [XmlAttribute] [ProtoMember(1)] public string Subtype;
            [ProtoMember(2)] [XmlAttribute] public int Index;
        }

        [ProtoContract]
        public class Variant
        {
            [XmlAttribute] [ProtoMember(1)] public string Color;
            [XmlAttribute] [ProtoMember(2)] public string Suffix;
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
            [ProtoMember(1)] [XmlAttribute] [ModdableContentFile("mwm")] public string Model;
            [ProtoMember(2)] [XmlIgnore] public SerializableVector2I PatternSize;

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
            [XmlAttribute] [ProtoMember(1)] public float BuildPercentUpperBound;
            [XmlAttribute] [ModdableContentFile("mwm")] [ProtoMember(2)] public string File;
            [ProtoMember(3)] [DefaultValue(false)] [XmlAttribute] public bool RandomOrientation;
        }

        [ProtoContract]
        public class MyAdditionalModelDefinition
        {
            [XmlAttribute] [ProtoMember(1)] public string Type;
            [ProtoMember(2)] [ModdableContentFile("mwm")] [XmlAttribute] public string File;
            [XmlAttribute] [ProtoMember(3)] [DefaultValue(false)] public bool EnablePhysics;
        }

        [ProtoContract]
        public class MySubBlockDefinition
        {
            [XmlAttribute] [ProtoMember(1)] public string SubBlock;
            [ProtoMember(2)] public SerializableDefinitionId Id;
        }
    }
}