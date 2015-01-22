// Decompiled with JetBrains decompiler
// Type: System.StreamExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.IO;
using System.IO.Compression;
using System.Text;

namespace System
{
  public static class StreamExtensions
  {
    [ThreadStatic]
    private static byte[] m_buffer;

    private static byte[] Buffer
    {
      get
      {
        if (StreamExtensions.m_buffer == null)
          StreamExtensions.m_buffer = new byte[256];
        return StreamExtensions.m_buffer;
      }
    }

    public static bool CheckGZipHeader(this Stream stream)
    {
      long position = stream.Position;
      byte[] buffer = new byte[2];
      stream.Seek(0L, SeekOrigin.Begin);
      stream.Read(buffer, 0, 2);
      if ((int) buffer[0] == 31 && (int) buffer[1] == 139)
      {
        stream.Seek(position, SeekOrigin.Begin);
        return true;
      }
      else
      {
        stream.Seek(position, SeekOrigin.Begin);
        return false;
      }
    }

    public static Stream UnwrapGZip(this Stream stream)
    {
      if (!StreamExtensions.CheckGZipHeader(stream))
        return stream;
      else
        return (Stream) new GZipStream(stream, CompressionMode.Decompress, false);
    }

    public static Stream WrapGZip(this Stream stream, bool buffered = true)
    {
      GZipStream gzipStream = new GZipStream(stream, CompressionMode.Compress, false);
      if (!buffered)
        return (Stream) gzipStream;
      else
        return (Stream) new BufferedStream((Stream) gzipStream, 32768);
    }

    public static int Read7BitEncodedInt(this Stream stream)
    {
      byte[] buffer = StreamExtensions.Buffer;
      int num1 = 0;
      int num2 = 0;
      while (num2 != 35)
      {
        if (stream.Read(buffer, 0, 1) == 0)
          throw new EndOfStreamException();
        byte num3 = buffer[0];
        num1 |= ((int) num3 & (int) sbyte.MaxValue) << num2;
        num2 += 7;
        if (((int) num3 & 128) == 0)
          return num1;
      }
      throw new FormatException("Bad string length. 7bit Int32 format");
    }

    public static void Write7BitEncodedInt(this Stream stream, int value)
    {
      byte[] buffer = StreamExtensions.Buffer;
      int count1 = 0;
      uint num1 = (uint) value;
      while (num1 >= 128U)
      {
        buffer[count1++] = (byte) (num1 | 128U);
        num1 >>= 7;
        if (count1 == buffer.Length)
        {
          stream.Write(buffer, 0, count1);
          count1 = 0;
        }
      }
      byte[] numArray = buffer;
      int index = count1;
      int num2 = 1;
      int count2 = index + num2;
      int num3 = (int) (byte) num1;
      numArray[index] = (byte) num3;
      stream.Write(buffer, 0, count2);
    }

    public static byte ReadByteNoAlloc(this Stream stream)
    {
      byte[] buffer = StreamExtensions.Buffer;
      if (stream.Read(buffer, 0, 1) == 0)
        throw new EndOfStreamException();
      else
        return buffer[0];
    }

    public static unsafe void WriteNoAlloc(this Stream stream, byte* bytes, int offset, int count)
    {
      byte[] buffer = StreamExtensions.Buffer;
      int count1 = 0;
      int num1 = offset;
      int num2 = offset + count;
      while (num1 != num2)
      {
        buffer[count1++] = bytes[num1++];
        if (count1 == buffer.Length)
        {
          stream.Write(buffer, 0, count1);
          count1 = 0;
        }
      }
      if (count1 == 0)
        return;
      stream.Write(buffer, 0, count1);
    }

    public static unsafe void ReadNoAlloc(this Stream stream, byte* bytes, int offset, int count)
    {
      byte[] buffer = StreamExtensions.Buffer;
      int num1 = offset;
      int num2 = offset + count;
      while (num1 != num2)
      {
        int count1 = Math.Min(count, buffer.Length);
        stream.Read(buffer, 0, count1);
        count -= count1;
        for (int index = 0; index < count1; ++index)
          bytes[num1++] = buffer[index];
      }
    }

    public static void WriteNoAlloc(this Stream stream, string text, Encoding encoding = null)
    {
      encoding = encoding ?? Encoding.UTF8;
      int byteCount = encoding.GetByteCount(text);
      StreamExtensions.Write7BitEncodedInt(stream, byteCount);
      byte[] numArray = StreamExtensions.Buffer;
      if (byteCount > numArray.Length)
        numArray = new byte[byteCount];
      int bytes = encoding.GetBytes(text, 0, text.Length, numArray, 0);
      stream.Write(numArray, 0, bytes);
    }

