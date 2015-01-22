// Decompiled with JetBrains decompiler
// Type: System.Net.IPAddressExtensions
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;

namespace System.Net
{
  public static class IPAddressExtensions
  {
    public static uint ToIPv4NetworkOrder(this IPAddress ip)
    {
      return (uint) IPAddress.HostToNetworkOrder((int) (uint) ip.Address);
    }

    public static IPAddress FromIPv4NetworkOrder(uint ip)
    {
      return new IPAddress((long) (uint) IPAddress.NetworkToHostOrder((int) ip));
    }

    public static IPAddress ParseOrAny(string ip)
    {
      IPAddress address;
      if (!IPAddress.TryParse(ip, out address))
        return IPAddress.Any;
      else
        return address;
    }

    public static bool TryParseEndpoint(string ipAndPort, out IPEndPoint result)
    {
      try
      {
        string[] strArray = ipAndPort.Replace(" ", string.Empty).Split(new string[1]
        {
          ":"
        }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length == 2)
        {
          IPAddress address;
          if (IPAddress.TryParse(strArray[0], out address))
          {
            int result1;
            if (int.TryParse(strArray[1], out result1))
            {
              result = new IPEndPoint(address, result1);
              return true;
            }
          }
        }
      }
      catch
      {
      }
      result = (IPEndPoint) null;
      return false;
    }
  }
}
