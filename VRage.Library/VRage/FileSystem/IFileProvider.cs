// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.IFileProvider
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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