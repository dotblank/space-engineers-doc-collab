// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.IMyGamePaths
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

namespace Sandbox.ModAPI
{
    public interface IMyGamePaths
    {
        string ContentPath { get; }

        string ModsPath { get; }

        string UserDataPath { get; }

        string SavesPath { get; }
    }
}