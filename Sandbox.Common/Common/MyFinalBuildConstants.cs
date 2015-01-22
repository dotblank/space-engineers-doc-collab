﻿// Decompiled with JetBrains decompiler
// Type: Sandbox.Common.MyFinalBuildConstants
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System.Text;
using SysUtils.Utils;

namespace Sandbox.Common
{
    public class MyFinalBuildConstants
    {
        public static bool STEAM_DEMO = false;

        public static readonly StringBuilder APP_VERSION_STRING =
            new StringBuilder(MyBuildNumbers.ConvertBuildNumberFromIntToString(1066011));

        public const int APP_VERSION = 1066011;
        public const bool IS_CLOUD_GAMING = false;
        public const bool IS_OFFICIAL = true;
        public const bool IS_DEBUG = false;
        public const int IP_ADDRESS_ANY = 0;
        public const short DEDICATED_SERVER_PORT = (short) 27015;
        public const short DEDICATED_STEAM_AUTH_PORT = (short) 8766;
    }
}