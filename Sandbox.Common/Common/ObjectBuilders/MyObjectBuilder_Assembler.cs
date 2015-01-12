// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Assembler
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.Xml.Serialization;

namespace Sandbox.Common.ObjectBuilders
{
    [ProtoContract]
    [MyObjectBuilderDefinition]
    public class MyObjectBuilder_Assembler : MyObjectBuilder_ProductionBlock
    {
        [ProtoMember(2)] public float CurrentProgress;
        [ProtoMember(4)] public bool DisassembleEnabled;
        [ProtoMember(5)] [XmlArrayItem("Item")] public MyObjectBuilder_ProductionBlock.QueueItem[] OtherQueue;
        [ProtoMember(3)] public bool RepeatAssembleEnabled;
        [ProtoMember(6)] public bool RepeatDisassembleEnabled;
        [ProtoMember(7)] public bool SlaveEnabled;
    }
}