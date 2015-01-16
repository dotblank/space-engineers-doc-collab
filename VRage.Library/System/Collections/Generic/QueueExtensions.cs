// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.QueueExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections;

namespace System.Collections.Generic
{
    public static class QueueExtensions
    {
        public static bool TryDequeue<T>(this Queue<T> queue, out T result)
        {
            if (queue.Count > 0)
            {
                result = queue.Dequeue();
                return true;
            }
            else
            {
                result = default (T);
                return false;
            }
        }

        public static bool TryDequeueSync<T>(this Queue<T> queue, out T result)
        {
            lock (((ICollection) queue).SyncRoot)
                return QueueExtensions.TryDequeue<T>(queue, out result);
        }
    }
}