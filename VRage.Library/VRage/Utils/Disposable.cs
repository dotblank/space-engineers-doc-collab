// Decompiled with JetBrains decompiler
// Type: VRage.Utils.Disposable
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
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
            
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize((object) this);
        }
    }
}