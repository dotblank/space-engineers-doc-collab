// Decompiled with JetBrains decompiler
// Type: System.SteamHelpers
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace System
{
    public static class SteamHelpers
    {
        public static bool IsSteamPath(string path)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                return directoryInfo.Parent.Name.Equals("Common", StringComparison.InvariantCultureIgnoreCase) &&
                       directoryInfo.Parent.Parent.Name.Equals("SteamApps", StringComparison.InvariantCultureIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsAppManifestPresent(string path, uint appId)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                return SteamHelpers.IsSteamPath(path) &&
                       Enumerable.Contains<string>(
                           (IEnumerable<string>) Directory.GetFiles(directoryInfo.Parent.Parent.FullName),
                           "AppManifest_" + (object) appId + ".acf",
                           (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}