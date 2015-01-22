// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MySwapQueue
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace VRage.Collections
{
  public class MySwapQueue
  {
    public static MySwapQueue<T> Create<T>() where T : class, new()
    {
      return new MySwapQueue<T>(Activator.CreateInstance<T>(), Activator.CreateInstance<T>(), Activator.CreateInstance<T>());
    }
  }
}
