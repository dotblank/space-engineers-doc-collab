// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMySoundBlock
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI;

namespace Sandbox.ModAPI.Ingame
{
  public interface IMySoundBlock : IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
  {
    float Volume { get; }

    float Range { get; }

    bool IsSoundSelected { get; }

    float LoopPeriod { get; }
  }
}
