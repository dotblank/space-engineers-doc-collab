// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMyCubeGrid
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
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