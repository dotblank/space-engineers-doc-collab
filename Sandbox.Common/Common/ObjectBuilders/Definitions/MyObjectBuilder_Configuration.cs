// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_Configuration
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
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
            [XmlAttribute] [ProtoMember(2)] public float Small;
            [ProtoMember(3)] [XmlAttribute] public float Medium;
        }

        [ProtoContract]
        public struct BaseBlockSettings
        {
            [XmlAttribute] [ProtoMember(1)] public string SmallStatic;
            [ProtoMember(2)] [XmlAttribute] public string LargeStatic;
            [ProtoMember(3)] [XmlAttribute] public string SmallDynamic;
            [ProtoMember(4)] [XmlAttribute] public string LargeDynamic;
            [XmlAttribute] [ProtoMember(5)] public string MediumStatic;
            [ProtoMember(6)] [XmlAttribute] public string MediumDynamic;
        }
    }
}