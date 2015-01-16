// Decompiled with JetBrains decompiler
// Type: VRage.Utils.Disposable
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Diagnostics;

namespace VRage.Utils
{
    public class Disposable : IDisposable
    {
        public Disposable(bool collectStack = false)
        {
        }

        ~Disposable()
        {
            // lazy fix for decomplier shenanigans
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize((object) this);
        }
    }
}