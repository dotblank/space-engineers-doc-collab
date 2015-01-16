// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Input.MyInputSnapshot
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common;
using System.Collections.Generic;
using System.Reflection;
using VRageMath;

namespace Sandbox.Common.Input
{
    [Obfuscation(Exclude = true, Feature = "cw symbol renaming")]
    public class MyInputSnapshot
    {
        public MyMouseSnapshot MouseSnapshot { get; set; }

        public List<Keys> KeyboardSnapshot { get; set; }

        public MyJoystickStateSnapshot JoystickSnapshot { get; set; }

        public int SnapshotTimestamp { get; set; }

        public Vector2 MouseCursorPosition { get; set; }
    }
}