// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.MyFileHelper
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.IO;

namespace VRage.FileSystem
{
  public class MyFileHelper
  {
    public static bool CanWrite(string path)
    {
      if (!File.Exists(path))
        return true;
      try
      {
        using (File.Open(path, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
          return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
