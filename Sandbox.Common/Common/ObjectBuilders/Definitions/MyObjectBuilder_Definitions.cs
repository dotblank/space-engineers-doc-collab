// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_Definitions
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
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
        [XmlArrayItem("AmmoMagazine")] [ProtoMember(1)] public MyObjectBuilder_AmmoMagazineDefinition[] AmmoMagazines;
        [XmlArrayItem("Blueprint")] [ProtoMember(2)] public MyObjectBuilder_BlueprintDefinition[] Blueprints;
        [ProtoMember(3)] [XmlArrayItem("Component")] public MyObjectBuilder_ComponentDefinition[] Components;
        [XmlArrayItem("ContainerType")] [ProtoMember(4)] public MyObjectBuilder_ContainerTypeDefinition[] ContainerTypes;
        [XmlArrayItem("Definition", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_CubeBlockDefinition>))] [ProtoMember(5)] public MyObjectBuilder_CubeBlockDefinition[] CubeBlocks;
        [XmlArrayItem("BlockPosition")] [ProtoMember(6)] public MyBlockPosition[] BlockPositions;
        [ProtoMember(7)] public MyObjectBuilder_Configuration Configuration;
        [ProtoMember(8)] public MyObjectBuilder_EnvironmentDefinition Environment;
        [XmlArrayItem("GlobalEvent")] [ProtoMember(9)] public MyObjectBuilder_GlobalEventDefinition[] GlobalEvents;
        [ProtoMember(10)] [XmlArrayItem("HandItem")] public MyObjectBuilder_HandItemDefinition[] HandItems;
        [XmlArrayItem("PhysicalItem", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_PhysicalItemDefinition>))] [ProtoMember(11)] public MyObjectBuilder_PhysicalItemDefinition[] PhysicalItems;
        [ProtoMember(12)] [XmlArrayItem("SpawnGroup")] public MyObjectBuilder_SpawnGroupDefinition[] SpawnGroups;

        [XmlArrayItem("TransparentMaterial")] [ProtoMember(13)] public MyObjectBuilder_TransparentMaterialDefinition[]
            TransparentMaterials;

        [ProtoMember(14)] [XmlArrayItem("VoxelMaterial", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_VoxelMaterialDefinition>))
                          ] public MyObjectBuilder_VoxelMaterialDefinition[] VoxelMaterials;

        [XmlArrayItem("Character")] [ProtoMember(15)] public MyObjectBuilder_CharacterDefinition[] Characters;
        [XmlArrayItem("Animation")] [ProtoMember(16)] public MyObjectBuilder_AnimationDefinition[] Animations;
        [ProtoMember(17)] [XmlArrayItem("Debris")] public MyObjectBuilder_DebrisDefinition[] Debris;
        [ProtoMember(18)] [XmlArrayItem("Edges")] public MyObjectBuilder_EdgesDefinition[] Edges;
        [XmlArrayItem("Prefab")] [ProtoMember(19)] public MyObjectBuilder_PrefabDefinition[] Prefabs;
        [XmlArrayItem("Class")] [ProtoMember(20)] public MyObjectBuilder_BlueprintClassDefinition[] BlueprintClasses;
        [XmlArrayItem("Entry")] [ProtoMember(21)] public BlueprintClassEntry[] BlueprintClassEntries;

        [ProtoMember(22)] [XmlArrayItem("EnvironmentItem",
            Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_EnvironmentItemDefinition>))] public
            MyObjectBuilder_EnvironmentItemDefinition[] EnvironmentItems;

        [XmlArrayItem("Template",
            Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_CompoundBlockTemplateDefinition>))] [ProtoMember(23)] public MyObjectBuilder_CompoundBlockTemplateDefinition[] CompoundBlockTemplates;

        [XmlArrayItem("Ship", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_RespawnShipDefinition>))] [ProtoMember(24)] public MyObjectBuilder_RespawnShipDefinition[] RespawnShips;
        [ProtoMember(25)] [XmlArrayItem("Category")] public MyObjectBuilder_GuiBlockCategoryDefinition[] CategoryClasses;

        [XmlArrayItem("ShipBlueprint")] [ProtoMember(26)] public MyObjectBuilder_ShipBlueprintDefinition[]
            ShipBlueprints;

        [ProtoMember(27)] [XmlArrayItem("Weapon")] public MyObjectBuilder_WeaponDefinition[] Weapons;
        [XmlArrayItem("Ammo")] [ProtoMember(28)] public MyObjectBuilder_AmmoDefinition[] Ammos;
        [ProtoMember(29)] [XmlArrayItem("Sound")] public MyObjectBuilder_AudioDefinition[] Sounds;
        [XmlArrayItem("VoxelHand")] [ProtoMember(30)] public MyObjectBuilder_VoxelHandDefinition[] VoxelHands;
        [XmlArrayItem("MultiBlock")] [ProtoMember(31)] public MyObjectBuilder_MultiBlockDefinition[] MultiBlocks;

        [ProtoMember(32)] [XmlArrayItem("PrefabThrower")] public MyObjectBuilder_PrefabThrowerDefinition[]
            PrefabThrowers;

        [ProtoMember(33)] [XmlArrayItem("SoundCategory")] public MyObjectBuilder_SoundCategoryDefinition[]
            SoundCategories;
    }
}