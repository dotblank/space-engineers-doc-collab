// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.MyFileHelper
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
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