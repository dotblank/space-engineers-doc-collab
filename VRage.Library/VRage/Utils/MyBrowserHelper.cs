// Decompiled with JetBrains decompiler
// Type: VRage.Utils.MyBrowserHelper
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace VRage.Utils
{
    public static class MyBrowserHelper
    {
        public const string IE_PROCESS = "IExplore.exe";

        public static bool OpenInternetBrowser(string url)
        {
            try
            {
                try
                {
                    Process.Start(url);
                }
                catch (Win32Exception ex)
                {
                    Process.Start(new ProcessStartInfo("IExplore.exe", url));
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}