// Decompiled with JetBrains decompiler
// Type: VRage.Common.Utils.MyCheckSumStream
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: F987C912-6032-4943-850E-69DEE0217B30
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;

namespace VRage.Common.Utils
{
  internal class MyCheckSumStream : Stream
  {
    private byte[] m_tmpArray = new byte[1];
    private MyRSA m_verifier;
    private Stream m_stream;
    private string m_filename;
    private byte[] m_signedHash;
    private byte[] m_publicKey;
    private Action<string, string> m_failHandler;
    private long m_lastPosition;

    public override bool CanRead
    {
      get
      {
        return this.m_stream.CanRead;
      }
    }

    public override bool CanSeek
    {
      get
      {
        return this.m_stream.CanSeek;
      }
    }

    public override bool CanWrite
    {
      get
      {
        return this.m_stream.CanWrite;
      }
    }

    public override long Length
    {
      get
      {
        return this.m_stream.Length;
      }
    }

    public override long Position
    {
      get
      {
        return this.m_stream.Position;
      }
      set
      {
        this.m_stream.Position = value;
      }
    }

    internal MyCheckSumStream(Stream stream, string filename, byte[] signedHash, byte[] publicKey, Action<string, string> failHandler)
    {
      this.m_stream = stream;
      this.m_verifier = new MyRSA();
      this.m_signedHash = signedHash;
      this.m_publicKey = publicKey;
      this.m_filename = filename;
      this.m_failHandler = failHandler;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.m_verifier.HashObject.TransformFinalBlock(new byte[0], 0, 0);
        if (!this.m_verifier.VerifyHash(this.m_verifier.HashObject.Hash, this.m_signedHash, this.m_publicKey))
          this.m_failHandler(this.m_filename, Convert.ToBase64String(this.m_verifier.HashObject.Hash));
        this.m_stream.Dispose();
      }
      base.Dispose(disposing);
    }

    public override int Read(byte[] array, int offset, int count)
    {
      int num1 = (int) (this.m_lastPosition - this.m_stream.Position);
      int num2 = this.m_stream.Read(array, offset, count);
      int num3 = offset + num1;
      if (num2 - num1 > 0 && num3 > 0)
        this.m_verifier.HashObject.TransformBlock(array, offset + num1, num2 - num1, (byte[]) null, 0);
      this.m_lastPosition = this.m_stream.Position;
      return num2;
    }

    public override void Flush()
    {
      this.m_stream.Flush();
    }

    public override int ReadByte()
    {
      if (this.Read(this.m_tmpArray, 0, 1) == 0)
        return -1;
      else
        return (int) this.m_tmpArray[0];
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      return this.m_stream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
      this.m_stream.SetLength(value);
    }

    public override void Write(byte[] array, int offset, int count)
    {
      this.m_stream.Write(array, offset, count);
    }

    public override void WriteByte(byte value)
    {
      this.m_stream.WriteByte(value);
    }
  }
}
