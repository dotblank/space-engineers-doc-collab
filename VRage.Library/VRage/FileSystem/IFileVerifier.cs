// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.IFileVerifier
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
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