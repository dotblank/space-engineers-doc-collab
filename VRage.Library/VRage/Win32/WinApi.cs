// Decompiled with JetBrains decompiler
// Type: VRage.Win32.WinApi
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Runtime.InteropServices;

namespace VRage.Win32
{
    public static class WinApi
    {
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        public const int ENUM_CURRENT_SETTINGS = -1;
        public const int ENUM_REGISTRY_SETTINGS = -2;
        public const int MF_BYPOSITION = 1024;
        public const int MF_REMOVE = 4096;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeConsole();

        [DllImport("kernel32", SetLastError = true)]
        public static extern bool AttachConsole(int dwProcessId);

        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(WinApi.ConsoleEventHandler handler, bool add);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalMemoryStatusEx([In, Out] WinApi.MEMORYSTATUSEX lpBuffer);

        [DllImport("user32.dll")]
        public static extern sbyte GetMessage(out WinApi.MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        public static extern bool PeekMessage(out WinApi.MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax,
            uint wRemoveMsg);

        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref WinApi.MSG lpMsg);

        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref WinApi.MSG lpmsg);

        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired,
            uint fuLoad);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref WinApi.DEVMODE devMode);

        public delegate bool ConsoleEventHandler(WinApi.CtrlType sig);

        public enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6,
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MEMORYSTATUSEX
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong ullTotalPhys;
            public ulong ullAvailPhys;
            public ulong ullTotalPageFile;
            public ulong ullAvailPageFile;
            public ulong ullTotalVirtual;
            public ulong ullAvailVirtual;
            public ulong ullAvailExtendedVirtual;

            public MEMORYSTATUSEX()
            {
                this.dwLength = (uint) Marshal.SizeOf(typeof (WinApi.MEMORYSTATUSEX));
            }
        }

        public struct POINT
        {
            public int X;
            public int Y;
        }

        public struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public WinApi.POINT pt;
        }

        public enum SystemCommands
        {
            SC_SIZE = 61440,
            SC_SEPARATOR = 61455,
            SC_MOVE = 61456,
            SC_MINIMIZE = 61472,
            SC_MAXIMIZE = 61488,
            SC_MAXIMIZE2 = 61490,
            SC_NEXTWINDOW = 61504,
            SC_PREVWINDOW = 61520,
            SC_CLOSE = 61536,
            SC_VSCROLL = 61552,
            SC_HSCROLL = 61568,
            SC_MOUSEMENU = 61584,
            SC_KEYMENU = 61696,
            SC_ARRANGE = 61712,
            SC_RESTORE = 61728,
            SC_RESTORE2 = 61730,
            SC_TASKLIST = 61744,
            SC_SCREENSAVE = 61760,
            SC_HOTKEY = 61776,
            SC_DEFAULT = 61792,
            SC_MONITORPOWER = 61808,
            SC_CONTEXTHELP = 61824,
        }

        [Flags]
        public enum DM
        {
            Orientation = 1,
            PaperSize = 2,
            PaperLength = 4,
            PaperWidth = 8,
            Scale = 16,
            Position = 32,
            NUP = 64,
            DisplayOrientation = 128,
            Copies = 256,
            DefaultSource = 512,
            PrintQuality = 1024,
            Color = 2048,
            Duplex = 4096,
            YResolution = 8192,
            TTOption = 16384,
            Collate = 32768,
            FormName = 65536,
            LogPixels = 131072,
            BitsPerPixel = 262144,
            PelsWidth = 524288,
            PelsHeight = 1048576,
            DisplayFlags = 2097152,
            DisplayFrequency = 4194304,
            ICMMethod = 8388608,
            ICMIntent = 16777216,
            MediaType = 33554432,
            DitherType = 67108864,
            PanningWidth = 134217728,
            PanningHeight = 268435456,
            DisplayFixedOutput = 536870912,
        }

        public struct POINTL
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DEVMODE
        {
            public const int CCHDEVICENAME = 32;
            public const int CCHFORMNAME = 32;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] [FieldOffset(0)] public string dmDeviceName;
            [FieldOffset(32)] public short dmSpecVersion;
            [FieldOffset(34)] public short dmDriverVersion;
            [FieldOffset(36)] public short dmSize;
            [FieldOffset(38)] public short dmDriverExtra;
            [FieldOffset(40)] public WinApi.DM dmFields;
            [FieldOffset(44)] private short dmOrientation;
            [FieldOffset(46)] private short dmPaperSize;
            [FieldOffset(48)] private short dmPaperLength;
            [FieldOffset(50)] private short dmPaperWidth;
            [FieldOffset(52)] private short dmScale;
            [FieldOffset(54)] private short dmCopies;
            [FieldOffset(56)] private short dmDefaultSource;
            [FieldOffset(58)] private short dmPrintQuality;
            [FieldOffset(44)] public WinApi.POINTL dmPosition;
            [FieldOffset(52)] public int dmDisplayOrientation;
            [FieldOffset(56)] public int dmDisplayFixedOutput;
            [FieldOffset(60)] public short dmColor;
            [FieldOffset(62)] public short dmDuplex;
            [FieldOffset(64)] public short dmYResolution;
            [FieldOffset(66)] public short dmTTOption;
            [FieldOffset(68)] public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] [FieldOffset(72)] public string dmFormName;
            [FieldOffset(102)] public short dmLogPixels;
            [FieldOffset(104)] public int dmBitsPerPel;
            [FieldOffset(108)] public int dmPelsWidth;
            [FieldOffset(112)] public int dmPelsHeight;
            [FieldOffset(116)] public int dmDisplayFlags;
            [FieldOffset(116)] public int dmNup;
            [FieldOffset(120)] public int dmDisplayFrequency;
        }
    }
}