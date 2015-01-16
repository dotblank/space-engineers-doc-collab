// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyObjectBuilder_LightingBlockDefinition
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Common.ObjectBuilders.VRageData;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_LightingBlockDefinition : MyObjectBuilder_CubeBlockDefinition
    {
        [ProtoMember(1)] public SerializableBounds LightRadius = new SerializableBounds(2f, 10f, 2.8f);
        [ProtoMember(2)] public SerializableBounds LightFalloff = new SerializableBounds(1f, 3f, 1.5f);
        [ProtoMember(3)] public SerializableBounds LightIntensity = new SerializableBounds(0.5f, 5f, 2f);
        [ProtoMember(4)] public float RequiredPowerInput = 1.0f/1000.0f;
        [ProtoMember(5)] public string LightGlare = "GlareLsLight";
        [ProtoMember(6)] public SerializableBounds LightBlinkIntervalSeconds = new SerializableBounds(0.0f, 30f, 0.0f);
        [ProtoMember(7)] public SerializableBounds LightBlinkLenght = new SerializableBounds(0.0f, 100f, 10f);
        [ProtoMember(8)] public SerializableBounds LightBlinkOffset = new SerializableBounds(0.0f, 100f, 0.0f);
    }
}