// Decompiled with JetBrains decompiler
// Type: VRage.ByteStream
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;

namespace VRage
{
  public class ByteStream : Stream
  {
    private byte[] m_baseArray;
    private int m_position;
    private int m_length;
    public readonly bool Expandable;
    public readonly bool Resetable;

    public byte[] Data
    {
      get
      {
        return this.m_baseArray;
      }
    }

    public override bool CanRead
    {
      get
      {
        return true;
      }
    }

    public override bool CanSeek
    {
      get
      {
        return true;
      }
    }

    public override bool CanWrite
    {
      get
      {
        return true;
      }
    }

    public override long Length
    {
      get
      {
        return (long) this.m_length;
      }
    }

    public override long Position
    {
      get
      {
        return (long) this.m_position;
      }
      set
      {
        this.m_position = (int) value;
      }
    }

    public ByteStream(int capacity, bool expandable = true)
    {
      this.Expandable = true;
      this.Resetable = false;
      this.m_baseArray = new byte[capacity];
      this.m_length = this.m_baseArray.Length;
    }

    public ByteStream()
    {
      this.Resetable = true;
      this.Expandable = false;
    }

    public ByteStream(byte[] newBaseArray, int length)
      : this()
    {
      this.Reset(newBaseArray, length);
    }

    public override void Flush()
    {
    }

    public void Reset(byte[] newBaseArray, int length)
    {
      if (!this.Resetable)
        throw new InvalidOperationException("Stream is not created as resetable");
      if (newBaseArray.Length < length)
        throw new ArgumentException("Length must be >= newBaseArray.Length");
      this.m_baseArray = newBaseArray;
      this.m_length = length;
      this.m_position = 0;
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

    public void EnsureCapacity(long minimumSize)
    {
      if ((long) this.m_length >= minimumSize)
        return;
      if (!this.Expandable)
        throw new EndOfStreamException("ByteSteam is not large enough and is not expandable");
      if (minimumSize < 256L)
        minimumSize = 256L;
      if (minimumSize < (long) (this.m_length * 2))
        minimumSize = (long) (this.m_length * 2);
      this.Resize(minimumSize);
    }

    public void CheckCapacity(long minimumSize)
    {
      if ((long) this.m_length < minimumSize)
        throw new EndOfStreamException("Stream does not have enough size");
    }

    private void Resize(long size)
    {
      Array.Resize<byte>(ref this.m_baseArray, (int) size);
      this.m_length = this.m_baseArray.Length;
    }

    public override void SetLength(long value)
    {
      if (!this.Expandable)
        throw new InvalidOperationException("ByteStream is not expandable");
      this.Resize((long) (int) value);
    }

    public byte ReadByte()
    {
      this.CheckCapacity((long) (this.m_position + 1));
      byte num = this.m_baseArray[this.m_position];
      ++this.m_position;
      return num;
    }

    public new void WriteByte(byte value)
    {
      this.EnsureCapacity((long) (this.m_position + 1));
      this.m_baseArray[this.m_position] = value;
      ++this.m_position;
    }

    public unsafe ushort ReadUShort()
    {
      this.CheckCapacity((long) (this.m_position + 2));
      fixed (byte* numPtr = &this.m_baseArray[this.m_position])
      {
        this.m_position += 2;
        return *(ushort*) numPtr;
      }
    }

    public unsafe void WriteUShort(ushort value)
    {
      this.EnsureCapacity((long) (this.m_position + 2));
      fixed (byte* numPtr = &this.m_baseArray[this.m_position])
        *(short*) numPtr = (short) value;
      this.m_position += 2;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      this.EnsureCapacity((long) (this.m_position + count));
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
