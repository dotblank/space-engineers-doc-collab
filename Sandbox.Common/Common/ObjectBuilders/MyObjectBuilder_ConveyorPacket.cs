// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_ConveyorPacket
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
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
        [DefaultValue(0)] [ProtoMember(2)] public int LinePosition;

        public bool ShouldSerializeLinePosition()
        {
            return this.LinePosition != 0;
        }
    }
}