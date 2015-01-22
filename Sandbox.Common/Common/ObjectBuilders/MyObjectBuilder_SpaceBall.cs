// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_SpaceBall
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_SpaceBall : MyObjectBuilder_FunctionalBlock
    {
        [ProtoMember(1)] public float VirtualMass = 100f;
        [ProtoMember(2)] public float Friction = 0.5f;
        [ProtoMember(3)] public float Restitution = 0.5f;
        [ProtoMember(4)] public bool EnableBroadcast = true;
    }
}