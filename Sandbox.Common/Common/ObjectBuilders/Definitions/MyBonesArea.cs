// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.Definitions.MyBonesArea
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Common.ObjectBuilders.Definitions
{
    [Flags]
    public enum MyBonesArea
    {
        Body = 1,
        LeftHand = 2,
        RightHand = 4,
        LeftFingers = 8,
        RightFingers = 16,
        Head = 32,
    }
}