    public static string ReadString(this Stream stream, Encoding encoding = null)
    {
      encoding = encoding ?? Encoding.UTF8;
      int count = StreamExtensions.Read7BitEncodedInt(stream);
      byte[] numArray = StreamExtensions.Buffer;
      if (count > numArray.Length)
        numArray = new byte[count];
      stream.Read(numArray, 0, count);
      return encoding.GetString(numArray, 0, count);
    }

    public static void WriteNoAlloc(this Stream stream, byte value)
    {
      byte[] buffer = StreamExtensions.Buffer;
      buffer[0] = value;
      stream.Write(buffer, 0, 1);
    }

    public static unsafe void WriteNoAlloc(this Stream stream, short v)
    {
      StreamExtensions.WriteNoAlloc(stream, (byte*) &v, 0, 2);
    }

    public static unsafe void WriteNoAlloc(this Stream stream, int v)
    {
      StreamExtensions.WriteNoAlloc(stream, (byte*) &v, 0, 4);
    }

    public static unsafe void WriteNoAlloc(this Stream stream, long v)
    {
      StreamExtensions.WriteNoAlloc(stream, (byte*) &v, 0, 8);
    }

    public static unsafe void WriteNoAlloc(this Stream stream, ushort v)
    {
      StreamExtensions.WriteNoAlloc(stream, (byte*) &v, 0, 2);
    }

    public static unsafe void WriteNoAlloc(this Stream stream, uint v)
    {
      StreamExtensions.WriteNoAlloc(stream, (byte*) &v, 0, 4);
    }

    public static unsafe void WriteNoAlloc(this Stream stream, ulong v)
    {
      StreamExtensions.WriteNoAlloc(stream, (byte*) &v, 0, 8);
    }

    public static unsafe void WriteNoAlloc(this Stream stream, float v)
    {
      StreamExtensions.WriteNoAlloc(stream, (byte*) &v, 0, 4);
    }

    public static unsafe void WriteNoAlloc(this Stream stream, double v)
    {
      StreamExtensions.WriteNoAlloc(stream, (byte*) &v, 0, 8);
    }

    public static unsafe void WriteNoAlloc(this Stream stream, Decimal v)
    {
      StreamExtensions.WriteNoAlloc(stream, (byte*) &v, 0, 16);
    }

    public static unsafe short ReadInt16(this Stream stream)
    {
      short num;
      StreamExtensions.ReadNoAlloc(stream, (byte*) &num, 0, 2);
      return num;
    }

    public static unsafe int ReadInt32(this Stream stream)
    {
      int num;
      StreamExtensions.ReadNoAlloc(stream, (byte*) &num, 0, 4);
      return num;
    }

    public static unsafe long ReadInt64(this Stream stream)
    {
      long num;
      StreamExtensions.ReadNoAlloc(stream, (byte*) &num, 0, 8);
      return num;
    }

    public static unsafe ushort ReadUInt16(this Stream stream)
    {
      ushort num;
      StreamExtensions.ReadNoAlloc(stream, (byte*) &num, 0, 2);
      return num;
    }

    public static unsafe uint ReadUInt32(this Stream stream)
    {
      uint num;
      StreamExtensions.ReadNoAlloc(stream, (byte*) &num, 0, 4);
      return num;
    }

    public static unsafe ulong ReadUInt64(this Stream stream)
    {
      ulong num;
      StreamExtensions.ReadNoAlloc(stream, (byte*) &num, 0, 8);
      return num;
    }

    public static unsafe float ReadFloat(this Stream stream)
    {
      float num;
      StreamExtensions.ReadNoAlloc(stream, (byte*) &num, 0, 4);
      return num;
    }

    public static unsafe double ReadDouble(this Stream stream)
    {
      double num;
      StreamExtensions.ReadNoAlloc(stream, (byte*) &num, 0, 8);
      return num;
    }

    public static unsafe Decimal ReadDecimal(this Stream stream)
    {
      Decimal num;
      StreamExtensions.ReadNoAlloc(stream, (byte*) &num, 0, 16);
      return num;
    }

    public static void SkipBytes(this Stream stream, int byteCount)
    {
      byte[] buffer = StreamExtensions.Buffer;
      while (byteCount > 0)
      {
        int count = byteCount > buffer.Length ? buffer.Length : byteCount;
        stream.Read(buffer, 0, count);
        byteCount -= count;
      }
    }
  }
}
