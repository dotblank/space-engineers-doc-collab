// Decompiled with JetBrains decompiler
// Type: VRage.DirectoryExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.IO;

namespace VRage
{
  public static class DirectoryExtensions
  {
    public static void CopyAll(string source, string target)
    {
      DirectoryExtensions.EnsureDirectoryExists(target);
      foreach (FileInfo fileInfo in new DirectoryInfo(source).GetFiles())
        fileInfo.CopyTo(Path.Combine(target, fileInfo.Name), true);
      foreach (DirectoryInfo directoryInfo in new DirectoryInfo(source).GetDirectories())
      {
        DirectoryInfo directory = Directory.CreateDirectory(Path.Combine(target, directoryInfo.Name));
        DirectoryExtensions.CopyAll(directoryInfo.FullName, directory.FullName);
      }
    }

    public static void EnsureDirectoryExists(string path)
    {
      DirectoryInfo directoryInfo = new DirectoryInfo(path);
      if (directoryInfo.Parent != null)
        DirectoryExtensions.EnsureDirectoryExists(directoryInfo.Parent.FullName);
      if (directoryInfo.Exists)
        return;
      directoryInfo.Create();
    }
  }
}
