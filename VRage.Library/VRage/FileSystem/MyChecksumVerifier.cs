// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.MyChecksumVerifier
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.IO;
using VRage.Common.Utils;

namespace VRage.FileSystem
{
    public class MyChecksumVerifier : IFileVerifier
    {
        public readonly string BaseChecksumDir;
        public readonly byte[] PublicKey;
        private Dictionary<string, string> m_checksums;

        public event Action<IFileVerifier, string> ChecksumNotFound;

        public event Action<string, string> ChecksumFailed;

        public MyChecksumVerifier(MyChecksums checksums, string baseChecksumDir)
        {
            this.PublicKey = checksums.PublicKeyAsArray;
            this.BaseChecksumDir = baseChecksumDir;
            this.m_checksums = checksums.Items.Dictionary;
        }

        public Stream Verify(string filename, Stream stream)
        {
            Action<string, string> failHandler = this.ChecksumFailed;
            Action<IFileVerifier, string> action = this.ChecksumNotFound;
            if ((failHandler != null || action != null) &&
                filename.StartsWith(this.BaseChecksumDir, StringComparison.InvariantCultureIgnoreCase))
            {
                string s;
                if (this.m_checksums.TryGetValue(filename.Substring(this.BaseChecksumDir.Length + 1), out s))
                {
                    if (failHandler != null)
                        return
                            (Stream)
                                new MyCheckSumStream(stream, filename, Convert.FromBase64String(s), this.PublicKey,
                                    failHandler);
                }
                else if (action != null)
                    action((IFileVerifier) this, filename);
            }
            return stream;
        }
    }
}