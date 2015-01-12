// Decompiled with JetBrains decompiler
// Type: System.TestScriptHelpers
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
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