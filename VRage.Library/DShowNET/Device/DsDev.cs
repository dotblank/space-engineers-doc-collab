// Decompiled with JetBrains decompiler
// Type: DShowNET.Device.DsDev
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using DShowNET;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace DShowNET.Device
{
  [ComVisible(false)]
  public class DsDev
  {
    public static bool GetDevicesOfCat(Guid cat, out ArrayList devs)
    {
      devs = (ArrayList) null;
      object o = (object) null;
      ICreateDevEnum createDevEnum = (ICreateDevEnum) null;
      UCOMIEnumMoniker ppEnumMoniker = (UCOMIEnumMoniker) null;
      UCOMIMoniker[] rgelt = new UCOMIMoniker[1];
      try
      {
        Type typeFromClsid = Type.GetTypeFromCLSID(Clsid.SystemDeviceEnum);
        if (typeFromClsid == (Type) null)
          throw new NotImplementedException("System Device Enumerator");
        o = Activator.CreateInstance(typeFromClsid);
        if (((ICreateDevEnum) o).CreateClassEnumerator(ref cat, out ppEnumMoniker, 0) != 0)
          throw new NotSupportedException("No devices of the category");
        int num = 0;
        int pceltFetched;
        while (ppEnumMoniker.Next(1, rgelt, out pceltFetched) == 0 && rgelt[0] != null)
        {
          DsDevice dsDevice = new DsDevice();
          dsDevice.Name = DsDev.GetFriendlyName(rgelt[0]);
          if (devs == null)
            devs = new ArrayList();
          dsDevice.Mon = rgelt[0];
          rgelt[0] = (UCOMIMoniker) null;
          devs.Add((object) dsDevice);
          ++num;
        }
        return num > 0;
      }
      catch (Exception ex)
      {
        if (devs != null)
        {
          foreach (DsDevice dsDevice in devs)
            dsDevice.Dispose();
          devs = (ArrayList) null;
        }
        return false;
      }
      finally
      {
        createDevEnum = (ICreateDevEnum) null;
        if (rgelt[0] != null)
          Marshal.ReleaseComObject((object) rgelt[0]);
        rgelt[0] = (UCOMIMoniker) null;
        if (ppEnumMoniker != null)
          Marshal.ReleaseComObject((object) ppEnumMoniker);
        if (o != null)
          Marshal.ReleaseComObject(o);
      }
    }

    private static string GetFriendlyName(UCOMIMoniker mon)
    {
        return null;
    }
  }
}
