// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_WeaponItemDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_WeaponItemDefinition : MyObjectBuilder_PhysicalItemDefinition
    {
        [ProtoMember(1)] public MyObjectBuilder_WeaponItemDefinition.PhysicalItemWeaponDefinitionId WeaponDefinitionId;

        [ProtoContract]
        public class PhysicalItemWeaponDefinitionId
        {
            [XmlIgnore] public MyObjectBuilderType Type =
                (MyObjectBuilderType) typeof (MyObjectBuilder_WeaponDefinition);

            [XmlAttribute] [ProtoMember(1)] public string Subtype;
        }
    }
}