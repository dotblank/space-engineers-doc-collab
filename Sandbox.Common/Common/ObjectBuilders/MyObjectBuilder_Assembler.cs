// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_Assembler
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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