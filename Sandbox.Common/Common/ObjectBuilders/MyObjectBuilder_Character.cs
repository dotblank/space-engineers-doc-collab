// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Character
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_Character : MyObjectBuilder_EntityBase
  {
    public static Dictionary<string, SerializableVector3> CharacterModels = new Dictionary<string, SerializableVector3>()
    {
      {
        "Soldier",
        new SerializableVector3(0.0f, 0.0f, 0.05f)
      },
      {
        "Astronaut",
        new SerializableVector3(0.0f, -1f, 0.0f)
      },
      {
        "Astronaut_Black",
        new SerializableVector3(0.0f, -0.96f, -0.5f)
      },
      {
        "Astronaut_Blue",
        new SerializableVector3(0.575f, 0.15f, 0.2f)
      },
      {
        "Astronaut_Green",
        new SerializableVector3(0.333f, -0.33f, -0.05f)
      },
      {
        "Astronaut_Red",
        new SerializableVector3(0.0f, 0.0f, 0.05f)
      },
      {
        "Astronaut_White",
        new SerializableVector3(0.0f, -0.8f, 0.6f)
      },
      {
        "Astronaut_Yellow",
        new SerializableVector3(0.122f, 0.05f, 0.46f)
      }
    };
    [ProtoMember(6)]
    [DefaultValue(true)]
    public bool DampenersEnabled = true;
    [ProtoMember(17)]
    public bool IsInFirstPersonView = true;
    [ProtoMember(18)]
    public bool EnableBroadcasting = true;
    [ProtoMember(1)]
    public string CharacterModel;
    [ProtoMember(2)]
    public MyObjectBuilder_Inventory Inventory;
    [ProtoMember(3)]
    [XmlElement("HandWeapon", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_EntityBase>))]
    public MyObjectBuilder_EntityBase HandWeapon;
    [ProtoMember(4)]
    public MyObjectBuilder_Battery Battery;
    [ProtoMember(5)]
    public bool LightEnabled;
    [ProtoMember(7)]
    public long? UsingLadder;
    [ProtoMember(8)]
    public SerializableVector2 HeadAngle;
    [ProtoMember(9)]
    public SerializableVector3 LinearVelocity;
    [ProtoMember(10)]
    public float AutoenableJetpackDelay;
    [ProtoMember(11)]
    public bool JetpackEnabled;
    [ProtoMember(12)]
    public float? Health;
    [ProtoMember(13)]
    [DefaultValue(false)]
    public bool AIMode;
    [ProtoMember(14)]
    public SerializableVector3 ColorMaskHSV;
    [ProtoMember(15)]
    public float LootingCounter;
    [ProtoMember(16)]
    public string DisplayName;
  }
}
