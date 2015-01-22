// Decompiled with JetBrains decompiler
// Type: DShowNET.Device.DsDevice
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace DShowNET.Device
{
  [ComVisible(false)]
  public class DsDevice : IDisposable
  {
    public string Name;
    public UCOMIMoniker Mon;

    public void Dispose()
    {
      if (this.Mon != null)
        Marshal.ReleaseComObject((object) this.Mon);
      this.Mon = (UCOMIMoniker) null;
    }
  }
}
