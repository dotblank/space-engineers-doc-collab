// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_Configuration
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
    public class MyObjectBuilder_Configuration : MyObjectBuilder_Base
    {
        [ProtoMember(1)] public MyObjectBuilder_Configuration.CubeSizeSettings CubeSizes;
        [ProtoMember(2)] public MyObjectBuilder_Configuration.BaseBlockSettings BaseBlockPrefabs;
        [ProtoMember(3)] public MyObjectBuilder_Configuration.BaseBlockSettings BaseBlockPrefabsSurvival;

        [ProtoContract]
        public struct CubeSizeSettings
        {
            [XmlAttribute] [ProtoMember(1)] public float Large;
            [ProtoMember(2)] [XmlAttribute] public float Small;
            [XmlAttribute] [ProtoMember(3)] public float Medium;
        }

        [ProtoContract]
        public struct BaseBlockSettings
        {
            [ProtoMember(1)] [XmlAttribute] public string SmallStatic;
            [XmlAttribute] [ProtoMember(2)] public string LargeStatic;
            [ProtoMember(3)] [XmlAttribute] public string SmallDynamic;
            [XmlAttribute] [ProtoMember(4)] public string LargeDynamic;
            [ProtoMember(5)] [XmlAttribute] public string MediumStatic;
            [ProtoMember(6)] [XmlAttribute] public string MediumDynamic;
        }
    }
}