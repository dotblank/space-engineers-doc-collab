// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.IFileVerifier
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;

namespace VRage.FileSystem
{
    public interface IFileVerifier
    {
        event Action<IFileVerifier, string> ChecksumNotFound;

        event Action<string, string> ChecksumFailed;

        Stream Verify(string filename, Stream stream);
    }
}