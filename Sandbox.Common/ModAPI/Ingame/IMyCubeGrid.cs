// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMyCubeGrid
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders;
using VRageMath;

namespace Sandbox.ModAPI.Ingame
{
    public interface IMyCubeGrid
    {
        float GridSize { get; }

        MyCubeSize GridSizeEnum { get; }

        bool IsStatic { get; }

        Vector3I Max { get; }

        Vector3I Min { get; }

        IMySlimBlock GetCubeBlock(Vector3I pos);
    }
}