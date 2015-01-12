// Decompiled with JetBrains decompiler
// Type: VRage.Service.ExitListener
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Threading;
using VRage.Win32;

namespace VRage.Service
{
    public static class ExitListener
    {
        private static Thread m_listenThread;
        private static ApplicationExitHandler m_onExit;

        public static event ApplicationExitHandler OnExit
        {
            add
            {
                ExitListener.m_onExit += value;
                ExitListener.OnHandlerAdded();
            }
            remove
            {
                ExitListener.m_onExit -= value;
                ExitListener.OnHandlerRemoved();
            }
        }

        private static void Listen()
        {
            WinApi.MSG lpMsg;
            while ((int) WinApi.GetMessage(out lpMsg, IntPtr.Zero, 0U, 0U) != 0)
            {
                WinApi.TranslateMessage(ref lpMsg);
                WinApi.DispatchMessage(ref lpMsg);
                if ((int) lpMsg.message == 16 && ExitListener.Raise())
                    break;
            }
        }

        private static bool Raise()
        {
            bool stopListening = true;
            ApplicationExitHandler applicationExitHandler = ExitListener.m_onExit;
            if (applicationExitHandler != null)
                applicationExitHandler(ref stopListening);
            return stopListening;
        }

        private static void OnHandlerAdded()
        {
            if (ExitListener.m_listenThread != null)
                return;
            ExitListener.m_listenThread = new Thread(new ThreadStart(ExitListener.Listen));
            ExitListener.m_listenThread.IsBackground = true;
            ExitListener.m_listenThread.SetApartmentState(ApartmentState.STA);
            ExitListener.m_listenThread.Start();
        }

        private static void OnHandlerRemoved()
        {
            if (ExitListener.m_listenThread == null || ExitListener.m_onExit != null)
                return;
            if (ExitListener.m_listenThread.IsAlive)
            {
                WinApi.PostQuitMessage(0);
                if (!ExitListener.m_listenThread.Join(100))
                    ExitListener.m_listenThread.Abort();
            }
            ExitListener.m_listenThread = (Thread) null;
        }
    }
}