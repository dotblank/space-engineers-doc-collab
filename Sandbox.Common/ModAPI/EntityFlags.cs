// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.EntityFlags
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;

namespace Sandbox.ModAPI
{
    [Flags]
    public enum EntityFlags
    {
        None = 1,
        Visible = 2,
        Save = 8,
        Near = 16,
        NeedsUpdate = 32,
        NeedsResolveCastShadow = 64,
        FastCastShadowResolve = 128,
        SkipIfTooSmall = 256,
        NeedsUpdate10 = 512,
        NeedsUpdate100 = 1024,
        NeedsDraw = 2048,
        InvalidateOnMove = 4096,
        Sync = 8192,
        NeedsDrawFromParent = 16384,
        ShadowBoxLod = 32768,
        Transparent = 65536,
        NeedsUpdateBeforeNextFrame = 131072,
    }
}