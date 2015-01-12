// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.MyFileProviderAggregator
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;
using System.IO;
using VRage.Collections;

namespace VRage.FileSystem
{
    public class MyFileProviderAggregator : IFileProvider
    {
        private HashSet<IFileProvider> m_providers = new HashSet<IFileProvider>();

        public HashSetReader<IFileProvider> Providers
        {
            get { return new HashSetReader<IFileProvider>(this.m_providers); }
        }

        public MyFileProviderAggregator(params IFileProvider[] providers)
        {
            foreach (IFileProvider provider in providers)
                this.AddProvider(provider);
        }

        public void AddProvider(IFileProvider provider)
        {
            this.m_providers.Add(provider);
        }

        public void RemoveProvider(IFileProvider provider)
        {
            this.m_providers.Remove(provider);
        }

        public Stream OpenRead(string path)
        {
            return this.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public Stream OpenWrite(string path, FileMode mode = FileMode.OpenOrCreate)
        {
            return this.Open(path, mode, FileAccess.Write, FileShare.Read);
        }

        public Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            foreach (IFileProvider fileProvider in this.m_providers)
            {
                try
                {
                    Stream stream = fileProvider.Open(path, mode, access, share);
                    if (stream != null)
                        return stream;
                }
                catch
                {
                }
            }
            return (Stream) null;
        }

        public bool DirectoryExists(string path)
        {
            foreach (IFileProvider fileProvider in this.m_providers)
            {
                try
                {
                    if (fileProvider.DirectoryExists(path))
                        return true;
                }
                catch
                {
                }
            }
            return false;
        }

        public IEnumerable<string> GetFiles(string path, string filter, SearchOption searchOption)
        {
            foreach (IFileProvider fileProvider in this.m_providers)
            {
                try
                {
                    IEnumerable<string> files = fileProvider.GetFiles(path, filter, searchOption);
                    if (files != null)
                        return files;
                }
                catch
                {
                }
            }
            return (IEnumerable<string>) null;
        }

        public bool FileExists(string path)
        {
            foreach (IFileProvider fileProvider in this.m_providers)
            {
                try
                {
                    if (fileProvider.FileExists(path))
                        return true;
                }
                catch
                {
                }
            }
            return false;
        }
    }
}