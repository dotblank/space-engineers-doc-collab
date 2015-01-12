// Decompiled with JetBrains decompiler
// Type: KeenSoftwareHouse.Library.IO.Win32Processes
// Assembly: VRage.Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: FD5D66CE-92BD-4D2D-A5F6-2A600D10290D
// Assembly location: D:\Games\Steam Library\SteamApps\common\SpaceEngineers\Bin64\VRage.Library.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace KeenSoftwareHouse.Library.IO
{
    public class Win32Processes
    {
        private const int CNST_SYSTEM_HANDLE_INFORMATION = 16;

        public static List<Process> GetProcessesLockingFile(string filePath)
        {
            List<Process> list = new List<Process>();
            foreach (Process process in Process.GetProcesses())
            {
                if (process.Id > 4 && Win32Processes.GetFilesLockedBy(process).Contains(filePath))
                    list.Add(process);
            }
            return list;
        }

        public static List<string> GetFilesLockedBy(Process process)
        {
            List<string> outp = new List<string>();
            ThreadStart start = (ThreadStart) (() =>
            {
                try
                {
                    outp = Win32Processes.UnsafeGetFilesLockedBy(process);
                }
                catch
                {
                    Win32Processes.Ignore();
                }
            });
            try
            {
                Thread thread = new Thread(start);
                thread.IsBackground = true;
                thread.Start();
                if (!thread.Join(250))
                {
                    try
                    {
                        thread.Interrupt();
                        thread.Abort();
                    }
                    catch
                    {
                        Win32Processes.Ignore();
                    }
                }
            }
            catch
            {
                Win32Processes.Ignore();
            }
            return outp;
        }

        private static void Ignore()
        {
        }

        private static List<string> UnsafeGetFilesLockedBy(Process process)
        {
            try
            {
                IEnumerable<Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION> handles =
                    Win32Processes.GetHandles(process);
                List<string> list = new List<string>();
                foreach (Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION systemHandleInformation in handles)
                {
                    string filePath = Win32Processes.GetFilePath(systemHandleInformation, process);
                    if (filePath != null)
                        list.Add(filePath);
                }
                return list;
            }
            catch
            {
                return new List<string>();
            }
        }

        private static string GetFilePath(Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION systemHandleInformation,
            Process process)
        {
            IntPtr hSourceProcessHandle =
                Win32Processes.Win32API.OpenProcess(Win32Processes.Win32API.ProcessAccessFlags.All, false, process.Id);
            Win32Processes.Win32API.OBJECT_BASIC_INFORMATION basicInformation1 =
                new Win32Processes.Win32API.OBJECT_BASIC_INFORMATION();
            Win32Processes.Win32API.OBJECT_TYPE_INFORMATION objectTypeInformation1 =
                new Win32Processes.Win32API.OBJECT_TYPE_INFORMATION();
            Win32Processes.Win32API.OBJECT_NAME_INFORMATION objectNameInformation1 =
                new Win32Processes.Win32API.OBJECT_NAME_INFORMATION();
            string strRawName = "";
            int returnLength = 0;
            IntPtr lpTargetHandle;
            if (
                !Win32Processes.Win32API.DuplicateHandle(hSourceProcessHandle, systemHandleInformation.Handle,
                    Win32Processes.Win32API.GetCurrentProcess(), out lpTargetHandle, 0U, false, 2U))
                return (string) null;
            IntPtr num1 = Marshal.AllocHGlobal(Marshal.SizeOf((object) basicInformation1));
            Win32Processes.Win32API.NtQueryObject(lpTargetHandle, 0, num1, Marshal.SizeOf((object) basicInformation1),
                ref returnLength);
            Win32Processes.Win32API.OBJECT_BASIC_INFORMATION basicInformation2 =
                (Win32Processes.Win32API.OBJECT_BASIC_INFORMATION)
                    Marshal.PtrToStructure(num1, basicInformation1.GetType());
            Marshal.FreeHGlobal(num1);
            IntPtr num2 = Marshal.AllocHGlobal(basicInformation2.TypeInformationLength);
            for (returnLength = basicInformation2.TypeInformationLength;
                Win32Processes.Win32API.NtQueryObject(lpTargetHandle, 2, num2, returnLength, ref returnLength) ==
                -1073741820;
                num2 = Marshal.AllocHGlobal(returnLength))
            {
                if (returnLength == 0)
                {
                    Console.WriteLine("nLength returned at zero! ");
                    return (string) null;
                }
                else
                    Marshal.FreeHGlobal(num2);
            }
            Win32Processes.Win32API.OBJECT_TYPE_INFORMATION objectTypeInformation2 =
                (Win32Processes.Win32API.OBJECT_TYPE_INFORMATION)
                    Marshal.PtrToStructure(num2, objectTypeInformation1.GetType());
            IntPtr num3 = !Win32Processes.Is64Bits()
                ? objectTypeInformation2.Name.Buffer
                : new IntPtr(Convert.ToInt64(objectTypeInformation2.Name.Buffer.ToString(), 10) >> 32);
            string str = Marshal.PtrToStringUni(num3, (int) objectTypeInformation2.Name.Length >> 1);
            Marshal.FreeHGlobal(num2);
            if (str != "File")
                return (string) null;
            returnLength = basicInformation2.NameInformationLength;
            IntPtr num4;
            for (num4 = Marshal.AllocHGlobal(returnLength);
                Win32Processes.Win32API.NtQueryObject(lpTargetHandle, 1, num4, returnLength, ref returnLength) ==
                -1073741820;
                num4 = Marshal.AllocHGlobal(returnLength))
            {
                Marshal.FreeHGlobal(num4);
                if (returnLength == 0)
                {
                    Console.WriteLine("nLength returned at zero! " + str);
                    return (string) null;
                }
            }
            Win32Processes.Win32API.OBJECT_NAME_INFORMATION objectNameInformation2 =
                (Win32Processes.Win32API.OBJECT_NAME_INFORMATION)
                    Marshal.PtrToStructure(num4, objectNameInformation1.GetType());
            num3 = !Win32Processes.Is64Bits()
                ? objectNameInformation2.Name.Buffer
                : new IntPtr(Convert.ToInt64(objectNameInformation2.Name.Buffer.ToString(), 10) >> 32);
            if (num3 != IntPtr.Zero)
            {
                byte[] destination = new byte[returnLength];
                try
                {
                    Marshal.Copy(num3, destination, 0, returnLength);
                    strRawName =
                        Marshal.PtrToStringUni(Win32Processes.Is64Bits()
                            ? new IntPtr(num3.ToInt64())
                            : new IntPtr(num3.ToInt32()));
                }
                catch (AccessViolationException ex)
                {
                    return (string) null;
                }
                finally
                {
                    Marshal.FreeHGlobal(num4);
                    Win32Processes.Win32API.CloseHandle(lpTargetHandle);
                }
            }
            string fileNameFromDevice = Win32Processes.GetRegularFileNameFromDevice(strRawName);
            try
            {
                return fileNameFromDevice;
            }
            catch
            {
                return (string) null;
            }
        }

        private static string GetRegularFileNameFromDevice(string strRawName)
        {
            string str1 = strRawName;
            foreach (string str2 in Environment.GetLogicalDrives())
            {
                StringBuilder lpTargetPath = new StringBuilder(260);
                if ((int) Win32Processes.Win32API.QueryDosDevice(str2.Substring(0, 2), lpTargetPath, 260) == 0)
                    return strRawName;
                string oldValue = ((object) lpTargetPath).ToString();
                if (str1.StartsWith(oldValue))
                {
                    str1 = str1.Replace(oldValue, str2.Substring(0, 2));
                    break;
                }
            }
            return str1;
        }

        private static IEnumerable<Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION> GetHandles(Process process)
        {
            int num1 = 65536;
            IntPtr num2 = Marshal.AllocHGlobal(num1);
            int returnLength;
            for (returnLength = 0;
                (int) Win32Processes.Win32API.NtQuerySystemInformation(16, num2, num1, ref returnLength) == -1073741820;
                num2 = Marshal.AllocHGlobal(returnLength))
            {
                num1 = returnLength;
                Marshal.FreeHGlobal(num2);
            }
            byte[] destination = new byte[returnLength];
            Marshal.Copy(num2, destination, 0, returnLength);
            long num3;
            IntPtr ptr;
            if (Win32Processes.Is64Bits())
            {
                num3 = Marshal.ReadInt64(num2);
                ptr = new IntPtr(num2.ToInt64() + 8L);
            }
            else
            {
                num3 = (long) Marshal.ReadInt32(num2);
                ptr = new IntPtr(num2.ToInt32() + 4);
            }
            List<Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION> list =
                new List<Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION>();
            for (long index = 0L; index < num3; ++index)
            {
                Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION handleInformation1 =
                    new Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION();
                Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION handleInformation2;
                if (Win32Processes.Is64Bits())
                {
                    handleInformation2 =
                        (Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION)
                            Marshal.PtrToStructure(ptr, handleInformation1.GetType());
                    ptr = new IntPtr(ptr.ToInt64() + (long) Marshal.SizeOf((object) handleInformation2) + 8L);
                }
                else
                {
                    ptr = new IntPtr(ptr.ToInt64() + (long) Marshal.SizeOf((object) handleInformation1));
                    handleInformation2 =
                        (Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION)
                            Marshal.PtrToStructure(ptr, handleInformation1.GetType());
                }
                if (handleInformation2.ProcessID == process.Id)
                    list.Add(handleInformation2);
            }
            return (IEnumerable<Win32Processes.Win32API.SYSTEM_HANDLE_INFORMATION>) list;
        }

        private static bool Is64Bits()
        {
            return Marshal.SizeOf(typeof (IntPtr)) == 8;
        }

        internal class Win32API
        {
            public const int MAX_PATH = 260;
            public const uint STATUS_INFO_LENGTH_MISMATCH = 3221225476U;
            public const int DUPLICATE_SAME_ACCESS = 2;
            public const uint FILE_SEQUENTIAL_ONLY = 4U;

            [DllImport("ntdll.dll")]
            public static extern int NtQueryObject(IntPtr ObjectHandle, int ObjectInformationClass,
                IntPtr ObjectInformation, int ObjectInformationLength, ref int returnLength);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);

            [DllImport("ntdll.dll")]
            public static extern uint NtQuerySystemInformation(int SystemInformationClass, IntPtr SystemInformation,
                int SystemInformationLength, ref int returnLength);

            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenProcess(Win32Processes.Win32API.ProcessAccessFlags dwDesiredAccess,
                [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

            [DllImport("kernel32.dll")]
            public static extern int CloseHandle(IntPtr hObject);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DuplicateHandle(IntPtr hSourceProcessHandle, ushort hSourceHandle,
                IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle, uint dwDesiredAccess,
                [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);

            [DllImport("kernel32.dll")]
            public static extern IntPtr GetCurrentProcess();

            public enum ObjectInformationClass
            {
                ObjectBasicInformation,
                ObjectNameInformation,
                ObjectTypeInformation,
                ObjectAllTypesInformation,
                ObjectHandleInformation,
            }

            [System.Flags]
            public enum ProcessAccessFlags : uint
            {
                All = 2035711U,
                Terminate = 1U,
                CreateThread = 2U,
                VMOperation = 8U,
                VMRead = 16U,
                VMWrite = 32U,
                DupHandle = 64U,
                SetInformation = 512U,
                QueryInformation = 1024U,
                Synchronize = 1048576U,
            }

            public struct OBJECT_BASIC_INFORMATION
            {
                public int Attributes;
                public int GrantedAccess;
                public int HandleCount;
                public int PointerCount;
                public int PagedPoolUsage;
                public int NonPagedPoolUsage;
                public int Reserved1;
                public int Reserved2;
                public int Reserved3;
                public int NameInformationLength;
                public int TypeInformationLength;
                public int SecurityDescriptorLength;
                public System.Runtime.InteropServices.ComTypes.FILETIME CreateTime;
            }

            public struct OBJECT_TYPE_INFORMATION
            {
                public Win32Processes.Win32API.UNICODE_STRING Name;
                public int ObjectCount;
                public int HandleCount;
                public int Reserved1;
                public int Reserved2;
                public int Reserved3;
                public int Reserved4;
                public int PeakObjectCount;
                public int PeakHandleCount;
                public int Reserved5;
                public int Reserved6;
                public int Reserved7;
                public int Reserved8;
                public int InvalidAttributes;
                public Win32Processes.Win32API.GENERIC_MAPPING GenericMapping;
                public int ValidAccess;
                public byte Unknown;
                public byte MaintainHandleDatabase;
                public int PoolType;
                public int PagedPoolUsage;
                public int NonPagedPoolUsage;
            }

            public struct OBJECT_NAME_INFORMATION
            {
                public Win32Processes.Win32API.UNICODE_STRING Name;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct UNICODE_STRING
            {
                public ushort Length;
                public ushort MaximumLength;
                public IntPtr Buffer;
            }

            public struct GENERIC_MAPPING
            {
                public int GenericRead;
                public int GenericWrite;
                public int GenericExecute;
                public int GenericAll;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct SYSTEM_HANDLE_INFORMATION
            {
                public int ProcessID;
                public byte ObjectTypeNumber;
                public byte Flags;
                public ushort Handle;
                public int Object_Pointer;
                public uint GrantedAccess;
            }
        }
    }
}