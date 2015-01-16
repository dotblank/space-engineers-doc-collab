// Decompiled with JetBrains decompiler
// Type: VRage.MyCompression
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;
using System.IO.Compression;

namespace VRage
{
    public static class MyCompression
    {
        private static byte[] m_buffer = new byte[16384];

        public static byte[] Compress(byte[] buffer)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream((Stream) memoryStream, CompressionMode.Compress, true))
                {
                    gzipStream.Write(buffer, 0, buffer.Length);
                    gzipStream.Close();
                    memoryStream.Position = 0L;
                    byte[] buffer1 = new byte[memoryStream.Length + 4L];
                    memoryStream.Read(buffer1, 4, (int) memoryStream.Length);
                    Buffer.BlockCopy((Array) BitConverter.GetBytes(buffer.Length), 0, (Array) buffer1, 0, 4);
                    return buffer1;
                }
            }
        }

        public static void CompressFile(string fileName)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Buffer.BlockCopy((Array) BitConverter.GetBytes(new FileInfo(fileName).Length), 0,
                    (Array) MyCompression.m_buffer, 0, 4);
                memoryStream.Write(MyCompression.m_buffer, 0, 4);
                using (GZipStream gzipStream = new GZipStream((Stream) memoryStream, CompressionMode.Compress, true))
                {
                    using (FileStream fileStream = File.OpenRead(fileName))
                    {
                        for (int count = fileStream.Read(MyCompression.m_buffer, 0, MyCompression.m_buffer.Length);
                            count > 0;
                            count = fileStream.Read(MyCompression.m_buffer, 0, MyCompression.m_buffer.Length))
                            gzipStream.Write(MyCompression.m_buffer, 0, count);
                    }
                    gzipStream.Close();
                    memoryStream.Position = 0L;
                    using (FileStream fileStream = File.Create(fileName))
                    {
                        for (int count = memoryStream.Read(MyCompression.m_buffer, 0, MyCompression.m_buffer.Length);
                            count > 0;
                            count = memoryStream.Read(MyCompression.m_buffer, 0, MyCompression.m_buffer.Length))
                        {
                            fileStream.Write(MyCompression.m_buffer, 0, count);
                            ((Stream) fileStream).Flush();
                        }
                    }
                }
            }
        }

        public static byte[] Decompress(byte[] gzBuffer)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int length = BitConverter.ToInt32(gzBuffer, 0);
                memoryStream.Write(gzBuffer, 4, gzBuffer.Length - 4);
                memoryStream.Position = 0L;
                byte[] buffer = new byte[length];
                using (GZipStream gzipStream = new GZipStream((Stream) memoryStream, CompressionMode.Decompress))
                {
                    gzipStream.Read(buffer, 0, buffer.Length);
                    return buffer;
                }
            }
        }

        public static void DecompressFile(string fileName)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (FileStream fileStream = File.OpenRead(fileName))
                {
                    fileStream.Read(MyCompression.m_buffer, 0, 4);
                    using (GZipStream gzipStream = new GZipStream((Stream) fileStream, CompressionMode.Decompress))
                    {
                        for (int count = gzipStream.Read(MyCompression.m_buffer, 0, MyCompression.m_buffer.Length);
                            count > 0;
                            count = gzipStream.Read(MyCompression.m_buffer, 0, MyCompression.m_buffer.Length))
                            memoryStream.Write(MyCompression.m_buffer, 0, count);
                    }
                }
                memoryStream.Position = 0L;
                using (FileStream fileStream = File.Create(fileName))
                {
                    for (int count = memoryStream.Read(MyCompression.m_buffer, 0, MyCompression.m_buffer.Length);
                        count > 0;
                        count = memoryStream.Read(MyCompression.m_buffer, 0, MyCompression.m_buffer.Length))
                    {
                        fileStream.Write(MyCompression.m_buffer, 0, count);
                        ((Stream) fileStream).Flush();
                    }
                }
            }
        }
    }
}