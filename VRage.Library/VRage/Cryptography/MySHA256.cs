// Decompiled with JetBrains decompiler
// Type: VRage.Cryptography.MySHA256
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Security.Cryptography;

namespace VRage.Cryptography
{
    public static class MySHA256
    {
        private static bool m_supportsFips = true;

        private static SHA256 CreateInternal()
        {
            if (MySHA256.m_supportsFips)
                return (SHA256) new SHA256CryptoServiceProvider();
            else
                return (SHA256) new SHA256Managed();
        }

        public static SHA256 Create()
        {
            try
            {
                return MySHA256.CreateInternal();
            }
            catch
            {
                MySHA256.m_supportsFips = false;
                return MySHA256.CreateInternal();
            }
        }
    }
}