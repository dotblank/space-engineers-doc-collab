// Decompiled with JetBrains decompiler
// Type: VRage.Cryptography.MySHA256
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
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
