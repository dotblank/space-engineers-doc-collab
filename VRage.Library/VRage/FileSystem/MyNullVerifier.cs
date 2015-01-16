// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.MyNullVerifier
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;

namespace VRage.FileSystem
{
    public class MyNullVerifier : IFileVerifier
    {
        public event Action<IFileVerifier, string> ChecksumNotFound;

        public event Action<string, string> ChecksumFailed;

        public Stream Verify(string filename, Stream stream)
        {
            return stream;
        }
    }
}