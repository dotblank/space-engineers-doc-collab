// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_CubeBlockDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
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
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_CubeBlockDefinition : MyObjectBuilder_DefinitionBase
  {
    [ProtoMember(11)]
    [DefaultValue(MyPhysicsOption.Box)]
    public MyPhysicsOption PhysicsOption = MyPhysicsOption.Box;
    [DefaultValue(1f)]
    [ProtoMember(21)]
    public float DeformationRatio = 1f;
    [ProtoMember(23)]
    [DefaultValue(10f)]
    public float BuildTimeSeconds = 10f;
    [DefaultValue(1f)]
    [ProtoMember(24)]
    public float DisassembleRatio = 1f;
    [ProtoMember(35)]
    [DefaultValue(null)]
    public MyFractureMaterial? FractureType = new MyFractureMaterial?();
    [ProtoMember(37)]
    [DefaultValue(true)]
    public bool GuiVisible = true;
    [DefaultValue(MyBlockDirection.Both)]
    [ProtoMember(39)]
    public MyBlockDirection Direction = MyBlockDirection.Both;
    [DefaultValue(MyBlockRotation.Both)]
    [ProtoMember(40)]
    public MyBlockRotation Rotation = MyBlockRotation.Both;
    [ProtoMember(1)]
    public MyCubeSize CubeSize;
    [ProtoMember(2)]
    public MyBlockTopology BlockTopology;
    [ProtoMember(3)]
    public SerializableVector3I Size;
    [ProtoMember(4)]
    public SerializableVector3 ModelOffset;
    [ModdableContentFile("mwm")]
    [ProtoMember(5)]
    public string Model;
    [ProtoMember(6)]
    public MyObjectBuilder_CubeBlockDefinition.PatternDefinition CubeDefinition;
    [XmlArrayItem("Component")]
    [ProtoMember(7)]
    public MyObjectBuilder_CubeBlockDefinition.CubeBlockComponent[] Components;
    [ProtoMember(8)]
    public MyObjectBuilder_CubeBlockDefinition.CriticalPart CriticalComponent;
    [ProtoMember(9)]
    public MyObjectBuilder_CubeBlockDefinition.MountPoint[] MountPoints;
    [ProtoMember(10)]
    public MyObjectBuilder_CubeBlockDefinition.Variant[] Variants;
    [XmlArrayItem("Model")]
    [ProtoMember(12)]
    [DefaultValue(null)]
    public List<MyObjectBuilder_CubeBlockDefinition.BuildProgressModel> BuildProgressModels;
    [ProtoMember(15)]
    public string BlockPairName;
    [ProtoMember(17)]
    public SerializableVector3I? Center;
    [ProtoMember(18)]
    [DefaultValue(MySymmetryAxisEnum.None)]
    public MySymmetryAxisEnum MirroringX;
    [ProtoMember(19)]
    [DefaultValue(MySymmetryAxisEnum.None)]
    public MySymmetryAxisEnum MirroringY;
    [ProtoMember(20)]
    [DefaultValue(MySymmetryAxisEnum.None)]
    public MySymmetryAxisEnum MirroringZ;
    [ProtoMember(22)]
    public string EdgeType;
    [ProtoMember(25)]
    public MyAutorotateMode AutorotateMode;
    [ProtoMember(26)]
    public string MirroringBlock;
    [ProtoMember(27)]
    public bool UseModelIntersection;
    [ProtoMember(29)]
    public string PrimarySound;
    [ProtoMember(30)]
    [DefaultValue(null)]
    public string BuildType;
    [ProtoMember(32)]
    [DefaultValue(null)]
    [XmlArrayItem("Template")]
    public string[] CompoundTemplates;
    [ProtoMember(33)]
    [XmlArrayItem("Definition")]
    [DefaultValue(null)]
    public MyObjectBuilder_CubeBlockDefinition.MySubBlockDefinition[] SubBlockDefinitions;
    [DefaultValue(null)]
    [ProtoMember(34)]
    public string MultiBlock;
    [ProtoMember(36)]
    [DefaultValue(null)]
    public MyObjectBuilder_BlockNavigationInfo NavigationInfo;
    [ProtoMember(38)]
    [DefaultValue(null)]
    [XmlArrayItem("BlockStage")]
    public SerializableDefinitionId[] BlockStages;
    [XmlArrayItem("GeneratedBlock")]
    [ProtoMember(41)]
    [DefaultValue(null)]
    public SerializableDefinitionId[] GeneratedBlocks;
    [DefaultValue(null)]
    [ProtoMember(42)]
    public string GeneratedBlockType;
    [ProtoMember(43)]
    [DefaultValue(false)]
    public bool Mirrored;

    public bool ShouldSerializeCenter()
    {
      return this.Center.HasValue;
    }

    [ProtoContract]
    public class MountPoint
    {
      [ProtoMember(1)]
      [XmlAttribute]
      public BlockSideEnum Side;
      [ProtoMember(2)]
      [XmlIgnore]
      public SerializableVector2 Start;
      [ProtoMember(3)]
      [XmlIgnore]
      public SerializableVector2 End;
      [ProtoMember(4)]
      [XmlAttribute]
      [DefaultValue(0)]
      public byte ExclusionMask;
      [DefaultValue(0)]
      [ProtoMember(5)]
      [XmlAttribute]
      public byte PropertiesMask;

      [XmlAttribute]
      public float StartX
      {
        get
        {
          return this.Start.X;
        }
        set
        {
          this.Start.X = value;
        }
      }

      [XmlAttribute]
      public float StartY
      {
        get
        {
          return this.Start.Y;
        }
        set
        {
          this.Start.Y = value;
        }
      }

      [XmlAttribute]
      public float EndX
      {
        get
        {
          return this.End.X;
        }
        set
        {
          this.End.X = value;
        }
      }

      [XmlAttribute]
      public float EndY
      {
        get
        {
          return this.End.Y;
        }
        set
        {
          this.End.Y = value;
        }
      }
    }

    [ProtoContract]
    public class CubeBlockComponent
    {
      [XmlIgnore]
      public MyObjectBuilderType Type = (MyObjectBuilderType) typeof (MyObjectBuilder_Component);
      [XmlAttribute]
      [ProtoMember(1)]
      public string Subtype;
      [ProtoMember(2)]
      [XmlAttribute]
      public ushort Count;
    }

    [ProtoContract]
    public class CriticalPart
    {
      [XmlIgnore]
      public MyObjectBuilderType Type = (MyObjectBuilderType) typeof (MyObjectBuilder_Component);
      [XmlAttribute]
      [ProtoMember(1)]
      public string Subtype;
      [XmlAttribute]
      [ProtoMember(2)]
      public int Index;
    }

    [ProtoContract]
    public class Variant
    {
      [XmlAttribute]
      [ProtoMember(1)]
      public string Color;
      [XmlAttribute]
      [ProtoMember(2)]
      public string Suffix;
    }

    [ProtoContract]
    public class PatternDefinition
    {
      [ProtoMember(1)]
      public MyCubeTopology CubeTopology;
      [ProtoMember(2)]
      public MyObjectBuilder_CubeBlockDefinition.Side[] Sides;
      [ProtoMember(3)]
      public bool ShowEdges;
    }

    [ProtoContract]
    public class Side
    {
      [ModdableContentFile("mwm")]
      [XmlAttribute]
      [ProtoMember(1)]
      public string Model;
      [ProtoMember(2)]
      [XmlIgnore]
      public SerializableVector2I PatternSize;

      [XmlAttribute]
      public int PatternWidth
      {
        get
        {
          return this.PatternSize.X;
        }
        set
        {
          this.PatternSize.X = value;
        }
      }

      [XmlAttribute]
      public int PatternHeight
      {
        get
        {
          return this.PatternSize.Y;
        }
        set
        {
          this.PatternSize.Y = value;
        }
      }
    }

    [ProtoContract]
    public class BuildProgressModel
    {
      [XmlAttribute]
      [ProtoMember(1)]
      public float BuildPercentUpperBound;
      [ProtoMember(2)]
      [ModdableContentFile("mwm")]
      [XmlAttribute]
      public string File;
      [ProtoMember(3)]
      [XmlAttribute]
      [DefaultValue(false)]
      public bool RandomOrientation;
    }

    [ProtoContract]
    public class MyAdditionalModelDefinition
    {
      [XmlAttribute]
      [ProtoMember(1)]
      public string Type;
      [XmlAttribute]
      [ProtoMember(2)]
      [ModdableContentFile("mwm")]
      public string File;
      [DefaultValue(false)]
      [ProtoMember(3)]
      [XmlAttribute]
      public bool EnablePhysics;
    }

    [ProtoContract]
    public class MyGeneratedBlockDefinition
    {
      [XmlAttribute]
      [ProtoMember(1)]
      public string Type;
      [ProtoMember(2)]
      public SerializableDefinitionId Id;
    }

    [ProtoContract]
    public class MySubBlockDefinition
    {
      [ProtoMember(1)]
      [XmlAttribute]
      public string SubBlock;
      [ProtoMember(2)]
      public SerializableDefinitionId Id;
    }
  }
}
