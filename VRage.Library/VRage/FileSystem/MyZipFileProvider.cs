// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.MyZipFileProvider
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using VRage.Compression;

namespace VRage.FileSystem
{
  public class MyZipFileProvider : IFileProvider
  {
    public readonly char[] Separators = new char[2]
    {
      Path.AltDirectorySeparatorChar,
      Path.DirectorySeparatorChar
    };

    public Stream Open(string path, FileMode mode, FileAccess access, FileShare share)
    {
      if (mode != FileMode.Open || access != FileAccess.Read)
        return (Stream) null;
      else
        return this.TryDoZipAction<Stream>(path, new Func<string, string, Stream>(this.TryOpen), (Stream) null);
    }

    private T TryDoZipAction<T>(string path, Func<string, string, T> action, T defaultValue)
    {
      for (int length = path.Length; length >= 0; length = path.LastIndexOfAny(this.Separators, length - 1))
      {
        string path1 = path.Substring(0, length);
        if (File.Exists(path1))
          return action(path1, path.Substring(Math.Min(path.Length, length + 1)));
      }
      return defaultValue;
    }

    private Stream TryOpen(string zipFile, string subpath)
    {
      MyZipArchive myZipArchive = MyZipArchive.OpenOnFile(zipFile, FileMode.Open, FileAccess.Read, FileShare.Read, false);
      try
      {
        return myZipArchive.FileExists(subpath) ? (Stream) new MyStreamWrapper(myZipArchive.GetFile(subpath).GetStream(FileMode.Open, FileAccess.Read), (IDisposable) myZipArchive) : (Stream) null;
      }
      catch
      {
        myZipArchive.Dispose();
        return (Stream) null;
      }
    }

    public bool DirectoryExists(string path)
    {
      return this.TryDoZipAction<bool>(path, new Func<string, string, bool>(this.DirectoryExistsInZip), false);
    }

    private bool DirectoryExistsInZip(string zipFile, string subpath)
    {
      MyZipArchive myZipArchive = MyZipArchive.OpenOnFile(zipFile, FileMode.Open, FileAccess.Read, FileShare.Read, false);
      try
      {
        return subpath == string.Empty || myZipArchive.DirectoryExists(subpath + "/");
      }
      finally
      {
        myZipArchive.Dispose();
      }
    }

    private MyZipArchive TryGetZipArchive(string zipFile, string subpath)
    {
        return null;
    }

    private string TryGetSubpath(string zipFile, string subpath)
    {
      return subpath;
    }

    public IEnumerable<string> GetFiles(string path, string filter, SearchOption searchOption)
    {
      MyZipArchive zipFile = this.TryDoZipAction<MyZipArchive>(path, new Func<string, string, MyZipArchive>(this.TryGetZipArchive), (MyZipArchive) null);
      string subpath = "";
      if (searchOption == SearchOption.TopDirectoryOnly)
        subpath = this.TryDoZipAction<string>(path, new Func<string, string, string>(this.TryGetSubpath), (string) null);
      if (zipFile != null)
      {
        string pattern = Regex.Escape(filter).Replace("\\*", ".*").Replace("\\?", ".");
        foreach (string str in zipFile.FileNames)
        {
          if ((searchOption != SearchOption.TopDirectoryOnly || Enumerable.Count<char>((IEnumerable<char>) str, (Func<char, bool>) (x => (int) x == 92)) == Enumerable.Count<char>((IEnumerable<char>) subpath, (Func<char, bool>) (x => (int) x == 92)) + 1) && Regex.IsMatch(str, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
            yield return Path.Combine(zipFile.ZipPath, str);
        }
        zipFile.Dispose();
      }
    }

    public bool FileExists(string path)
    {
      return this.TryDoZipAction<bool>(path, new Func<string, string, bool>(this.FileExistsInZip), false);
    }

    private bool FileExistsInZip(string zipFile, string subpath)
    {
      MyZipArchive myZipArchive = MyZipArchive.OpenOnFile(zipFile, FileMode.Open, FileAccess.Read, FileShare.Read, false);
      try
      {
        return myZipArchive.FileExists(subpath);
      }
      finally
      {
        myZipArchive.Dispose();
      }
    }

    public static bool IsZipFile(string path)
    {
      return !Directory.Exists(path);
    }
  }
}
