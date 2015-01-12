// Decompiled with JetBrains decompiler
// Type: VRage.Compression.MyZipArchive
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VRage.Compression
{
    public class MyZipArchive : IDisposable
    {
        private Dictionary<string, string> m_mixedCaseHelper =
            new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);

        private object m_zip;

        public string ZipPath { get; private set; }

        public IEnumerable<string> FileNames
        {
            get
            {
                return
                    (IEnumerable<string>)
                        Enumerable.OrderBy<string, string>(
                            Enumerable.Select<MyZipFileInfo, string>((IEnumerable<MyZipFileInfo>) this.Files,
                                (Func<MyZipFileInfo, string>) (p => p.Name)), (Func<string, string>) (p => p));
            }
        }

        public MyZipArchive.Enumerator Files
        {
            get
            {
                return
                    new MyZipArchive.Enumerator(
                        ((IEnumerable) MyZipArchiveReflection.GetFiles(this.m_zip)).GetEnumerator());
            }
        }

        private MyZipArchive(object zipObject, string path = null)
        {
            this.m_zip = zipObject;
            this.ZipPath = path;
            foreach (MyZipFileInfo myZipFileInfo in this.Files)
                this.m_mixedCaseHelper[myZipFileInfo.Name.Replace('/', '\\')] = myZipFileInfo.Name;
        }

        private static void FixName(ref string name)
        {
            name = name.Replace('/', '\\');
        }

        public static MyZipArchive OpenOnFile(string path, FileMode mode = FileMode.Open,
            FileAccess access = FileAccess.Read, FileShare share = FileShare.Read, bool streaming = false)
        {
            return new MyZipArchive(MyZipArchiveReflection.OpenOnFile(path, mode, access, share, streaming), path);
        }

        public static MyZipArchive OpenOnStream(Stream stream, FileMode mode = FileMode.OpenOrCreate,
            FileAccess access = FileAccess.ReadWrite, bool streaming = false)
        {
            return new MyZipArchive(MyZipArchiveReflection.OpenOnStream(stream, mode, access, streaming), (string) null);
        }

        public MyZipFileInfo AddFile(string path,
            CompressionMethodEnum compressionMethod = CompressionMethodEnum.Deflated,
            DeflateOptionEnum deflateOption = DeflateOptionEnum.Normal)
        {
            return
                new MyZipFileInfo(MyZipArchiveReflection.AddFile(this.m_zip, path, (ushort) compressionMethod,
                    (byte) deflateOption));
        }

        public void DeleteFile(string name)
        {
            MyZipArchive.FixName(ref name);
            MyZipArchiveReflection.DeleteFile(this.m_zip, name);
        }

        public MyZipFileInfo GetFile(string name)
        {
            MyZipArchive.FixName(ref name);
            return new MyZipFileInfo(MyZipArchiveReflection.GetFile(this.m_zip, this.m_mixedCaseHelper[name]));
        }

        public bool FileExists(string name)
        {
            MyZipArchive.FixName(ref name);
            return this.m_mixedCaseHelper.ContainsKey(name);
        }

        public bool DirectoryExists(string name)
        {
            MyZipArchive.FixName(ref name);
            foreach (string str in this.m_mixedCaseHelper.Keys)
            {
                if (str.StartsWith(name, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public void Dispose()
        {
            ((IDisposable) this.m_zip).Dispose();
        }

        public static void CreateFromDirectory(string sourceDirectoryName, string destinationArchiveFileName,
            DeflateOptionEnum compressionLevel, bool includeBaseDirectory, string[] ignoredExtensions = null)
        {
            if (File.Exists(destinationArchiveFileName))
                File.Delete(destinationArchiveFileName);
            int startIndex = sourceDirectoryName.Length + 1;
            using (
                MyZipArchive myZipArchive = MyZipArchive.OpenOnFile(destinationArchiveFileName, FileMode.Create,
                    FileAccess.ReadWrite, FileShare.None, false))
            {
                foreach (string path in Directory.GetFiles(sourceDirectoryName, "*", SearchOption.AllDirectories))
                {
                    if (ignoredExtensions == null ||
                        !Enumerable.Contains<string>((IEnumerable<string>) ignoredExtensions,
                            Path.GetExtension(path).ToLower()))
                    {
                        using (FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            using (
                                Stream stream =
                                    myZipArchive.AddFile(path.Substring(startIndex), CompressionMethodEnum.Deflated,
                                        compressionLevel).GetStream(FileMode.Open, FileAccess.Write))
                                fileStream.CopyTo(stream, 4096);
                        }
                    }
                }
            }
        }

        public static void ExtractToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
        {
            if (!Directory.Exists(destinationDirectoryName))
                Directory.CreateDirectory(destinationDirectoryName);
            using (
                MyZipArchive myZipArchive = MyZipArchive.OpenOnFile(sourceArchiveFileName, FileMode.Open,
                    FileAccess.Read, FileShare.Read, false))
            {
                foreach (string str in myZipArchive.FileNames)
                {
                    using (Stream stream = myZipArchive.GetFile(str).GetStream(FileMode.Open, FileAccess.Read))
                    {
                        string path = Path.Combine(destinationDirectoryName, str);
                        string directoryName = Path.GetDirectoryName(path);
                        if (!Directory.Exists(directoryName))
                            Directory.CreateDirectory(directoryName);
                        using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write))
                            stream.CopyTo((Stream) fileStream, 4096);
                    }
                }
            }
        }

        public struct Enumerator : IEnumerator<MyZipFileInfo>, IDisposable, IEnumerator, IEnumerable<MyZipFileInfo>,
            IEnumerable
        {
            public IEnumerator m_enumerator;

            public MyZipFileInfo Current
            {
                get { return new MyZipFileInfo(this.m_enumerator.Current); }
            }

            object IEnumerator.Current
            {
                get { return (object) this.Current; }
            }

            public Enumerator(IEnumerator enumerator)
            {
                this.m_enumerator = enumerator;
            }

            public bool MoveNext()
            {
                return this.m_enumerator.MoveNext();
            }

            public void Reset()
            {
                this.m_enumerator.Reset();
            }

            void IDisposable.Dispose()
            {
            }

            public IEnumerator<MyZipFileInfo> GetEnumerator()
            {
                return (IEnumerator<MyZipFileInfo>) this;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator) this.GetEnumerator();
            }
        }
    }
}