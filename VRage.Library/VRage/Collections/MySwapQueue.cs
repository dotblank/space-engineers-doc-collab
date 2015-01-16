// Decompiled with JetBrains decompiler
// Type: VRage.Collections.MySwapQueue
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace VRage.Collections
{
    public class MySwapQueue
    {
        public static MySwapQueue<T> Create<T>() where T : class, new()
        {
            return new MySwapQueue<T>(Activator.CreateInstance<T>(), Activator.CreateInstance<T>(),
                Activator.CreateInstance<T>());
        }
    }
}