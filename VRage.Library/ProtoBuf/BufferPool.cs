// Decompiled with JetBrains decompiler
// Type: ProtoBuf.BufferPool
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Threading;

namespace ProtoBuf
{
    internal class BufferPool
    {
        private static readonly object[] pool = new object[20];
        private const int PoolSize = 20;
        internal const int BufferLength = 1024;

        private BufferPool()
        {
        }

        internal static void Flush()
        {
            for (int index = 0; index < BufferPool.pool.Length; ++index)
                Interlocked.Exchange(ref BufferPool.pool[index], (object) null);
        }

        internal static byte[] GetBuffer()
        {
            for (int index = 0; index < BufferPool.pool.Length; ++index)
            {
                object obj;
                if ((obj = Interlocked.Exchange(ref BufferPool.pool[index], (object) null)) != null)
                    return (byte[]) obj;
            }
            return new byte[1024];
        }

        internal static void ResizeAndFlushLeft(ref byte[] buffer, int toFitAtLeastBytes, int copyFromIndex,
            int copyBytes)
        {
            int length = buffer.Length*2;
            if (length < toFitAtLeastBytes)
                length = toFitAtLeastBytes;
            byte[] to = new byte[length];
            if (copyBytes > 0)
                Helpers.BlockCopy(buffer, copyFromIndex, to, 0, copyBytes);
            if (buffer.Length == 1024)
                BufferPool.ReleaseBufferToPool(ref buffer);
            buffer = to;
        }

        internal static void ReleaseBufferToPool(ref byte[] buffer)
        {
            if (buffer == null)
                return;
            if (buffer.Length == 1024)
            {
                int index = 0;
                while (index < BufferPool.pool.Length &&
                       Interlocked.CompareExchange(ref BufferPool.pool[index], (object) buffer, (object) null) != null)
                    ++index;
            }
            buffer = (byte[]) null;
        }
    }
}