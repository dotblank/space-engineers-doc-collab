// Decompiled with JetBrains decompiler
// Type: Sandbox.Engine.Physics.RigidBodyFlag
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Engine.Physics
{
    [Flags]
    public enum RigidBodyFlag
    {
        RBF_DEFAULT = 0,
        RBF_KINEMATIC = 2,
        RBF_STATIC = 4,
        RBF_DISABLE_COLLISION_RESPONSE = 64,
        RBF_DOUBLED_KINEMATIC = 128,
        RBF_BULLET = 256,
        RBF_DEBRIS = 512,
        RBF_KEYFRAMED_REPORTING = 1024,
    }
}