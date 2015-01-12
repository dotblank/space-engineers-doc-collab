// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.ObjectBuilders.MyItemFlags
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.Common.ObjectBuilders
{
    /// <summary>
    ///     An object's status flags.
    /// </summary>
    [Flags]
    public enum MyItemFlags : byte
    {
        /// <summary>
        ///     Object has no applied status effects
        /// </summary>
        None = (byte) 0,

        /// <summary>
        ///     Object has been damaged
        /// </summary>
        Damaged = (byte) 2,
    }
}