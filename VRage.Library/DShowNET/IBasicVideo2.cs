// Decompiled with JetBrains decompiler
// Type: DShowNET.IBasicVideo2
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [Guid("329bb360-f6ea-11d1-9038-00a0c9697298")]
  [ComVisible(true)]
  [ComImport]
  public interface IBasicVideo2
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int AvgTimePerFrame(out double pAvgTimePerFrame);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int BitRate(out int pBitRate);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int BitErrorRate(out int pBitRate);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int VideoWidth(out int pVideoWidth);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int VideoHeight(out int pVideoHeight);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_SourceLeft(int SourceLeft);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_SourceLeft(out int pSourceLeft);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_SourceWidth(int SourceWidth);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_SourceWidth(out int pSourceWidth);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_SourceTop(int SourceTop);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_SourceTop(out int pSourceTop);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_SourceHeight(int SourceHeight);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_SourceHeight(out int pSourceHeight);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_DestinationLeft(int DestinationLeft);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_DestinationLeft(out int pDestinationLeft);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_DestinationWidth(int DestinationWidth);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_DestinationWidth(out int pDestinationWidth);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_DestinationTop(int DestinationTop);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_DestinationTop(out int pDestinationTop);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int put_DestinationHeight(int DestinationHeight);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int get_DestinationHeight(out int pDestinationHeight);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetSourcePosition(int left, int top, int width, int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetSourcePosition(out int left, out int top, out int width, out int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetDefaultSourcePosition();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetDestinationPosition(int left, int top, int width, int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetDestinationPosition(out int left, out int top, out int width, out int height);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int SetDefaultDestinationPosition();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetVideoSize(out int pWidth, out int pHeight);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetVideoPaletteEntries(int StartIndex, int Entries, out int pRetrieved, IntPtr pPalette);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetCurrentImage(ref int pBufferSize, IntPtr pDIBImage);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsUsingDefaultSource();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsUsingDefaultDestination();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetPreferredAspectRatio(out int plAspectX, out int plAspectY);
  }
}
