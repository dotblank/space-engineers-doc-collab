// Decompiled with JetBrains decompiler
// Type: System.TestScriptHelpers
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3595035D-D240-4390-9773-1FE64718FDDB
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System.Runtime.InteropServices;

namespace System
{
  [StructLayout(LayoutKind.Sequential, Size = 1)]
  public struct TestScriptHelpers
  {
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern uint MessageBox(IntPtr hWndle, string text, string caption, int buttons);

    public static void DoEvilThings()
    {
    }
  }
}
