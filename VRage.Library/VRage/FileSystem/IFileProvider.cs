// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.IFileProvider
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;
using System.IO;

namespace VRage.FileSystem
{
    public interface IFileProvider
    {
        Stream Open(string path, FileMode mode, FileAccess access, FileShare share);

        bool DirectoryExists(string path);

        IEnumerable<string> GetFiles(string path, string filter, SearchOption searchOption);

        bool FileExists(string path);
    }
}