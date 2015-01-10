// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.Input.MyInputSnapshot
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
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
