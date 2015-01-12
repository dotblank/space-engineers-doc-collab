// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.Interfaces.IMyDestroyableObject
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using Sandbox.Common.ObjectBuilders.Definitions;

namespace Sandbox.ModAPI.Interfaces
{
    public interface IMyDestroyableObject
    {
        float Integrity { get; }

        void OnDestroy();

        void DoDamage(float damage, MyDamageType damageType, bool sync);
    }
}