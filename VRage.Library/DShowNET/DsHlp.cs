// Decompiled with JetBrains decompiler
// Type: DShowNET.DsHlp
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;
using System.Text;

namespace DShowNET
{
  [ComVisible(false)]
  public class DsHlp
  {
    public const int OATRUE = -1;
    public const int OAFALSE = 0;

    [DllImport("quartz.dll", CharSet = CharSet.Auto)]
    public static extern int AMGetErrorText(int hr, StringBuilder buf, int max);
  }
}
