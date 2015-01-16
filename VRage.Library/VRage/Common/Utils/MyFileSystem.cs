// Decompiled with JetBrains decompiler
// Type: VRage.Common.Utils.MyFileSystem
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using VRage.FileSystem;

namespace VRage.Common.Utils
{
    public static class MyFileSystem
    {
        public static readonly Assembly MainAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
        public static readonly string MainAssemblyName = MyFileSystem.MainAssembly.GetName().Name;
        public static readonly string ExePath = new FileInfo(MyFileSystem.MainAssembly.Location).DirectoryName;
        public static IFileVerifier FileVerifier = (IFileVerifier) new MyNullVerifier();

        private static MyFileProviderAggregator m_fileProvider = new MyFileProviderAggregator(new IFileProvider[2]
        {
            (IFileProvider) new MyClassicFileProvider(),
            (IFileProvider) new MyZipFileProvider()
        });

        private static string m_contentPath;
        private static string m_modsPath;
        private static string m_userDataPath;
        private static string m_savesPath;

        public static string ContentPath
        {
            get
            {
                MyFileSystem.CheckInitialized();
                return MyFileSystem.m_contentPath;
            }
        }

        public static string ModsPath
        {
            get
            {
                MyFileSystem.CheckInitialized();
                return MyFileSystem.m_modsPath;
            }
        }

        public static string UserDataPath
        {
            get
            {
                MyFileSystem.CheckInitialized();
                return MyFileSystem.m_userDataPath;
            }
        }

        public static string SavesPath
        {
            get
            {
                MyFileSystem.CheckUserSpecificInitialized();
                return MyFileSystem.m_savesPath;
            }
        }

        private static void CheckInitialized()
        {
            if (MyFileSystem.m_contentPath == null)
                throw new InvalidOperationException("Paths are not initialized, call 'Init'");
        }

        private static void CheckUserSpecificInitialized()
        {
            if (MyFileSystem.m_userDataPath == null)
                throw new InvalidOperationException("User specific path not initialized, call 'InitUserSpecific'");
        }

        public static void Init(string contentPath, string userData, string modDirName = "Mods")
        {
            if (MyFileSystem.m_contentPath != null)
                throw new InvalidOperationException("Paths already initialized");
            MyFileSystem.m_contentPath = contentPath;
            MyFileSystem.m_userDataPath = userData;
            MyFileSystem.m_modsPath = Path.Combine(MyFileSystem.m_userDataPath, modDirName);
            Directory.CreateDirectory(MyFileSystem.m_modsPath);
        }

        public static void InitUserSpecific(string userSpecificName, string saveDirName = "Saves")
        {
            MyFileSystem.CheckInitialized();
            if (MyFileSystem.m_savesPath != null)
                throw new InvalidOperationException("User specific paths already initialized");
            MyFileSystem.m_savesPath = Path.Combine(MyFileSystem.m_userDataPath, saveDirName,
                userSpecificName ?? string.Empty);
            Directory.CreateDirectory(MyFileSystem.m_savesPath);
        }

        public static void Reset()
        {
            // lazy fix for decomplier shenanigans
        }

        public static Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            bool flag = mode == FileMode.Open && access != FileAccess.Write;
            Stream stream = MyFileSystem.m_fileProvider.Open(path, mode, access, share);
            if (!flag || stream == null)
                return stream;
            else
                return MyFileSystem.FileVerifier.Verify(path, stream);
        }

        public static Stream OpenRead(string path)
        {
            return MyFileSystem.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        public static Stream OpenRead(string path, string subpath)
        {
            return MyFileSystem.OpenRead(Path.Combine(path, subpath));
        }

        public static Stream OpenWrite(string path, FileMode mode = FileMode.Create)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            return (Stream) File.Open(path, mode, FileAccess.Write, FileShare.Read);
        }

        public static Stream OpenWrite(string path, string subpath, FileMode mode = FileMode.Create)
        {
            return MyFileSystem.OpenWrite(Path.Combine(path, subpath), mode);
        }

        public static bool CheckFileWriteAccess(string path)
        {
            try
            {
                using (MyFileSystem.OpenWrite(path, FileMode.Append))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool FileExists(string path)
        {
            return MyFileSystem.m_fileProvider.FileExists(path);
        }

        public static bool DirectoryExists(string path)
        {
            return MyFileSystem.m_fileProvider.DirectoryExists(path);
        }

        public static IEnumerable<string> GetFiles(string path, string filter = "*",
            VRage.FileSystem.SearchOption searchOption = VRage.FileSystem.SearchOption.AllDirectories)
        {
            return MyFileSystem.m_fileProvider.GetFiles(path, filter, searchOption);
        }
    }
}