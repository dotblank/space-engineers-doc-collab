// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_LandingGear
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
    [MyObjectBuilderDefinition]
    [ProtoContract]
    public class MyObjectBuilder_LandingGear : MyObjectBuilder_FunctionalBlock
    {
        [ProtoMember(2)] public float BrakeForce = 1f;
        [ProtoMember(1)] public bool IsLocked;
        [ProtoMember(3)] public bool AutoLock;
        [ProtoMember(4)] public string LockSound;
        [ProtoMember(5)] public string UnlockSound;
        [ProtoMember(6)] public string FailedAttachSound;
    }
}