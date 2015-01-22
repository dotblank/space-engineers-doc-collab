// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_Definitions
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.Audio;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [XmlRoot("Definitions")]
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_Definitions : MyObjectBuilder_Base
    {
        [XmlArrayItem("AmmoMagazine")] [ProtoMember(1)] public MyObjectBuilder_AmmoMagazineDefinition[] AmmoMagazines;
        [ProtoMember(2)] [XmlArrayItem("Blueprint")] public MyObjectBuilder_BlueprintDefinition[] Blueprints;
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
        [XmlArrayItem("Edges")] [ProtoMember(18)] public MyObjectBuilder_EdgesDefinition[] Edges;
        [XmlArrayItem("Prefab")] [ProtoMember(19)] public MyObjectBuilder_PrefabDefinition[] Prefabs;
        [ProtoMember(20)] [XmlArrayItem("Class")] public MyObjectBuilder_BlueprintClassDefinition[] BlueprintClasses;
        [XmlArrayItem("Entry")] [ProtoMember(21)] public BlueprintClassEntry[] BlueprintClassEntries;

        [XmlArrayItem("EnvironmentItem",
            Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_EnvironmentItemDefinition>))] [ProtoMember(22)] public MyObjectBuilder_EnvironmentItemDefinition[] EnvironmentItems;

        [ProtoMember(23)] [XmlArrayItem("Template",
            Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_CompoundBlockTemplateDefinition>))] public
            MyObjectBuilder_CompoundBlockTemplateDefinition[] CompoundBlockTemplates;

        [ProtoMember(24)] [XmlArrayItem("Ship", Type = typeof (MyAbstractXmlSerializer<MyObjectBuilder_RespawnShipDefinition>))] public
            MyObjectBuilder_RespawnShipDefinition[] RespawnShips;

        [ProtoMember(25)] [XmlArrayItem("Category")] public MyObjectBuilder_GuiBlockCategoryDefinition[] CategoryClasses;

        [XmlArrayItem("ShipBlueprint")] [ProtoMember(26)] public MyObjectBuilder_ShipBlueprintDefinition[]
            ShipBlueprints;

        [XmlArrayItem("Weapon")] [ProtoMember(27)] public MyObjectBuilder_WeaponDefinition[] Weapons;
        [XmlArrayItem("Ammo")] [ProtoMember(28)] public MyObjectBuilder_AmmoDefinition[] Ammos;
        [ProtoMember(29)] [XmlArrayItem("Sound")] public MyObjectBuilder_AudioDefinition[] Sounds;
        [ProtoMember(30)] [XmlArrayItem("VoxelHand")] public MyObjectBuilder_VoxelHandDefinition[] VoxelHands;
        [XmlArrayItem("MultiBlock")] [ProtoMember(31)] public MyObjectBuilder_MultiBlockDefinition[] MultiBlocks;

        [XmlArrayItem("PrefabThrower")] [ProtoMember(32)] public MyObjectBuilder_PrefabThrowerDefinition[]
            PrefabThrowers;

        [XmlArrayItem("SoundCategory")] [ProtoMember(33)] public MyObjectBuilder_SoundCategoryDefinition[]
            SoundCategories;
    }
}