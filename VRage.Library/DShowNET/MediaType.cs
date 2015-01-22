// Decompiled with JetBrains decompiler
// Type: DShowNET.MediaType
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(false)]
  public class MediaType
  {
    public static readonly Guid Video = new Guid(1935960438, (short) 0, (short) 16, (byte) byte.MinValue, (byte) 0, (byte) 0, (byte) 170, (byte) 0, (byte) 56, (byte) 155, (byte) 113);
    public static readonly Guid Interleaved = new Guid(1937138025, (short) 0, (short) 16, (byte) byte.MinValue, (byte) 0, (byte) 0, (byte) 170, (byte) 0, (byte) 56, (byte) 155, (byte) 113);
    public static readonly Guid Audio = new Guid(1935963489, (short) 0, (short) 16, (byte) byte.MinValue, (byte) 0, (byte) 0, (byte) 170, (byte) 0, (byte) 56, (byte) 155, (byte) 113);
    public static readonly Guid Text = new Guid(1937012852, (short) 0, (short) 16, (byte) byte.MinValue, (byte) 0, (byte) 0, (byte) 170, (byte) 0, (byte) 56, (byte) 155, (byte) 113);
    public static readonly Guid Stream = new Guid(3828804483U, (ushort) 21071, (ushort) 4558, (byte) 159, (byte) 83, (byte) 0, (byte) 32, (byte) 175, (byte) 11, (byte) 167, (byte) 112);
  }
}
