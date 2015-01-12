// Decompiled with JetBrains decompiler
// Type: VRage.ResetableMemoryStream
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;

namespace VRage
{
    public class ResetableMemoryStream : Stream
    {
        private byte[] m_baseArray;
        private int m_position;
        private int m_length;

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return true; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override long Length
        {
            get { return (long) this.m_length; }
        }

        public override long Position
        {
            get { return (long) this.m_position; }
            set { this.m_position = (int) value; }
        }

        public ResetableMemoryStream()
        {
        }

        public ResetableMemoryStream(byte[] baseArray, int length)
        {
            this.Reset(baseArray, length);
        }

        public void Reset(byte[] newBaseArray, int length)
        {
            if (newBaseArray.Length < length)
                throw new ArgumentException("Length must be >= newBaseArray.Length");
            this.m_baseArray = newBaseArray;
            this.m_length = length;
            this.m_position = 0;
        }

        public byte[] GetInternalBuffer()
        {
            return this.m_baseArray;
        }

        public override void Flush()
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int count1 = this.m_length - this.m_position;
            if (count1 > count)
                count1 = count;
            if (count1 <= 0)
                return 0;
            if (count1 <= 8)
            {
                int num = count1;
                while (--num >= 0)
                    buffer[offset + num] = this.m_baseArray[this.m_position + num];
            }
            else
                Buffer.BlockCopy((Array) this.m_baseArray, this.m_position, (Array) buffer, offset, count1);
            this.m_position += count1;
            return count1;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    this.m_position = (int) offset;
                    break;
                case SeekOrigin.Current:
                    this.m_position += (int) offset;
                    break;
                case SeekOrigin.End:
                    this.m_position = this.m_length + (int) offset;
                    break;
                default:
                    throw new ArgumentException("Invalid seek origin");
            }
            return (long) this.m_position;
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException("Operation not supported");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.m_length < this.m_position + count)
                throw new EndOfStreamException();
            int num1 = this.m_position + count;
            if (count <= 8 && buffer != this.m_baseArray)
            {
                int num2 = count;
                while (--num2 >= 0)
                    this.m_baseArray[this.m_position + num2] = buffer[offset + num2];
            }
            else
                Buffer.BlockCopy((Array) buffer, offset, (Array) this.m_baseArray, this.m_position, count);
            this.m_position = num1;
        }
    }
}