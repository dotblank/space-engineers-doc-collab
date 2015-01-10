// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyFinalBuildConstants
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AEA4A40D-6023-45C7-A56E-9FAD0E8F073F
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Text;
using SysUtils.Utils;

namespace Sandbox.Common
{
  public class MyFinalBuildConstants
  {
    public static bool STEAM_DEMO = false;
    public static readonly StringBuilder APP_VERSION_STRING = new StringBuilder(MyBuildNumbers.ConvertBuildNumberFromIntToString(1064007));
    public const int APP_VERSION = 1064007;
    public const bool IS_CLOUD_GAMING = false;
    public const bool IS_OFFICIAL = true;
    public const bool IS_DEBUG = false;
    public const int IP_ADDRESS_ANY = 0;
    public const short DEDICATED_SERVER_PORT = (short) 27015;
    public const short DEDICATED_STEAM_AUTH_PORT = (short) 8766;
  }
}
