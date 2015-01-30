// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_CubeGrid
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders.VRageData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
  [MyObjectBuilderDefinition]
  [ProtoContract]
  public class MyObjectBuilder_CubeGrid : MyObjectBuilder_EntityBase
  {
    [ProtoMember(46)]
    [XmlArrayItem("MyObjectBuilder_CubeBlock", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_CubeBlock>))]
    public List<MyObjectBuilder_CubeBlock> CubeBlocks = new List<MyObjectBuilder_CubeBlock>();
    [ProtoMember(57)]
    [DefaultValue(true)]
    public bool DampenersEnabled = true;
    [ProtoMember(58)]
    public List<MyObjectBuilder_ConveyorLine> ConveyorLines = new List<MyObjectBuilder_ConveyorLine>();
    [ProtoMember(59)]
    public List<MyObjectBuilder_BlockGroup> BlockGroups = new List<MyObjectBuilder_BlockGroup>();
    [NonSerialized]
    public bool CreatePhysics = true;
    [ProtoMember(44)]
    public MyCubeSize GridSizeEnum;
    [ProtoMember(47)]
    public bool IsStatic;
    [ProtoMember(48)]
    public List<BoneInfo> Skeleton;
    [ProtoMember(49)]
    public SerializableVector3 LinearVelocity;
    [ProtoMember(50)]
    public SerializableVector3 AngularVelocity;
    [ProtoMember(51)]
    public SerializableVector3I? XMirroxPlane;
    [ProtoMember(52)]
    public SerializableVector3I? YMirroxPlane;
    [ProtoMember(53)]
    public SerializableVector3I? ZMirroxPlane;
    [ProtoMember(54)]
    [DefaultValue(false)]
    public bool XMirroxOdd;
    [ProtoMember(55)]
    [DefaultValue(false)]
    public bool YMirroxOdd;
    [ProtoMember(56)]
    [DefaultValue(false)]
    public bool ZMirroxOdd;
    [ProtoMember(60)]
    public bool Handbrake;
    [ProtoMember(61)]
    public string DisplayName;

    public override void Remap(IMyRemapHelper remapHelper)
    {
      base.Remap(remapHelper);
      foreach (MyObjectBuilder_CubeBlock builderCubeBlock in this.CubeBlocks)
        builderCubeBlock.Remap(remapHelper);
    }
  }
}
