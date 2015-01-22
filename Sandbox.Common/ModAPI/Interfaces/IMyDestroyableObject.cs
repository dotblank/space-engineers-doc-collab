// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.IMyDestroyableObject
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders.Definitions;

namespace Sandbox.ModAPI.Interfaces
{
    /// <summary>
    /// Inaccessible as of version 01.066 (22.1.2015)
    /// </summary>
    public interface IMyDestroyableObject
    {
        float Integrity { get; }

        void OnDestroy();

        void DoDamage(float damage, MyDamageType damageType, bool sync);
    }
}