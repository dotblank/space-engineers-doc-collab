// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.MyClassicFileProvider
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace VRage.FileSystem
{
    public class MyClassicFileProvider : IFileProvider
    {
        public Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            if (!File.Exists(path))
                return (Stream) null;
            try
            {
                return (Stream) File.Open(path, mode, access, share);
            }
            catch (Exception ex)
            {
                return (Stream) null;
            }
        }

        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public IEnumerable<string> GetFiles(string path, string filter, SearchOption searchOption)
        {
            if (!Directory.Exists(path))
                return (IEnumerable<string>) null;
            else
                return (IEnumerable<string>) Directory.GetFiles(path, filter, (System.IO.SearchOption) searchOption);
        }

        public bool FileExists(string path)
        {
            return File.Exists(path);
        }
    }
}