// Decompiled with JetBrains decompiler
// Type: DShowNET.DsUtils
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(false)]
  public class DsUtils
  {
    public static bool IsCorrectDirectXVersion()
    {
      return File.Exists(Path.Combine(Environment.SystemDirectory, "dpnhpast.dll"));
    }

    public static bool ShowCapPinDialog(ICaptureGraphBuilder2 bld, IBaseFilter flt, IntPtr hwnd)
    {
      object ppint = (object) null;
      ISpecifyPropertyPages specifyPropertyPages1 = (ISpecifyPropertyPages) null;
      DsCAUUID pPages = new DsCAUUID();
      try
      {
        Guid pCategory = PinCategory.Capture;
        Guid pType1 = MediaType.Interleaved;
        Guid guid = typeof (IAMStreamConfig).GUID;
        if (bld.FindInterface(ref pCategory, ref pType1, flt, ref guid, out ppint) != 0)
        {
          Guid pType2 = MediaType.Video;
          if (bld.FindInterface(ref pCategory, ref pType2, flt, ref guid, out ppint) != 0)
            return false;
        }
        ISpecifyPropertyPages specifyPropertyPages2 = ppint as ISpecifyPropertyPages;
        if (specifyPropertyPages2 == null)
          return false;
        int num = specifyPropertyPages2.GetPages(out pPages);
        num = DsUtils.OleCreatePropertyFrame(hwnd, 30, 30, (string) null, 1, ref ppint, pPages.cElems, pPages.pElems, 0, 0, IntPtr.Zero);
        return true;
      }
      catch (Exception ex)
      {
        Trace.WriteLine("!Ds.NET: ShowCapPinDialog " + ex.Message);
        return false;
      }
      finally
      {
        if (pPages.pElems != IntPtr.Zero)
          Marshal.FreeCoTaskMem(pPages.pElems);
        specifyPropertyPages1 = (ISpecifyPropertyPages) null;
        if (ppint != null)
          Marshal.ReleaseComObject(ppint);
      }
    }

    public static bool ShowTunerPinDialog(ICaptureGraphBuilder2 bld, IBaseFilter flt, IntPtr hwnd)
    {
      object ppint = (object) null;
      ISpecifyPropertyPages specifyPropertyPages1 = (ISpecifyPropertyPages) null;
      DsCAUUID pPages = new DsCAUUID();
      try
      {
        Guid pCategory = PinCategory.Capture;
        Guid pType1 = MediaType.Interleaved;
        Guid guid = typeof (IAMTVTuner).GUID;
        if (bld.FindInterface(ref pCategory, ref pType1, flt, ref guid, out ppint) != 0)
        {
          Guid pType2 = MediaType.Video;
          if (bld.FindInterface(ref pCategory, ref pType2, flt, ref guid, out ppint) != 0)
            return false;
        }
        ISpecifyPropertyPages specifyPropertyPages2 = ppint as ISpecifyPropertyPages;
        if (specifyPropertyPages2 == null)
          return false;
        int num = specifyPropertyPages2.GetPages(out pPages);
        num = DsUtils.OleCreatePropertyFrame(hwnd, 30, 30, (string) null, 1, ref ppint, pPages.cElems, pPages.pElems, 0, 0, IntPtr.Zero);
        return true;
      }
      catch (Exception ex)
      {
        Trace.WriteLine("!Ds.NET: ShowCapPinDialog " + ex.Message);
        return false;
      }
      finally
      {
        if (pPages.pElems != IntPtr.Zero)
          Marshal.FreeCoTaskMem(pPages.pElems);
        specifyPropertyPages1 = (ISpecifyPropertyPages) null;
        if (ppint != null)
          Marshal.ReleaseComObject(ppint);
      }
    }

    public int GetPin(IBaseFilter filter, PinDirection dirrequired, int num, out IPin ppPin)
    {
      ppPin = (IPin) null;
      IEnumPins ppEnum;
      int num1 = filter.EnumPins(out ppEnum);
      if (num1 < 0 || ppEnum == null)
        return num1;
      IPin[] ppPins = new IPin[1];
      int num2;
      do
      {
        int pcFetched;
        num2 = ppEnum.Next(1, out ppPins, out pcFetched);
        if (num2 == 0 && ppPins[0] != null)
        {
          PinDirection pPinDir = (PinDirection) 3;
          num2 = ppPins[0].QueryDirection(out pPinDir);
          if (num2 == 0 && pPinDir == dirrequired)
          {
            if (num == 0)
            {
              ppPin = ppPins[0];
              ppPins[0] = (IPin) null;
              break;
            }
            else
              --num;
          }
          Marshal.ReleaseComObject((object) ppPins[0]);
          ppPins[0] = (IPin) null;
        }
        else
          break;
      }
      while (num2 == 0);
      Marshal.ReleaseComObject((object) ppEnum);
      return num2;
    }

    [DllImport("olepro32.dll", CharSet = CharSet.Unicode)]
    private static extern int OleCreatePropertyFrame(IntPtr hwndOwner, int x, int y, string lpszCaption, int cObjects, [MarshalAs(UnmanagedType.Interface), In] ref object ppUnk, int cPages, IntPtr pPageClsID, int lcid, int dwReserved, IntPtr pvReserved);
  }
}
