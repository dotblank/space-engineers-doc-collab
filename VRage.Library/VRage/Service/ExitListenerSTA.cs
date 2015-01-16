// Decompiled with JetBrains decompiler
// Type: VRage.Service.ExitListenerSTA
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using VRage.Win32;

namespace VRage.Service
{
    public static class ExitListenerSTA
    {
        private static ApplicationExitHandler m_onExit;

        public static event ApplicationExitHandler OnExit
        {
            add { ExitListenerSTA.m_onExit += value; }
            remove { ExitListenerSTA.m_onExit -= value; }
        }

        public static void Listen()
        {
            WinApi.MSG lpMsg;
            if (!WinApi.PeekMessage(out lpMsg, IntPtr.Zero, 0U, 0U, 0U))
                return;
            WinApi.TranslateMessage(ref lpMsg);
            WinApi.DispatchMessage(ref lpMsg);
            if ((int) lpMsg.message != 16)
                return;
            ExitListenerSTA.Raise();
        }

        private static bool Raise()
        {
            bool stopListening = true;
            ApplicationExitHandler applicationExitHandler = ExitListenerSTA.m_onExit;
            if (applicationExitHandler != null)
                applicationExitHandler(ref stopListening);
            return stopListening;
        }
    }
}