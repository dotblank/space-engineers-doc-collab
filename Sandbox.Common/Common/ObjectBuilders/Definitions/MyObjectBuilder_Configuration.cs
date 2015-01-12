// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_Configuration
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_Configuration : MyObjectBuilder_Base
    {
        [ProtoMember(1)] public MyObjectBuilder_Configuration.CubeSizeSettings CubeSizes;
        [ProtoMember(2)] public MyObjectBuilder_Configuration.BaseBlockSettings BaseBlockPrefabs;
        [ProtoMember(3)] public MyObjectBuilder_Configuration.BaseBlockSettings BaseBlockPrefabsSurvival;

        [ProtoContract]
        public struct CubeSizeSettings
        {
            [ProtoMember(1)] [XmlAttribute] public float Large;
            [ProtoMember(2)] [XmlAttribute] public float Small;
            [XmlAttribute] [ProtoMember(3)] public float Medium;
        }

        [ProtoContract]
        public struct BaseBlockSettings
        {
            [XmlAttribute] [ProtoMember(1)] public string SmallStatic;
            [ProtoMember(2)] [XmlAttribute] public string LargeStatic;
            [XmlAttribute] [ProtoMember(3)] public string SmallDynamic;
            [XmlAttribute] [ProtoMember(4)] public string LargeDynamic;
            [XmlAttribute] [ProtoMember(5)] public string MediumStatic;
            [ProtoMember(6)] [XmlAttribute] public string MediumDynamic;
        }
    }
}