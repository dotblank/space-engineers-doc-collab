// Decompiled with JetBrains decompiler
// Type: VRage.MyCompressionStreamSave
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;
using System.IO.Compression;

namespace VRage
{
    public class MyCompressionStreamSave : IMyCompressionSave, IDisposable
    {
        private MemoryStream m_output;
        private GZipStream m_gz;
        private BufferedStream m_buffer;

        public MyCompressionStreamSave()
        {
            this.m_output = new MemoryStream();
            this.m_output.Write(BitConverter.GetBytes(0), 0, 4);
            this.m_gz = new GZipStream((Stream) this.m_output, CompressionMode.Compress);
            this.m_buffer = new BufferedStream((Stream) this.m_gz, 16384);
        }

        public byte[] Compress()
        {
            byte[] numArray = (byte[]) null;
            if (this.m_output != null)
            {
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
                try
                {
                    this.m_output.Close();
                }
                finally
                {
                    numArray = this.m_output.ToArray();
                    this.m_output = (MemoryStream) null;
                }
            }
            return numArray;
        }

        public void Dispose()
        {
            this.Compress();
        }

        public void Add(byte[] value)
        {
            this.m_buffer.Write(value, 0, value.Length);
        }

        public void Add(byte[] value, int count)
        {
            this.m_buffer.Write(value, 0, count);
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
        }
    }
}