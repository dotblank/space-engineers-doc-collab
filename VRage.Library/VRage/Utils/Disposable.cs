// Decompiled with JetBrains decompiler
// Type: VRage.Utils.Disposable
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
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
