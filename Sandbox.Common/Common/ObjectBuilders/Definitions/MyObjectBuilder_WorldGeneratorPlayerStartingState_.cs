// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_WorldGeneratorPlayerStartingState_Transform
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [XmlType("Transform")]
    public class MyObjectBuilder_WorldGeneratorPlayerStartingState_Transform :
        MyObjectBuilder_WorldGeneratorPlayerStartingState
    {
        [ProtoMember(1)] public MyPositionAndOrientation? Transform;
        [XmlAttribute] [ProtoMember(2)] public bool JetpackEnabled;
        [ProtoMember(3)] [XmlAttribute] public bool DampenersEnabled;

        public bool ShouldSerializeTransform()
        {
            return this.Transform.HasValue;
        }
    }
}