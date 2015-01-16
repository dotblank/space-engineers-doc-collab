// Decompiled with JetBrains decompiler
// Type: VRage.Compression.MyStreamWrapper
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;

namespace VRage.Compression
{
    public class MyStreamWrapper : Stream
    {
        private readonly IDisposable m_obj;
        private readonly Stream m_innerStream;

        public override bool CanRead
        {
            get { return this.m_innerStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.m_innerStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return this.m_innerStream.CanWrite; }
        }

        public override long Length
        {
            get { return this.m_innerStream.Length; }
        }

        public override long Position
        {
            get { return this.m_innerStream.Position; }
            set { this.m_innerStream.Position = value; }
        }

        public MyStreamWrapper(Stream innerStream, IDisposable objectToClose)
        {
            this.m_innerStream = innerStream;
            this.m_obj = objectToClose;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.m_obj != null)
                this.m_obj.Dispose();
            base.Dispose(disposing);
        }

        public override void Flush()
        {
            this.m_innerStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.m_innerStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.m_innerStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.m_innerStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.m_innerStream.Write(buffer, offset, count);
        }
    }
}