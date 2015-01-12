// Decompiled with JetBrains decompiler
// Type: ParallelTasks.Singleton`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Threading;

namespace ParallelTasks
{
    public abstract class Singleton<T> where T : class, new()
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if ((object) Singleton<T>.instance == null)
                {
                    T instance = Activator.CreateInstance<T>();
                    Interlocked.CompareExchange<T>(ref Singleton<T>.instance, instance, default (T));
                }
                return Singleton<T>.instance;
            }
        }
    }
}