// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_Definitions
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Audio;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [XmlRoot("Definitions")]
    [ProtoContract]
    public class MyObjectBuilder_Definitions : MyObjectBuilder_Base
    {
        [ProtoMember(1)] [XmlArrayItem("AmmoMagazine")] public MyObjectBuilder_AmmoMagazineDefinition[] AmmoMagazines;
        [XmlArrayItem("Blueprint")] [ProtoMember(2)] public MyObjectBuilder_BlueprintDefinition[] Blueprints;
        [ProtoMember(3)] [XmlArrayItem("Component")] public MyObjectBuilder_ComponentDefinition[] Components;
        [ProtoMember(4)] [XmlArrayItem("ContainerType")] public MyObjectBuilder_ContainerTypeDefinition[] ContainerTypes;
        [XmlArrayItem("Definition", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_CubeBlockDefinition>))] [ProtoMember(5)] public MyObjectBuilder_CubeBlockDefinition[] CubeBlocks;
        [ProtoMember(6)] [XmlArrayItem("BlockPosition")] public MyBlockPosition[] BlockPositions;
        [ProtoMember(7)] public MyObjectBuilder_Configuration Configuration;
        [ProtoMember(8)] public MyObjectBuilder_EnvironmentDefinition Environment;
        [XmlArrayItem("GlobalEvent")] [ProtoMember(9)] public MyObjectBuilder_GlobalEventDefinition[] GlobalEvents;
        [ProtoMember(10)] [XmlArrayItem("HandItem")] public MyObjectBuilder_HandItemDefinition[] HandItems;
        [ProtoMember(11)] [XmlArrayItem("PhysicalItem", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_PhysicalItemDefinition>))] public MyObjectBuilder_PhysicalItemDefinition[] PhysicalItems;
        [XmlArrayItem("SpawnGroup")] [ProtoMember(12)] public MyObjectBuilder_SpawnGroupDefinition[] SpawnGroups;

        [XmlArrayItem("TransparentMaterial")] [ProtoMember(13)] public MyObjectBuilder_TransparentMaterialDefinition[]
            TransparentMaterials;

        [XmlArrayItem("VoxelMaterial", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_VoxelMaterialDefinition>))
        ] [ProtoMember(14)] public MyObjectBuilder_VoxelMaterialDefinition[] VoxelMaterials;

        [ProtoMember(15)] [XmlArrayItem("Character")] public MyObjectBuilder_CharacterDefinition[] Characters;
        [ProtoMember(16)] [XmlArrayItem("Animation")] public MyObjectBuilder_AnimationDefinition[] Animations;
        [ProtoMember(17)] [XmlArrayItem("Debris")] public MyObjectBuilder_DebrisDefinition[] Debris;
        [XmlArrayItem("Edges")] [ProtoMember(18)] public MyObjectBuilder_EdgesDefinition[] Edges;
        [ProtoMember(19)] [XmlArrayItem("Prefab")] public MyObjectBuilder_PrefabDefinition[] Prefabs;
        [ProtoMember(20)] [XmlArrayItem("Class")] public MyObjectBuilder_BlueprintClassDefinition[] BlueprintClasses;
        [ProtoMember(21)] [XmlArrayItem("Entry")] public BlueprintClassEntry[] BlueprintClassEntries;

        [ProtoMember(22)] [XmlArrayItem("EnvironmentItem",
            Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_EnvironmentItemDefinition>))] public
            MyObjectBuilder_EnvironmentItemDefinition[] EnvironmentItems;

        [ProtoMember(23)] [XmlArrayItem("Template",
            Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_CompoundBlockTemplateDefinition>))] public
            MyObjectBuilder_CompoundBlockTemplateDefinition[] CompoundBlockTemplates;

        [XmlArrayItem("Ship", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_RespawnShipDefinition>))] [ProtoMember(24)] public MyObjectBuilder_RespawnShipDefinition[] RespawnShips;
        [ProtoMember(25)] [XmlArrayItem("Category")] public MyObjectBuilder_GuiBlockCategoryDefinition[] CategoryClasses;

        [ProtoMember(26)] [XmlArrayItem("ShipBlueprint")] public MyObjectBuilder_ShipBlueprintDefinition[]
            ShipBlueprints;

        [XmlArrayItem("Weapon")] [ProtoMember(27)] public MyObjectBuilder_WeaponDefinition[] Weapons;
        [ProtoMember(28)] [XmlArrayItem("Ammo")] public MyObjectBuilder_AmmoDefinition[] Ammos;
        [ProtoMember(29)] [XmlArrayItem("Sound")] public MyObjectBuilder_AudioDefinition[] Sounds;
        [ProtoMember(30)] [XmlArrayItem("VoxelHand")] public MyObjectBuilder_VoxelHandDefinition[] VoxelHands;
        [XmlArrayItem("MultiBlock")] [ProtoMember(31)] public MyObjectBuilder_MultiBlockDefinition[] MultiBlocks;

        [XmlArrayItem("PrefabThrower")] [ProtoMember(32)] public MyObjectBuilder_PrefabThrowerDefinition[]
            PrefabThrowers;

        [XmlArrayItem("SoundCategory")] [ProtoMember(33)] public MyObjectBuilder_SoundCategoryDefinition[]
            SoundCategories;
    }
}