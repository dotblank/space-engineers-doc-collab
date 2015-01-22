// Decompiled with JetBrains decompiler
// Type: VRage.MyCompressionFileSave
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;
using System.IO.Compression;

namespace VRage
{
  public class MyCompressionFileSave : IMyCompressionSave, IDisposable
  {
    private int m_uncompressedSize;
    private FileStream m_output;
    private GZipStream m_gz;
    private BufferedStream m_buffer;

    public MyCompressionFileSave(string targetFile)
    {
      this.m_output = new FileStream(targetFile, FileMode.Create, FileAccess.Write);
      for (int index = 0; index < 4; ++index)
        this.m_output.WriteByte((byte) 0);
      this.m_gz = new GZipStream((Stream) this.m_output, CompressionMode.Compress, true);
      this.m_buffer = new BufferedStream((Stream) this.m_gz, 16384);
    }

    public void Dispose()
    {
      if (this.m_output == null)
        return;
      try
      {
        this.m_buffer.Close();
      }
      finally
      {
        this.m_buffer = (BufferedStream) null;
      }
      try
      {
        this.m_gz.Close();
      }
      finally
      {
        this.m_gz = (GZipStream) null;
      }
      this.m_output.Position = 0L;
      MyCompressionFileSave.WriteUncompressedSize(this.m_output, this.m_uncompressedSize);
      try
      {
        this.m_output.Close();
      }
      finally
      {
        this.m_output = (FileStream) null;
      }
    }

    public void Add(byte[] value)
    {
      this.Add(value, value.Length);
    }

    public void Add(byte[] value, int count)
    {
      this.m_buffer.Write(value, 0, count);
      this.m_uncompressedSize += count;
    }

    public void Add(float value)
    {
      this.Add(BitConverter.GetBytes(value));
    }

    public void Add(int value)
    {
      this.Add(BitConverter.GetBytes(value));
    }

    public void Add(byte value)
    {
      this.m_buffer.WriteByte(value);
      ++this.m_uncompressedSize;
    }

    private static unsafe void WriteUncompressedSize(FileStream output, int uncompressedSize)
    {
      byte* numPtr = (byte*) &uncompressedSize;
      for (int index = 0; index < 4; ++index)
        output.WriteByte(numPtr[index]);
    }
  }
}
