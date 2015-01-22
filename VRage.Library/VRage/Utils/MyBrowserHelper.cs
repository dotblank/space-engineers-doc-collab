// Decompiled with JetBrains decompiler
// Type: VRage.Utils.MyBrowserHelper
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
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
