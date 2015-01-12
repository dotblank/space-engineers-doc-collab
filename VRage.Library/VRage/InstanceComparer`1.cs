// Decompiled with JetBrains decompiler
// Type: VRage.InstanceComparer`1
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace VRage
{
    public class InstanceComparer<T> : IEqualityComparer<T> where T : class
    {
        public static readonly InstanceComparer<T> Default = new InstanceComparer<T>();

        public bool Equals(T x, T y)
        {
            return object.ReferenceEquals((object) x, (object) y);
        }

        public int GetHashCode(T obj)
        {
            return RuntimeHelpers.GetHashCode((object) obj);
        }
    }
}