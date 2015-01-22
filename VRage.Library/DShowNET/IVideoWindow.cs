// Decompiled with JetBrains decompiler
// Type: DShowNET.IVideoWindow
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(true)]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("56a868b4-0ad4-11ce-b03a-0020af0ba770")]
  [ComImport]
  public interface IVideoWindow
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Caption(string caption);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Caption(out string caption);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_WindowStyle(int windowStyle);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_WindowStyle(out int windowStyle);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_WindowStyleEx(int windowStyleEx);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_WindowStyleEx(out int windowStyleEx);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_AutoShow(int autoShow);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_AutoShow(out int autoShow);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_WindowState(int windowState);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_WindowState(out int windowState);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_BackgroundPalette(int backgroundPalette);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_BackgroundPalette(out int backgroundPalette);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Visible(int visible);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Visible(out int visible);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Left(int left);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Left(out int left);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Width(int width);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Width(out int width);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Top(int top);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Top(out int top);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Height(int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Height(out int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_Owner(IntPtr owner);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_Owner(out IntPtr owner);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_MessageDrain(IntPtr drain);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_MessageDrain(out IntPtr drain);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_BorderColor(out int color);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_BorderColor(int color);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_FullScreenMode(out int fullScreenMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_FullScreenMode(int fullScreenMode);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetWindowForeground(int focus);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int NotifyOwnerMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetWindowPosition(int left, int top, int width, int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetWindowPosition(out int left, out int top, out int width, out int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMinIdealImageSize(out int width, out int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetMaxIdealImageSize(out int width, out int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetRestorePosition(out int left, out int top, out int width, out int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int HideCursor(int hideCursor);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsCursorHidden(out int hideCursor);
  }
}
