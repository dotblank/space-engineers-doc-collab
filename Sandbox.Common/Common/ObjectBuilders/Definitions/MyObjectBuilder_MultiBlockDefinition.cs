// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_MultiBlockDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_MultiBlockDefinition : MyObjectBuilder_DefinitionBase
    {
        [ProtoMember(1)] [DefaultValue(null)] [XmlArrayItem("BlockDefinition")] public
            MyObjectBuilder_MultiBlockDefinition.MyOBMultiBlockPartDefinition[] BlockDefinitions;

        [ProtoContract]
        public class MyOBMultiBlockPartDefinition
        {
            [ProtoMember(1)] public SerializableDefinitionId Id;
            [ProtoMember(2)] public SerializableVector3I Position;
            [ProtoMember(3)] public SerializableBlockOrientation Orientation;
        }
    }
}