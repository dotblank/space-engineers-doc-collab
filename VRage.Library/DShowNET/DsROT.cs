// Decompiled with JetBrains decompiler
// Type: DShowNET.DsROT
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(false)]
  public class DsROT
  {
    private const int ROTFLAGS_REGISTRATIONKEEPSALIVE = 1;

    public static bool AddGraphToRot(object graph, out int cookie)
    {
      cookie = 0;
      UCOMIRunningObjectTable pprot = (UCOMIRunningObjectTable) null;
      UCOMIMoniker ppmk = (UCOMIMoniker) null;
      try
      {
        int runningObjectTable = DsROT.GetRunningObjectTable(0, out pprot);
        if (runningObjectTable < 0)
          Marshal.ThrowExceptionForHR(runningObjectTable);
        int currentProcessId = DsROT.GetCurrentProcessId();
        IntPtr iunknownForObject = Marshal.GetIUnknownForObject(graph);
        int num = (int) iunknownForObject;
        Marshal.Release(iunknownForObject);
        int itemMoniker = DsROT.CreateItemMoniker("!", string.Format("FilterGraph {0} pid {1}", (object) num.ToString("x8"), (object) currentProcessId.ToString("x8")), out ppmk);
        if (itemMoniker < 0)
          Marshal.ThrowExceptionForHR(itemMoniker);
        pprot.Register(1, graph, ppmk, out cookie);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
      finally
      {
        if (ppmk != null)
          Marshal.ReleaseComObject((object) ppmk);
        if (pprot != null)
          Marshal.ReleaseComObject((object) pprot);
      }
    }

    public static bool RemoveGraphFromRot(ref int cookie)
    {
      UCOMIRunningObjectTable pprot = (UCOMIRunningObjectTable) null;
      try
      {
        int runningObjectTable = DsROT.GetRunningObjectTable(0, out pprot);
        if (runningObjectTable < 0)
          Marshal.ThrowExceptionForHR(runningObjectTable);
        pprot.Revoke(cookie);
        cookie = 0;
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
      finally
      {
        if (pprot != null)
          Marshal.ReleaseComObject((object) pprot);
      }
    }

    [DllImport("ole32.dll")]
    private static extern int GetRunningObjectTable(int r, out UCOMIRunningObjectTable pprot);

    [DllImport("ole32.dll", CharSet = CharSet.Unicode)]
    private static extern int CreateItemMoniker(string delim, string item, out UCOMIMoniker ppmk);

    [DllImport("kernel32.dll")]
    private static extern int GetCurrentProcessId();
  }
}
