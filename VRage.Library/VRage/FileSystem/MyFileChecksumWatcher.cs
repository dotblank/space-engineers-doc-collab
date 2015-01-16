// Decompiled with JetBrains decompiler
// Type: VRage.Filesystem.MyFileChecksumWatcher
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using VRage.Common.Utils;
using VRage.FileSystem;

namespace VRage.Filesystem
{
    internal class MyFileChecksumWatcher : IDisposable
    {
        public bool ChecksumFound { get; private set; }

        public bool ChecksumValid { get; private set; }

        public MyFileChecksumWatcher()
        {
            this.ChecksumFound = true;
            this.ChecksumValid = true;
            MyFileSystem.FileVerifier.ChecksumFailed += new Action<string, string>(this.FileVerifier_ChecksumFailed);
            MyFileSystem.FileVerifier.ChecksumNotFound +=
                new Action<IFileVerifier, string>(this.FileVerifier_ChecksumNotFound);
        }

        public void Reset()
        {
            this.ChecksumValid = true;
            this.ChecksumFound = true;
        }

        private void FileVerifier_ChecksumNotFound(IFileVerifier arg1, string arg2)
        {
            this.ChecksumFound = false;
            this.ChecksumValid = false;
        }

        private void FileVerifier_ChecksumFailed(string arg1, string arg2)
        {
            this.ChecksumFound = true;
            this.ChecksumValid = false;
        }

        void IDisposable.Dispose()
        {
            MyFileSystem.FileVerifier.ChecksumFailed -= new Action<string, string>(this.FileVerifier_ChecksumFailed);
            MyFileSystem.FileVerifier.ChecksumNotFound -=
                new Action<IFileVerifier, string>(this.FileVerifier_ChecksumNotFound);
        }
    }
}