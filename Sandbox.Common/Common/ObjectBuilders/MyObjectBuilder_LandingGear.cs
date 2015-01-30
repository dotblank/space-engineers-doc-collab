// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyObjectBuilder_LandingGear
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 65B9437C-6443-4388-AFE3-5DD75CE6625F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using ProtoBuf;

namespace Sandbox.Common.ObjectBuilders
{
  [ProtoContract]
  [MyObjectBuilderDefinition]
  public class MyObjectBuilder_LandingGear : MyObjectBuilder_FunctionalBlock
  {
    [ProtoMember(2)]
    public float BrakeForce = 1f;
    [ProtoMember(1)]
    public bool IsLocked;
    [ProtoMember(3)]
    public bool AutoLock;
    [ProtoMember(4)]
    public string LockSound;
    [ProtoMember(5)]
    public string UnlockSound;
    [ProtoMember(6)]
    public string FailedAttachSound;
  }
}
