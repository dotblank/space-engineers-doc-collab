// Decompiled with JetBrains decompiler
// Type: VRage.FileSystem.MyFileHelper
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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