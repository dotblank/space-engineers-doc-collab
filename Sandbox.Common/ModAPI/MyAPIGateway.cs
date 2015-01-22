// Decompiled with JetBrains decompiler
// Type: Sandbox.ModAPI.MyAPIGateway
// Assembly: Sandbox.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87AD5BE9-1B9D-42F5-8000-067AE4AE8CE7
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\Sandbox.Common.dll

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Sandbox.ModAPI
{
    public static class MyAPIGateway
    {
        public static IMySession Session;
        public static IMyEntities Entities;
        public static IMyPlayerCollection Players;
        public static IMyCubeBuilder CubeBuilder;
        public static IMyTerminalActionsHelper TerminalActionsHelper;
        public static IMyUtilities Utilities;
        public static IMyMultiplayer Multiplayer;
        public static IMyParallelTask Parallel;

        [Conditional("DEBUG")]
        public static void GetMessageBoxPointer(ref IntPtr pointer)
        {
            IntPtr hModule = MyAPIGateway.LoadLibrary("user32.dll");
            pointer = MyAPIGateway.GetProcAddress(hModule, "MessageBoxW");
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllname);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procname);
    }
}