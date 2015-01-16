// Decompiled with JetBrains decompiler
// Type: VRage.Trace.MyWintraceWrapper
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 98EC8A66-D3FB-4994-A617-48E1C71F8818
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.IO;
using System.Reflection;
using VRage.Common.Utils;

namespace VRage.Trace
{
    internal class MyWintraceWrapper : ITrace
    {
        private static Type m_winTraceType;
        private static object m_winWatches;
        private object m_trace;
        private Action m_clearAll;
        private Action<string, object> m_send;
        private Action<string, string> m_debugSend;

        static MyWintraceWrapper()
        {
            Assembly assembly = MyWintraceWrapper.TryLoad("TraceTool.dll") ??
                                MyWintraceWrapper.TryLoad(MyFileSystem.ExePath +
                                                          "/../../../../../../3rd/TraceTool/TraceTool.dll");
            if (!(assembly != (Assembly) null))
                return;
            MyWintraceWrapper.m_winTraceType = assembly.GetType("TraceTool.WinTrace");
            MyWintraceWrapper.m_winWatches =
                assembly.GetType("TraceTool.TTrace")
                    .GetProperty("Watches")
                    .GetGetMethod()
                    .Invoke((object) null, new object[0]);
        }

        private MyWintraceWrapper(object trace)
        {
            // ISSUE: unable to decompile the method.
        }

        private static Assembly TryLoad(string assembly)
        {
            if (!File.Exists(assembly))
                return (Assembly) null;
            try
            {
                return Assembly.LoadFrom(assembly);
            }
            catch (Exception ex)
            {
                return (Assembly) null;
            }
        }

        public static ITrace CreateTrace(string id, string name)
        {
            if (!(MyWintraceWrapper.m_winTraceType != (Type) null))
                return (ITrace) new MyNullTrace();
            return
                (ITrace) new MyWintraceWrapper(Activator.CreateInstance(MyWintraceWrapper.m_winTraceType, new object[2]
                {
                    (object) id,
                    (object) name
                }));
        }

        public void Send(string msg, string comment = null)
        {
            this.m_debugSend(msg, comment);
        }

        public void Watch(string name, object value)
        {
            try
            {
                this.m_send(name, value);
            }
            catch
            {
            }
        }
    }
}