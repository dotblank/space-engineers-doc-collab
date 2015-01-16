// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMyMotorSuspension
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI;

namespace Sandbox.ModAPI.Ingame
{
    public interface IMyMotorSuspension : IMyMotorBase, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock, IMyEntity
    {
        bool Steering { get; }

        bool Propulsion { get; }

        float Damping { get; }

        float Strength { get; }

        float Friction { get; }

        float Power { get; }
    }
}