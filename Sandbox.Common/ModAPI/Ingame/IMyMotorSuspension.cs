// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMyMotorSuspension
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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