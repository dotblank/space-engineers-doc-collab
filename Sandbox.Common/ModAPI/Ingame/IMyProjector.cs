// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Ingame.IMyProjector
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4C37CB42-F216-4F7D-B6D1-CA0779A47F38
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

namespace Sandbox.ModAPI.Ingame
{
    public interface IMyProjector
    {
        int ProjectionOffsetX { get; }

        int ProjectionOffsetY { get; }

        int ProjectionOffsetZ { get; }

        int ProjectionRotX { get; }

        int ProjectionRotY { get; }

        int ProjectionRotZ { get; }
    }
}