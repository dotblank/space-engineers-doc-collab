// Decompiled with JetBrains decompiler
// Type: ProtoBuf.BufferExtension
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.IO;

namespace ProtoBuf
{
    public sealed class BufferExtension : IExtension
    {
        private byte[] buffer;

        int IExtension.GetLength()
        {
            if (this.buffer != null)
                return this.buffer.Length;
            else
                return 0;
        }

        Stream IExtension.BeginAppend()
        {
            return (Stream) new MemoryStream();
        }

        void IExtension.EndAppend(Stream stream, bool commit)
        {
            using (stream)
            {
                int count;
                if (!commit || (count = (int) stream.Length) <= 0)
                    return;
                MemoryStream memoryStream = (MemoryStream) stream;
                if (this.buffer == null)
                {
                    this.buffer = memoryStream.ToArray();
                }
                else
                {
                    int length = this.buffer.Length;
                    byte[] to = new byte[length + count];
                    Helpers.BlockCopy(this.buffer, 0, to, 0, length);
                    Helpers.BlockCopy(memoryStream.GetBuffer(), 0, to, length, count);
                    this.buffer = to;
                }
            }
        }

        Stream IExtension.BeginQuery()
        {
            if (this.buffer != null)
                return (Stream) new MemoryStream(this.buffer);
            else
                return Stream.Null;
        }

        void IExtension.EndQuery(Stream stream)
        {
            using (stream)
                ;
        }
    }
}