// Decompiled with JetBrains decompiler
// Type: DShowNET.Dvd.DvdSubPicAttr
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;

namespace DShowNET.Dvd
{
  [ComVisible(false)]
  [StructLayout(LayoutKind.Sequential, Pack = 1)]
  public struct DvdSubPicAttr
  {
    public DvdSubPicType type;
    public DvdSubPicCoding coding;
    public int language;
    public DvdSubPicLangExt languageExt;
  }
}
