// Decompiled with JetBrains decompiler
// Type: DShowNET.AMMediaType
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace DShowNET
{
  [ComVisible(false)]
  [StructLayout(LayoutKind.Sequential)]
  public class AMMediaType
  {
    public Guid majorType;
    public Guid subType;
    [MarshalAs(UnmanagedType.Bool)]
    public bool fixedSizeSamples;
    [MarshalAs(UnmanagedType.Bool)]
    public bool temporalCompression;
    public int sampleSize;
    public Guid formatType;
    public IntPtr unkPtr;
    public int formatSize;
    public IntPtr formatPtr;
  }
}
