// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ConveyorPacket
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using System.ComponentModel;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_ConveyorPacket : MyObjectBuilder_Base
    {
        [ProtoMember(1)] public MyObjectBuilder_InventoryItem Item;
        [ProtoMember(2)] [DefaultValue(0)] public int LinePosition;

        public bool ShouldSerializeLinePosition()
        {
            return this.LinePosition != 0;
        }
    }
}