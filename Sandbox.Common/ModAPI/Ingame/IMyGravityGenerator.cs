// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMyGravityGenerator
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI;

namespace Sandbox.ModAPI.Ingame
{
    public interface IMyGravityGenerator : IMyGravityGeneratorBase, IMyFunctionalBlock, IMyTerminalBlock, IMyCubeBlock,
        IMyEntity
    {
        float FieldWidth { get; }

        float FieldHeight { get; }

        float FieldDepth { get; }

        float Gravity { get; }
    }
}