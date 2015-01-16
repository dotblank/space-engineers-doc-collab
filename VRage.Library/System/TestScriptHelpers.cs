// Decompiled with JetBrains decompiler
// Type: System.TestScriptHelpers
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
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