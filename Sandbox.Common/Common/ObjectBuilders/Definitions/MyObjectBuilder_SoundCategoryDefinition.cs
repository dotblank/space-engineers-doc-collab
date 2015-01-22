// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_SoundCategoryDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_SoundCategoryDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(1)] public MyObjectBuilder_SoundCategoryDefinition.SoundDesc[] Sounds;

        [ProtoContract]
        public struct SoundDesc
        {
            [XmlAttribute] public string Id;
            [XmlAttribute] public string SoundName;
        }
    }
}