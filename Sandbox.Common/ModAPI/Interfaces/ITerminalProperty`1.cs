// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.ITerminalProperty`1
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.ModAPI.Ingame;

namespace Sandbox.ModAPI.Interfaces
{
    public interface ITerminalProperty<TValue> : ITerminalProperty
    {
        TValue GetValue(IMyCubeBlock block);

        void SetValue(IMyCubeBlock block, TValue value);

        TValue GetDefaultValue(IMyCubeBlock block);

        TValue GetMininum(IMyCubeBlock block);

        TValue GetMaximum(IMyCubeBlock block);
    }
}