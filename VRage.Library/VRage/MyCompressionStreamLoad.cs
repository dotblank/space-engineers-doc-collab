// Decompiled with JetBrains decompiler
// Type: VRage.MyCompressionStreamLoad
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;
using System.IO.Compression;

namespace VRage
{
    public class MyCompressionStreamLoad : IMyCompressionLoad
    {
        private static byte[] m_intBytesBuffer = new byte[4];
        private MemoryStream m_input;
        private GZipStream m_gz;
        private BufferedStream m_buffer;

        public MyCompressionStreamLoad(byte[] compressedData)
        {
            this.m_input = new MemoryStream(compressedData);
            this.m_input.Read(MyCompressionStreamLoad.m_intBytesBuffer, 0, 4);
            this.m_gz = new GZipStream((Stream) this.m_input, CompressionMode.Decompress);
            this.m_buffer = new BufferedStream((Stream) this.m_gz, 16384);
        }

        public int GetInt32()
        {
            this.m_buffer.Read(MyCompressionStreamLoad.m_intBytesBuffer, 0, 4);
            return BitConverter.ToInt32(MyCompressionStreamLoad.m_intBytesBuffer, 0);
        }

        public byte GetByte()
        {
            return (byte) this.m_buffer.ReadByte();
        }

        public int GetBytes(int bytes, byte[] output)
        {
            return this.m_buffer.Read(output, 0, bytes);
        }

        public bool EndOfFile()
        {
            return this.m_input.Position == this.m_input.Length;
        }
    }
}